using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public WeaponData weaponData;

    // References to scene objects
    public GameObject smoke;    // Reference to SmokeSystem
    public GameObject locke;    // Reference to LockonAimer
    public GameObject wanimate; // Reference to WeaponAnimator
    public GameObject bulletholesystem; // Reference to BulltHoldeSystem
    public GameObject bloodsplatsystem; // Reference to BloodSpat
    public GameObject plr; // Reference to Player
    public MyInputManager inputmanager; // References to InputManager
    public SelectionScript selectionScript; // References to SelectionScript
    public Animator animator; // References to WeaponAnimator Animator

    public bool doattack;
    public bool didattack;
    public float AttackDelayTimer = 0;

    // Use this for initialization
    protected virtual void Start () {

        GameObject DasMenu = GameObject.FindWithTag("DasMenu");
        GameObject WeaponAnimator = GameObject.FindWithTag("WeaponAnimator");

        // Initialize the references to scene objects
        this.smoke = GameObject.Find("SmokeSystem");
        this.locke = GameObject.Find("LockonAimer");
        this.wanimate = GameObject.Find("WeaponAnimator");
        this.plr = GameObject.Find("Player");
        this.bulletholesystem = UnityEngine.Object.Instantiate<GameObject>(weaponData.bulletholes, this.transform.position, Quaternion.identity);
        this.bloodsplatsystem = UnityEngine.Object.Instantiate<GameObject>(weaponData.dynamicbloodsplats, this.transform.position, Quaternion.identity);
        inputmanager = DasMenu.GetComponent<MyInputManager>();
        selectionScript = WeaponAnimator.GetComponent<SelectionScript>();
        animator = WeaponAnimator.GetComponent<Animator>();
        // End initialize to scene objects

    }

    protected virtual void Update()
    {

    }

    public virtual GameObject shootbullet(float inaccuracy, float range, int numberofbullets, float damage, int deathstyle, float forwardpower, float uppower, bool doricnoise, bool ignoretracer)
    {
        ((CrosshairSizeScript)GameObject.Find("Crosshair").GetComponent(typeof(CrosshairSizeScript))).plus = 0.7f;
        RaycastHit raycastHit = default(RaycastHit);
        GameObject gameObject = GameObject.Find("PlayerHand");
        Color startColor = default(Color);
        AudioClip audioClip = null;
        GameObject gameObject2 = null;
        bool flag = true;
        float num = (float)0;
        float num2 = (float)0;
        //this.superhotsuddenspeedup();
        int num3 = 0;
        int i = 0;
        while (i < numberofbullets)
        {
            i++;
            Vector3 direction = default(Vector3);
            direction.z = (float)1;
            direction.x = UnityEngine.Random.Range(-inaccuracy, inaccuracy);
            direction.y = UnityEngine.Random.Range(-inaccuracy / (float)2, inaccuracy / (float)2);
            if (Physics.Raycast(gameObject.transform.position, this.locke.transform.TransformDirection(direction), out raycastHit, range, weaponData.hitlayers))
            {
                gameObject2 = raycastHit.transform.gameObject;
                DestructibleObjectScript destructibleObjectScript = (DestructibleObjectScript)gameObject2.GetComponent(typeof(DestructibleObjectScript));
                if (!ignoretracer)
                {
                    GameObject gameObject3 = GameObject.Find("TracerAnchor2");
                    gameObject3.transform.position = this.wanimate.transform.position + this.wanimate.transform.right * weaponData.traceroffset;
                    GameObject gameObject4 = UnityEngine.Object.Instantiate<GameObject>(weaponData.tracerobject, this.transform.position, Quaternion.identity);
                    ((LineRenderer)gameObject4.GetComponent(typeof(LineRenderer))).SetPosition(0, gameObject3.transform.position);
                    ((LineRenderer)gameObject4.GetComponent(typeof(LineRenderer))).SetPosition(1, raycastHit.point);
                }
                if (gameObject2.tag == "ButtonTag" && flag && ((ButtonScript)gameObject2.GetComponent(typeof(ButtonScript))).canshoot)
                {
                    ButtonScript buttonScript = (ButtonScript)gameObject2.GetComponent(typeof(ButtonScript));
                    buttonScript.dopress();
                    flag = false;
                }
                if (gameObject2.tag == "GrappleTag")
                {
                    Debug.Log("DoGrapple not impletemented");
                    //this.DoGrapple(gameObject2);
                }
                if (destructibleObjectScript)
                {
                    if (deathstyle == 3)
                    {
                        deathstyle = UnityEngine.Random.Range(0, 2);
                    }
                    if (deathstyle == 2)
                    {
                        destructibleObjectScript.doragdoll = false;
                    }
                    if (deathstyle == 1)
                    {
                        destructibleObjectScript.dampen = true;
                        destructibleObjectScript.doragdoll = false;
                    }
                    if (deathstyle == 0)
                    {
                        destructibleObjectScript.doragdoll = true;
                        destructibleObjectScript.ragdollvelocity = this.transform.forward * forwardpower + this.transform.up * uppower;
                    }
                    if (gameObject2.tag == "EnemyTag" && (BasicAIScript)gameObject2.GetComponent(typeof(BasicAIScript)))
                    {
                        BasicAIScript basicAIScript = (BasicAIScript)gameObject2.GetComponent(typeof(BasicAIScript));
                        basicAIScript.MyTarget = this.plr;
                        if (basicAIScript.AMAWAKE || ignoretracer)
                        {
                        }
                    }
                    if (gameObject2.tag == "EnemyTag")
                    {
                        GameObject gameObject5 = UnityEngine.Object.Instantiate<GameObject>(weaponData.bloodarcs, raycastHit.point, Quaternion.identity);
                        gameObject5.transform.LookAt(GameObject.Find("MainCamera").transform);
                        destructibleObjectScript.wasdamaged = true;
                        ((BasicAIScript)gameObject2.GetComponent(typeof(BasicAIScript))).BasicCheckDamage();
                    }
                    //if (gameObject2.tag == "EnemyTag" && this.persist)
                    //{
                    //    this.persist.pacifistaward = false;
                    //    if (!ignoretracer)
                    //    {
                    //        this.persist.lowtechaward = false;
                    //    }
                    //}
                    destructibleObjectScript.myhealth -= damage;
                    destructibleObjectScript.checkhealth();
                    startColor = destructibleObjectScript.damagecolor;
                    audioClip = destructibleObjectScript.damagesound;
                }
                else
                {
                    startColor = weaponData.defaultparticlecolor;
                    if (doricnoise)
                    {
                        audioClip = weaponData.defaulthitsound;
                    }
                    if (weaponData.spawndust)
                    {
                        this.smoke.transform.position = raycastHit.point;
                        this.smoke.transform.forward = raycastHit.normal;
                        ((ParticleSystem)this.smoke.GetComponent(typeof(ParticleSystem))).Emit(UnityEngine.Random.Range(3, 10));
                    }
                }
                if (weaponData.spawndecals)
                {
                    if ((gameObject2.layer == 0 || gameObject2.layer == 26) && !destructibleObjectScript && !(DoorScript)gameObject2.GetComponent(typeof(DoorScript)) && gameObject2.tag == "Untagged")
                    {
                        ParticleSystem particleSystem = (ParticleSystem)this.bulletholesystem.GetComponent(typeof(ParticleSystem));
                        this.bulletholesystem.transform.position = raycastHit.point + raycastHit.normal * 0.02f;
                        Vector3 eulerAngles = Quaternion.LookRotation(raycastHit.normal).eulerAngles;
                        particleSystem.startRotation3D = eulerAngles * 0.017453292f;
                        particleSystem.Emit(1);
                        if (particleSystem.particleCount > particleSystem.maxParticles)
                        {
                            particleSystem.Clear();
                        }
                    }
                    if (gameObject2.layer == 14)
                    {
                        this.spawnbloodsplat(raycastHit.point, this.locke);
                    }
                }
                if (gameObject2.layer == 9)
                {
                    startColor = weaponData.ragdollbloodcolor;
                }
                if ((Rigidbody)gameObject2.GetComponent(typeof(Rigidbody)))
                {
                    ((Rigidbody)gameObject2.GetComponent(typeof(Rigidbody))).velocity = this.transform.forward * (float)20 + this.transform.up * (float)10;
                    ((Rigidbody)gameObject2.GetComponent(typeof(Rigidbody))).angularVelocity = this.transform.forward * (float)20;
                }
                GameObject gameObject6 = UnityEngine.Object.Instantiate<GameObject>(weaponData.hitparticles, raycastHit.point, Quaternion.identity);
                gameObject6.transform.forward = raycastHit.normal;
                ((ParticleSystem)gameObject6.GetComponent(typeof(ParticleSystem))).startColor = startColor;
                if (gameObject2.tag == "EnemyTag")
                {
                    if (num3 > 3)
                    {
                        ((AudioSource)gameObject6.GetComponent(typeof(AudioSource))).volume = (float)0;
                    }
                    else
                    {
                        num3++;
                    }
                }
                if (audioClip != null)
                {
                    ((AudioSource)gameObject6.GetComponent(typeof(AudioSource))).clip = audioClip;
                    ((AudioSource)gameObject6.GetComponent(typeof(AudioSource))).pitch = UnityEngine.Random.Range(0.85f, 1.15f);
                    ((AudioSource)gameObject6.GetComponent(typeof(AudioSource))).Play();
                }
            }
        }
        return gameObject2;
    }

    // Token: 0x0600004C RID: 76 RVA: 0x000073C0 File Offset: 0x000055C0
    public virtual void spawnbloodsplat(Vector3 origin, GameObject ob)
    {
        float num = 0.05f;
        RaycastHit raycastHit = default(RaycastHit);
        int i = 0;
        ParticleSystem particleSystem = (ParticleSystem)this.bloodsplatsystem.GetComponent(typeof(ParticleSystem));
        while (i < 3)
        {
            i++;
            Vector3 direction = default(Vector3);
            direction.z = (float)1;
            direction.x = UnityEngine.Random.Range(-num, num);
            direction.y = UnityEngine.Random.Range(-num, num);
            Vector3 direction2 = ob.transform.TransformDirection(direction);
            if (Physics.Raycast(origin, direction2, out raycastHit, (float)12, weaponData.bloodsplatlayers) && !(DestructibleObjectScript)raycastHit.transform.gameObject.GetComponent(typeof(DestructibleObjectScript)) && !(DoorScript)raycastHit.transform.gameObject.GetComponent(typeof(DoorScript)) && raycastHit.transform.gameObject.tag == "Untagged")
            {
                this.bloodsplatsystem.transform.position = raycastHit.point + raycastHit.normal * 0.02f;
                Vector3 eulerAngles = Quaternion.LookRotation(raycastHit.normal).eulerAngles;
                particleSystem.startRotation3D = eulerAngles * 0.017453292f;
                particleSystem.Emit(1);
                if (particleSystem.particleCount > particleSystem.maxParticles)
                {
                    particleSystem.Clear();
                }
            }
        }
    }
}
