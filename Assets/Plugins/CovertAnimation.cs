using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using System.IO;
using System.Runtime.CompilerServices;

public class CovertAnimation : Editor
{
    [MenuItem("Tools/Animation/SetAnimLoop")]
    public static void CovertAnim()
    {
        Object[] obj= Selection.objects;
        for (int i = 0; i < obj.Length; i++)
        {
            if (obj[i] is AnimationClip)
            {
                AnimationClip clip = obj[i] as AnimationClip;

                AnimationClipSettings set= AnimationUtility.GetAnimationClipSettings(clip);
                set.loopTime = true;
                AnimationUtility.SetAnimationClipSettings(clip,set);
            }


        }
        AssetDatabase.Refresh();

    }
    [MenuItem("Tools/Animation/SetMeshAnim")]
    public static void SetMeshAnim()
    {
        Object[] obj = Selection.objects;
        for (int i = 0; i < obj.Length; i++)
        {
            if (obj[i] is GameObject)
            {
                GameObject mesh = obj[i] as GameObject;
                string path = AssetDatabase.GetAssetPath(mesh);
                ModelImporter modelimporter = ModelImporter.GetAtPath(path) as ModelImporter;
                modelimporter.globalScale = 1.0f;
                modelimporter.meshCompression = ModelImporterMeshCompression.Off;
                //modelimporter.animationType = ModelImporterAnimationType.Generic;
                modelimporter.isReadable = false;
               // modelimporter.optimizeMesh = true;
                modelimporter.optimizeMeshPolygons = true;
                modelimporter.optimizeMeshVertices = false;
                modelimporter.optimizeGameObjects = true;
                ModelImporterClipAnimation[] animations= modelimporter.defaultClipAnimations;
              
                for (int j = 0; j < animations.Length; j++)
                {
                    animations[j].lockRootPositionXZ = true;
                    animations[j].lockRootHeightY = true;
                    animations[j].lockRootRotation = true;
                    if (j==0)
                        animations[j].name = mesh.name;
                    else
                        animations[j].name = mesh.name+j;

                }
                modelimporter.clipAnimations = animations;
                AssetDatabase.ImportAsset(path);
            }
        }
        AssetDatabase.Refresh();
    }
    [MenuItem("Tools/Animation/CopyAnimation")]
    public static void CopyAnimation()
    {
        Object[] objects = Selection.objects;
        foreach (UnityEngine.Object o in objects)   
        {
            if (o is GameObject)
            {
                AnimationClip clip = new AnimationClip();
                string path = AssetDatabase.GetAssetPath(o);
                string name = o.name;
                Debug.Log(path);
                AnimationClip fbxClip = AssetDatabase.LoadAssetAtPath<AnimationClip>(path);
                if (fbxClip!=null)
                {
                    EditorUtility.CopySerialized(fbxClip, clip);
                    string[] strs = path.Split('/');
                    string s = "";
                    for (int i = 0; i < strs.Length; i++)
                    {
                        
                        if (i==2)
                        {
                            s += "/HumanAnimation";
                        }
                        else if (i == strs.Length - 1)
                        {
                            s += ("/" + name+".anim");
                        }
                        else if (i == 0)
                        {
                            s += strs[i];
                        }
                        else
                        {
                            s +=("/"+ strs[i]);
                        }
                    }
                   
                    Debug.Log(s);
                    AssetDatabase.CreateAsset(clip, "Assets/Character/HumanAnimation/" + name + ".anim");
                }

                
            }
        }
    }

}
