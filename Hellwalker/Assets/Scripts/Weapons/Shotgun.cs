using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    public int shotgunpellets;
    // References to scene objects
    public ParticleSystem shotgunparticles; //References to the Shotgun particles
    public ParticleSystem shotgunshells; //References to the Shotgun shell particles

    protected override void Start () {

        base.Start();

        animator = this.GetComponent<Animator>();

        this.didattack = true;

    }

    // Update is called once per frame
    protected override void Update()
    {
        bool keyInput = this.inputmanager.GetKeyInput("fire", 2);

        if (this.inputmanager.GetKeyInput("fire", 0))
        {
            this.doattack = true;
        }

        if (keyInput)
        {
            this.doattack = false;
        }

        this.AttackDelayTimer -= Time.deltaTime;

        if (this.AttackDelayTimer < (float)0)
        {
            print("heck");
            this.AttackDelayTimer = (float)0;
        }

        if (this.AttackDelayTimer <= (float)0)
        {
            if (doattack)
            {
                animator.SetTrigger("RightShotgunTrigger");
                weaponData.traceroffset = 0.25f;
                this.AttackDelayTimer = weaponData.WeaponSpeed / weaponData.FireSpeed;
                this.didattack = false;
            }
        }

        if (!this.didattack)
        {
            if (this.AttackDelayTimer <= weaponData.AttackFrame)
            {
                ((MyMouseLook)Camera.main.GetComponent(typeof(MyMouseLook))).buck = weaponData.Buck;

                shotgunparticles.Play();

                shotgunshells.Emit(1);

                GameObject go = this.shootbullet(weaponData.Inaccuracy, (float)1000, weaponData.Pellet, weaponData.Damage, 0, (float)3, (float)1, true, false);

                ((WeaponFlashScript)GameObject.Find("WeaponFlash").GetComponent(typeof(WeaponFlashScript))).flash = (float)20;
                this.didattack = true;
            }
        }
    }
}
