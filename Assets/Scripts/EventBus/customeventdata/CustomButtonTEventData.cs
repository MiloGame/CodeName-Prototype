using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomButtonTEventData: EventArgs
{
    public CustomButtonTEventData()
    {
        
    }

    public CustomButtonTEventData(CustomButtonTEventData copyobj)
    {
        message = copyobj.message;
        ToJumpIndex = copyobj.ToJumpIndex;
        DialogManager = copyobj.DialogManager;
    }
    public string message;
    public int ToJumpIndex;
    public DialogManager DialogManager;
}
