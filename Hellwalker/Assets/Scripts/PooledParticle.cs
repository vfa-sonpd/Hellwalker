using Lean;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledParticle : MonoBehaviour {

    public LeanPool originPool;

    public float time = 0.5f;

    ParticleSystem ps;

    private void Awake()
    {
        ps = this.GetComponent<ParticleSystem>();
    }

    // Event called by LeanPool on spawn
    void OnSpawn()
    {
        StartCoroutine(Despawn());
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(ps.duration);
        originPool.FastDespawn(gameObject);
    }
}
