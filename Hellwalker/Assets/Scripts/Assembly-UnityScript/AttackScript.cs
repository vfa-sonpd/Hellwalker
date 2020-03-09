using System;
using TMPro;
using UnityEngine;

// Token: 0x0200000C RID: 12
[Serializable]
public class AttackScript : MonoBehaviour
{
	// Token: 0x06000046 RID: 70 RVA: 0x00004620 File Offset: 0x00002820
	public AttackScript()
	{
		this.spawndecals = true;
		this.spawndust = true;
		this.firespeed = 1f;
		this.canchargesword = true;
	}

	// Token: 0x06000047 RID: 71 RVA: 0x00004654 File Offset: 0x00002854
	public virtual void Start()
	{
		if (GameObject.Find("PERSIST"))
		{
			this.persist = (PersistScript)GameObject.Find("PERSIST").GetComponent(typeof(PersistScript));
		}
		this.lockonscript = (LockOnScript)GameObject.Find("LockonAimer").GetComponent(typeof(LockOnScript));
		this.locke = GameObject.Find("LockonAimer");
		this.inputmanager = (MyInputManager)GameObject.Find("DasMenu").GetComponent(typeof(MyInputManager));
		this.wcam = (Camera)GameObject.Find("WeaponCam").GetComponent(typeof(Camera));
		this.wanimate = GameObject.Find("WeaponAnimator");
		this.msg = GameObject.Find("MessageText");
		this.smoke = GameObject.Find("SmokeSystem");
		this.bulletholesystem = UnityEngine.Object.Instantiate<GameObject>(this.bulletholes, this.transform.position, Quaternion.identity);
		this.bloodsplatsystem = UnityEngine.Object.Instantiate<GameObject>(this.dynamicbloodsplats, this.transform.position, Quaternion.identity);
		this.doattack = false;
		this.attackdelaytimer = (float)0;
		this.didattack = true;
		this.zoomedin = false;
		this.plrhealth = (PlayerHealthManagement)GameObject.Find("Player").GetComponent(typeof(PlayerHealthManagement));
		this.plr = GameObject.Find("Player");
		this.alertfloat = (float)0;
		this.cam = GameObject.Find("MainCamera");
	}

