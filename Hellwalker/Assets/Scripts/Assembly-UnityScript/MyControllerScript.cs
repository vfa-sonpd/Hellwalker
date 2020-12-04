using System;
using UnityEngine;

// Token: 0x0200007F RID: 127
[Serializable]
public class MyControllerScript : MonoBehaviour
{
	// Token: 0x0600031C RID: 796 RVA: 0x0001B768 File Offset: 0x00019968
	public MyControllerScript()
	{
		this.canjump = true;
		this.dolandbob = true;
		this.crouchtoggle = true;
	}

	// Token: 0x0600031D RID: 797 RVA: 0x0001B788 File Offset: 0x00019988
	public virtual void Start()
	{
		this.inputmanager = Essential.Instance.inputManager;
        this.superhot = false;
		this.originalCrouchHeight = ((CharacterController)this.GetComponent(typeof(CharacterController))).height;
		this.currentHeight = this.originalCrouchHeight;
		this.normalCamHeight = GameObject.Find("MainCamera").transform.localPosition.y;
		this.dascam = GameObject.Find("MainCamera");
		this.currentCamHeight = this.normalCamHeight;
		this.originaltimestep = Time.fixedDeltaTime;
		this.gravityslowmultiplier = (float)1;
	}

	// Token: 0x0600031E RID: 798 RVA: 0x0001B840 File Offset: 0x00019A40
	public virtual void Update()
	{
		if (!this.CheckGrounded())
		{
			this.rigidbox.enabled = false;
		}
		else
		{
			this.rigidbox.enabled = true;
		}
		this.SuperHot();
		RaycastHit raycastHit = default(RaycastHit);
		if (Physics.Raycast(this.transform.position, this.transform.up, out raycastHit, this.originalCrouchHeight / (float)2 + 0.1f, this.groundlayers) && this.gravityforce > (float)0)
		{
			this.gravityforce = (float)0;
			this.realrocketjump.y = (float)0;
		}
		this.freezetimer -= Time.deltaTime;
		if (this.freezetimer < (float)0)
		{
			this.freezetimer = (float)0;
		}
		if (this.inputmanager.GetKeyInput("left", 0))
		{
			this.currenthopkey = false;
		}
		if (this.inputmanager.GetKeyInput("right", 0))
		{
			this.currenthopkey = true;
		}
		if (this.CheckGrounded())
		{
			this.bunnyspeed -= this.bunnydecrease * Time.deltaTime;
			if (this.bunnyspeed < (float)0)
			{
				this.bunnyspeed = (float)0;
			}
		}
		this.bunnyvector = this.InputDir.normalized;
		this.bunnyvector.y = (float)0;
		if (this.bunnyspeed > this.maxbunnyspeed / (float)2)
		{
			this.bunnyvector *= this.bunnyspeed;
		}
		else
		{
			this.bunnyvector *= (float)0;
		}
		if (this.bunnyspeed > this.maxbunnyspeed)
		{
			this.bunnyspeed = this.maxbunnyspeed;
		}
		if (this.gravityforce < (float)-1)
		{
			this.gravityforce = (float)-1;
		}
		if (this.hangtime > (float)0)
		{
			this.hangtime -= Time.deltaTime;
		}
		GameObject gameObject = GameObject.Find("MainCamera");
		if (this.inputmanager.GetKeyInput("jump", 0) && this.canjump)
		{
			this.dojump = true;
			this.canjump = false;
		}
		if (this.inputmanager.GetKeyInput("jump", 1) && this.candoublejump)
		{
			this.candoublejump = false;
		}
		this.canflip = false;
		if (this.inputmanager.GetKeyInput("jump", 1) && this.canflip)
		{
			if (!((MyMouseLook)gameObject.GetComponent(typeof(MyMouseLook))).stayupsidedown)
			{
				this.gravityforce += 0.3f;
				this.hangtime = (float)1;
				((AudioSource)this.flipsound.GetComponent(typeof(AudioSource))).Play();
			}
			((MyMouseLook)gameObject.GetComponent(typeof(MyMouseLook))).doflip = true;
			((MyMouseLook)gameObject.GetComponent(typeof(MyMouseLook))).stayupsidedown = true;
			this.canjump = false;
			this.dojump = false;
		}
		if (this.inputmanager.GetKeyInput("jump", 2))
		{
			this.dojump = false;
			this.canjump = true;
			this.canflip = true;
		}
		this.canslidefloat -= Time.deltaTime;
		if (this.canslidefloat < (float)0)
		{
			this.canslidefloat = (float)0;
		}
		if (this.CheckGrounded() || this.SecondCheckGrounded())
		{
			this.canslidefloat = 0.03f;
		}
		if (!this.crouchtoggle && this.inputmanager.GetKeyInput("crouch", 2) && !this.inwater)
		{
			this.CrouchState = false;
		}
		if (this.inputmanager.GetKeyInput("crouch", 1) && !this.inwater)
		{
			if (!this.CrouchState && (this.inputmanager.GetKeyInput("right", 0) || this.inputmanager.GetKeyInput("left", 0) || this.inputmanager.GetKeyInput("forward", 0) || this.inputmanager.GetKeyInput("backward", 0)) && this.canslidefloat > (float)0 && this.gravityforce <= (float)0)
			{
				this.crouchVector = this.InputDir * 1.5f;
				((AudioSource)this.SlideSound.GetComponent(typeof(AudioSource))).Play();
				((MyMouseLook)GameObject.Find("MainCamera").GetComponent(typeof(MyMouseLook))).buck = ((MyMouseLook)GameObject.Find("MainCamera").GetComponent(typeof(MyMouseLook))).buck + (float)7;
				float y = 0.06f;
				Vector3 localPosition = GameObject.Find("WeaponCam").transform.localPosition;
				float num = localPosition.y = y;
				Vector3 vector = GameObject.Find("WeaponCam").transform.localPosition = localPosition;
			}
			this.CrouchState = !this.CrouchState;
		}
		if (this.inwater)
		{
			this.CrouchState = true;
		}
		if (this.CrouchState)
		{
			this.currentHeight -= Time.deltaTime * this.crouchSpeed;
			this.currentCamHeight -= Time.deltaTime * this.crouchSpeed;
		}
		else
		{
			this.currentHeight += Time.deltaTime * this.crouchSpeed;
			this.currentCamHeight += Time.deltaTime * this.crouchSpeed;
			this.crouchVector = new Vector3((float)0, (float)0, (float)0);
		}
		if ((this.inputmanager.GetKeyInput("right", 0) || this.inputmanager.GetKeyInput("left", 0) || this.inputmanager.GetKeyInput("forward", 0) || this.inputmanager.GetKeyInput("backward", 0)) && !this.CrouchState)
		{
			this.bobindex += Time.deltaTime * this.bobspeed;
			if (this.bobindex > (float)1)
			{
				this.bobindex -= (float)1;
			}
			this.currentbob = this.bobcurve.Evaluate(this.bobindex) * this.bobmultiplier;
		}
		else
		{
			if (this.currentbob != (float)0)
			{
				this.currentbob -= Mathf.Sign(this.currentbob) * this.bobspeed * Time.deltaTime;
			}
			if (Mathf.Abs(this.currentbob) < 0.05f)
			{
				this.currentbob = (float)0;
			}
		}
		if (this.currentHeight > this.originalCrouchHeight)
		{
			this.currentHeight = this.originalCrouchHeight;
		}
		if (this.currentHeight < this.crouchHeight)
		{
			this.currentHeight = this.crouchHeight;
		}
		if (this.currentCamHeight > this.normalCamHeight)
		{
			this.currentCamHeight = this.normalCamHeight;
		}
		if (this.currentCamHeight < this.crouchCamHeight)
		{
			this.currentCamHeight = this.crouchCamHeight;
		}
		((CharacterController)this.GetComponent(typeof(CharacterController))).height = this.currentHeight;
		float y2 = this.currentCamHeight + this.currentbob;
		Vector3 localPosition2 = GameObject.Find("MainCamera").transform.localPosition;
		float num2 = localPosition2.y = y2;
		Vector3 vector2 = GameObject.Find("MainCamera").transform.localPosition = localPosition2;
		if (Time.timeScale == (float)1)
		{
			Vector3 vector3 = (this.InputDir + this.ExplosionDir + this.crouchVector + this.bunnyvector + this.GrappleVector + this.realrocketjump) * (float)((SelectionScript)GameObject.Find("WeaponAnimator").GetComponent(typeof(SelectionScript))).speedmultiply;
			vector3 *= Time.deltaTime * (float)50;
			((CharacterController)this.GetComponent(typeof(CharacterController))).Move(vector3);
		}
	}

