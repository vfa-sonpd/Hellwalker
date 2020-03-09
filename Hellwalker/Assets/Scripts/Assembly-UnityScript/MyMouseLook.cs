using System;
using UnityEngine;

// Token: 0x02000081 RID: 129
[Serializable]
public class MyMouseLook : MonoBehaviour
{
	// Token: 0x06000334 RID: 820 RVA: 0x0001DD4C File Offset: 0x0001BF4C
	public MyMouseLook()
	{
		this.lookSensitivity = (float)5;
		this.currentYRotation2 = (float)4;
		this.dobob = true;
	}

	// Token: 0x06000335 RID: 821 RVA: 0x0001DD6C File Offset: 0x0001BF6C
	public virtual void Awake()
	{
		GameObject gameObject = GameObject.Find("LookAtNode");
		if (gameObject)
		{
			this.controller.transform.LookAt(new Vector3(gameObject.transform.position.x, this.transform.position.y, this.transform.position.z));
			this.transform.LookAt(new Vector3(this.transform.position.x, gameObject.transform.position.y, this.transform.position.z));
			this.xRotation = this.controller.transform.eulerAngles.x;
			this.yRotation = this.controller.transform.eulerAngles.y;
		}
	}

	// Token: 0x06000336 RID: 822 RVA: 0x0001DE68 File Offset: 0x0001C068
	public virtual void Start()
	{
		Input.ResetInputAxes();
		this.zrotation = (float)0;
		this.phand = GameObject.Find("PlayerHand");
		this.flash = GameObject.Find("Flashlight");
		this.inputmanager = (MyInputManager)GameObject.Find("DasMenu").GetComponent(typeof(MyInputManager));
		this.spawnlocktimer = 0.4f;
		this.flipamount = (float)0;
		this.cont = (MyControllerScript)GameObject.Find("Player").GetComponent(typeof(MyControllerScript));
		this.hel = (PlayerHealthManagement)GameObject.Find("Player").GetComponent(typeof(PlayerHealthManagement));
		this.select = (SelectionScript)GameObject.Find("WeaponAnimator").GetComponent(typeof(SelectionScript));
		this.drunkincreasex = (float)1;
		this.drunkincreasey = (float)1;
	}

