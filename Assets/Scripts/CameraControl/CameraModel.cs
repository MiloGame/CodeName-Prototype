using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraModel : MonoBehaviour
{
    public Vector3 TpsoffsetVector3 =new Vector3(0.7f, 1.54f, 0);
    public bool ShortPressMouseLeft;
    public bool LongPressMouseLeft;
    public bool ShortPressV;
    public bool LongPressV;
    public bool EnableFreeLook =true;
    public float angleX;
    public float angleY;
    public float radius=10.0f;
    public Vector3 LookAtPos;
    public float camhitColliderpos;
    public bool Ishitcollider;
    public Vector3 orbit;
    public float LookAtPosRadius;
    public Vector3 LookPosorbit;
    public Vector3 CamerahitPos;
    public bool EnableSkillLook;
    public float SkillangleY;
    public float SkillangleX;
    public Vector3 SkillLookAtPos;
    public float Skillradius;
    public Vector3 Skillorbit;
    public enum mode
    {
        Freelook,
        Skill
    }

    public mode CurrMode;
}