	// Token: 0x0600031F RID: 799 RVA: 0x0001C098 File Offset: 0x0001A298
	public virtual void FixedUpdate()
	{
		this.GetInput();
		if (!this.inwater)
		{
			this.DoGravity();
		}
		this.DoFriction();
		this.CheckEnemyCollision();
		if (Time.timeScale != (float)1)
		{
			Vector3 motion = (this.InputDir + this.ExplosionDir + this.crouchVector + this.bunnyvector + this.GrappleVector + this.realrocketjump) * (float)((SelectionScript)GameObject.Find("WeaponAnimator").GetComponent(typeof(SelectionScript))).speedmultiply;
			((CharacterController)this.GetComponent(typeof(CharacterController))).Move(motion);
		}
	}

	// Token: 0x06000320 RID: 800 RVA: 0x0001C158 File Offset: 0x0001A358
	public virtual void SuperHot()
	{
		if (this.superhot)
		{
			this.superhottimer -= Time.deltaTime;
			if (this.superhottimer < (float)0)
			{
				this.superhottimer = (float)0;
				this.superhot = false;
			}
			if (!((InGameMenuScript)GameObject.Find("DasMenu").GetComponent(typeof(InGameMenuScript))).isPaused)
			{
				if (this.inputmanager.GetKeyInput("right", 0) || this.inputmanager.GetKeyInput("left", 0) || this.inputmanager.GetKeyInput("forward", 0) || this.inputmanager.GetKeyInput("backward", 0) || this.inputmanager.GetKeyInput("jump", 0) || this.crouchVector.magnitude > 0.5f)
				{
					Time.timeScale += Time.deltaTime * (float)8;
					if (Time.timeScale > (float)1)
					{
						Time.timeScale = (float)1;
					}
					Time.fixedDeltaTime = Time.timeScale * this.originaltimestep;
					this.gravityslowmultiplier = (float)1;
				}
				else
				{
					Time.timeScale -= Time.deltaTime * (float)8;
					if (Time.timeScale < 0.05f)
					{
						Time.timeScale = 0.05f;
					}
					this.gravityslowmultiplier = 0.025f;
					Time.fixedDeltaTime = Time.timeScale * this.originaltimestep;
				}
			}
		}
		//else 
  //      if (!((InGameMenuScript)GameObject.Find("DasMenu").GetComponent(typeof(InGameMenuScript))).isPaused)
		//{
		//	Time.timeScale += Time.deltaTime * (float)8;
		//	if (Time.timeScale > (float)1)
		//	{
		//		Time.timeScale = (float)1;
		//	}
		//	Time.fixedDeltaTime = Time.timeScale * this.originaltimestep;
		//	this.gravityslowmultiplier = (float)1;
		//	if (((SaveManagerScript)GameObject.Find("SaveManager").GetComponent(typeof(SaveManagerScript))).dosave)
		//	{
		//		Time.timeScale = 0.01f;
		//	}
		//}
	}