	// Token: 0x06000048 RID: 72 RVA: 0x000047F0 File Offset: 0x000029F0
	public virtual void Update()
	{
		Animator animator = (Animator)this.GetComponent(typeof(Animator));
		int selectedweapon = ((SelectionScript)this.GetComponent(typeof(SelectionScript))).selectedweapon;
		this.detonatetimer -= Time.deltaTime;
		if (this.detonatetimer < (float)0)
		{
			this.detonatetimer = (float)0;
		}
		this.firespeedtimer -= Time.deltaTime;
		if (this.firespeedtimer < (float)0)
		{
			this.firespeedtimer = (float)0;
			this.firespeed = (float)1;
		}
		this.altfire();
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
		if (this.doblock)
		{
			this.block();
		}
		if (this.reloadbuck != (float)0)
		{
			((MyMouseLook)this.cam.GetComponent(typeof(MyMouseLook))).buck = ((MyMouseLook)this.cam.GetComponent(typeof(MyMouseLook))).buck + this.reloadbuck * (Time.deltaTime * (float)100);
		}
		this.reloadbuck = (float)0;
		this.attackdelaytimer -= Time.deltaTime;
		if (this.attackdelaytimer < (float)0)
		{
			this.attackdelaytimer = (float)0;
		}
		if (this.inputmanager.GetKeyInput("quick melee", 1))
		{
			((TextMeshProUGUI)this.msg.GetComponent(typeof(TextMeshProUGUI))).text = "Mighty Sickles Activated";
			((ClearMessageAfterTime)this.msg.GetComponent(typeof(ClearMessageAfterTime))).timer = ((ClearMessageAfterTime)GameObject.Find("MessageText").GetComponent(typeof(ClearMessageAfterTime))).defaulttime;
			((SelectionScript)this.GetComponent(typeof(SelectionScript))).weapontogetto = 1;
			((SelectionScript)this.GetComponent(typeof(SelectionScript))).havedaikatana = false;
			if (((SelectionScript)this.GetComponent(typeof(SelectionScript))).selectedweapon != 1)
			{
				((SelectionScript)this.GetComponent(typeof(SelectionScript))).lastselectedweapon = ((SelectionScript)this.GetComponent(typeof(SelectionScript))).selectedweapon;
			}
		}
		if (this.inputmanager.GetKeyInput("quick melee", 0) && ((SelectionScript)this.GetComponent(typeof(SelectionScript))).selectedweapon != 0)
		{
			this.doattack = true;
		}
		if (this.inputmanager.GetKeyInput("quick melee", 2))
		{
			this.doattack = false;
		}
		if (this.inputmanager.GetKeyInput("fire", 1) && selectedweapon > 0 && ((SelectionScript)this.GetComponent(typeof(SelectionScript))).ammoinventory[selectedweapon - 1] <= (float)0)
		{
			((AudioSource)this.emptygunsound.GetComponent(typeof(AudioSource))).Play();
		}
		bool keyInput = this.inputmanager.GetKeyInput("fire", 2);
		if (selectedweapon - 1 == ((SelectionScript)this.GetComponent(typeof(SelectionScript))).weapontogetto - 1 && selectedweapon != 8 && keyInput && selectedweapon > 0)
		{
			if (((SelectionScript)this.GetComponent(typeof(SelectionScript))).ammoinventory[selectedweapon - 1] <= (float)0)
			{
				bool flag = false;
				if (((SelectionScript)this.GetComponent(typeof(SelectionScript))).weapontogetto - 1 == 0)
				{
					flag = true;
				}
				if (!flag)
				{
					while (!flag)
					{
						((SelectionScript)this.GetComponent(typeof(SelectionScript))).weapontogetto = ((SelectionScript)this.GetComponent(typeof(SelectionScript))).weapontogetto - 1;
						if (((SelectionScript)this.GetComponent(typeof(SelectionScript))).weapontogetto <= 0)
						{
							((SelectionScript)this.GetComponent(typeof(SelectionScript))).weapontogetto = 0;
							flag = true;
						}
						if (((SelectionScript)this.GetComponent(typeof(SelectionScript))).ammoinventory[((SelectionScript)this.GetComponent(typeof(SelectionScript))).weapontogetto - 1] > (float)0 && ((SelectionScript)this.GetComponent(typeof(SelectionScript))).weaponinventory[((SelectionScript)this.GetComponent(typeof(SelectionScript))).weapontogetto - 1])
						{
							flag = true;
						}
					}
				}
			}
			if (((SelectionScript)this.GetComponent(typeof(SelectionScript))).weapontogetto < 0)
			{
				((SelectionScript)this.GetComponent(typeof(SelectionScript))).weapontogetto = 0;
			}
		}
		this.staffmeleetimer -= Time.deltaTime;
		if (this.staffmeleetimer < (float)0)
		{
			this.staffmeleetimer = (float)0;
		}
		if (this.inputmanager.GetKeyInput("flip", 1))
		{
			animator.SetTrigger("TrickTrigger");
			((Animator)GameObject.Find("leftshotgun").GetComponent(typeof(Animator))).SetTrigger("LeftShotgunTrick");
			((Animator)GameObject.Find("rightshotgun").GetComponent(typeof(Animator))).SetTrigger("RightShotgunTrick");
			((AudioSource)this.meleeswish.GetComponent(typeof(AudioSource))).Play();
			if ((selectedweapon == 2 || selectedweapon == 3 || selectedweapon == 5 || selectedweapon == 6 || selectedweapon == 8 || selectedweapon == 9) && this.staffmeleetimer == (float)0)
			{
				this.staffmeleetimer = 0.06f;
				GameObject gameObject = this.shootbullet((float)0, 1.5f, 1, (float)10, 0, (float)0, (float)0, false, true);
				if (gameObject)
				{
					if (gameObject.layer != 14)
					{
						((AudioSource)this.meleemetalhitsound.GetComponent(typeof(AudioSource))).Play();
					}
					else
					{
						((AudioSource)this.meleegorehitsound.GetComponent(typeof(AudioSource))).Play();
					}
				}
			}
		}
		this.cigartimer += Time.deltaTime;
		if (this.cigartimer >= (float)4)
		{
			this.cigartimer = (float)4;
		}
		if (this.inputmanager.GetKeyInput("fire", 1) && selectedweapon == 10 && !this.pauseafterthrowing && this.cigartimer == (float)4)
		{
			animator.SetTrigger("CigarTrigger");
			this.cigartimer = (float)0;
			if (((PlayerHealthManagement)GameObject.Find("Player").GetComponent(typeof(PlayerHealthManagement))).myhealth < (float)100)
			{
				((PlayerHealthManagement)GameObject.Find("Player").GetComponent(typeof(PlayerHealthManagement))).myhealth = ((PlayerHealthManagement)GameObject.Find("Player").GetComponent(typeof(PlayerHealthManagement))).myhealth + (float)1;
			}
		}
		if (this.inputmanager.GetKeyInput("fire", 0))
		{
			if (selectedweapon == 0)
			{
				((SelectionScript)this.GetComponent(typeof(SelectionScript))).weapontogetto = ((SelectionScript)this.GetComponent(typeof(SelectionScript))).holsteredweapon;
			}
			if (selectedweapon == 1 && ((SelectionScript)this.GetComponent(typeof(SelectionScript))).havedaikatana && this.plrhealth.myhealth >= (float)100 && !this.pauseafterthrowing)
			{
				this.initialswordchargetimer += Time.deltaTime;
				if (this.initialswordchargetimer >= 0.15f)
				{
					this.swordcharge += Time.deltaTime * 2.45f;
					if (this.swordcharge > (float)1)
					{
						this.swordcharge = (float)1;
					}
					this.doattack = false;
					if (this.canchargesword)
					{
						animator.SetTrigger("SwordChargeTrigger");
						((AudioSource)this.swordchargesound.GetComponent(typeof(AudioSource))).Play();
					}
					this.canchargesword = false;
				}
			}
			else
			{
				this.doattack = true;
			}
		}
		if (keyInput)
		{
			this.doattack = false;
			this.canchargesword = true;
			this.initialswordchargetimer = (float)0;
			if (selectedweapon == 1 && ((SelectionScript)this.GetComponent(typeof(SelectionScript))).havedaikatana && this.plrhealth.myhealth >= (float)100 && !this.pauseafterthrowing)
			{
				if (this.swordcharge >= (float)1)
				{
					animator.SetTrigger("SwordChargeStrikeTrigger");
					this.doattack = true;
					this.attackdelaytimer = (float)0;
				}
				else if (this.attackdelaytimer <= (float)0)
				{
					this.doattack = true;
					this.attackdelaytimer = (float)0;
					this.swordcharge = (float)0;
					((AudioSource)this.swordchargesound.GetComponent(typeof(AudioSource))).Stop();
				}
			}
			this.pauseafterthrowing = false;
			this.totemtimer = (float)0;
		}
		if (((SelectionScript)this.GetComponent(typeof(SelectionScript))).selectedweapon != ((SelectionScript)this.GetComponent(typeof(SelectionScript))).weapontogetto)
		{
			this.doattack = false;
		}
		if (this.attackdelaytimer <= (float)0)
		{
			if (selectedweapon == 1 && this.doattack && !this.pauseafterthrowing)
			{
				if (!((SelectionScript)this.GetComponent(typeof(SelectionScript))).havedaikatana)
				{
					if (!this.sickletoggle)
					{
						if (UnityEngine.Random.Range((float)0, 1f) < 0.5f)
						{
							animator.SetTrigger("HammerSwingTrigger");
						}
						else
						{
							animator.SetTrigger("HammerSwingTrigger3");
						}
					}
					else if (UnityEngine.Random.Range((float)0, 1f) < 0.5f)
					{
						animator.SetTrigger("HammerSwingTrigger2");
					}
					else
					{
						animator.SetTrigger("HammerSwingTrigger4");
					}
					this.sickletoggle = !this.sickletoggle;
				}
				else if (this.swordcharge < (float)1)
				{
					if (UnityEngine.Random.Range((float)0, 1f) < 0.5f)
					{
						animator.SetTrigger("SwordTrigger");
					}
					else
					{
						animator.SetTrigger("SwordTrigger2");
					}
				}
				if (((SelectionScript)this.GetComponent(typeof(SelectionScript))).weaponenhance)
				{
					((MyControllerScript)GameObject.Find("Player").GetComponent(typeof(MyControllerScript))).ExplosionDir = ((MyControllerScript)GameObject.Find("Player").GetComponent(typeof(MyControllerScript))).ExplosionDir + this.cam.transform.forward * (float)1;
				}
				if (!((SelectionScript)this.GetComponent(typeof(SelectionScript))).havedaikatana)
				{
					((AudioSource)this.meleeswish.GetComponent(typeof(AudioSource))).Play();
				}
				else if (this.swordcharge < (float)1)
				{
					((AudioSource)this.swordsound.GetComponent(typeof(AudioSource))).Play();
				}
				if (!((SelectionScript)this.GetComponent(typeof(SelectionScript))).havedaikatana)
				{
					this.attackdelaytimer = this.axespeed / this.firespeed;
				}
				else
				{
					this.attackdelaytimer = this.swordspeed / this.firespeed;
				}
				this.didattack = false;
			}
			if (selectedweapon == 3 && this.doattack && ((SelectionScript)this.GetComponent(typeof(SelectionScript))).ammoinventory[selectedweapon - 1] > (float)0 && !this.pauseafterthrowing)
			{
				if (!((SelectionScript)this.GetComponent(typeof(SelectionScript))).havedualshotguns)
				{
					this.traceroffset = 0.08f;
					((SelectionScript)this.GetComponent(typeof(SelectionScript))).ammoinventory[3] = ((SelectionScript)this.GetComponent(typeof(SelectionScript))).ammoinventory[3] - (float)1;
					((SelectionScript)this.GetComponent(typeof(SelectionScript))).ammoinventory[2] = ((SelectionScript)this.GetComponent(typeof(SelectionScript))).ammoinventory[2] - (float)1;
					animator.SetTrigger("ShotgunFireTrigger");
					((AudioSource)this.shotgunsound.GetComponent(typeof(AudioSource))).Play();
					this.attackdelaytimer = this.shotgunspeed / this.firespeed;
					this.didattack = false;
				}
				else
				{
					((SelectionScript)this.GetComponent(typeof(SelectionScript))).ammoinventory[3] = ((SelectionScript)this.GetComponent(typeof(SelectionScript))).ammoinventory[3] - (float)1;
					((SelectionScript)this.GetComponent(typeof(SelectionScript))).ammoinventory[2] = ((SelectionScript)this.GetComponent(typeof(SelectionScript))).ammoinventory[2] - (float)1;
					if (this.dualshotguntoggle)
					{
						this.traceroffset = 0.08f;
						((Animator)GameObject.Find("rightshotgun").GetComponent(typeof(Animator))).SetTrigger("RightShotgunTrigger");
					}
					else
					{
						this.traceroffset = -0.08f;
						((Animator)GameObject.Find("leftshotgun").GetComponent(typeof(Animator))).SetTrigger("LeftShotgunTrigger");
					}
					this.dualshotguntoggle = !this.dualshotguntoggle;
					((AudioSource)this.shotgunsound.GetComponent(typeof(AudioSource))).Play();
					this.attackdelaytimer = this.shotgunspeed / 1.45f / this.firespeed;
					this.didattack = false;
				}
			}
			if (selectedweapon == 4 && this.doattack && ((SelectionScript)this.GetComponent(typeof(SelectionScript))).ammoinventory[selectedweapon - 1] > (float)0 && !this.pauseafterthrowing)
			{
				if (((SelectionScript)this.GetComponent(typeof(SelectionScript))).ammoinventory[selectedweapon - 1] > (float)1)
				{
					this.traceroffset = (float)0;
					((SelectionScript)this.GetComponent(typeof(SelectionScript))).ammoinventory[3] = ((SelectionScript)this.GetComponent(typeof(SelectionScript))).ammoinventory[3] - (float)2;
					((SelectionScript)this.GetComponent(typeof(SelectionScript))).ammoinventory[2] = ((SelectionScript)this.GetComponent(typeof(SelectionScript))).ammoinventory[2] - (float)2;
					animator.SetTrigger("SuperShotgunFireTrigger");
					((AudioSource)this.supershotgunsound.GetComponent(typeof(AudioSource))).Play();
					this.attackdelaytimer = this.supershotgunspeed / this.firespeed;
					this.didattack = false;
				}
				else
				{
					((AudioSource)this.emptygunsound.GetComponent(typeof(AudioSource))).Play();
				}
			}
			if (selectedweapon == 2 && this.doattack && ((SelectionScript)this.GetComponent(typeof(SelectionScript))).ammoinventory[selectedweapon - 1] > (float)0 && !this.pauseafterthrowing)
			{
				((SelectionScript)this.GetComponent(typeof(SelectionScript))).ammoinventory[selectedweapon - 1] = ((SelectionScript)this.GetComponent(typeof(SelectionScript))).ammoinventory[selectedweapon - 1] - (float)1;
				if (!((SelectionScript)this.GetComponent(typeof(SelectionScript))).havedualpistols)
				{
					animator.SetTrigger("PistolFireTrigger");
					this.traceroffset = 0.1f;
				}
				else
				{
					this.pistoltoggle = !this.pistoltoggle;
					if (this.pistoltoggle)
					{
						animator.SetTrigger("RightPistolFireTrigger");
						this.traceroffset = 0.15f;
					}
					else
					{
						animator.SetTrigger("LeftPistolFireTrigger");
						this.traceroffset = -0.1f;
					}
				}
				((AudioSource)this.pistolsound.GetComponent(typeof(AudioSource))).Play();
				((AudioSource)this.pistolsound.GetComponent(typeof(AudioSource))).pitch = UnityEngine.Random.Range((float)1, 1.08f);
				if (!((SelectionScript)this.GetComponent(typeof(SelectionScript))).havedualpistols)
				{
					this.attackdelaytimer = this.pistolspeed / this.firespeed;
				}
				else
				{
					this.attackdelaytimer = this.dualpistolspeed / this.firespeed;
				}
				this.didattack = false;
			}
			if (selectedweapon == 6 && this.doattack && ((SelectionScript)this.GetComponent(typeof(SelectionScript))).ammoinventory[selectedweapon - 1] > (float)0 && !this.pauseafterthrowing)
			{
				this.traceroffset = (float)0;
				((SelectionScript)this.GetComponent(typeof(SelectionScript))).ammoinventory[selectedweapon - 1] = ((SelectionScript)this.GetComponent(typeof(SelectionScript))).ammoinventory[selectedweapon - 1] - (float)1;
				animator.SetTrigger("RifleFireTrigger");
				((AudioSource)this.riflesound.GetComponent(typeof(AudioSource))).Play();
				this.attackdelaytimer = this.riflespeed / this.firespeed;
				this.didattack = false;
			}
			if (selectedweapon == 7 && this.doattack && ((SelectionScript)this.GetComponent(typeof(SelectionScript))).ammoinventory[selectedweapon - 1] > (float)0 && !this.pauseafterthrowing)
			{
				if (!((SelectionScript)this.GetComponent(typeof(SelectionScript))).weaponenhance)
				{
					((SelectionScript)this.GetComponent(typeof(SelectionScript))).ammoinventory[selectedweapon - 1] = ((SelectionScript)this.GetComponent(typeof(SelectionScript))).ammoinventory[selectedweapon - 1] - (float)1;
				}
				animator.SetTrigger("ChainsawFireTrigger");
				((AudioSource)this.chainsawsound.GetComponent(typeof(AudioSource))).Play();
				this.attackdelaytimer = this.chainsawspeed / this.firespeed;
				this.didattack = false;
			}
			if (selectedweapon == 5 && this.doattack && ((SelectionScript)this.GetComponent(typeof(SelectionScript))).ammoinventory[selectedweapon - 1] > (float)0 && !this.pauseafterthrowing)
			{
				this.traceroffset = 0.08f;
				((SelectionScript)this.GetComponent(typeof(SelectionScript))).ammoinventory[selectedweapon - 1] = ((SelectionScript)this.GetComponent(typeof(SelectionScript))).ammoinventory[selectedweapon - 1] - (float)1;
				animator.SetTrigger("M16FireTrigger");
				((AudioSource)this.m16sound.GetComponent(typeof(AudioSource))).Play();
				((AudioSource)this.m16sound.GetComponent(typeof(AudioSource))).pitch = UnityEngine.Random.Range((float)1, 1.1f);
				this.attackdelaytimer = this.m16speed / this.firespeed;
				this.didattack = false;
			}
			if (selectedweapon == 8 && this.doattack && ((SelectionScript)this.GetComponent(typeof(SelectionScript))).ammoinventory[selectedweapon - 1] > (float)0 && !this.pauseafterthrowing)
			{
				((SelectionScript)this.GetComponent(typeof(SelectionScript))).ammoinventory[selectedweapon - 1] = ((SelectionScript)this.GetComponent(typeof(SelectionScript))).ammoinventory[selectedweapon - 1] - (float)1;
				animator.SetTrigger("BuzzSawFireTrigger");
				((AudioSource)this.buzzsawsound.GetComponent(typeof(AudioSource))).Play();
				this.attackdelaytimer = this.buzzsawspeed / this.firespeed;
				this.didattack = false;
			}
			if (selectedweapon == 9 && this.doattack && ((SelectionScript)this.GetComponent(typeof(SelectionScript))).ammoinventory[selectedweapon - 1] > (float)0 && !this.pauseafterthrowing)
			{
				((SelectionScript)this.GetComponent(typeof(SelectionScript))).ammoinventory[selectedweapon - 1] = ((SelectionScript)this.GetComponent(typeof(SelectionScript))).ammoinventory[selectedweapon - 1] - (float)1;
				animator.SetTrigger("RiveterFireTrigger");
				((AudioSource)this.rivetersound.GetComponent(typeof(AudioSource))).Play();
				this.attackdelaytimer = this.riveterspeed / this.firespeed;
				this.didattack = false;
			}
			this.doattack = false;
		}
		float num = (float)1;
		if (this.plrhealth.drunkness >= (float)4)
		{
			num = (float)3;
		}
		if (!this.didattack)
		{
			if (selectedweapon == 1 && this.attackdelaytimer <= this.axeattackframe && !((SelectionScript)this.GetComponent(typeof(SelectionScript))).havedaikatana)
			{
				GameObject gameObject2;
				if (!((SelectionScript)this.GetComponent(typeof(SelectionScript))).weaponenhance)
				{
					gameObject2 = this.shootbullet((float)0, (float)3, 1, this.axedamage * num, 0, (float)0, (float)0, false, true);
				}
				else
				{
					gameObject2 = this.shootbullet((float)0, (float)5, 1, (float)100, 0, (float)5, (float)3, false, true);
				}
				if (gameObject2)
				{
					((MyMouseLook)this.cam.GetComponent(typeof(MyMouseLook))).buck = this.axebuck;
					if (gameObject2.layer != 14)
					{
						((AudioSource)this.meleemetalhitsound.GetComponent(typeof(AudioSource))).Play();
					}
					else
					{
						((AudioSource)this.meleegorehitsound.GetComponent(typeof(AudioSource))).Play();
					}
				}
				this.didattack = true;
			}
			float num2 = this.swordattackframe;
			if (this.swordcharge >= (float)1)
			{
				num2 = this.attackdelaytimer;
			}
			if (selectedweapon == 1 && this.attackdelaytimer <= num2 && ((SelectionScript)this.GetComponent(typeof(SelectionScript))).havedaikatana)
			{
				GameObject gameObject2 = this.shootbullet((float)0, (float)5, 1, this.sworddamage + this.sworddamage * (this.swordcharge * (float)2) * num, 1, (float)5, (float)3, false, true);
				if (gameObject2)
				{
					((MyMouseLook)this.cam.GetComponent(typeof(MyMouseLook))).buck = (float)10;
					if (this.swordcharge >= (float)1)
					{
						((AudioSource)this.amuletsound2.GetComponent(typeof(AudioSource))).Play();
						GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(this.swordchargehitparticles, this.cam.transform.position + this.cam.transform.forward * (float)2, Quaternion.identity);
					}
					if (gameObject2.layer != 14)
					{
						((AudioSource)this.swordhitsound.GetComponent(typeof(AudioSource))).Play();
					}
					else
					{
						((AudioSource)this.swordgore.GetComponent(typeof(AudioSource))).Play();
					}
				}
				else if (this.swordcharge >= (float)1)
				{
					((AudioSource)this.meleeswish.GetComponent(typeof(AudioSource))).Play();
				}
				this.didattack = true;
				this.swordcharge = (float)0;
			}
			if (selectedweapon == 3 && this.attackdelaytimer <= this.shotgunattackframe)
			{
				if (!((SelectionScript)this.GetComponent(typeof(SelectionScript))).havedualshotguns)
				{
					((ParticleSystem)this.singleshotgunshells.GetComponent(typeof(ParticleSystem))).Play();
					((ParticleSystem)this.singleshotgunshells.GetComponent(typeof(ParticleSystem))).startLifetime = ((ParticleSystem)this.shotgunshells.GetComponent(typeof(ParticleSystem))).startLifetime;
					((ParticleSystem)this.shotgunparticles.GetComponent(typeof(ParticleSystem))).Play();
				}
				else if (this.dualshotguntoggle)
				{
					((ParticleSystem)this.leftshotgunparticles.GetComponent(typeof(ParticleSystem))).Play();
					((ParticleSystem)this.shotgunshellsLeft.GetComponent(typeof(ParticleSystem))).Play();
				}
				else
				{
					((ParticleSystem)this.rightshotgunparticles.GetComponent(typeof(ParticleSystem))).Play();
					((ParticleSystem)this.shotgunshellsRight.GetComponent(typeof(ParticleSystem))).Play();
				}
				((MyMouseLook)this.cam.GetComponent(typeof(MyMouseLook))).buck = this.shotgunbuck;
				if (!((SelectionScript)this.GetComponent(typeof(SelectionScript))).weaponenhance)
				{
					this.shootbullet(this.shotguninaccuracy, (float)1000, (int)this.shotgunpellets, this.shotgundamage, 0, (float)12, (float)4, true, false);
				}
				if (((SelectionScript)this.GetComponent(typeof(SelectionScript))).weaponenhance)
				{
					this.shootbullet(this.shotguninaccuracy * (float)2, (float)1000, (int)(this.shotgunpellets * (float)2), this.shotgundamage * (float)2, 3, (float)10, (float)6, true, false);
				}
				this.alertfloat = 0.1f;
				((WeaponFlashScript)GameObject.Find("WeaponFlash").GetComponent(typeof(WeaponFlashScript))).flash = (float)20;
				this.didattack = true;
			}
			if (selectedweapon == 4 && this.attackdelaytimer <= this.supershotgunframe)
			{
				((ParticleSystem)this.shotgunshells.GetComponent(typeof(ParticleSystem))).Play();
				((ParticleSystem)this.shotgunshells.GetComponent(typeof(ParticleSystem))).startLifetime = ((ParticleSystem)this.shotgunshells.GetComponent(typeof(ParticleSystem))).startLifetime;
				((MyMouseLook)this.cam.GetComponent(typeof(MyMouseLook))).buck = this.supershotgunbuck;
				if (!((SelectionScript)this.GetComponent(typeof(SelectionScript))).weaponenhance)
				{
					this.shootbullet(this.supershotguninaccuracy, (float)1000, (int)this.supershotgunpellets, this.shotgundamage, 3, (float)12, (float)4, true, false);
				}
				if (((SelectionScript)this.GetComponent(typeof(SelectionScript))).weaponenhance)
				{
					this.shootbullet(this.supershotguninaccuracy * (float)2, (float)1000, (int)(this.supershotgunpellets * (float)2), this.shotgundamage * (float)2, 3, (float)10, (float)6, true, false);
				}
				this.alertfloat = 0.1f;
				((ParticleSystem)this.shotgunparticles2.GetComponent(typeof(ParticleSystem))).Play();
				((WeaponFlashScript)GameObject.Find("WeaponFlash").GetComponent(typeof(WeaponFlashScript))).flash = (float)20;
				this.didattack = true;
			}
			if (selectedweapon == 2 && this.attackdelaytimer <= this.pistolattackframe)
			{
				((MyMouseLook)this.cam.GetComponent(typeof(MyMouseLook))).buck = this.pistolbuck;
				if (!((SelectionScript)this.GetComponent(typeof(SelectionScript))).havedualpistols)
				{
					((ParticleSystem)this.pistolparticles.GetComponent(typeof(ParticleSystem))).Play();
					((ParticleSystem)this.pistolshells.GetComponent(typeof(ParticleSystem))).Emit(1);
				}
				else if (this.pistoltoggle)
				{
					((ParticleSystem)this.rightpistolparticles.GetComponent(typeof(ParticleSystem))).Play();
					((ParticleSystem)this.rightpistolshells.GetComponent(typeof(ParticleSystem))).Emit(1);
				}
				else
				{
					((ParticleSystem)this.leftpistolparticles.GetComponent(typeof(ParticleSystem))).Play();
					((ParticleSystem)this.leftpistolshells.GetComponent(typeof(ParticleSystem))).Emit(1);
				}
				float num3 = this.pistolinaccuracy;
				if (((SelectionScript)this.GetComponent(typeof(SelectionScript))).havedualpistols)
				{
					num3 += 0.03f;
				}
				if (!((SelectionScript)this.GetComponent(typeof(SelectionScript))).weaponenhance)
				{
					this.shootbullet(num3, (float)1000, 1, this.pistoldamage, 0, (float)3, (float)1, true, false);
				}
				if (((SelectionScript)this.GetComponent(typeof(SelectionScript))).weaponenhance)
				{
					this.shootbullet(num3, (float)1000, 2, this.pistoldamage, 0, (float)3, (float)1, true, false);
				}
				this.alertfloat = 0.1f;
				((WeaponFlashScript)GameObject.Find("WeaponFlash").GetComponent(typeof(WeaponFlashScript))).flash = (float)20;
				this.didattack = true;
			}
			if (selectedweapon == 6 && this.attackdelaytimer <= this.rifleattackframe)
			{
				((MyMouseLook)this.cam.GetComponent(typeof(MyMouseLook))).buck = this.riflebuck;
				((ParticleSystem)this.rifleparticles.GetComponent(typeof(ParticleSystem))).Play();
				((ParticleSystem)this.rifleshells.GetComponent(typeof(ParticleSystem))).Play();
				if (!((SelectionScript)this.GetComponent(typeof(SelectionScript))).weaponenhance)
				{
					this.shootbullet((float)0, (float)1000, 2, this.rifledamage, 0, (float)16, (float)5, true, false);
				}
				if (((SelectionScript)this.GetComponent(typeof(SelectionScript))).weaponenhance)
				{
					this.shootbullet((float)0, (float)1000, 2, this.rifledamage, 0, (float)16, (float)5, true, false);
				}
				this.alertfloat = 0.1f;
				((WeaponFlashScript)GameObject.Find("WeaponFlash").GetComponent(typeof(WeaponFlashScript))).flash = (float)20;
				this.didattack = true;
			}
			if (selectedweapon == 5 && this.attackdelaytimer <= this.m16attackframe)
			{
				((ParticleSystem)this.m16shells.GetComponent(typeof(ParticleSystem))).Emit(1);
				((MyMouseLook)this.cam.GetComponent(typeof(MyMouseLook))).buck = this.m16buck;
				((MyMouseLook)this.cam.GetComponent(typeof(MyMouseLook))).horbuck = -this.m16buck / (float)2;
				((ParticleSystem)this.m16particles.GetComponent(typeof(ParticleSystem))).Play();
				if (!((SelectionScript)this.GetComponent(typeof(SelectionScript))).weaponenhance)
				{
					this.shootbullet(this.currentm16spread, (float)1000, 1, this.m16damage, 0, (float)3, (float)1, true, false);
				}
				if (((SelectionScript)this.GetComponent(typeof(SelectionScript))).weaponenhance)
				{
					this.shootbullet((float)0, (float)1000, 1, this.m16damage, 0, (float)3, (float)1, true, false);
				}
				this.alertfloat = 0.1f;
				((WeaponFlashScript)GameObject.Find("WeaponFlash").GetComponent(typeof(WeaponFlashScript))).flash = (float)20;
				this.didattack = true;
				this.currentm16spread += Time.deltaTime * (float)3;
				if (this.currentm16spread > this.m16inaccuracy)
				{
					this.currentm16spread = this.m16inaccuracy;
				}
			}
			if (selectedweapon == 8 && this.attackdelaytimer <= this.buzzsawattackframe)
			{
				((MyMouseLook)this.cam.GetComponent(typeof(MyMouseLook))).buck = this.buzzsawbuck;
				this.throwprojectile(this.buzzsawprojectile, (float)15, (float)1, (float)0);
				this.detonatetimer = this.detonatedelay;
				this.alertfloat = 0.1f;
				this.didattack = true;
			}
			if (selectedweapon == 9 && this.attackdelaytimer <= this.riveterattackframe)
			{
				((MyMouseLook)this.cam.GetComponent(typeof(MyMouseLook))).buck = this.riveterbuck;
				this.throwprojectile(this.rivetprojectile, (float)50, (float)1, (float)0);
				((ParticleSystem)this.riveterparticles.GetComponent(typeof(ParticleSystem))).Play();
				this.alertfloat = 0.1f;
				this.didattack = true;
			}
			if (selectedweapon == 7 && this.attackdelaytimer <= this.chainsawattackframe)
			{
				((MyMouseLook)this.cam.GetComponent(typeof(MyMouseLook))).buck = this.chainsawbuck;
				this.throwprojectile(this.arrowprojectile, (float)100, (float)1, (float)0);
				MyControllerScript myControllerScript = (MyControllerScript)GameObject.Find("Player").GetComponent(typeof(MyControllerScript));
				if (myControllerScript.gravityforce < (float)0)
				{
					myControllerScript.gravityforce -= this.transform.forward.normalized.y * (Mathf.Abs(myControllerScript.gravityforce) + 0.12f);
				}
				myControllerScript.realrocketjump -= this.transform.forward.normalized * 0.1f;
				((LadderUseScript)GameObject.Find("Player").GetComponent(typeof(LadderUseScript))).ignoreladdertoggle = true;
				this.didattack = true;
			}
		}
	}

