using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Weapon
{
    // References to scene objects
    public ParticleSystem machinegunparticles; //References to the MachineGun particles
    public ParticleSystem machinegunshellls; //References to the MachineGun shell particles

    public float currentm16spread;

    protected override void Start()
    {
        base.Start();

        this.didattack = true;
    }

    // Update is called once per frame
    protected override void Update()
    {

        bool keyInput = this.inputmanager.GetKeyInput("fire", 2);

        if (this.inputmanager.GetKeyInput("fire", 0))
        {
            if (CanFire())
            {
                this.doattack = true;
            }
        }

        // reduce spread when not firing ?
        if (!this.inputmanager.GetKeyInput("fire", 0))
        {
            if (this.currentm16spread > (float)0)
            {
                this.currentm16spread -= Time.deltaTime * (float)7;
            }
            if (this.currentm16spread < (float)0)
            {
                this.currentm16spread = (float)0;
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
                animator.SetTrigger("M16FireTrigger");
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

                machinegunparticles.Play();

                machinegunshellls.Emit(1);

                GameObject go = this.shootbullet(currentm16spread, (float)1000, 1, weaponData.Damage, 0, (float)3, (float)1, true, false);

                ((WeaponFlashScript)GameObject.Find("WeaponFlash").GetComponent(typeof(WeaponFlashScript))).flash = (float)20;
                this.didattack = true;

                this.currentm16spread += Time.deltaTime * (float)3;
                if (this.currentm16spread > weaponData.Inaccuracy)
                {
                    this.currentm16spread = weaponData.Inaccuracy;
                }

                // deduct ammo
                ammo--;
            }
        }
    }
}
