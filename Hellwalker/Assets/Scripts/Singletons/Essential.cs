using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Essential : MonoBehaviour
{
    public static Essential Instance;
    [Header("References")]
    public MyInputManager inputManager;
    public MyControllerScript controllerScript;

    void Awake()
    {
        Instance = this;
        inputManager = this.GetComponent<MyInputManager>();
        controllerScript = this.GetComponent<MyControllerScript>();
    }
}