	// Token: 0x06000321 RID: 801 RVA: 0x0001C374 File Offset: 0x0001A574
	public virtual void CheckEnemyCollision()
	{
		RaycastHit raycastHit = default(RaycastHit);
		if (Physics.Raycast(this.transform.position, new Vector3(this.InputDir.x, (float)0, (float)0), out raycastHit, 1.3f, this.enemylayers))
		{
			this.InputDir.x = (float)0;
		}
		if (Physics.Raycast(this.transform.position, new Vector3((float)0, this.InputDir.y, (float)0), out raycastHit, 1.3f, this.enemylayers))
		{
			this.InputDir.y = (float)0;
		}
		if (Physics.Raycast(this.transform.position, new Vector3((float)0, (float)0, this.InputDir.z), out raycastHit, 1.3f, this.enemylayers))
		{
			this.InputDir.z = (float)0;
		}
	}

	// Token: 0x06000322 RID: 802 RVA: 0x0001C464 File Offset: 0x0001A664
	public virtual bool CheckGrounded()
	{
		CharacterController characterController = (CharacterController)this.GetComponent(typeof(CharacterController));
		return characterController.isGrounded;
	}

	// Token: 0x06000323 RID: 803 RVA: 0x0001C494 File Offset: 0x0001A694
	public virtual bool SecondCheckGrounded()
	{
		bool result = false;
		CharacterController characterController = (CharacterController)this.GetComponent(typeof(CharacterController));
		RaycastHit raycastHit = default(RaycastHit);
		if (Physics.Raycast(this.transform.position, -this.transform.up, out raycastHit, (float)4, this.groundlayers))
		{
			float num = Mathf.Abs(Vector3.Angle(this.transform.up, raycastHit.normal));
			float num2 = num / (float)90;
			float num3 = characterController.height / (float)2 + 0.5f + num2;
			if (Vector3.Distance(this.transform.position, raycastHit.point) <= num3)
			{
				result = true;
			}
		}
		if (this.gravityforce > (float)0)
		{
			result = false;
		}
		return result;
	}