	// Token: 0x06000049 RID: 73 RVA: 0x00006B14 File Offset: 0x00004D14
	public virtual void altfire()
	{
		int selectedweapon = ((SelectionScript)this.GetComponent(typeof(SelectionScript))).selectedweapon;
		if (this.inputmanager.GetKeyInput("zoom", 1))
		{
			if (selectedweapon == 8)
			{
				if (this.detonatetimer <= (float)0)
				{
					this.doingdetonation = true;
				}
			}
			else
			{
				this.zoomedin = true;
				((AudioSource)this.zoomsound.GetComponent(typeof(AudioSource))).Play();
			}
		}
		if (this.doingdetonation)
		{
			this.detonatemortars();
		}
		if (this.inputmanager.GetKeyInput("zoom", 2))
		{
			this.zoomedin = false;
		}
	}

	// Token: 0x0600004A RID: 74 RVA: 0x00006BC8 File Offset: 0x00004DC8
	public virtual void detonatemortars()
	{
		BuzzSawHitScript[] array = (BuzzSawHitScript[])UnityEngine.Object.FindObjectsOfType(typeof(BuzzSawHitScript));
		this.mortardetonatedelay -= Time.deltaTime;
		if (this.mortardetonatedelay <= (float)0)
		{
			if (array.Length > 0)
			{
				array[array.Length - 1].detonate = true;
				this.mortardetonatedelay = 0.15f;
			}
			else
			{
				this.mortardetonatedelay = (float)0;
				this.doingdetonation = false;
			}
		}
	}

