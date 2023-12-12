/*
bgolus and ifurkend (official Unity forum).

Real-time directional light is required for this shader to work.
In particle system, enable "custom vertex stream", add "Tangent", "UV2" and "Custom1.x" ("UV2 must be before/above "Custom1.x").

If you use texture sheet, deselect all UVs in Texture Sheet Animation module 
but only enable "UV0" for correctly dividing the main texture, instead of the gradient map.

If the scene has no directional light, just use the plain Particle Alpha Blended shader and
merge the main texture and gradient map in graphic editor beforehand.
*/
Shader "Particles/Alpha Blended Amb Dir Lit" {
	Properties {
		_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
		_MainTex ("Main Texture", 2D) = "white" {}
		_GradMap ("Gradient Map", 2D) = "white" {} //The thicker the smoke, the more visible shadow.
		_GradPow ("Gradient Power", Range(0,2)) = 1 //This line can be removed if you have no intention to animate gradient power.
		_AmbientPow ("Ambient Power", Range(0,1)) = 0.5 // Can be used in HDR effect. Intensity greater than 2 causes glitch in HDR rendering.
		_DLightPow ("Dir Light Power", Range(0,1)) = 0.5
		_Glow ("Emissive Color", Color) = (0,0,0,0.5)
		_InvFade ("Soft Particles Factor", Range(0.01,3.0)) = 1.0
	}
	SubShader {
		Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" "LightMode" = "ForwardBase" "PreviewType" = "Plane"}
		LOD 100
		Cull Back
		ZWrite Off
		AlphaTest Greater 0.5
		Blend SrcAlpha OneMinusSrcAlpha

		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 2.0
			#pragma multi_compile_particles
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"
			#include "UnityLightingCommon.cginc"

			struct appdata {
				float3 vertex : POSITION;
				float4 texcoord : TEXCOORD0;
				float3 normal : NORMAL;
				float4 tangent : TANGENT;
				fixed4 color : COLOR;
				float3 custom1 : TEXCOORD1; //Additional intensity power
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				float4 uv : TEXCOORD0;
				fixed4 color : COLOR;
				UNITY_FOG_COORDS(3)
				#ifdef SOFTPARTICLES_ON
				float4 projPos : TEXCOORD4;
				#endif
				UNITY_VERTEX_OUTPUT_STEREO
			};

			fixed4 _TintColor;
			sampler2D _MainTex;
			sampler2D _GradMap;
			float4 _MainTex_ST;
			float4 _GradMap_ST;
			half _GradPow;
			half _AmbientPow;
			half _DLightPow;
			fixed3 _Glow;
			sampler2D_float _CameraDepthTexture;
			float _InvFade;
			
			v2f vert (appdata v) {
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv.xy = TRANSFORM_TEX(v.texcoord.xy, _MainTex);
				#ifdef SOFTPARTICLES_ON
				o.projPos = ComputeScreenPos (o.vertex);
				COMPUTE_EYEDEPTH(o.projPos.z);
				#endif
								
				// Calculate the world normal, tangent, and binormal
				half3 worldNormal = UnityObjectToWorldNormal(v.normal);
				float3 worldTangent = UnityObjectToWorldDir(v.tangent.xyz);
				float tangentSign = v.tangent.w * unity_WorldTransformParams.w;
				float3 worldBinormal = cross(worldNormal, worldTangent) * tangentSign;
				// Construct a world to tangent rotation matrix
				float3x3 worldToTangent = float3x3(worldTangent, worldBinormal, worldNormal);
				 
				// Rotate world space light direction
				float3 tangentLightDir = mul(worldToTangent, _WorldSpaceLightPos0.xyz);
				 
				// Apply UV space rotation
				float2 lightVec = normalize(tangentLightDir.xy);
				float2x2 lightVecRotationMatrix = float2x2(lightVec.x, -lightVec.y, lightVec.y, lightVec.x);
				o.uv.zw = mul(mul (TRANSFORM_TEX(v.texcoord.zw, _GradMap)-0.5, lightVecRotationMatrix),float2x2(0,1,-1,0))+0.5;
				
				o.color = v.color * _TintColor;
				o.color.rgb += ShadeSH9(half4(worldNormal,1)) * _AmbientPow;
				o.color.rgb *= 1+(_DLightPow * (1-v.custom1) * (saturate(_LightColor0)-1));
				//o.color.rgb *= 1+(_DLightPow * (saturate(_LightColor0)-1));
				o.color.rgb += _Glow * v.custom1;
				
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			half4 frag (v2f i) : SV_Target {
				#ifdef SOFTPARTICLES_ON
				float sceneZ = LinearEyeDepth (SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)));
				float partZ = i.projPos.z;
				float fade = saturate (_InvFade * (sceneZ-partZ));
				i.color.a *= fade;
				#endif
				
				half4 col = 2 * tex2D(_MainTex, i.uv.xy);
				half4 col2 = tex2D(_GradMap, i.uv.zw);
				col2.rgb = 1+(saturate(i.color.a*2)*_GradPow*(col2.rgb-1));
				col *= col2 * i.color;
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
	FallBack "Mobile/Particles/Alpha Blended"
}