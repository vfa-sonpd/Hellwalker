using System;
using System.Collections;
using Boo.Lang.Runtime;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x0200000E RID: 14
[Serializable]
public class BasicAIScript : MonoBehaviour
{
	// Token: 0x06000056 RID: 86 RVA: 0x00007B44 File Offset: 0x00005D44
	public BasicAIScript()
	{
		this.awakeondamage = true;
		this.infighting = true;
		this.bossname = string.Empty;
		this.numshots = 1;
		this.numshotsequence = 1;
		this.mydamage = (float)10;
		this.damagechance = (float)1;
	}

	// Token: 0x06000057 RID: 87 RVA: 0x00007B90 File Offset: 0x00005D90
	public void Start()
	{
		this.ThisStoresBossStartingHealth = ((DestructibleObjectScript)this.GetComponent(typeof(DestructibleObjectScript))).myhealth;

		this.origcoors = this.transform.position;
		this.idlesoundtimer = UnityEngine.Random.Range(this.idlesoundactivatetimerange.x, this.idlesoundactivatetimerange.y);
	//	this.AddVarsToMoreArray();
		this.sequencetimer = (float)0;
		this.AITimer = UnityEngine.Random.Range(0.1f, this.AIUpdateSpeed);
		this.attacktimer = UnityEngine.Random.Range(this.attacktime / (float)4, this.attacktime / (float)2);
		this.MyTarget = GameObject.Find("Player");
		this.originalattacktime = this.attacktime;
		this.nav = (NavMeshAgent)this.GetComponent(typeof(NavMeshAgent));
		if (this.mymesh)
		{
			this.rend = (Renderer)this.mymesh.GetComponent(typeof(Renderer));
		}
		if (this.nav)
		{
			this.initialnavoffset = this.nav.baseOffset;
			this.currentnavoffset = this.nav.baseOffset;
			this.nav.enabled = false;
			this.nav.enabled = true;
		}
		//this.aud = (AudioSource)this.GetComponent(typeof(AudioSource));
		this.disbeplayer = GameObject.Find("Player");
        this.disbecamera = Camera.main.gameObject;
        this.disbeattack = GameObject.FindObjectOfType<AttackScript>();
	}

