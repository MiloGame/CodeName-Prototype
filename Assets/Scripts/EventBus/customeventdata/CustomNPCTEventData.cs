using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomNPCTEventData : EventArgs
{
    public CustomNPCTEventData()
    {
        
    }

    public CustomNPCTEventData(CustomNPCTEventData data)
    {
        NPCID = data.NPCID;
        DialogTextAsset = data.DialogTextAsset;
    }
    public int NPCID;
    public TextAsset DialogTextAsset;
}
