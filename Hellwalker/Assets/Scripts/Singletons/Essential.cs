using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Essential : MonoBehaviour
{
    public static Essential Instance;
    [Header("References")]
    public MyInputManager inputManager;
    public ShaderScript shaderScript;
    public LightTriggerMASTERSCRIPT lightMASTERScript;
    public LowSpecModeScript lowSpecModeScript;
    void Awake()
    {
        Instance = this;
        inputManager = this.GetComponent<MyInputManager>();
        shaderScript = this.GetComponent<ShaderScript>();
        lightMASTERScript = this.GetComponent<LightTriggerMASTERSCRIPT>();
        lowSpecModeScript = this.GetComponent<LowSpecModeScript>();
    }
}
