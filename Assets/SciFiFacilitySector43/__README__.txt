Thank you for purchasing the Sci-Fi Facility Sector 43. We hope you enjoy our 3D environments.

This package uses linear color space (Edit->Project Settings->Player->Color Space) and the deferred render path (Edit->Project Settings->Graphics->Rendering Path). Please change your project settings accordingly.

To achieve the same visuals as shown in the screenshots install the Post Processing stack via the Windows->Packet Manager. 
Follow the Unity docs to set up the Postprocessing stack: https://unity3d.com/how-to/set-up-post-processing-stack
You find the appropriate profiles under scenes/scene*Profiles

This package comes with a few custom shaders. In URP and HDRP you can edit these shaders with Shadergraph. 
For moving objects use the *OS* shader (OS -> Object Space)
For static objects use the *WS* shader (WS - World Space)

Using the HDRP version.
1. Create a new HDRP Unity project using Unity 2019.4 or higher. 
2. Import the HDRP package found in HDRP-URP-Package folder.
Do not double click the HDRP package in the current Standard project. Manuell Renderpipeline conversion is not necessary.

Using the URP version.
1. Create a new URP Unity project using Unity 2019.4 or higher. 
2. Import the URP package found in HDRP-URP-Package folder.
Do not double click the URP package in the current Standard project. Manuell Renderpipeline conversion is not necessary.