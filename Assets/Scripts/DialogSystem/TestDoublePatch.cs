using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaserClass
{
    protected string pathstring;

}

public class child1 : BaserClass
{

}
public class child2 : BaserClass
{

}

public class Extract{
    public void ext(child1 cc)
    {
        Debug.Log("extract child1");
    }
    public void ext(child2 cc)
    {
        Debug.Log("extract child2");
    }
}
public class TestDoublePatch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Extract ecExtract = new Extract();
        List<BaserClass> listClasses = new List<BaserClass>();
        listClasses.Add(new child1());
        listClasses.Add(new child2());
        foreach (var baserClass in listClasses)
        {
            //ecExtract.ext(baserClass);
            //Debug.Log(baserClass.GetType());
        }
    }

   
}
