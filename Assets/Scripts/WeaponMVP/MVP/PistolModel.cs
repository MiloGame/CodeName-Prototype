using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolModel :GunBaseData
{


    public PistolModel()
    {
        gravity = 0.1f;
        totalammo = 10;
        bulletLife = 30f;
        speed = 50f;
        remains = totalammo;
        CanContinuShoot = false;
        bulletdamage = 2f;
    }
  
}
