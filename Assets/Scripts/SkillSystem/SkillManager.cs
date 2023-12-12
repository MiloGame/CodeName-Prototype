using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [SerializeField] public List<SkillTreeManager> skillTreeManagers;
    // Start is called before the first frame update
    public void FreshStart()
    {
        skillTreeManagers = new List<SkillTreeManager>();
        skillTreeManagers.Add(new SkillTreeManager("GetHeal"));
        skillTreeManagers.Add( new SkillTreeManager("Grenade"));
        skillTreeManagers.Add(new SkillTreeManager("EarthShatter"));
        skillTreeManagers.Add(new SkillTreeManager("BuildWall"));
        skillTreeManagers.Add(new SkillTreeManager("ControlEnemy"));
        foreach (var skillTreeManager in skillTreeManagers)
        {
            skillTreeManager.Init();
        }
    }

    // Update is called once per frame
    public void FreshUpdate()
    {
        foreach (var skillTreeManager in skillTreeManagers)
        {
            skillTreeManager.Fresh();
        }
    }
}