	// Token: 0x06000324 RID: 804 RVA: 0x0001C560 File Offset: 0x0001A760
	public virtual void GetInput()
	{
		if (this.inputmanager.GetKeyInput("walk", 0))
		{
			if (this.runtoggle)
			{
				this.MaxRunSpeed = this.RunButtonSpeed;
				this.InputAccel = this.RunAccel;
			}
			else
			{
				this.InputAccel = this.WalkAccel;
				this.MaxRunSpeed = this.WalkSpeed;
			}
		}
		else if (this.runtoggle)
		{
			this.InputAccel = this.WalkAccel;
			this.MaxRunSpeed = this.WalkSpeed;
		}
		else
		{
			this.MaxRunSpeed = this.RunButtonSpeed;
			this.InputAccel = this.RunAccel;
		}
		if (this.currentHeight < this.originalCrouchHeight)
		{
			this.MaxRunSpeed = this.crouchMoveSpeed;
		}
		if (!this.inwater)
		{
			if (this.inputmanager.GetKeyInput("right", 0))
			{
				this.InputDir += this.transform.right * this.InputAccel;
			}
			if (this.inputmanager.GetKeyInput("left", 0))
			{
				this.InputDir -= this.transform.right * this.InputAccel;
			}
			if (this.inputmanager.GetKeyInput("forward", 0))
			{
				this.InputDir += this.transform.forward * this.InputAccel;
			}
			if (this.inputmanager.GetKeyInput("backward", 0))
			{
				this.InputDir -= this.transform.forward * this.InputAccel;
			}
			this.InputDir.y = (float)0;
		}
		if (this.SixDegreesOfFreedomSwimming)
		{
			if (this.inwater)
			{
				if (this.inputmanager.GetKeyInput("right", 0))
				{
					this.InputDir += this.dascam.transform.right * this.InputAccel;
				}
				if (this.inputmanager.GetKeyInput("left", 0))
				{
					this.InputDir -= this.dascam.transform.right * this.InputAccel;
				}
				if (this.inputmanager.GetKeyInput("forward", 0))
				{
					this.InputDir += this.dascam.transform.forward * this.InputAccel;
				}
				if (this.inputmanager.GetKeyInput("backward", 0))
				{
					this.InputDir -= this.dascam.transform.forward * this.InputAccel;
				}
				if (this.inputmanager.GetKeyInput("jump", 0))
				{
					this.InputDir = this.dascam.transform.up * 0.13f;
				}
				if (this.inputmanager.GetKeyInput("crouch", 0))
				{
					this.InputDir = -this.dascam.transform.up * 0.13f;
				}
			}
		}
		else if (this.inwater)
		{
			if (this.inputmanager.GetKeyInput("right", 0))
			{
				this.InputDir += this.transform.right * this.InputAccel;
			}
			if (this.inputmanager.GetKeyInput("left", 0))
			{
				this.InputDir -= this.transform.right * this.InputAccel;
			}
			if (this.inputmanager.GetKeyInput("forward", 0))
			{
				this.InputDir += this.dascam.transform.forward * this.InputAccel;
			}
			if (this.inputmanager.GetKeyInput("backward", 0))
			{
				this.InputDir -= this.dascam.transform.forward * this.InputAccel;
			}
			if (this.inputmanager.GetKeyInput("jump", 0))
			{
				this.InputDir = Vector3.up * 0.13f;
			}
			if (this.inputmanager.GetKeyInput("crouch", 0))
			{
				this.InputDir = -Vector3.up * 0.13f;
			}
		}
		if (Vector3.Magnitude(this.InputDir) > this.MaxRunSpeed)
		{
			this.InputDir = Vector3.Normalize(this.InputDir) * this.MaxRunSpeed;
		}
		if (((SelectionScript)GameObject.Find("WeaponAnimator").GetComponent(typeof(SelectionScript))).havejetpack && !this.inwater)
		{
			this.InputDir = new Vector3((float)0, (float)0, (float)0);
		}
		this.stepsoundtimer += (float)1;
		if (this.stepsoundtimer >= this.walkstepsoundfrequency && this.InputDir.magnitude > 0.25f && this.CheckGrounded())
		{
			((AudioSource)this.walksounds[UnityEngine.Random.Range(0, this.walksounds.Length)].GetComponent(typeof(AudioSource))).pitch = UnityEngine.Random.Range(0.7f, 1.4f);
			((AudioSource)this.walksounds[UnityEngine.Random.Range(0, this.walksounds.Length)].GetComponent(typeof(AudioSource))).Play();
			this.stepsoundtimer = (float)0;
		}
		if (this.freezetimer > (float)0)
		{
			this.InputDir = new Vector3((float)0, (float)0, (float)0);
		}
	}

