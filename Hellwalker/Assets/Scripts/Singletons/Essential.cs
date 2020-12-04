using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Essential : Singleton<Essential>
{
    [Header("References")]
    public MyInputManager inputManager;
    public MyControllerScript controllerScript;

    void Awake()
    {
        inputManager = this.GetComponent<MyInputManager>();
        controllerScript = this.GetComponent<MyControllerScript>();
    }
}
