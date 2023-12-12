using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestREF : MonoBehaviour
{
    public aaa a1;
    // Start is called before the first frame update
    void Start()
    {
        a1 = new aaa();
    }

    void changeval(aaa s)
    {
        s.val = 1;
        Debug.Log("s val"+s.val);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            changeval(a1);
            Debug.Log("a1 val"+a1.val);
        }
    }
}

public class aaa
{
    public int val;

    public aaa()
    {
        val = 0;
    }
}