	// Token: 0x06000337 RID: 823 RVA: 0x0001DF58 File Offset: 0x0001C158
	public virtual void Update()
	{
		this.checkgroundedstate -= Time.deltaTime * (float)2;
		if ((this.xRotation > (float)89 || this.xRotation < (float)-89) && this.checkgroundedstate <= (float)0 && !this.cont.inwater)
		{
			this.doroll = true;
			this.addvelocity = true;
		}
		if (this.cont.inwater)
		{
			this.doroll = false;
		}
		if (this.doroll)
		{
			this.cont.CrouchState = true;
			if (this.addvelocity)
			{
				this.addvelocity = false;
			}
		}
		if (this.resetz)
		{
			this.actuallyresettingz = true;
		}
		if (this.doroll)
		{
			if (this.cont.CheckGrounded())
			{
				this.actuallyexecutingroll = true;
			}
			if (this.actuallyexecutingroll)
			{
				this.xRotation += Time.deltaTime * (float)450;
				if (this.xRotation > (float)360 || (this.xRotation > (float)0 && this.xRotation < (float)90))
				{
					this.xRotation = (float)0;
					this.currentXRotation = this.xRotation;
					this.doroll = false;
					this.actuallyexecutingroll = false;
					this.cont.CrouchState = false;
				}
			}
		}
		if (this.cont.CheckGrounded())
		{
			this.checkgroundedstate = (float)1;
		}
		this.dotilt();
		float num = Input.GetAxis("Mouse X");
		float num2 = Input.GetAxis("Mouse Y");
		float num3 = Input.GetAxis("joystickaxis4");
		float num4 = Input.GetAxis("joystickaxis5");
		if (Mathf.Abs(num3) < 0.1f)
		{
			num3 = (float)0;
		}
		if (Mathf.Abs(num4) < 0.1f)
		{
			num4 = (float)0;
		}
		num3 *= (float)400 * Time.deltaTime;
		num4 *= (float)250 * Time.deltaTime;
		num2 += num4;
		num -= num3;
		this.spawnlocktimer -= Time.deltaTime;
		if (this.spawnlocktimer < (float)0)
		{
			this.spawnlocktimer = (float)0;
		}
		if (this.spawnlocktimer > (float)0)
		{
			num = (float)0;
			num2 = (float)0;
		}
		float num5 = (float)200;
		float num6 = (float)6;
		float num7 = (float)3;
		if (this.inputmanager.GetKeyInput("turn left", 0))
		{
			num -= Time.deltaTime * num5 * this.turnaccel;
			this.turnaccel += Time.deltaTime * num6;
		}
		else if (this.inputmanager.GetKeyInput("turn right", 0))
		{
			num += Time.deltaTime * num5 * this.turnaccel;
			this.turnaccel += Time.deltaTime * num6;
		}
		else if (this.inputmanager.GetKeyInput("look up", 0))
		{
			num2 += Time.deltaTime * num5 * this.turnaccel;
			this.turnaccel += Time.deltaTime * num6;
		}
		else if (this.inputmanager.GetKeyInput("look down", 0))
		{
			num2 -= Time.deltaTime * num5 * this.turnaccel;
			this.turnaccel += Time.deltaTime * num6;
		}
		else
		{
			this.turnaccel = (float)0;
		}
		if (this.turnaccel > num7)
		{
			this.turnaccel = num7;
		}
		this.drunktimer -= Time.deltaTime * num6;
		if (this.drunktimer < (float)0)
		{
			this.drunktimer = UnityEngine.Random.Range(1f, 3f);
			this.drunkincreasex = (float)UnityEngine.Random.Range(-1, 2);
			this.drunkincreasey = (float)UnityEngine.Random.Range(-1, 2);
		}
		if (this.hel.drunkness >= (float)4)
		{
			num += this.drunkincreasex / (float)2;
			num2 += this.drunkincreasey / (float)2;
		}
		if (this.doroll && this.cont.CheckGrounded())
		{
			num = (float)0;
			num2 = (float)0;
		}
		this.mousemomentum -= Time.deltaTime * (float)20;
		if (this.mousemomentum < (float)0)
		{
			this.mousemomentum = (float)0;
		}
		if (!this.lockfreelook)
		{
			if ((this.xRotation < (float)90 && this.xRotation > (float)-90) || (this.xRotation > (float)275 && this.xRotation < (float)360))
			{
				this.yRotation += num * this.lookSensitivity;
			}
			else
			{
				this.yRotation -= num * this.lookSensitivity;
			}
			if (!this.inverted)
			{
				this.xRotation -= num2 * this.lookSensitivity;
			}
			else
			{
				this.xRotation += num2 * this.lookSensitivity;
			}
			if (this.xRotation > (float)360)
			{
				this.xRotation -= (float)360;
			}
			if (this.xRotation < (float)-360)
			{
				this.xRotation += (float)360;
			}
			if (this.flipamount > (float)360)
			{
				this.flipamount -= (float)360;
			}
			if (this.inputmanager.GetKeyInput("jump", 0) && this.flipamount >= (float)180)
			{
				this.stayupsidedown = false;
			}
			if (((MyControllerScript)this.controller.GetComponent(typeof(MyControllerScript))).CheckGrounded())
			{
				this.stayupsidedown = false;
			}
			if (this.flipamount > (float)180 && this.stayupsidedown)
			{
				this.flipamount = (float)180;
			}
			if (this.doflip)
			{
				this.flipamount += Time.deltaTime * (float)360;
			}
			if (this.flipamount > (float)360)
			{
				this.flipamount = (float)0;
				this.doflip = false;
			}
			if (this.inputmanager.GetKeyInput("center view", 0))
			{
				if (this.transform.localEulerAngles.z > (float)1 && this.transform.localEulerAngles.z < (float)359)
				{
					if (this.transform.localEulerAngles.z > (float)180)
					{
						float z = this.transform.localEulerAngles.z + Time.deltaTime * (float)180;
						Vector3 localEulerAngles = this.transform.localEulerAngles;
						float num8 = localEulerAngles.z = z;
						Vector3 vector = this.transform.localEulerAngles = localEulerAngles;
						if (this.transform.localEulerAngles.z > (float)360)
						{
							int num9 = 0;
							Vector3 localEulerAngles2 = this.transform.localEulerAngles;
							float num10 = localEulerAngles2.z = (float)num9;
							Vector3 vector2 = this.transform.localEulerAngles = localEulerAngles2;
						}
					}
					else
					{
						float z2 = this.transform.localEulerAngles.z - Time.deltaTime * (float)180;
						Vector3 localEulerAngles3 = this.transform.localEulerAngles;
						float num11 = localEulerAngles3.z = z2;
						Vector3 vector3 = this.transform.localEulerAngles = localEulerAngles3;
						if (this.transform.localEulerAngles.z < (float)0)
						{
							int num12 = 0;
							Vector3 localEulerAngles4 = this.transform.localEulerAngles;
							float num13 = localEulerAngles4.z = (float)num12;
							Vector3 vector4 = this.transform.localEulerAngles = localEulerAngles4;
							this.zrotation = this.transform.localEulerAngles.z;
						}
					}
				}
				else
				{
					int num14 = 0;
					Vector3 localEulerAngles5 = this.transform.localEulerAngles;
					float num15 = localEulerAngles5.z = (float)num14;
					Vector3 vector5 = this.transform.localEulerAngles = localEulerAngles5;
					this.zrotation = this.transform.localEulerAngles.z;
				}
				if (this.xRotation > (float)360)
				{
					this.xRotation -= (float)360;
				}
				if (this.xRotation < (float)0)
				{
					this.xRotation += (float)360;
				}
				if (this.xRotation > (float)1 && this.xRotation < (float)359)
				{
					if (this.xRotation > (float)180)
					{
						this.xRotation += Time.deltaTime * (float)180;
						if (this.xRotation > (float)360)
						{
							this.xRotation = (float)0;
						}
					}
					else
					{
						this.xRotation -= Time.deltaTime * (float)180;
						if (this.xRotation < (float)0)
						{
							this.xRotation = (float)0;
						}
					}
				}
				else
				{
					this.xRotation = (float)0;
				}
			}
			if (this.dobob)
			{
				if (!this.inputmanager.GetKeyInput("zoom", 0))
				{
					this.weaponoffsetx += num * this.weaponsensitivity;
					this.weaponoffsety += -(num2 * this.weaponsensitivity);
				}
				if (Mathf.Abs(this.weaponoffsetx) > this.maxweaponoffset)
				{
					this.weaponoffsetx = this.maxweaponoffset * Mathf.Sign(this.weaponoffsetx);
				}
				if (Mathf.Abs(this.weaponoffsety) > this.maxweaponoffset)
				{
					this.weaponoffsety = this.maxweaponoffset * Mathf.Sign(this.weaponoffsety);
				}
				float y = this.weaponoffsetx;
				Vector3 localEulerAngles6 = this.phand.transform.localEulerAngles;
				float num16 = localEulerAngles6.y = y;
				Vector3 vector6 = this.phand.transform.localEulerAngles = localEulerAngles6;
				float x = this.weaponoffsety;
				Vector3 localEulerAngles7 = this.phand.transform.localEulerAngles;
				float num17 = localEulerAngles7.x = x;
				Vector3 vector7 = this.phand.transform.localEulerAngles = localEulerAngles7;
				float y2 = this.weaponoffsetx * 1.5f;
				Vector3 localEulerAngles8 = this.flash.transform.localEulerAngles;
				float num18 = localEulerAngles8.y = y2;
				Vector3 vector8 = this.flash.transform.localEulerAngles = localEulerAngles8;
				float x2 = this.weaponoffsety * 1.5f;
				Vector3 localEulerAngles9 = this.flash.transform.localEulerAngles;
				float num19 = localEulerAngles9.x = x2;
				Vector3 vector9 = this.flash.transform.localEulerAngles = localEulerAngles9;
				this.weaponoffsetx /= Time.deltaTime * this.weaponresolvespeed + (float)1;
				if (Mathf.Abs(this.weaponoffsetx) < 0.001f)
				{
					this.weaponoffsetx = (float)0;
				}
				this.weaponoffsety /= Time.deltaTime * this.weaponresolvespeed + (float)1;
				if (Mathf.Abs(this.weaponoffsety) < 0.001f)
				{
					this.weaponoffsety = (float)0;
				}
			}
			if (((MyControllerScript)this.controller.GetComponent(typeof(MyControllerScript))).CheckGrounded() && !this.doroll && !this.cont.inwater)
			{
				if (this.xRotation < (float)-180)
				{
					this.xRotation += (float)360;
				}
				if (this.xRotation > (float)180)
				{
					this.xRotation -= (float)360;
				}
				this.xRotation = Mathf.Clamp(this.xRotation, (float)-89, (float)89);
			}
			if (this.select.havejetpack)
			{
				if (this.xRotation < (float)-180)
				{
					this.xRotation += (float)360;
				}
				if (this.xRotation > (float)180)
				{
					this.xRotation -= (float)360;
				}
				this.xRotation = Mathf.Clamp(this.xRotation, (float)-89, (float)89);
			}
			if (this.disablefreeaxis)
			{
				if (this.xRotation < (float)-180)
				{
					this.xRotation += (float)360;
				}
				if (this.xRotation > (float)180)
				{
					this.xRotation -= (float)360;
				}
				this.xRotation = Mathf.Clamp(this.xRotation, (float)-89, (float)89);
			}
			this.currentXRotation = Mathf.SmoothDamp(this.currentXRotation, this.xRotation, ref this.xRotationV, this.lookSmoothDamp);
			this.currentYRotation = Mathf.SmoothDamp(this.currentYRotation, this.yRotation, ref this.yRotationV, this.lookSmoothDamp);
			if (this.disablebuck)
			{
				this.buck = (float)0;
				this.horbuck = (float)0;
			}
			if (!this.cont.SixDegreesOfFreedomSwimming || !this.cont.inwater)
			{
				this.transform.localEulerAngles = new Vector3(this.currentXRotation + this.buck + this.flipamount, (float)0, this.currenttilt + this.zrotation);
				this.controller.transform.localEulerAngles = new Vector3((float)0, this.currentYRotation + this.horbuck, (float)0);
				if (this.actuallyresettingz)
				{
					if (this.zrotation > (float)180)
					{
						this.zrotation += Time.deltaTime * (float)230;
						if (this.zrotation > (float)360)
						{
							this.zrotation = (float)0;
							this.resetz = false;
							this.actuallyresettingz = false;
						}
					}
					else
					{
						this.zrotation -= Time.deltaTime * (float)230;
						if (this.zrotation < (float)0)
						{
							this.zrotation = (float)0;
							this.resetz = false;
							this.actuallyresettingz = false;
						}
					}
				}
			}
			else
			{
				this.transform.Rotate(-num2 * this.lookSensitivity, num * this.lookSensitivity, (float)0, Space.Self);
				if (this.transform.localEulerAngles.z > (float)360)
				{
					float z3 = this.transform.localEulerAngles.z - (float)360;
					Vector3 localEulerAngles10 = this.transform.localEulerAngles;
					float num20 = localEulerAngles10.z = z3;
					Vector3 vector10 = this.transform.localEulerAngles = localEulerAngles10;
				}
				if (this.transform.localEulerAngles.z < (float)0)
				{
					float z4 = this.transform.localEulerAngles.z + (float)360;
					Vector3 localEulerAngles11 = this.transform.localEulerAngles;
					float num21 = localEulerAngles11.z = z4;
					Vector3 vector11 = this.transform.localEulerAngles = localEulerAngles11;
				}
				this.zrotation = this.transform.localEulerAngles.z;
				this.yRotation = this.transform.eulerAngles.y;
				this.xRotation = this.transform.eulerAngles.x;
				this.resetz = true;
			}
			this.buck /= (float)1 + Time.deltaTime * this.buckreset;
			if (Mathf.Abs(this.buck) < 0.01f)
			{
				this.buck = (float)0;
			}
			this.horbuck /= (float)1 + Time.deltaTime * this.buckreset;
			if (Mathf.Abs(this.horbuck) < 0.01f)
			{
				this.horbuck = (float)0;
			}
		}
	}

