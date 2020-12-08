using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SoldierRagdollView : ObjectView
{
    [Header("Timer")]
    [SerializeField] private int timer = 1;
    [Header("References")]
    [SerializeField] private DestructibleObjectScript destructible;
    [SerializeField] private float initialHP;
    [SerializeField] private ParticleSystem bloodMistParticle;
    [SerializeField] private Rigidbody mainRigidbody;

    public override void OnCreate(Context context)
    {
        destructible.doragdoll = false;
        destructible.myhealth = initialHP;

        // Setup based on received context
        SoldierRagdollContext ragdollContext = context as SoldierRagdollContext;
        transform.position = ragdollContext.position;
        transform.rotation = ragdollContext.rotation;

        if (mainRigidbody)
        {
            mainRigidbody.velocity = ragdollContext.ragdollVelocity;
        }

        bloodMistParticle.Play();
    }

    // Start is called before the first frame update
    void Awake()
    {
        destructible = this.GetComponent<DestructibleObjectScript>();
        bloodMistParticle = this.GetComponentInChildren<ParticleSystem>();
        mainRigidbody = this.GetComponent<Rigidbody>();
        initialHP = destructible.myhealth;
    }

    private async void OnEnable()
    {
        await Task.Delay(timer * 1000);
        try
        {
            bloodMistParticle.Stop();
            gameObject.SetActive(false);
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message + "...should be harmless.");
        }

    }
}
