using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreComponent : MonoBehaviour
{
    protected Core core; 
    public bool debugMode = true;

    public virtual void Init(Core core)
    {
        this.core = core;
    }
    protected virtual void Awake()
    {

    }

    public void Log(string message)
    {
        if (debugMode)
        {
            Debug.Log(message);
        }
    }
    public virtual void LogicUpdate()
    {
        
    }

}