	// Token: 0x06000338 RID: 824 RVA: 0x0001EF8C File Offset: 0x0001D18C
	public virtual void dotilt()
	{
		if (!this.disablebuck)
		{
			if (this.currenttilt < this.tiltamount)
			{
				this.currenttilt += Time.deltaTime * this.tiltspeed;
			}
			if (this.currenttilt > this.tiltamount)
			{
				this.currenttilt -= Time.deltaTime * this.tiltspeed;
			}
			if (Mathf.Abs(this.currenttilt) > this.maxtilt)
			{
				this.currenttilt = this.maxtilt * Mathf.Sign(this.currenttilt);
			}
			if (this.inputmanager.GetKeyInput("right", 0))
			{
				this.tiltamount = (float)-1 * this.maxtilt;
			}
			if (this.inputmanager.GetKeyInput("left", 0))
			{
				this.tiltamount = (float)1 * this.maxtilt;
			}
			this.tiltamount /= (float)1 + Time.deltaTime * this.tiltresolvespeed;
			if (Mathf.Abs(this.tiltamount) < 0.01f)
			{
				this.tiltamount = (float)0;
			}
		}
		else
		{
			this.currenttilt = (float)0;
		}
	}

	// Token: 0x06000339 RID: 825 RVA: 0x0001F0B8 File Offset: 0x0001D2B8
	public virtual void Main()
	{
	}