	// Token: 0x06000325 RID: 805 RVA: 0x0001CB78 File Offset: 0x0001AD78
	public virtual void CheckLadder()
	{
		MyMouseLook myMouseLook = (MyMouseLook)GameObject.Find("MainCamera").GetComponent(typeof(MyMouseLook));
		if (((SelectionScript)GameObject.Find("WeaponAnimator").GetComponent(typeof(SelectionScript))).havejetpack)
		{
			if ((this.inputmanager.GetKeyInput("forward", 0) && myMouseLook.xRotation <= (float)0) || (this.inputmanager.GetKeyInput("backward", 0) && myMouseLook.xRotation > (float)0) || this.inputmanager.GetKeyInput("jump", 0))
			{
				this.gravityforce = 0.26f;
			}
			else if ((this.inputmanager.GetKeyInput("backward", 0) && myMouseLook.xRotation <= (float)0) || (this.inputmanager.GetKeyInput("forward", 0) && myMouseLook.xRotation > (float)0) || this.inputmanager.GetKeyInput("crouch", 0))
			{
				this.gravityforce = -0.26f;
			}
			else
			{
				this.gravityforce = (float)0;
			}
		}
	}

	// Token: 0x06000326 RID: 806 RVA: 0x0001CCAC File Offset: 0x0001AEAC
	public virtual void DoGravity()
	{
		if (this.hangtime > (float)0)
		{
			this.gravityforce = (float)0;
		}
		GameObject gameObject = GameObject.Find("MainCamera");
		if (this.CheckGrounded() && this.dolandbob)
		{
			float y = 0.05f;
			Vector3 localPosition = this.WeaponCam.transform.localPosition;
			float num = localPosition.y = y;
			Vector3 vector = this.WeaponCam.transform.localPosition = localPosition;
			if (this.doepiclandbob)
			{
				float y2 = 0.1f;
				Vector3 localPosition2 = this.WeaponCam.transform.localPosition;
				float num2 = localPosition2.y = y2;
				Vector3 vector2 = this.WeaponCam.transform.localPosition = localPosition2;
			}
			((AudioSource)this.JumpLandSound.GetComponent(typeof(AudioSource))).Play();
			this.dolandbob = false;
			this.doepiclandbob = false;
			this.landbobtimer = (float)0;
		}
		if (this.dojump && (this.CheckGrounded() || this.SecondCheckGrounded()))
		{
			this.gravityforce = this.jumpamount;
			this.candoublejump = true;
			if (this.inputmanager.GetKeyInput("right", 0) || this.inputmanager.GetKeyInput("left", 0))
			{
				this.bunnyspeed += this.bunnyincrease;
				this.lasthopkey = this.currenthopkey;
			}
			((MyMouseLook)gameObject.GetComponent(typeof(MyMouseLook))).flipamount = (float)0;
			if (this.bunnyspeed <= (float)0)
			{
				float y3 = -0.03f;
				Vector3 localPosition3 = this.WeaponCam.transform.localPosition;
				float num3 = localPosition3.y = y3;
				Vector3 vector3 = this.WeaponCam.transform.localPosition = localPosition3;
			}
			if (!((SelectionScript)GameObject.Find("WeaponAnimator").GetComponent(typeof(SelectionScript))).havejetpack)
			{
				((AudioSource)this.JumpSound.GetComponent(typeof(AudioSource))).Play();
			}
			this.dojump = false;
		}
		if (this.CheckGrounded())
		{
			if (this.gravityforce < (float)0)
			{
				this.gravityforce = (float)0;
			}
			this.landbobtimer = (float)0;
			this.canflip = false;
		}
		else
		{
			this.gravityforce -= this.gravity * Time.timeScale;
			if (((SelectionScript)GameObject.Find("WeaponAnimator").GetComponent(typeof(SelectionScript))).havejetpack)
			{
				this.landbobtimer = (float)0;
			}
			this.landbobtimer += Time.deltaTime;
			if (this.landbobtimer >= 0.5f && this.bunnyspeed <= 0.1f && !((MyMouseLook)this.MainCam.GetComponent(typeof(MyMouseLook))).doroll)
			{
				this.dolandbob = true;
			}
			if (this.landbobtimer >= (float)1 && this.bunnyspeed <= (float)0 && !((MyMouseLook)this.MainCam.GetComponent(typeof(MyMouseLook))).doroll)
			{
				this.doepiclandbob = true;
			}
		}
		this.CheckLadder();
		this.InputDir += new Vector3((float)0, this.gravityforce * Time.timeScale, (float)0);
	}