	// Token: 0x0600004B RID: 75 RVA: 0x00006C44 File Offset: 0x00004E44
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
		this.superhotsuddenspeedup();
		int num3 = 0;
		int i = 0;
		while (i < numberofbullets)
		{
			i++;
			Vector3 direction = default(Vector3);
			direction.z = (float)1;
			direction.x = UnityEngine.Random.Range(-inaccuracy, inaccuracy);
			direction.y = UnityEngine.Random.Range(-inaccuracy / (float)2, inaccuracy / (float)2);
			if (Physics.Raycast(gameObject.transform.position, this.locke.transform.TransformDirection(direction), out raycastHit, range, this.hitlayers))
			{
				gameObject2 = raycastHit.transform.gameObject;
				DestructibleObjectScript destructibleObjectScript = (DestructibleObjectScript)gameObject2.GetComponent(typeof(DestructibleObjectScript));
				if (!ignoretracer)
				{
					GameObject gameObject3 = GameObject.Find("TracerAnchor2");
					gameObject3.transform.position = this.wanimate.transform.position + this.wanimate.transform.right * this.traceroffset;
					GameObject gameObject4 = UnityEngine.Object.Instantiate<GameObject>(this.tracerobject, this.transform.position, Quaternion.identity);
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
					this.DoGrapple(gameObject2);
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
						GameObject gameObject5 = UnityEngine.Object.Instantiate<GameObject>(this.bloodarcs, raycastHit.point, Quaternion.identity);
						gameObject5.transform.LookAt(GameObject.Find("MainCamera").transform);
						destructibleObjectScript.wasdamaged = true;
						((BasicAIScript)gameObject2.GetComponent(typeof(BasicAIScript))).BasicCheckDamage();
					}
					if (gameObject2.tag == "EnemyTag" && this.persist)
					{
						this.persist.pacifistaward = false;
						if (!ignoretracer)
						{
							this.persist.lowtechaward = false;
						}
					}
					destructibleObjectScript.myhealth -= damage;
					destructibleObjectScript.checkhealth();
					startColor = destructibleObjectScript.damagecolor;
					audioClip = destructibleObjectScript.damagesound;
				}
				else
				{
					startColor = this.defaultparticlecolor;
					if (doricnoise)
					{
						audioClip = this.defaulthitsound;
					}
					if (this.spawndust)
					{
						this.smoke.transform.position = raycastHit.point;
						this.smoke.transform.forward = raycastHit.normal;
						((ParticleSystem)this.smoke.GetComponent(typeof(ParticleSystem))).Emit(UnityEngine.Random.Range(3, 10));
					}
				}
				if (this.spawndecals)
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
					startColor = this.ragdollbloodcolor;
				}
				if ((Rigidbody)gameObject2.GetComponent(typeof(Rigidbody)))
				{
					((Rigidbody)gameObject2.GetComponent(typeof(Rigidbody))).velocity = this.transform.forward * (float)20 + this.transform.up * (float)10;
					((Rigidbody)gameObject2.GetComponent(typeof(Rigidbody))).angularVelocity = this.transform.forward * (float)20;
				}
				GameObject gameObject6 = UnityEngine.Object.Instantiate<GameObject>(this.hitparticles, raycastHit.point, Quaternion.identity);
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
			if (Physics.Raycast(origin, direction2, out raycastHit, (float)12, this.bloodsplatlayers) && !(DestructibleObjectScript)raycastHit.transform.gameObject.GetComponent(typeof(DestructibleObjectScript)) && !(DoorScript)raycastHit.transform.gameObject.GetComponent(typeof(DoorScript)) && raycastHit.transform.gameObject.tag == "Untagged")
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