	// Token: 0x040003C5 RID: 965
	public LayerMask locklayers;

	// Token: 0x040003C6 RID: 966
	public float lockonamount;

	// Token: 0x040003C7 RID: 967
	public float lockondist;

	// Token: 0x040003C8 RID: 968
	public float lookSensitivity;

	// Token: 0x040003C9 RID: 969
	[HideInInspector]
	public bool disablefreeaxis;

	// Token: 0x040003CA RID: 970
	[HideInInspector]
	public float yRotation;

	// Token: 0x040003CB RID: 971
	[HideInInspector]
	public float xRotation;

	// Token: 0x040003CC RID: 972
	[HideInInspector]
	public float currentYRotation;

	// Token: 0x040003CD RID: 973
	[HideInInspector]
	public float currentXRotation;

	// Token: 0x040003CE RID: 974
	[HideInInspector]
	public float currentYRotation2;

	// Token: 0x040003CF RID: 975
	[HideInInspector]
	public float yRotationV;

	// Token: 0x040003D0 RID: 976
	[HideInInspector]
	public float xRotationV;

	// Token: 0x040003D1 RID: 977
	[HideInInspector]
	public float zrotation;

	// Token: 0x040003D2 RID: 978
	[HideInInspector]
	public bool resetz;

	// Token: 0x040003D3 RID: 979
	[HideInInspector]
	public bool actuallyresettingz;

