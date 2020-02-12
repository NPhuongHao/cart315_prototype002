using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VarStorage : MonoBehaviour
{
    // Start is called before the first frame update
    
    protected string[] boxStatus = new string[10];
    public int currentBox = 0;
    void Start()
    {
        Debug.Log("running VarStorage");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }
}

