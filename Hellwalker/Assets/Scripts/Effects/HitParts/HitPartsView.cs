using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class HitPartsView : ObjectView
{
    [Header("Particle Timer")]
    [SerializeField] private int timer = 5;
    [Header("References")]
    [SerializeField] private ParticleSystem particle;
    private void Awake()
    {
        particle = this.GetComponent<ParticleSystem>();
    }
    public override void OnCreate(Context context)
    {
        transform.position = context.position;
        transform.rotation = context.rotation;

        // Play particle
        particle.Play();
    }
    private async void OnEnable()
    {
        await Task.Delay(timer * 1000);
        try
        {
            particle.Stop();
            gameObject.SetActive(false);
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message + "...should be harmless.");
        }
    }
}