	// Token: 0x040003D4 RID: 980
	[HideInInspector]
	public float controllerYstorage;

	// Token: 0x040003D5 RID: 981
	public float tiltresolvespeed;

	// Token: 0x040003D6 RID: 982
	public float tiltspeed;

	// Token: 0x040003D7 RID: 983
	public float maxtilt;

	// Token: 0x040003D8 RID: 984
	[HideInInspector]
	public float tiltamount;

	// Token: 0x040003D9 RID: 985
	[HideInInspector]
	public float currenttilt;

	// Token: 0x040003DA RID: 986
	[HideInInspector]
	public float weaponoffsetx;

	// Token: 0x040003DB RID: 987
	[HideInInspector]
	public float weaponoffsety;

	// Token: 0x040003DC RID: 988
	[HideInInspector]
	public GameObject phand;

	// Token: 0x040003DD RID: 989
	[HideInInspector]
	public GameObject flash;

	// Token: 0x040003DE RID: 990
	public float weaponresolvespeed;

	// Token: 0x040003DF RID: 991
	public float maxweaponoffset;

	// Token: 0x040003E0 RID: 992
	public float weaponsensitivity;

	// Token: 0x040003E1 RID: 993
	public float buck;

	// Token: 0x040003E2 RID: 994
	public float horbuck;

