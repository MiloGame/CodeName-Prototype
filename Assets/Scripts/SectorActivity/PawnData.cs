using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
[Serializable]
public class PawnData
{
    public PawnData(float xVectorX,float yVectorY,float zVectorZ,string prefabname,float rotatex=0,float rotatey=0,float rotatez=0)
    {
        VectorX = xVectorX;
        VectorY = yVectorY;
        VectorZ = zVectorZ;
        Prefabname = prefabname;
        Rotatex = rotatex;
        Rotatey = rotatey;
        Rotatez = rotatez;
    }
    public float VectorX;
    public float VectorY;
    public float VectorZ;
    public float Rotatex;
    public float Rotatey;
    public float Rotatez;
    public string Prefabname;

}
