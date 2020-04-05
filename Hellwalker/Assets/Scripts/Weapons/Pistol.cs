using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    // References to scene objects
    public ParticleSystem rightpistolparticles; //References to the RIGHT pistol particles
    public ParticleSystem rightpistolshells; //References to the RIGHT pistol particles

    // Use this for initialization
    protected override void Start () {
        base.Start();

        rightpistolparticles = GetComponentInChildren<ParticleSystem>();

        this.didattack = true;
    }

    // Update is called once per frame
    protected override void Update () {

        bool keyInput = this.inputmanager.GetKeyInput("fire", 2);

        if (this.inputmanager.GetKeyInput("fire", 0))
        {
            if (CanFire())
            {
                this.doattack = true;
            }
        }

        if (keyInput)
        {
            this.doattack = false;
        }

        this.AttackDelayTimer -= Time.deltaTime;

        if (this.AttackDelayTimer < (float)0)
        {
            this.AttackDelayTimer = (float)0;
        }

        if (this.AttackDelayTimer <= (float)0)
        {
            if (doattack)
            {
                animator.SetTrigger("PistolFireTrigger");
                weaponData.traceroffset = 0.1f;
                this.AttackDelayTimer = weaponData.WeaponSpeed / weaponData.FireSpeed;
                this.didattack = false;
            }
        }

        if (!this.didattack)
        {
            if (this.AttackDelayTimer <= weaponData.AttackFrame)
            {
                ((MyMouseLook)Camera.main.GetComponent(typeof(MyMouseLook))).buck = weaponData.Buck;

                rightpistolparticles.Play();

                rightpistolshells.Emit(1);

                GameObject go = this.shootbullet(weaponData.Inaccuracy, (float)1000, 1, weaponData.Damage, 0, (float)3, (float)1, true, false);

                ((WeaponFlashScript)GameObject.Find("WeaponFlash").GetComponent(typeof(WeaponFlashScript))).flash = (float)20;
                this.didattack = true;

                // deduct ammo
                ammo--;
            }
        }
    }


}
