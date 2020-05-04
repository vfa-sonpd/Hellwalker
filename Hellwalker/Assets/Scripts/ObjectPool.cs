using com.ootii.Messages;
using Lean;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

    [SerializeField]
    private LeanPool bloodMistPool;

	// Use this for initialization
	void Start () {
		MessageDispatcher.AddListener(GameEvent.SPANW_BLOOD_MIST, OnSpawnBloodMist);
	}
	
	void OnSpawnBloodMist(IMessage rInstance)
    {
        Vector3 worldPosition = (Vector3)rInstance.Data;
        bloodMistPool.FastSpawn(worldPosition, default(Quaternion));
    }
}