	// Token: 0x06000058 RID: 88 RVA: 0x00007D88 File Offset: 0x00005F88
	public virtual void Update()
	{
		if (this.idlefootsteps)
		{
			if (this.previousposition != this.transform.position && !this.idleplaying)
			{
				((AudioSource)this.idlesound.GetComponent(typeof(AudioSource))).Play();
				this.idleplaying = true;
			}
			if (this.previousposition == this.transform.position)
			{
				((AudioSource)this.idlesound.GetComponent(typeof(AudioSource))).Stop();
				this.idleplaying = false;
			}
			this.previousposition = this.transform.position;
		}
		this.dosavestuff();
		if (this.idlesound && !this.loopingidlesound && !this.idlefootsteps && this.AMAWAKE)
		{
			this.idlesoundtimer -= Time.deltaTime;
			if (this.idlesoundtimer <= (float)0)
			{
				((AudioSource)this.idlesound.GetComponent(typeof(AudioSource))).pitch = UnityEngine.Random.Range(0.8f, 1f);
				((AudioSource)this.idlesound.GetComponent(typeof(AudioSource))).Play();
				this.idlesoundtimer = UnityEngine.Random.Range(this.idlesoundactivatetimerange.x, this.idlesoundactivatetimerange.y);
			}
		}
		if (this.mymesh)
		{
			if (this.invisibru)
			{
				this.rend.enabled = false;
				this.mymovementtype = 1;
			}
			else
			{
				this.rend.enabled = true;
				this.mymovementtype = 0;
			}
		}
		if (this.nav)
		{
			this.currentnavoffset += this.jumpvelocity * Time.deltaTime;
			this.jumpvelocity -= Time.deltaTime * (float)20;
			if (this.currentnavoffset < this.initialnavoffset)
			{
				this.currentnavoffset = this.initialnavoffset;
				this.jumpvelocity = (float)0;
			}
			if (this.nav.baseOffset != this.currentnavoffset)
			{
				this.nav.baseOffset = this.currentnavoffset;
			}
		}
		if (!this.infighting)
		{
			this.MyTarget = this.disbeplayer;
		}
		this.rigiddamagedelay -= Time.deltaTime;
		if (this.rigiddamagedelay < (float)0)
		{
			this.rigiddamagedelay = (float)0;
		}
		if (this.MyTarget == null)
		{
			if (!this.berserkenemy)
			{
				this.MyTarget = this.disbeplayer;
			}
			else
			{
				this.MyTarget = this.FindClosestEnemy(this.transform.gameObject);
			}
		}
		if (this.mytype == 3)
		{
			this.MyTarget = this.disbeplayer;
		}
		if (this.MyTarget == this.disbeplayer)
		{
            this.disbecamera = Camera.main.gameObject;
            this.shottarget = this.disbecamera.transform.position;
		}
		else
		{
			this.shottarget = this.MyTarget.transform.position;
		}
		if (this.changemovementspeedbasedondifficulty)
		{
			this.myspeed = this.otherspeeds[this.statscript.difficulty];
		}
		if (this.nav != null)
		{
			if (this.stopmovement <= (float)0)
			{
				this.nav.speed = this.myspeed;
			}
			else
			{
				this.nav.speed = (float)0;
			}
		}
		if (this.stopmovement > (float)0)
		{
			this.transform.LookAt(new Vector3(this.shottarget.x, this.transform.position.y, this.shottarget.z));
		}
		this.stopmovement -= Time.deltaTime;
		DestructibleObjectScript destructibleObjectScript = (DestructibleObjectScript)this.GetComponent(typeof(DestructibleObjectScript));
		bool flag = false;
		if (destructibleObjectScript.wasdamaged && this.awakeondamage)
		{
			flag = true;
		}
		if ((this.CheckLoS() && this.CheckFoV()) || flag)
		{
			this.awakedelay -= Time.deltaTime;
			if (this.awakedelay < (float)0)
			{
				this.awakedelay = (float)0;
			}
			if (this.awakedelay <= (float)0)
			{
				if (!this.AMAWAKE)
				{
					this.awakestuff();
					if (this.isboss && (DestructibleObjectScript)this.GetComponent(typeof(DestructibleObjectScript)))
					{
						((BossControllerScript)GameObject.Find("Player").GetComponent(typeof(BossControllerScript))).BossStartingHealth = ((BossControllerScript)GameObject.Find("Player").GetComponent(typeof(BossControllerScript))).BossStartingHealth + this.ThisStoresBossStartingHealth;
						((BossControllerScript)GameObject.Find("Player").GetComponent(typeof(BossControllerScript))).BossName = this.bossname;
					}
				}
				this.AMAWAKE = true;
			}
		}
		if (this.AMAWAKE)
		{
			this.AITimer += Time.deltaTime;
			if (this.AITimer >= this.AIUpdateSpeed)
			{
				if (this.mymovementtype == 0)
				{
					this.BasicSetGoal();
				}
				if (this.mymovementtype == 1)
				{
					this.SetNearGoal();
				}
				this.AITimer = (float)0;
			}
			if (this.CheckLoS() && this.damagetimer <= (float)0)
			{
				this.attacktimer += Time.deltaTime;
			}
			if (this.attacktimer > this.attacktime)
			{
				this.attacktimer = this.attacktime;
			}
			if (this.prefiretimer > (float)0)
			{
				this.prefiretimer -= Time.deltaTime;
			}
			if (this.prefiretimer < (float)0 && ((AudioSource)this.GetComponent(typeof(AudioSource))).clip != null)
			{
				this.prefiretimer = (float)0;
				((AudioSource)this.GetComponent(typeof(AudioSource))).pitch = (float)1;
				((AudioSource)this.GetComponent(typeof(AudioSource))).clip = this.prefiresound;
				((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
			}
			if (this.mymovementtype == 1)
			{
				this.CheckDistanceFromGoal();
			}
			if (this.mymovementtype == 0)
			{
				this.TrackCheckDistanceFromGoal();
			}
			this.BasicCheckDamage();
			if (this.mytype == 0 || this.mytype == 1)
			{
				this.BasicAttack();
			}
			if (this.mytype == 2 || this.mytype == 3)
			{
				this.RigidAttack();
			}
		}
	}

	// Token: 0x06000059 RID: 89 RVA: 0x0000845C File Offset: 0x0000665C
	public virtual void FixedUpdate()
	{
		if (this.AMAWAKE)
		{
		}
	}

	// Token: 0x0600005A RID: 90 RVA: 0x0000846C File Offset: 0x0000666C
	public virtual AudioClip RandomAwakeSound()
	{
		return this.otherawakesounds[UnityEngine.Random.Range(0, this.otherawakesounds.Length)];
	}

	// Token: 0x0600005B RID: 91 RVA: 0x00008488 File Offset: 0x00006688
	public virtual void awakestuff()
	{
		Animator animator = (Animator)this.GetComponent(typeof(Animator));
		if (animator)
		{
			animator.SetTrigger("AlertedTrigger");
		}
		//this.aud.clip = this.alertsound;
		if (this.dootherawakesounds)
		{
			//this.aud.clip = this.RandomAwakeSound();
		}
		//this.aud.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
		//this.aud.Play();
		if (this.mymovementtype == 0 || this.mymovementtype == 1)
		{
			this.BasicSetGoal();
		}
		this.MyTarget = this.disbeplayer;
		if (this.berserkenemy)
		{
			this.MyTarget = this.FindClosestEnemy(this.transform.gameObject);
		}
		if (this.TargetThis != null)
		{
			this.MyTarget = this.TargetThis;
		}
		if (this.loopingidlesound && this.idlesound != null)
		{
			((AudioSource)this.idlesound.GetComponent(typeof(AudioSource))).Play();
		}
		if (this.isboss)
		{
			((BossControllerScript)this.disbeplayer.GetComponent(typeof(BossControllerScript))).addtolist(this.transform.gameObject);
		}
	}

	// Token: 0x0600005C RID: 92 RVA: 0x000085EC File Offset: 0x000067EC
	public virtual void RigidAttack()
	{
		Rigidbody rigidbody = (Rigidbody)this.GetComponent(typeof(Rigidbody));
		Vector3 a = new Vector3(this.shottarget.x, this.shottarget.y, this.shottarget.z);
		Vector3 normalized = (a - this.transform.position).normalized;
		if ((this.mytype == 2 || this.mytype == 3) && this.attacktimer >= this.attacktime)
		{
			rigidbody.velocity = normalized * this.myspeed;
			this.attacktimer = (float)0;
		}
	}

	// Token: 0x0600005D RID: 93 RVA: 0x00008694 File Offset: 0x00006894
	public virtual void BasicSetGoal()
	{
        GameObject myTarget = this.MyTarget;
		NavMeshAgent navMeshAgent = (NavMeshAgent)this.GetComponent(typeof(NavMeshAgent));
		this.mygoal = myTarget.transform.position;
		navMeshAgent.destination = this.mygoal;
	}

	// Token: 0x0600005E RID: 94 RVA: 0x000086DC File Offset: 0x000068DC
	public virtual void TrackCheckDistanceFromGoal()
	{
        print("alachol 5");
        GameObject myTarget = this.MyTarget;
		NavMeshAgent navMeshAgent = (NavMeshAgent)this.GetComponent(typeof(NavMeshAgent));
		if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance + 0.5f && Vector3.Distance(this.transform.position, myTarget.transform.position) > navMeshAgent.stoppingDistance + 0.5f)
		{
			this.BasicSetGoal();
		}
	}

	// Token: 0x0600005F RID: 95 RVA: 0x00008750 File Offset: 0x00006950
	public virtual void CheckDistanceFromGoal()
	{
		NavMeshAgent navMeshAgent = (NavMeshAgent)this.GetComponent(typeof(NavMeshAgent));
		if (navMeshAgent.remainingDistance < 1.9f)
		{
			this.SetNearGoal();
			this.AITimer = (float)0;
		}
	}

	// Token: 0x06000060 RID: 96 RVA: 0x00008794 File Offset: 0x00006994
	public virtual void SetNearGoal()
	{
        GameObject myTarget = this.MyTarget;
		int num = UnityEngine.Random.Range(3, 60);
		Vector3 vector = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
		RaycastHit raycastHit = default(RaycastHit);
		NavMeshAgent navMeshAgent = (NavMeshAgent)this.GetComponent(typeof(NavMeshAgent));
		if (Physics.Raycast(myTarget.transform.position, vector, out raycastHit, (float)num, this.BlockingLayers))
		{
			this.mygoal = raycastHit.point;
			navMeshAgent.destination = this.mygoal;
		}
		else
		{
			this.mygoal = vector * (float)num;
			navMeshAgent.destination = this.mygoal;
		}
	}

	// Token: 0x06000061 RID: 97 RVA: 0x00008864 File Offset: 0x00006A64
	public virtual void BasicCheckDamage()
	{
		DestructibleObjectScript destructibleObjectScript = (DestructibleObjectScript)this.GetComponent(typeof(DestructibleObjectScript));
		Animator animator = (Animator)this.GetComponent(typeof(Animator));
		if (destructibleObjectScript.wasdamaged)
		{
			this.invisibru = false;
			if (UnityEngine.Random.Range((float)0, this.flinchignorechance) < 1f && !((DestructibleObjectScript)this.GetComponent(typeof(DestructibleObjectScript))).onfire)
			{
				animator.SetTrigger("DamageTrigger");
				this.attackflag = false;
				this.ignoreattackflag = true;
				this.stopmovement = this.damagehalttime;
				this.damagetimer = (float)0;
				this.attacktimer = (float)0;
			}
		}
	}

	// Token: 0x06000062 RID: 98 RVA: 0x0000891C File Offset: 0x00006B1C
	public virtual void BasicAttack()
	{
		GameObject myTarget = this.MyTarget;
		float num = Vector3.Distance(this.transform.position, myTarget.transform.position);
		RaycastHit raycastHit = default(RaycastHit);
		Vector3 vector = this.transform.position + new Vector3((float)0, this.visionyoffset, (float)0);
		bool flag = Physics.Raycast(vector, (myTarget.transform.position - vector).normalized, out raycastHit, Vector3.Distance(vector, myTarget.transform.position), this.BlockingLayers);
		Animator animator = (Animator)this.GetComponent(typeof(Animator));
		AudioSource audioSource = (AudioSource)this.GetComponent(typeof(AudioSource));
		if (this.damagetimer == (float)0)
		{
			this.attackflag = false;
		}
		if (this.damagetimer > (float)0)
		{
			this.damagetimer -= Time.deltaTime;
			this.attackflag = false;
		}
		if (this.damagetimer < (float)0)
		{
			this.damagetimer = (float)0;
			this.attackflag = true;
			this.currentshotsequence++;
		}
		bool flag2 = false;
		if (flag)
		{
			if (raycastHit.transform.gameObject == this.MyTarget)
			{
				flag2 = true;
			}
		}
		else
		{
			flag2 = true;
		}
		if (flag2)
		{
			if (num <= this.attackrange && this.attacktimer >= this.attacktime)
			{
				if (this.stopmovement < (float)0 && this.currentshotsequence != 0)
				{
					this.currentshotsequence = this.numshotsequence;
				}
				if (this.currentshotsequence >= this.numshotsequence)
				{
					this.attacktimer = (float)0;
					this.attackflag = false;
					this.damagetimer = (float)0;
					this.currentshotsequence = 0;
					if (this.morethanoneattack)
					{
						this.attacktypeindex = UnityEngine.Random.Range(0, this.MORE2_mytype.Count);
						this.AssignAlternateAttacks();
					}
				}
				else
				{
					this.damagetimer = (float)-1;
					if (this.currentshotsequence == 0)
					{
						animator.SetTrigger("AttackTrigger");
						this.damagetimer = this.attackdamagedelay;
						this.stopmovement = this.attackhalttime;
						this.prefiretimer = this.prefiretime;
						if (this.changefirespeedbasedondifficulty)
						{
							this.attacktime = UnityEngine.Random.Range(this.originalattacktime - this.firespeeddecreases[this.statscript.difficulty] - this.attacktimerange, this.originalattacktime - this.firespeeddecreases[this.statscript.difficulty] + this.attacktimerange);
						}
						else
						{
							this.attacktime = UnityEngine.Random.Range(this.originalattacktime - this.attacktimerange, this.originalattacktime + this.attacktimerange);
						}
					}
					this.attacktimer = this.attacktime - this.shotsequencetimer;
				}
				this.ignoreattackflag = false;
				this.transform.LookAt(myTarget.transform);
			}
			if (this.attackflag && !this.ignoreattackflag)
			{
				if (this.mymuzzleflash != null)
				{
					this.mymuzzleflash.active = true;
				}
				if (this.mytype == 0)
				{
					if (UnityEngine.Random.Range((float)0, this.damagechance) < 1f)
					{
						this.transform.LookAt(new Vector3(this.shottarget.x, this.transform.position.y, this.shottarget.z));
						RaycastHit raycastHit2 = default(RaycastHit);
						bool flag3 = Physics.Raycast(this.transform.position + new Vector3((float)0, this.visionyoffset, (float)0), (myTarget.transform.position - this.transform.position).normalized, out raycastHit2, this.attackrange, this.BlockingLayers);
						if (flag3)
						{
							if (raycastHit2.transform.gameObject == this.disbeplayer)
							{
								((PlayerHealthManagement)myTarget.GetComponent(typeof(PlayerHealthManagement))).takedamage(this.mydamage * this.statscript.difficultymultiplier);
							}
							Vector3 position = default(Vector3);
							if (raycastHit2.transform.gameObject == this.disbeplayer)
							{
								position = this.disbecamera.transform.position;
							}
							else
							{
								position = raycastHit2.point;
							}
							GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.hitparticles, position, Quaternion.identity);
							gameObject.transform.LookAt(this.transform);
							DestructibleObjectScript destructibleObjectScript = (DestructibleObjectScript)raycastHit2.transform.gameObject.GetComponent(typeof(DestructibleObjectScript));
							if (destructibleObjectScript)
							{
								destructibleObjectScript.myhealth -= this.mygooddamage;
								destructibleObjectScript.dampen = false;
								destructibleObjectScript.doragdoll = true;
								((ParticleSystem)gameObject.GetComponent(typeof(ParticleSystem))).startColor = destructibleObjectScript.damagecolor;
								AudioClip damagesound = ((DestructibleObjectScript)raycastHit2.transform.gameObject.GetComponent(typeof(DestructibleObjectScript))).damagesound;
								if (damagesound != null)
								{
									((AudioSource)gameObject.GetComponent(typeof(AudioSource))).clip = damagesound;
									((AudioSource)gameObject.GetComponent(typeof(AudioSource))).pitch = UnityEngine.Random.Range(0.85f, 1.15f);
									((AudioSource)gameObject.GetComponent(typeof(AudioSource))).Play();
								}
							}
							if (raycastHit2.transform.gameObject.tag == "EnemyTag")
							{
                                print("alachol 1");
								BasicAIScript basicAIScript = (BasicAIScript)raycastHit2.transform.gameObject.GetComponent(typeof(BasicAIScript));
								basicAIScript.MyTarget = this.transform.gameObject;
							}
							if (raycastHit2.transform.gameObject.layer == 10)
							{
								((ParticleSystem)gameObject.GetComponent(typeof(ParticleSystem))).startColor = new Color(0.7f, (float)0, (float)0);
							}
						}
					}
					if (this.attacksound != null)
					{
						audioSource.pitch = (float)1;
						audioSource.clip = this.attacksound;
						audioSource.Play();
					}
				}
				if (this.mytype == 1)
				{
					int i = 0;
					while (i < this.numshots)
					{
						i++;
						this.transform.LookAt(new Vector3(this.shottarget.x, this.transform.position.y, this.shottarget.z));
						Vector3 b = (this.shottarget - this.transform.position).normalized * this.projectilespawnforwardoffset;
						GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.myprojectile, this.transform.position + b + new Vector3((float)0, this.projectileoffset, (float)0), Quaternion.identity);
						gameObject2.transform.LookAt(new Vector3(this.shottarget.x, this.shottarget.y, this.shottarget.z));
						float x = gameObject2.transform.eulerAngles.x + UnityEngine.Random.Range(-this.inaccuracy / (float)2, this.inaccuracy / (float)2);
						Vector3 eulerAngles = gameObject2.transform.eulerAngles;
						float num2 = eulerAngles.x = x;
						Vector3 vector2 = gameObject2.transform.eulerAngles = eulerAngles;
						float y = gameObject2.transform.eulerAngles.y + UnityEngine.Random.Range(-this.inaccuracy * (float)2, this.inaccuracy * (float)2);
						Vector3 eulerAngles2 = gameObject2.transform.eulerAngles;
						float num3 = eulerAngles2.y = y;
						Vector3 vector3 = gameObject2.transform.eulerAngles = eulerAngles2;
						float z = gameObject2.transform.eulerAngles.z + UnityEngine.Random.Range(-this.inaccuracy, this.inaccuracy);
						Vector3 eulerAngles3 = gameObject2.transform.eulerAngles;
						float num4 = eulerAngles3.z = z;
						Vector3 vector4 = gameObject2.transform.eulerAngles = eulerAngles3;
						float num5 = this.projectilespeed;
						if (this.changespeedbasedondifficulty)
						{
							num5 *= this.projectilespeedmultipliers[this.statscript.difficulty];
						}
						((Rigidbody)gameObject2.GetComponent(typeof(Rigidbody))).velocity = gameObject2.transform.forward * num5;
						gameObject2.transform.gameObject.layer = 15;
						if ((BasicEnemyProjectileScript)gameObject2.GetComponent(typeof(BasicEnemyProjectileScript)))
						{
							((BasicEnemyProjectileScript)gameObject2.GetComponent(typeof(BasicEnemyProjectileScript))).whospawnedme = this.transform.gameObject;
							((BasicEnemyProjectileScript)gameObject2.GetComponent(typeof(BasicEnemyProjectileScript))).mydamage = this.mydamage * this.statscript.difficultymultiplier;
							((BasicEnemyProjectileScript)gameObject2.GetComponent(typeof(BasicEnemyProjectileScript))).mygooddamage = this.mygooddamage;
							((BasicEnemyProjectileScript)gameObject2.GetComponent(typeof(BasicEnemyProjectileScript))).mytarget = new Vector3(this.shottarget.x, this.shottarget.y, this.shottarget.z);
							((BasicEnemyProjectileScript)gameObject2.GetComponent(typeof(BasicEnemyProjectileScript))).origspeed = num5;
						}
					}
					audioSource.pitch = (float)1;
					audioSource.clip = this.attacksound;
					audioSource.Play();
				}
				if (this.currentshotsequence > this.numshotsequence)
				{
					this.ignoreattackflag = true;
					this.attackflag = false;
				}
			}
		}
	}

	// Token: 0x06000063 RID: 99 RVA: 0x00009340 File Offset: 0x00007540
	public virtual bool CheckLoS()
	{
        RaycastHit raycastHit = default(RaycastHit);
		GameObject myTarget = this.MyTarget;
		bool result = false;
		Vector3 vector = this.transform.position + new Vector3((float)0, this.visionyoffset, (float)0);
		bool flag = Physics.Raycast(vector, (myTarget.transform.position - vector).normalized, out raycastHit, Vector3.Distance(vector, myTarget.transform.position), this.BlockingLayers);
		bool flag2 = false;
		if (flag)
		{
			if (raycastHit.transform.gameObject == this.MyTarget)
			{
				flag2 = true;
			}
		}
		else
		{
			flag2 = true;
		}
		if (flag2)
		{
			result = true;
		}
		return result;
	}

	// Token: 0x06000064 RID: 100 RVA: 0x000093FC File Offset: 0x000075FC
	public virtual bool CheckFoV()
	{
        GameObject myTarget = this.MyTarget;
		bool result = false;
		Vector3 from = myTarget.transform.position - this.transform.position;
		float num = Vector3.Angle(from, this.transform.forward);
		if (num < (float)100)
		{
			result = true;
		}

        if(!this.disbeattack)
        {
            disbeattack = GameObject.FindObjectOfType<AttackScript>();
        }

		if (this.disbeattack.alertfloat > (float)0)
		{
			result = true;
		}
		return result;
	}

	// Token: 0x06000065 RID: 101 RVA: 0x00009460 File Offset: 0x00007660
	public virtual void OnCollisionEnter(Collision hit)
	{
		Rigidbody rigidbody = (Rigidbody)this.GetComponent(typeof(Rigidbody));
		DestructibleObjectScript destructibleObjectScript = (DestructibleObjectScript)hit.transform.gameObject.GetComponent(typeof(DestructibleObjectScript));
		if ((this.mytype == 2 || this.mytype == 3) && rigidbody.velocity.magnitude > this.rigiddamagethreshold && this.rigiddamagedelay <= (float)0)
		{
			this.rigiddamagedelay = 0.65f;
			if (destructibleObjectScript && this.mytype != 3)
			{
				destructibleObjectScript.myhealth -= this.mygooddamage;
				destructibleObjectScript.dampen = false;
				destructibleObjectScript.doragdoll = true;
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.hitparticles, this.transform.position, Quaternion.identity);
				((ParticleSystem)gameObject.GetComponent(typeof(ParticleSystem))).startColor = destructibleObjectScript.damagecolor;
				AudioClip damagesound = ((DestructibleObjectScript)hit.transform.gameObject.GetComponent(typeof(DestructibleObjectScript))).damagesound;
				if (damagesound != null)
				{
					((AudioSource)gameObject.GetComponent(typeof(AudioSource))).clip = damagesound;
					((AudioSource)gameObject.GetComponent(typeof(AudioSource))).pitch = UnityEngine.Random.Range(0.85f, 1.15f);
					((AudioSource)gameObject.GetComponent(typeof(AudioSource))).Play();
				}
				if (hit.transform.gameObject.tag == "EnemyTag" && this.mytype != 3)
				{
					BasicAIScript basicAIScript = (BasicAIScript)hit.transform.gameObject.GetComponent(typeof(BasicAIScript));
					if (basicAIScript)
					{
                        print("alachol 2");
                        basicAIScript.MyTarget = this.transform.gameObject;
					}
				}
			}
			if (hit.transform.gameObject.layer == 10)
			{
				((PlayerHealthManagement)hit.transform.gameObject.GetComponent(typeof(PlayerHealthManagement))).takedamage(this.mydamage);
			}
		}
	}

	// Token: 0x06000066 RID: 102 RVA: 0x0000969C File Offset: 0x0000789C
	public virtual void AddVarsToMoreArray()
	{
        this.MORE2_mytype.AddRange(this.MORE_mytype);
        this.MORE2_numshots.AddRange(this.MORE_numshots);
		this.MORE2_numshotsequence.AddRange(this.MORE_numshotsequence);
        this.MORE2_shotsequencetimer.AddRange(this.MORE_shotsequencetimer);
        this.MORE2_inaccuracy.AddRange(this.MORE_inaccuracy);
        this.MORE2_myprojectile.AddRange(this.MORE_myprojectile);
        this.MORE2_projectilespeed.AddRange(this.MORE_projectilespeed);
        this.MORE2_mydamage.AddRange(this.MORE_mydamage);
        this.MORE2_mygooddamage.AddRange(this.MORE_mygooddamage);
        this.MORE2_attackrange.AddRange(this.MORE_attackrange);
        this.MORE2_attacksound.AddRange(this.MORE_attacksound);
        this.MORE2_prefiresound.AddRange(this.MORE_prefiresound);
        this.MORE2_prefiretime.AddRange(this.MORE_prefiretime);
        this.MORE2_mymuzzleflash.AddRange(this.MORE_mymuzzleflash);
        this.MORE2_mytype.Add(this.mytype);
		this.MORE2_numshots.Add(this.numshots);
		this.MORE2_numshotsequence.Add(this.numshotsequence);
		this.MORE2_shotsequencetimer.Add(this.shotsequencetimer);
		this.MORE2_inaccuracy.Add(this.inaccuracy);
		this.MORE2_myprojectile.Add(this.myprojectile);
		this.MORE2_projectilespeed.Add(this.projectilespeed);
		this.MORE2_mydamage.Add(this.mydamage);
		this.MORE2_mygooddamage.Add(this.mygooddamage);
		this.MORE2_attackrange.Add(this.attackrange);
		this.MORE2_attacksound.Add(this.attacksound);
		this.MORE2_prefiresound.Add(this.prefiresound);
		this.MORE2_prefiretime.Add(this.prefiretime);
		this.MORE2_mymuzzleflash.Add(this.mymuzzleflash);
	}

	// Token: 0x06000067 RID: 103 RVA: 0x000098B8 File Offset: 0x00007AB8
	public virtual void AssignAlternateAttacks()
	{
		this.mytype = RuntimeServices.UnboxInt32(this.MORE2_mytype[this.attacktypeindex]);
		this.numshots = RuntimeServices.UnboxInt32(this.MORE2_numshots[this.attacktypeindex]);
		this.numshotsequence = RuntimeServices.UnboxInt32(this.MORE2_numshotsequence[this.attacktypeindex]);
		this.shotsequencetimer = RuntimeServices.UnboxSingle(this.MORE2_shotsequencetimer[this.attacktypeindex]);
		this.inaccuracy = RuntimeServices.UnboxSingle(this.MORE2_inaccuracy[this.attacktypeindex]);
		object obj2;
		object obj = obj2 = this.MORE2_myprojectile[this.attacktypeindex];
		if (!(obj is GameObject))
		{
			obj2 = RuntimeServices.Coerce(obj, typeof(GameObject));
		}
		this.myprojectile = (GameObject)obj2;
		this.projectilespeed = RuntimeServices.UnboxSingle(this.MORE2_projectilespeed[this.attacktypeindex]);
		this.mydamage = RuntimeServices.UnboxSingle(this.MORE2_mydamage[this.attacktypeindex]);
		this.mygooddamage = RuntimeServices.UnboxSingle(this.MORE2_mygooddamage[this.attacktypeindex]);
		this.attackrange = RuntimeServices.UnboxSingle(this.MORE2_attackrange[this.attacktypeindex]);
		object obj4;
		object obj3 = obj4 = this.MORE2_attacksound[this.attacktypeindex];
		if (!(obj3 is AudioClip))
		{
			obj4 = RuntimeServices.Coerce(obj3, typeof(AudioClip));
		}
		this.attacksound = (AudioClip)obj4;
		object obj6;
		object obj5 = obj6 = this.MORE2_prefiresound[this.attacktypeindex];
		if (!(obj5 is AudioClip))
		{
			obj6 = RuntimeServices.Coerce(obj5, typeof(AudioClip));
		}
		this.prefiresound = (AudioClip)obj6;
		this.prefiretime = RuntimeServices.UnboxSingle(this.MORE2_prefiretime[this.attacktypeindex]);
		object obj8;
		object obj7 = obj8 = this.MORE2_mymuzzleflash[this.attacktypeindex];
		if (!(obj7 is GameObject))
		{
			obj8 = RuntimeServices.Coerce(obj7, typeof(GameObject));
		}
		this.mymuzzleflash = (GameObject)obj8;
	}

	// Token: 0x06000068 RID: 104 RVA: 0x00009AB8 File Offset: 0x00007CB8
	public virtual GameObject FindClosestEnemy(GameObject enem)
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("EnemyTag");
		GameObject result = null;
		float num = float.PositiveInfinity;
		Vector3 position = enem.transform.position;
		int i = 0;
		GameObject[] array2 = array;
		int length = array2.Length;
		while (i < length)
		{
			if (array2[i] != enem)
			{
				float sqrMagnitude = (array2[i].transform.position - position).sqrMagnitude;
				if (sqrMagnitude < num)
				{
					result = array2[i];
					num = sqrMagnitude;
				}
			}
			i++;
		}
		return result;
	}

	// Token: 0x06000069 RID: 105 RVA: 0x00009B4C File Offset: 0x00007D4C
	public virtual void dosavestuff()
	{

	}

	// Token: 0x0600006A RID: 106 RVA: 0x00009ED8 File Offset: 0x000080D8
	public virtual void Main()
	{
	}

	// Token: 0x040000E1 RID: 225
	public bool awakeondamage;

	// Token: 0x040000E2 RID: 226
	public bool invisibru;

	// Token: 0x040000E3 RID: 227
	public GameObject mymesh;

	// Token: 0x040000E4 RID: 228
	public float awakedelay;

	// Token: 0x040000E5 RID: 229
	public bool infighting;

	// Token: 0x040000E6 RID: 230
	public bool isboss;

	// Token: 0x040000E7 RID: 231
	public string bossname;

	// Token: 0x040000E8 RID: 232
	public int mytype;

	// Token: 0x040000E9 RID: 233
	public int mymovementtype;

	// Token: 0x040000EA RID: 234
	public float rigiddamagethreshold;

	// Token: 0x040000EB RID: 235
	public int numshots;

	// Token: 0x040000EC RID: 236
	public int numshotsequence;

	// Token: 0x040000ED RID: 237
	public float shotsequencetimer;

	// Token: 0x040000EE RID: 238
	public float inaccuracy;

	// Token: 0x040000EF RID: 239
	public GameObject myprojectile;

	// Token: 0x040000F0 RID: 240
	public float projectilespeed;

	// Token: 0x040000F1 RID: 241
	public bool changespeedbasedondifficulty;

	// Token: 0x040000F2 RID: 242
	public float[] projectilespeedmultipliers;

	// Token: 0x040000F3 RID: 243
	public bool changefirespeedbasedondifficulty;

	// Token: 0x040000F4 RID: 244
	public float[] firespeeddecreases;

	// Token: 0x040000F5 RID: 245
	public bool changemovementspeedbasedondifficulty;

	// Token: 0x040000F6 RID: 246
	public float[] otherspeeds;

	// Token: 0x040000F7 RID: 247
	public float projectileoffset;

	// Token: 0x040000F8 RID: 248
	public float projectilespawnforwardoffset;

	// Token: 0x040000F9 RID: 249
	public float visionyoffset;

	// Token: 0x040000FA RID: 250
	public float mydamage;

	// Token: 0x040000FB RID: 251
	public float mygooddamage;

	// Token: 0x040000FC RID: 252
	public GameObject hitparticles;

	// Token: 0x040000FD RID: 253
	public float attackdamagedelay;

	// Token: 0x040000FE RID: 254
	public GameObject mymuzzleflash;

	// Token: 0x040000FF RID: 255
	public float attackhalttime;

	// Token: 0x04000100 RID: 256
	public float damagehalttime;

	// Token: 0x04000101 RID: 257
	public float AIUpdateSpeed;

	// Token: 0x04000102 RID: 258
	public float myspeed;

	// Token: 0x04000103 RID: 259
	public float flinchignorechance;

	// Token: 0x04000104 RID: 260
	public float damagechance;

	// Token: 0x04000105 RID: 261
	public float attacktime;

	// Token: 0x04000106 RID: 262
	public float attacktimerange;

	// Token: 0x04000107 RID: 263
	public float attackrange;

	// Token: 0x04000108 RID: 264
	public LayerMask BlockingLayers;

	// Token: 0x04000109 RID: 265
	public AudioClip attacksound;

	// Token: 0x0400010A RID: 266
	public AudioClip alertsound;

	// Token: 0x0400010B RID: 267
	public AudioClip prefiresound;

	// Token: 0x0400010C RID: 268
	public float prefiretime;

	// Token: 0x0400010D RID: 269
	public bool AMAWAKE;

	// Token: 0x0400010E RID: 270
	[HideInInspector]
	public float idlesoundtimer;

	// Token: 0x0400010F RID: 271
	public bool loopingidlesound;

	// Token: 0x04000110 RID: 272
	public bool idlefootsteps;

	// Token: 0x04000111 RID: 273
	public Vector2 idlesoundactivatetimerange;

	// Token: 0x04000112 RID: 274
	public GameObject idlesound;

	// Token: 0x04000113 RID: 275
	public bool dootherawakesounds;

	// Token: 0x04000114 RID: 276
	public AudioClip[] otherawakesounds;

	// Token: 0x04000115 RID: 277
	public bool morethanoneattack;

	// Token: 0x04000116 RID: 278
	public int[] MORE_mytype;

	// Token: 0x04000117 RID: 279
	public int[] MORE_numshots;

	// Token: 0x04000118 RID: 280
	public int[] MORE_numshotsequence;

	// Token: 0x04000119 RID: 281
	public float[] MORE_shotsequencetimer;

	// Token: 0x0400011A RID: 282
	public float[] MORE_inaccuracy;

	// Token: 0x0400011B RID: 283
	public GameObject[] MORE_myprojectile;

	// Token: 0x0400011C RID: 284
	public float[] MORE_projectilespeed;

	// Token: 0x0400011D RID: 285
	public float[] MORE_mydamage;

	// Token: 0x0400011E RID: 286
	public float[] MORE_mygooddamage;

	// Token: 0x0400011F RID: 287
	public float[] MORE_attackrange;

	// Token: 0x04000120 RID: 288
	public AudioClip[] MORE_attacksound;

	// Token: 0x04000121 RID: 289
	public AudioClip[] MORE_prefiresound;

	// Token: 0x04000122 RID: 290
	public float[] MORE_prefiretime;

	// Token: 0x04000123 RID: 291
	public GameObject[] MORE_mymuzzleflash;

	// Token: 0x04000124 RID: 292
	public ArrayList MORE2_mytype;

	// Token: 0x04000125 RID: 293
	public ArrayList MORE2_numshots;

	// Token: 0x04000126 RID: 294
	public ArrayList MORE2_numshotsequence;

	// Token: 0x04000127 RID: 295
	public ArrayList MORE2_shotsequencetimer;

	// Token: 0x04000128 RID: 296
	public ArrayList MORE2_inaccuracy;

	// Token: 0x04000129 RID: 297
	public ArrayList MORE2_myprojectile;

	// Token: 0x0400012A RID: 298
	public ArrayList MORE2_projectilespeed;

	// Token: 0x0400012B RID: 299
	public ArrayList MORE2_mydamage;

	// Token: 0x0400012C RID: 300
	public ArrayList MORE2_mygooddamage;

	// Token: 0x0400012D RID: 301
	public ArrayList MORE2_attackrange;

	// Token: 0x0400012E RID: 302
	public ArrayList MORE2_attacksound;

	// Token: 0x0400012F RID: 303
	public ArrayList MORE2_prefiresound;

	// Token: 0x04000130 RID: 304
	public ArrayList MORE2_prefiretime;

	// Token: 0x04000131 RID: 305
	public ArrayList MORE2_mymuzzleflash;

	// Token: 0x04000132 RID: 306
	[HideInInspector]
	public Vector3 mygoal;

	// Token: 0x04000133 RID: 307
	[HideInInspector]
	public float stopmovement;

	// Token: 0x04000134 RID: 308
	[HideInInspector]
	public float AITimer;

	// Token: 0x04000135 RID: 309
	[HideInInspector]
	public float attacktimer;

	// Token: 0x04000136 RID: 310
	[HideInInspector]
	public bool attackflag;

	// Token: 0x04000137 RID: 311
	[HideInInspector]
	public bool ignoreattackflag;

	// Token: 0x04000138 RID: 312
	[HideInInspector]
	public float damagetimer;

	// Token: 0x04000139 RID: 313
	[HideInInspector]
	public float prefiretimer;

	// Token: 0x0400013A RID: 314

	public GameObject MyTarget;

	// Token: 0x0400013B RID: 315
	[HideInInspector]
	public Vector3 shottarget;

	// Token: 0x0400013C RID: 316
	[HideInInspector]
	public float rigiddamagedelay;

	// Token: 0x0400013D RID: 317
	[HideInInspector]
	public float originalattacktime;

	// Token: 0x0400013E RID: 318
	public NavMeshAgent nav;

	// Token: 0x0400013F RID: 319
	[HideInInspector]
	public AudioSource aud;

	// Token: 0x04000140 RID: 320
	[HideInInspector]
	public GameObject disbeplayer;

	// Token: 0x04000141 RID: 321
	[HideInInspector]
	public GameObject disbecamera;

	// Token: 0x04000142 RID: 322
	public AttackScript disbeattack;

	// Token: 0x04000143 RID: 323
	[HideInInspector]
	public float initialnavoffset;

	// Token: 0x04000144 RID: 324
	[HideInInspector]
	public float currentnavoffset;

	// Token: 0x04000145 RID: 325
	[HideInInspector]
	public float jumpvelocity;

	// Token: 0x04000146 RID: 326
	[HideInInspector]
	public float ThisStoresBossStartingHealth;

	// Token: 0x04000147 RID: 327
	public StatData statscript;

	// Token: 0x04000148 RID: 328
	[HideInInspector]
	public Renderer rend;

	// Token: 0x04000149 RID: 329
	[HideInInspector]
	public int currentshotsequence;

	// Token: 0x0400014A RID: 330
	[HideInInspector]
	public float sequencetimer;

	// Token: 0x0400014B RID: 331
	[HideInInspector]
	public int attacktypeindex;

	// Token: 0x0400014C RID: 332
	[HideInInspector]
	public SaveManagerScript sav;

	// Token: 0x0400014D RID: 333
	[HideInInspector]
	public Vector3 origcoors;

	// Token: 0x0400014E RID: 334
	[HideInInspector]
	public Vector3 previousposition;

	// Token: 0x0400014F RID: 335
	[HideInInspector]
	public bool idleplaying;

	// Token: 0x04000150 RID: 336
	public GameObject TargetThis;

	// Token: 0x04000151 RID: 337
	public bool berserkenemy;
}
