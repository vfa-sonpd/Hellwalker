using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : ObjectView {

    [Header("References")]
    [SerializeField] private MyControllerScript controller;
    [SerializeField] private MyMouseLook mouseController;

    public override void OnCreate(Context context)
    {
        throw new System.NotImplementedException();
    }

    // Use this for initialization
    void Awake () {
        controller = this.GetComponent<MyControllerScript>();
        mouseController = this.GetComponentInChildren<MyMouseLook>();
    }
	
	public void ControllerOverride(InputControllerOverride inputOverride)
    {
        controller.Override(inputOverride);
    }

    public void MouseInputOverride(bool isOn)
    {
        mouseController.enabled = isOn;
    }

    public void ForceLookAt(Transform t)
    {
        mouseController.transform.LookAt(t);
    }
}
