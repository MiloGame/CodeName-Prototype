using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class RifleModel : GunBaseData 
{



    public RifleModel()
    {
        gravity = 0.1f;
        totalammo = 35;
        bulletLife = 30f;
        speed = 100f;
        remains = totalammo;
        CanContinuShoot = true;
        CoolenTime = 0.05f;
        bulletdamage = 5f;
    }
    
}
