using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GibView : ObjectView
{
    [Header("Particle Timer")]
    [SerializeField] private int timer = 1;
    [Header("References")]
    [SerializeField] private ParticleSystem particle;

    public override void OnCreate(Context context)
    {
        try
        {
            GibContext gibContext = context as GibContext;

            particle.Play();

            particle.startSpeed = gibContext.dampenValue;
        }
        catch(System.Exception e)
        {
            Debug.Log(e.Message);
        }

    }

    // Start is called before the first frame update
    void Awake()
    {
        particle = this.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private async void OnEnable()
    {
        await Task.Delay(timer * 1000);
        try
        {
            particle.Stop();
            gameObject.SetActive(false);
        }
        catch(System.Exception e)
        {
            Debug.Log(e.Message + "...should be harmless.");
        }

    }
}
