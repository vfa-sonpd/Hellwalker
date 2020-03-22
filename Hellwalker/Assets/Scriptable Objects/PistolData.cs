
using UnityEngine;

[CreateAssetMenu(fileName = "PistolData", menuName = "Weapons/Pistol Data", order = 1)]
public class PistolData : WeaponData
{
    public GameObject gunObject;

    public GameObject rightPistolPrefab;

    public GameObject leftPistolPrefab;

    public ParticleSystem rightpistolparticles;

    public ParticleSystem rightpistolshells;

    //public override bool Equip(Transform t)
    //{
    //    equipped = true;

    //    gunObject = Instantiate(rightPistolPrefab, t, false);

    //    animator = t.GetComponent<Animator>();

    //    if (!inputmanager)
    //    {
    //        GameObject DasMenu = GameObject.FindWithTag("DasMenu");
    //        inputmanager = DasMenu.GetComponent<MyInputManager>();
    //    }

    //    this.didattack = true;

    //    selectionTransform = t;

    //    this.smoke = GameObject.Find("SmokeSystem");

    //    this.locke = GameObject.Find("LockonAimer");

    //    return true;
    //}

    //public override void Unequip()
    //{
    //    equipped = false;

    //    if (gunObject != null)
    //    {
    //        Destroy(gunObject);
    //    }
    //}

    //public override void Work()
    //{
    //    if (!equipped)
    //        return;

    //    // Detect when fire button raised ?
    //    bool keyInput = inputmanager.GetKeyInput("fire", 2);

    //    if (this.inputmanager.GetKeyInput("fire", 0))
    //    {
    //        this.doattack = true;
    //    }

    //    if (keyInput)
    //    {
    //        this.doattack = false;
    //    }

    //    this.AttackDelayTimer -= Time.deltaTime;
    //    if (this.AttackDelayTimer < (float)0)
    //    {
    //        this.AttackDelayTimer = (float)0;
    //    }

    //    if (this.AttackDelayTimer <= (float)0)
    //    {

    //        if (doattack)
    //        {
    //            Debug.Log("PistolFireTrigger;" );
    //            animator.SetTrigger("PistolFireTrigger");
    //            this.traceroffset = 0.1f;
    //            this.AttackDelayTimer = this.WeaponSpeed / this.FireSpeed;

    //            this.didattack = false;
    //        }


    //    }
    //    Debug.Log("this.didattack" + this.didattack);
    //    if (!this.didattack)
    //    {
    //        if (this.AttackDelayTimer <= this.AttackFrame)
    //        {
    //            Debug.Log("pistol attack frame MAIN" + this.AttackFrame);

    //            ((MyMouseLook)Camera.main.GetComponent(typeof(MyMouseLook))).buck = this.Buck;

    //            rightpistolparticles = gunObject.GetComponentInChildren<ParticleSystem>();
    //            rightpistolparticles.Play();

    //            //rightpistolshells.Emit(1);

    //            float num3 = this.Inaccuracy;

    //         //   GameObject go = this.shootbullet(num3, (float)1000, 1, 24 , 0, (float)3, (float)1, true, false);

    //            //Debug.Log("GO   " +go);
    //            //((WeaponFlashScript)GameObject.Find("WeaponFlash").GetComponent(typeof(WeaponFlashScript))).flash = (float)20;
    //            //this.didattack = true;

    //        }
    //    }


    //}





}