	// Token: 0x040003E3 RID: 995
	public float buckreset;

	// Token: 0x040003E4 RID: 996
	public float lookSmoothDamp;

	// Token: 0x040003E5 RID: 997
	public bool lockfreelook;

	// Token: 0x040003E6 RID: 998
	public GameObject controller;

	// Token: 0x040003E7 RID: 999
	[HideInInspector]
	public float flipamount;

	// Token: 0x040003E8 RID: 1000
	[HideInInspector]
	public bool doflip;

	// Token: 0x040003E9 RID: 1001
	[HideInInspector]
	public bool stayupsidedown;

	// Token: 0x040003EA RID: 1002
	[HideInInspector]
	public Quaternion orientation;

	// Token: 0x040003EB RID: 1003
	[HideInInspector]
	public bool disablebuck;

	// Token: 0x040003EC RID: 1004
	[HideInInspector]
	public bool dobob;

	// Token: 0x040003ED RID: 1005
	public bool inverted;

	// Token: 0x040003EE RID: 1006
	[HideInInspector]
	public MyControllerScript cont;

	// Token: 0x040003EF RID: 1007
	[HideInInspector]
	public float checkgroundedstate;

	// Token: 0x040003F0 RID: 1008
	[HideInInspector]
	public bool doroll;

	// Token: 0x040003F1 RID: 1009
	[HideInInspector]
	public bool addvelocity;

	// Token: 0x040003F2 RID: 1010
	[HideInInspector]
	public float mousemomentum;

	// Token: 0x040003F3 RID: 1011
	[HideInInspector]
	public float lastxdirection;

	// Token: 0x040003F4 RID: 1012
	[HideInInspector]
	public float drunkincreasex;

	// Token: 0x040003F5 RID: 1013
	[HideInInspector]
	public float drunkincreasey;

	// Token: 0x040003F6 RID: 1014
	[HideInInspector]
	public float drunktimer;

	// Token: 0x040003F7 RID: 1015
	[HideInInspector]
	public PlayerHealthManagement hel;

	// Token: 0x040003F8 RID: 1016
	[HideInInspector]
	public MyInputManager inputmanager;

	// Token: 0x040003F9 RID: 1017
	[HideInInspector]
	public SelectionScript select;

	// Token: 0x040003FA RID: 1018
	[HideInInspector]
	public float turnaccel;

	// Token: 0x040003FB RID: 1019
	[HideInInspector]
	public float spawnlocktimer;

	// Token: 0x040003FC RID: 1020
	[HideInInspector]
	public bool actuallyexecutingroll;
}