	// Token: 0x0600004D RID: 77 RVA: 0x0000754C File Offset: 0x0000574C
	public virtual void throwprojectile(GameObject projectile, float speed, float numshots, float inaccuracy)
	{
		GameObject gameObject = GameObject.Find("MainCamera");
		Vector3 direction = default(Vector3);
		int num = 0;
		((CrosshairSizeScript)GameObject.Find("Crosshair").GetComponent(typeof(CrosshairSizeScript))).plus = 0.7f;
		this.superhotsuddenspeedup();
		while ((float)num < numshots)
		{
			num++;
			direction.z = (float)1;
			direction.x = UnityEngine.Random.Range(-inaccuracy, inaccuracy);
			direction.y = UnityEngine.Random.Range(-inaccuracy, inaccuracy);
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(projectile, this.transform.position, this.transform.rotation);
			if ((RivetHitScript)gameObject2.GetComponent(typeof(RivetHitScript)) && ((SelectionScript)this.GetComponent(typeof(SelectionScript))).weaponenhance)
			{
				((RivetHitScript)gameObject2.GetComponent(typeof(RivetHitScript))).enhancedrivet = true;
			}
			Vector3 b = new Vector3((float)0, (float)0, (float)0);
			if ((BuzzSawHitScript)gameObject2.GetComponent(typeof(BuzzSawHitScript)))
			{
				if (((SelectionScript)this.GetComponent(typeof(SelectionScript))).weaponenhance)
				{
					((BuzzSawHitScript)gameObject2.GetComponent(typeof(BuzzSawHitScript))).enhancedbuzzsaw = true;
				}
				b = new Vector3((float)0, 0.2f, (float)0);
			}
			((Rigidbody)gameObject2.GetComponent(typeof(Rigidbody))).velocity = (GameObject.Find("LockonAimer").transform.forward + b) * speed;
			((Rigidbody)gameObject2.GetComponent(typeof(Rigidbody))).velocity = ((Rigidbody)gameObject2.GetComponent(typeof(Rigidbody))).velocity + gameObject2.transform.TransformDirection(direction);
		}
	}

