
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SkillTreeData
{
    public List<SkillBaseNode> NodesData = new List<SkillBaseNode>();
    private int index = 0;
    public SkillBaseNode CreateNode(SkillBaseNode.TreeNodeType nodeType)
    {
       // SkillBaseNode resNode;
        switch (nodeType)
        {
            case SkillBaseNode.TreeNodeType.DebugNode:
                DebugNode resdebug = new DebugNode();
                resdebug.Titlename = "DebugNode" + index;
                index++;
                resdebug.nodeGuid = Guid.NewGuid().ToString();
                resdebug.Nodetype = nodeType;
                Debug.Log("treedata create success"+ resdebug.Titlename+ resdebug.nodeGuid);
                return resdebug;
            case SkillBaseNode.TreeNodeType.SkillSecquenceNode:
                SkillBaseNode ressec = new SkillSecquenceNode();
                ressec.Titlename = "SkillSecquenceNode" + index;
                index++;
                ressec.nodeGuid = Guid.NewGuid().ToString();
                ressec.Nodetype = nodeType;
                Debug.Log("treedata create success" + ressec.Titlename + ressec.nodeGuid);
                return ressec;
            case SkillBaseNode.TreeNodeType.SkillForEachNode:
                SkillForEachNode resforeach = new SkillForEachNode();
                resforeach.Titlename = "SkillForEachNode" + index;
                index++;
                resforeach.nodeGuid = Guid.NewGuid().ToString();
                resforeach.Nodetype = nodeType;
                Debug.Log("treedata create success" + resforeach.Titlename + resforeach.nodeGuid);
                return resforeach;
            case SkillBaseNode.TreeNodeType.SkillInverterNode:
                SkillInverterNode resinv = new SkillInverterNode();
                resinv.Titlename = "SkillInverterNode" + index;
                index++;
                resinv.nodeGuid = Guid.NewGuid().ToString();
                resinv.Nodetype = nodeType;
                Debug.Log("treedata create success" + resinv.Titlename + resinv.nodeGuid);
                return resinv;
            case SkillBaseNode.TreeNodeType.SkillRepeateNode:
                SkillRepeateNode resrpt = new SkillRepeateNode();
                resrpt.Titlename = "SkillRepeateNode" + index;
                index++;
                resrpt.nodeGuid = Guid.NewGuid().ToString();
                resrpt.Nodetype = nodeType;
                Debug.Log("treedata create success" + resrpt.Titlename + resrpt.nodeGuid);
                return resrpt;
            case SkillBaseNode.TreeNodeType.SkillRootNode:
                SkillRootNode resroot = new SkillRootNode();
                resroot.Titlename = "SkillRootNode" + index;
                index++;
                resroot.nodeGuid = Guid.NewGuid().ToString();
                resroot.Nodetype = nodeType;
                Debug.Log("treedata create success" + resroot.Titlename + resroot.nodeGuid);
                return resroot;
            case SkillBaseNode.TreeNodeType.SkillWaitNode:
                SkillWaitNode reswait = new SkillWaitNode();
                reswait.Titlename = "SkillWaitNode" + index;
                index++;
                reswait.nodeGuid = Guid.NewGuid().ToString();
                reswait.Nodetype = nodeType;
                Debug.Log("treedata create success" + reswait.Titlename + reswait.nodeGuid);
                return reswait;
            case SkillBaseNode.TreeNodeType.SkillSingeClick:
                SkillSingeClick ressic = new SkillSingeClick();
                ressic.Titlename = "SkillSingeClick" + index;
                index++;
                ressic.nodeGuid = Guid.NewGuid().ToString();
                ressic.Nodetype = nodeType;
                Debug.Log("treedata create success" + ressic.Titlename + ressic.nodeGuid);
                return ressic;
            case SkillBaseNode.TreeNodeType.GetHurtNode:
                GetHurtNode reshurt = new GetHurtNode();
                reshurt.Titlename = "GetHurtNode" + index;
                index++;
                reshurt.nodeGuid = Guid.NewGuid().ToString();
                reshurt.Nodetype = nodeType;
                Debug.Log("treedata create success" + reshurt.Titlename + reshurt.nodeGuid);
                return reshurt;
            case SkillBaseNode.TreeNodeType.GetHealNode:
                GetHealNode resheal = new GetHealNode();
                resheal.Titlename = "GetHealNode" + index;
                index++;
                resheal.nodeGuid = Guid.NewGuid().ToString();
                resheal.Nodetype = nodeType;
                Debug.Log("treedata create success" + resheal.Titlename + resheal.nodeGuid);
                return resheal;
            case SkillBaseNode.TreeNodeType.SkillLongPressNode:
                SkillLongPressNode reslongpress = new SkillLongPressNode();
                reslongpress.Titlename = "SkillLongPressNode" + index;
                index++;
                reslongpress.nodeGuid = Guid.NewGuid().ToString();
                reslongpress.Nodetype = nodeType;
                Debug.Log("treedata create success" + reslongpress.Titlename + reslongpress.nodeGuid);
                return reslongpress;
            case SkillBaseNode.TreeNodeType.SheldSkillNode:
                SheldSkillNode resSheld = new SheldSkillNode();
                resSheld.Titlename = "SheldSkillNode" + index;
                index++;
                resSheld.nodeGuid = Guid.NewGuid().ToString();
                resSheld.Nodetype = nodeType;
                Debug.Log("treedata create success" + resSheld.Titlename + resSheld.nodeGuid);
                return resSheld;
            case SkillBaseNode.TreeNodeType.SkillFunIfNode:
                SkillFunIfNode resFunIf = new SkillFunIfNode();
                resFunIf.Titlename = "SkillFunIfNode" + index;
                index++;
                resFunIf.nodeGuid = Guid.NewGuid().ToString();
                resFunIf.Nodetype = nodeType;
                Debug.Log("treedata create success" + resFunIf.Titlename + resFunIf.nodeGuid);
                return resFunIf;
            case SkillBaseNode.TreeNodeType.ArmGrenadeNode:
                ArmGrenadeNode resArmGrenade = new ArmGrenadeNode();
                resArmGrenade.Titlename = "ArmGrenadeNode" + index;
                index++;
                resArmGrenade.nodeGuid = Guid.NewGuid().ToString();
                resArmGrenade.Nodetype = nodeType;
                Debug.Log("treedata create success" + resArmGrenade.Titlename + resArmGrenade.nodeGuid);
                return resArmGrenade;
            case SkillBaseNode.TreeNodeType.DisArmGrenadeNode:
                DisArmGrenadeNode resDisArmGrenade = new DisArmGrenadeNode();
                resDisArmGrenade.Titlename = "DisArmGrenadeNode" + index;
                index++;
                resDisArmGrenade.nodeGuid = Guid.NewGuid().ToString();
                resDisArmGrenade.Nodetype = nodeType;
                Debug.Log("treedata create success" + resDisArmGrenade.Titlename + resDisArmGrenade.nodeGuid);
                return resDisArmGrenade;
            case SkillBaseNode.TreeNodeType.SkillFunDoubleIfNode:
                SkillFunDoubleIfNode resSkillFunDoubleIf = new SkillFunDoubleIfNode();
                resSkillFunDoubleIf.Titlename = "SkillFunDoubleIfNode" + index;
                index++;
                resSkillFunDoubleIf.nodeGuid = Guid.NewGuid().ToString();
                resSkillFunDoubleIf.Nodetype = nodeType;
                Debug.Log("treedata create success" + resSkillFunDoubleIf.Titlename + resSkillFunDoubleIf.nodeGuid);
                return resSkillFunDoubleIf;
            case SkillBaseNode.TreeNodeType.EarthShatterNode:
                EarthShatterNode resEarthShatter = new EarthShatterNode();
                resEarthShatter.Titlename = "EarthShatterNode" + index;
                index++;
                resEarthShatter.nodeGuid = Guid.NewGuid().ToString();
                resEarthShatter.Nodetype = nodeType;
                Debug.Log("treedata create success" + resEarthShatter.Titlename + resEarthShatter.nodeGuid);
                return resEarthShatter;
            case SkillBaseNode.TreeNodeType.AimWallNode:
                AimWallNode resAimWallNode = new AimWallNode();
                resAimWallNode.Titlename = "AimWallNode" + index;
                index++;
                resAimWallNode.nodeGuid = Guid.NewGuid().ToString();
                resAimWallNode.Nodetype = nodeType;
                Debug.Log("treedata create success" + resAimWallNode.Titlename + resAimWallNode.nodeGuid);
                return resAimWallNode;
            case SkillBaseNode.TreeNodeType.ControlEnemyNode:
                ControlEnemyNode resControlEnemyNode = new ControlEnemyNode();
                resControlEnemyNode.Titlename = "AimWallNode" + index;
                index++;
                resControlEnemyNode.nodeGuid = Guid.NewGuid().ToString();
                resControlEnemyNode.Nodetype = nodeType;
                Debug.Log("treedata create success" + resControlEnemyNode.Titlename + resControlEnemyNode.nodeGuid);
                return resControlEnemyNode;

            default:
                throw new ArgumentOutOfRangeException(nameof(nodeType), nodeType, null);
        }

    }

    public void DeleteNode(SkillBaseNode elemNodeToView)
    {
       // Debug.Log("treedata remove success" + elemNodeToView.Titlename + elemNodeToView.nodeGuid);
        NodesData.Remove(elemNodeToView);
    }
}