	// Token: 0x06000327 RID: 807 RVA: 0x0001D03C File Offset: 0x0001B23C
	public virtual void DoFriction()
	{
		this.InputDir /= this.RunFrictionAmount;
		this.ExplosionDir /= (float)1 + 0.05f * Time.timeScale;
		this.crouchVector /= 1.03f;
		this.GrappleVector /= (float)1 + 0.05f * Time.timeScale;
		this.realrocketjump /= (float)1 + 0.05f * Time.timeScale;
	}

	// Token: 0x06000328 RID: 808 RVA: 0x0001D0D4 File Offset: 0x0001B2D4
	public virtual void OnCollisionEnter(Collision hit)
	{
		Rigidbody rigidbody = (Rigidbody)hit.transform.gameObject.GetComponent(typeof(Rigidbody));
		if (rigidbody != null)
		{
			rigidbody.velocity = this.transform.forward.normalized * (float)15;
		}
	}

	// Token: 0x06000329 RID: 809 RVA: 0x0001D130 File Offset: 0x0001B330
	public virtual void Main()
	{
	}

	// Token: 0x0400037A RID: 890
	public bool SixDegreesOfFreedomSwimming;

	// Token: 0x0400037B RID: 891
	public AnimationCurve bobcurve;

	// Token: 0x0400037C RID: 892
	public LayerMask enemylayers;

	// Token: 0x0400037D RID: 893
	public LayerMask groundlayers;

	// Token: 0x0400037E RID: 894
	public float bobspeed;

	// Token: 0x0400037F RID: 895
	public float bobmultiplier;

	// Token: 0x04000380 RID: 896
	[HideInInspector]
	public float currentbob;

	// Token: 0x04000381 RID: 897
	[HideInInspector]
	public float bobindex;

	// Token: 0x04000382 RID: 898
	[HideInInspector]
	public float canslidefloat;

	// Token: 0x04000383 RID: 899
	public bool runtoggle;

	// Token: 0x04000384 RID: 900
	public float walkstepsoundfrequency;

	// Token: 0x04000385 RID: 901
	public GameObject[] walksounds;

	// Token: 0x04000386 RID: 902
	[HideInInspector]
	public float stepsoundtimer;

	// Token: 0x04000387 RID: 903
	public float crouchHeight;

	// Token: 0x04000388 RID: 904
	public float crouchSpeed;

	// Token: 0x04000389 RID: 905
	public float crouchMoveSpeed;

	// Token: 0x0400038A RID: 906
	public float crouchCamHeight;

	// Token: 0x0400038B RID: 907
	public GameObject JumpLandSound;

	// Token: 0x0400038C RID: 908
	public GameObject SlideSound;

	// Token: 0x0400038D RID: 909
	public GameObject flipsound;

	// Token: 0x0400038E RID: 910
	public GameObject JumpSound;

	// Token: 0x0400038F RID: 911
	public GameObject MainCam;

	// Token: 0x04000390 RID: 912
	public GameObject WeaponCam;

	// Token: 0x04000391 RID: 913
	public float RunAccel;