	// Token: 0x0600004E RID: 78 RVA: 0x00007750 File Offset: 0x00005950
	public virtual void block()
	{
		RaycastHit raycastHit = default(RaycastHit);
		GameObject gameObject = GameObject.Find("MainCamera");
		if (Physics.SphereCast(this.transform.position, (float)1, gameObject.transform.forward, out raycastHit, 0.6f, this.blocklayers))
		{
			((AudioSource)this.blocksound.GetComponent(typeof(AudioSource))).Play();
			raycastHit.transform.gameObject.layer = 13;
			((Rigidbody)raycastHit.transform.gameObject.GetComponent(typeof(Rigidbody))).velocity = ((Rigidbody)raycastHit.transform.gameObject.GetComponent(typeof(Rigidbody))).velocity.magnitude * gameObject.transform.forward;
			if ((BasicEnemyProjectileScript)raycastHit.transform.gameObject.GetComponent(typeof(BasicEnemyProjectileScript)))
			{
				((BasicEnemyProjectileScript)raycastHit.transform.gameObject.GetComponent(typeof(BasicEnemyProjectileScript))).whospawnedme = null;
			}
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.hitparticles, raycastHit.point, Quaternion.identity);
			gameObject2.transform.forward = this.transform.forward;
		}
	}

	// Token: 0x0600004F RID: 79 RVA: 0x000078B8 File Offset: 0x00005AB8
	public virtual void DoGrapple(GameObject ob)
	{
		GameObject gameObject = GameObject.Find("MainCamera");
		float num = Vector3.Distance(gameObject.transform.position, ob.transform.position);
		Vector3 normalized = (ob.transform.position - gameObject.transform.position).normalized;
		MyControllerScript myControllerScript = (MyControllerScript)GameObject.Find("Player").GetComponent(typeof(MyControllerScript));
		num /= (float)15;
		if (num < (float)1)
		{
			num = (float)1;
		}
		myControllerScript.GrappleVector = normalized * num;
		myControllerScript.gravityforce = normalized.y / (float)5;
		GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.grappletracerobject, this.transform.position, Quaternion.identity);
		((LineRenderer)gameObject2.GetComponent(typeof(LineRenderer))).SetPosition(0, GameObject.Find("Player").transform.position - new Vector3((float)0, (float)5, (float)0));
		((LineRenderer)gameObject2.GetComponent(typeof(LineRenderer))).SetPosition(1, ob.transform.position);
		GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(this.grappleburst, ob.transform.position, Quaternion.identity);
	}

	// Token: 0x06000050 RID: 80 RVA: 0x00007A00 File Offset: 0x00005C00
	public virtual void superhotsuddenspeedup()
	{
		MyControllerScript myControllerScript = (MyControllerScript)GameObject.Find("Player").GetComponent(typeof(MyControllerScript));
		Time.timeScale = (float)1;
		Time.fixedDeltaTime = myControllerScript.originaltimestep;
		myControllerScript.gravityslowmultiplier = (float)1;
	}

	// Token: 0x06000051 RID: 81 RVA: 0x00007A48 File Offset: 0x00005C48
	public virtual void Main()
	{
	}

	// Token: 0x04000048 RID: 72
	public bool spawndecals;

	// Token: 0x04000049 RID: 73
	public bool spawndust;

	// Token: 0x0400004A RID: 74
	public float firespeed;

	// Token: 0x0400004B RID: 75
	[HideInInspector]
	public float firespeedtimer;

	// Token: 0x0400004C RID: 76
	public GameObject tracerobject;

	// Token: 0x0400004D RID: 77
	public GameObject swordchargehitparticles;

	// Token: 0x0400004E RID: 78
	public GameObject swordchargesound;

	// Token: 0x0400004F RID: 79
	public GameObject grappletracerobject;

	// Token: 0x04000050 RID: 80
	public GameObject grappleburst;

	// Token: 0x04000051 RID: 81
	[HideInInspector]
	public float traceroffset;

	// Token: 0x04000052 RID: 82
	public LayerMask hitlayers;

	// Token: 0x04000053 RID: 83
	public LayerMask blocklayers;

	// Token: 0x04000054 RID: 84
	public LayerMask bloodsplatlayers;

	// Token: 0x04000055 RID: 85
	public GameObject blocksound;

	// Token: 0x04000056 RID: 86
	public float chainsawdamage;

	// Token: 0x04000057 RID: 87
	public GameObject chainsawsound;

	// Token: 0x04000058 RID: 88
	public GameObject chainsawstopsound;

	// Token: 0x04000059 RID: 89
	public float chainsawattackframe;

	// Token: 0x0400005A RID: 90
	public float chainsawspeed;

	// Token: 0x0400005B RID: 91
	public float chainsawbuck;

	// Token: 0x0400005C RID: 92
	public GameObject arrowprojectile;

	// Token: 0x0400005D RID: 93
	public float axedamage;

	// Token: 0x0400005E RID: 94
	public float axespeed;

	// Token: 0x0400005F RID: 95
	public float axeattackframe;

	// Token: 0x04000060 RID: 96
	public float axebuck;

	// Token: 0x04000061 RID: 97
	public float swordspeed;

	// Token: 0x04000062 RID: 98
	public float swordattackframe;

	// Token: 0x04000063 RID: 99
	public GameObject swordsound;

	// Token: 0x04000064 RID: 100
	public float sworddamage;

	// Token: 0x04000065 RID: 101
	public GameObject swordhitsound;

	// Token: 0x04000066 RID: 102
	public GameObject swordgore;

	// Token: 0x04000067 RID: 103
	public GameObject swordprojectile;

	// Token: 0x04000068 RID: 104
	public GameObject meleemetalhitsound;

	// Token: 0x04000069 RID: 105
	public GameObject meleegorehitsound;

	// Token: 0x0400006A RID: 106
	public GameObject meleeswish;

	// Token: 0x0400006B RID: 107
	public float shotgundamage;

	// Token: 0x0400006C RID: 108
	public float shotgunspeed;

	// Token: 0x0400006D RID: 109
	public float shotgunattackframe;

	// Token: 0x0400006E RID: 110
	public float shotguninaccuracy;

	// Token: 0x0400006F RID: 111
	public float shotgunpellets;

	// Token: 0x04000070 RID: 112
	public float shotgunbuck;

	// Token: 0x04000071 RID: 113
	public GameObject shotgunsound;

	// Token: 0x04000072 RID: 114
	public GameObject supershotgunsound;

	// Token: 0x04000073 RID: 115
	public GameObject shotgunparticles;

	// Token: 0x04000074 RID: 116
	public GameObject shotgunparticles2;

	// Token: 0x04000075 RID: 117
	public GameObject rightshotgunparticles;

	// Token: 0x04000076 RID: 118
	public GameObject leftshotgunparticles;

	// Token: 0x04000077 RID: 119
	public GameObject shotgunshells;

	// Token: 0x04000078 RID: 120
	public GameObject singleshotgunshells;

	// Token: 0x04000079 RID: 121
	public GameObject shotgunshellsRight;

	// Token: 0x0400007A RID: 122
	public GameObject shotgunshellsLeft;

	// Token: 0x0400007B RID: 123
	public float supershotgunpellets;

	// Token: 0x0400007C RID: 124
	public float supershotgunbuck;

	// Token: 0x0400007D RID: 125
	public float supershotguninaccuracy;

	// Token: 0x0400007E RID: 126
	public float supershotgunspeed;

	// Token: 0x0400007F RID: 127
	public float supershotgunframe;

	// Token: 0x04000080 RID: 128
	public float pistoldamage;

	// Token: 0x04000081 RID: 129
	public float pistolspeed;

	// Token: 0x04000082 RID: 130
	public float pistolattackframe;

	// Token: 0x04000083 RID: 131
	public float pistolinaccuracy;

	// Token: 0x04000084 RID: 132
	public GameObject pistolparticles;

	// Token: 0x04000085 RID: 133
	public GameObject pistolshells;

	// Token: 0x04000086 RID: 134
	public GameObject leftpistolparticles;

	// Token: 0x04000087 RID: 135
	public GameObject leftpistolshells;

	// Token: 0x04000088 RID: 136
	public GameObject rightpistolparticles;

	// Token: 0x04000089 RID: 137
	public GameObject rightpistolshells;

	// Token: 0x0400008A RID: 138
	public float pistolbuck;

	// Token: 0x0400008B RID: 139
	public GameObject pistolsound;

	// Token: 0x0400008C RID: 140
	public float dualpistolspeed;

	// Token: 0x0400008D RID: 141
	public float rifledamage;

	// Token: 0x0400008E RID: 142
	public float riflespeed;

	// Token: 0x0400008F RID: 143
	public float rifleattackframe;

	// Token: 0x04000090 RID: 144
	public float riflebuck;

	// Token: 0x04000091 RID: 145
	public GameObject rifleparticles;

	// Token: 0x04000092 RID: 146
	public GameObject riflesound;

	// Token: 0x04000093 RID: 147
	public GameObject rifleshells;

	// Token: 0x04000094 RID: 148
	public float m16damage;

	// Token: 0x04000095 RID: 149
	public float m16speed;

	// Token: 0x04000096 RID: 150
	public float m16attackframe;

	// Token: 0x04000097 RID: 151
	public float m16inaccuracy;

	// Token: 0x04000098 RID: 152
	public float m16buck;

	// Token: 0x04000099 RID: 153
	public GameObject m16particles;

	// Token: 0x0400009A RID: 154
	public GameObject m16shells;

	// Token: 0x0400009B RID: 155
	public GameObject m16sound;

	// Token: 0x0400009C RID: 156
	public GameObject buzzsawsound;

	// Token: 0x0400009D RID: 157
	public float buzzsawspeed;

	// Token: 0x0400009E RID: 158
	public float buzzsawattackframe;

	// Token: 0x0400009F RID: 159
	public float buzzsawbuck;

	// Token: 0x040000A0 RID: 160
	public GameObject buzzsawprojectile;

	// Token: 0x040000A1 RID: 161
	public GameObject rivetersound;

	// Token: 0x040000A2 RID: 162
	public float riveterspeed;

	// Token: 0x040000A3 RID: 163
	public float riveterattackframe;

	// Token: 0x040000A4 RID: 164
	public float riveterbuck;

	// Token: 0x040000A5 RID: 165
	public GameObject rivetprojectile;

	// Token: 0x040000A6 RID: 166
	public GameObject riveterparticles;

	// Token: 0x040000A7 RID: 167
	public GameObject amuletmodel;

	// Token: 0x040000A8 RID: 168
	public float amuletspeed;

	// Token: 0x040000A9 RID: 169
	public float amuletattackframe;

	// Token: 0x040000AA RID: 170
	public GameObject amuletprojectile;

	// Token: 0x040000AB RID: 171
	public GameObject amuletparticles;

	// Token: 0x040000AC RID: 172
	public GameObject amuletsound;

	// Token: 0x040000AD RID: 173
	public GameObject amuletsound2;

	// Token: 0x040000AE RID: 174
	public float amuletalttime;

	// Token: 0x040000AF RID: 175
	public GameObject hitparticles;

	// Token: 0x040000B0 RID: 176
	public GameObject bulletholes;

	// Token: 0x040000B1 RID: 177
	public GameObject dynamicbloodsplats;

	// Token: 0x040000B2 RID: 178
	public GameObject bloodarcs;

	// Token: 0x040000B3 RID: 179
	public Color defaultparticlecolor;

	// Token: 0x040000B4 RID: 180
	public AudioClip defaulthitsound;

	// Token: 0x040000B5 RID: 181
	public Color ragdollbloodcolor;

	// Token: 0x040000B6 RID: 182
	public GameObject emptygunsound;

	// Token: 0x040000B7 RID: 183
	public GameObject zoomsound;

	// Token: 0x040000B8 RID: 184
	[HideInInspector]
	public float attackdelaytimer;

	// Token: 0x040000B9 RID: 185
	[HideInInspector]
	public bool doattack;

	// Token: 0x040000BA RID: 186
	[HideInInspector]
	public bool didattack;

	// Token: 0x040000BB RID: 187
	[HideInInspector]
	public bool pauseafterthrowing;

	// Token: 0x040000BC RID: 188
	[HideInInspector]
	public float reloadbuck;

	// Token: 0x040000BD RID: 189
	[HideInInspector]
	public bool pistoltoggle;

	// Token: 0x040000BE RID: 190
	[HideInInspector]
	public bool doblock;

	// Token: 0x040000BF RID: 191
	[HideInInspector]
	public float cigartimer;

	// Token: 0x040000C0 RID: 192
	[HideInInspector]
	public float totemtimer;

	// Token: 0x040000C1 RID: 193
	[HideInInspector]
	public float currentm16spread;

	// Token: 0x040000C2 RID: 194
	[HideInInspector]
	public float staffmeleetimer;

	// Token: 0x040000C3 RID: 195
	[HideInInspector]
	public bool sickletoggle;

	// Token: 0x040000C4 RID: 196
	[HideInInspector]
	public bool dualshotguntoggle;

	// Token: 0x040000C5 RID: 197
	[HideInInspector]
	public float alertfloat;

	// Token: 0x040000C6 RID: 198
	[HideInInspector]
	public PlayerHealthManagement plrhealth;

	// Token: 0x040000C7 RID: 199
	[HideInInspector]
	public float swordcharge;

	// Token: 0x040000C8 RID: 200
	[HideInInspector]
	public bool canchargesword;

	// Token: 0x040000C9 RID: 201
	[HideInInspector]
	public float initialswordchargetimer;

	// Token: 0x040000CA RID: 202
	[HideInInspector]
	public GameObject bulletholesystem;

	// Token: 0x040000CB RID: 203
	[HideInInspector]
	public GameObject bloodsplatsystem;

	// Token: 0x040000CC RID: 204
	[HideInInspector]
	public bool zoomedin;

	// Token: 0x040000CD RID: 205
	[HideInInspector]
	public Camera wcam;

	// Token: 0x040000CE RID: 206
	[HideInInspector]
	public MyInputManager inputmanager;

	// Token: 0x040000CF RID: 207
	public float detonatedelay;

	// Token: 0x040000D0 RID: 208
	[HideInInspector]
	public float detonatetimer;

	// Token: 0x040000D1 RID: 209
	[HideInInspector]
	public float mortardetonatedelay;

	// Token: 0x040000D2 RID: 210
	[HideInInspector]
	public bool doingdetonation;

	// Token: 0x040000D3 RID: 211
	[HideInInspector]
	public LockOnScript lockonscript;

	// Token: 0x040000D4 RID: 212
	[HideInInspector]
	public PersistScript persist;

	// Token: 0x040000D5 RID: 213
	[HideInInspector]
	public GameObject cam;

	// Token: 0x040000D6 RID: 214
	[HideInInspector]
	public GameObject msg;

	// Token: 0x040000D7 RID: 215
	[HideInInspector]
	public GameObject wanimate;

	// Token: 0x040000D8 RID: 216
	[HideInInspector]
	public GameObject locke;

	// Token: 0x040000D9 RID: 217
	[HideInInspector]
	public GameObject plr;

	// Token: 0x040000DA RID: 218
	[HideInInspector]
	public GameObject smoke;
}