	// Token: 0x04000392 RID: 914
	public float WalkAccel;

	// Token: 0x04000393 RID: 915
	public float RunFrictionAmount;

	// Token: 0x04000394 RID: 916
	public float gravity;

	// Token: 0x04000395 RID: 917
	public float jumpamount;

	// Token: 0x04000396 RID: 918
	public float MaxTotalSpeed;

	// Token: 0x04000397 RID: 919
	public float RunButtonSpeed;

	// Token: 0x04000398 RID: 920
	public float WalkSpeed;

	// Token: 0x04000399 RID: 921
	[HideInInspector]
	public float InputAccel;

	// Token: 0x0400039A RID: 922
	[HideInInspector]
	public Vector3 InputDir;

	// Token: 0x0400039B RID: 923
	[HideInInspector]
	public float MaxRunSpeed;

	// Token: 0x0400039C RID: 924
	[HideInInspector]
	public Vector3 MovDir;

	// Token: 0x0400039D RID: 925
	[HideInInspector]
	public float gravityforce;

	// Token: 0x0400039E RID: 926
	[HideInInspector]
	public bool dojump;

	// Token: 0x0400039F RID: 927
	[HideInInspector]
	public bool canjump;

	// Token: 0x040003A0 RID: 928
	[HideInInspector]
	public bool dolandbob;

	// Token: 0x040003A1 RID: 929
	[HideInInspector]
	public bool doepiclandbob;

	// Token: 0x040003A2 RID: 930
	[HideInInspector]
	public float landbobtimer;

	// Token: 0x040003A3 RID: 931
	[HideInInspector]
	public Vector3 ExplosionDir;

	// Token: 0x040003A4 RID: 932
	[HideInInspector]
	public float originalCrouchHeight;

	// Token: 0x040003A5 RID: 933
	[HideInInspector]
	public float currentHeight;

	// Token: 0x040003A6 RID: 934
	[HideInInspector]
	public float normalCamHeight;

	// Token: 0x040003A7 RID: 935
	[HideInInspector]
	public float currentCamHeight;

	// Token: 0x040003A8 RID: 936
	[HideInInspector]
	public Vector3 crouchVector;

	// Token: 0x040003A9 RID: 937
	[HideInInspector]
	public bool CrouchState;

	// Token: 0x040003AA RID: 938
	[HideInInspector]
	public bool canflip;

	// Token: 0x040003AB RID: 939
	[HideInInspector]
	public float hangtime;

	// Token: 0x040003AC RID: 940
	[HideInInspector]
	public bool inwater;

	// Token: 0x040003AD RID: 941
	[HideInInspector]
	public bool candoublejump;

	// Token: 0x040003AE RID: 942
	[HideInInspector]
	public Vector3 GrappleVector;

	// Token: 0x040003AF RID: 943
	public Vector3 bunnyvector;

	// Token: 0x040003B0 RID: 944
	public float bunnyspeed;

	// Token: 0x040003B1 RID: 945
	public float bunnyincrease;

	// Token: 0x040003B2 RID: 946
	public float bunnydecrease;

	// Token: 0x040003B3 RID: 947
	public float maxbunnyspeed;

	// Token: 0x040003B4 RID: 948
	[HideInInspector]
	public bool currenthopkey;

	// Token: 0x040003B5 RID: 949
	[HideInInspector]
	public bool lasthopkey;

	// Token: 0x040003B6 RID: 950
	[HideInInspector]
	public float freezetimer;

	// Token: 0x040003B7 RID: 951
	[HideInInspector]
	public Vector3 realrocketjump;

	// Token: 0x040003B8 RID: 952
	[HideInInspector]
	public bool superhot;

	// Token: 0x040003B9 RID: 953
	[HideInInspector]
	public float superhottimer;

	// Token: 0x040003BA RID: 954
	[HideInInspector]
	public float originaltimestep;

	// Token: 0x040003BB RID: 955
	[HideInInspector]
	public float gravityslowmultiplier;

	// Token: 0x040003BC RID: 956
	[HideInInspector]
	public MyInputManager inputmanager;

	// Token: 0x040003BD RID: 957
	[HideInInspector]
	public GameObject dascam;

	// Token: 0x040003BE RID: 958
	public Collider rigidbox;

	// Token: 0x040003BF RID: 959
	public bool crouchtoggle;
}
