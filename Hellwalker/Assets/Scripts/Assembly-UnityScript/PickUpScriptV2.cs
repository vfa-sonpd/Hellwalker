using System;
using TMPro;
using UnityEngine;

// Token: 0x02000087 RID: 135
[Serializable]
public class PickUpScriptV2 : MonoBehaviour
{
	// Token: 0x06000356 RID: 854 RVA: 0x0001F888 File Offset: 0x0001DA88
	public PickUpScriptV2()
	{
		this.drag = (float)1;
	}

	// Token: 0x06000357 RID: 855 RVA: 0x0001F898 File Offset: 0x0001DA98
	public virtual void Start()
	{
		this.inputmanager = Essential.Instance.inputManager;
        this.wep = GameObject.Find("WeaponAnimator");
		this.plr = GameObject.Find("Player");
		this.select = (SelectionScript)this.wep.GetComponent(typeof(SelectionScript));
		this.namelabel = (TextMeshProUGUI)GameObject.Find("NameLabelText").GetComponent(typeof(TextMeshProUGUI));
		this.namelabelshadow = (TextMeshProUGUI)GameObject.Find("NameLabelShadow").GetComponent(typeof(TextMeshProUGUI));
	}

	// Token: 0x06000358 RID: 856 RVA: 0x0001F954 File Offset: 0x0001DB54
	public virtual void Update()
	{
		if (this.inputmanager.GetKeyInput("fire", 0) && this.obihave != null)
		{
			this.dropobject(true);
		}
		if (this.inputmanager.GetKeyInput("use", 1))
		{
			if (this.obihave == null)
			{
				this.GetObject();
			}
			else
			{
				this.dropobject(false);
			}
		}
	}

	// Token: 0x06000359 RID: 857 RVA: 0x0001F9C8 File Offset: 0x0001DBC8
	public virtual void FixedUpdate()
	{
		bool flag = default(bool);
		flag = (this.obihave != null);
		if (!flag && this.laststate)
		{
			this.select.weapontogetto = this.holsteredweapon;
		}
		this.laststate = flag;
		if (this.obihave != null)
		{
			this.carryit();
			int num = 0;
			Color color = this.namelabel.color;
			float num2 = color.a = (float)num;
			Color color2 = this.namelabel.color = color;
			int num3 = 0;
			Color color3 = this.namelabelshadow.color;
			float num4 = color3.a = (float)num3;
			Color color4 = this.namelabelshadow.color = color3;
		}
		else
		{
			float a = 0.8f;
			Color color5 = this.namelabel.color;
			float num5 = color5.a = a;
			Color color6 = this.namelabel.color = color5;
			float a2 = 0.8f;
			Color color7 = this.namelabelshadow.color;
			float num6 = color7.a = a2;
			Color color8 = this.namelabelshadow.color = color7;
		}
	}

	// Token: 0x0600035A RID: 858 RVA: 0x0001FB1C File Offset: 0x0001DD1C
	public virtual void carryit()
	{
		Vector3 a = default(Vector3);
		a = this.transform.position + this.transform.forward * this.carrydistance;
		Rigidbody rigidbody = (Rigidbody)this.obihave.GetComponent(typeof(Rigidbody));
		rigidbody.useGravity = false;
		rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
		Vector3 vector = default(Vector3);
		vector = a - this.obihave.transform.position;
		vector = Vector3.Normalize(vector);
		float num = 0f;
		num = Vector3.Distance(a, this.obihave.transform.position);
		vector = vector * num / (Time.fixedDeltaTime * this.drag);
		rigidbody.velocity = vector;
		if (num > this.maxdist)
		{
			this.dropobject(false);
		}
	}

	// Token: 0x0600035B RID: 859 RVA: 0x0001FBFC File Offset: 0x0001DDFC
	public virtual void dropobject(bool th)
	{
		if (this.obihave != null)
		{
			if ((DestructibleObjectScript)this.obihave.GetComponent(typeof(DestructibleObjectScript)))
			{
				((DestructibleObjectScript)this.obihave.GetComponent(typeof(DestructibleObjectScript))).candodamage = true;
			}
			Rigidbody rigidbody = (Rigidbody)this.obihave.GetComponent(typeof(Rigidbody));
			if ((AudioSource)this.obihave.GetComponent(typeof(AudioSource)))
			{
				((AudioSource)this.obihave.GetComponent(typeof(AudioSource))).volume = (float)1;
			}
			this.obihave.layer = (int)this.origlayer;
			this.obihave = null;
			rigidbody.useGravity = true;
			rigidbody.constraints = RigidbodyConstraints.None;
			rigidbody.velocity = this.transform.forward;
			this.select.weapontogetto = this.holsteredweapon;
			this.select.selectedweapon = this.holsteredweapon;
			this.laststate = false;
			if (th)
			{
				rigidbody.velocity *= this.throwvelocity;
				((AttackScript)this.wep.GetComponent(typeof(AttackScript))).pauseafterthrowing = true;
				((AudioSource)this.throwsound.GetComponent(typeof(AudioSource))).Play();
			}
			else
			{
				rigidbody.velocity = ((MyControllerScript)this.plr.GetComponent(typeof(MyControllerScript))).InputDir * (float)40;
			}
		}
	}

	// Token: 0x0600035C RID: 860 RVA: 0x0001FDB0 File Offset: 0x0001DFB0
	public virtual void GetObject()
	{
		RaycastHit raycastHit = default(RaycastHit);
		if (Physics.Raycast(this.transform.position, this.transform.forward, out raycastHit, this.grabrange, this.layerstograb) && raycastHit.transform.gameObject.tag == "PickUpObject")
		{
			this.obihave = raycastHit.transform.gameObject;
			this.origlayer = (float)this.obihave.layer;
			this.obihave.layer = 11;
			if ((AudioSource)this.obihave.GetComponent(typeof(AudioSource)))
			{
				((AudioSource)this.obihave.GetComponent(typeof(AudioSource))).volume = (float)0;
			}
			this.holsteredweapon = this.select.selectedweapon;
			this.select.weapontogetto = 0;
		}
	}

	// Token: 0x0600035D RID: 861 RVA: 0x0001FEAC File Offset: 0x0001E0AC
	public virtual void Main()
	{
	}

	// Token: 0x04000440 RID: 1088
	public GameObject throwsound;

	// Token: 0x04000441 RID: 1089
	public LayerMask layerstograb;

	// Token: 0x04000442 RID: 1090
	public float throwvelocity;

	// Token: 0x04000443 RID: 1091
	public float grabrange;

	// Token: 0x04000444 RID: 1092
	public GameObject obihave;

	// Token: 0x04000445 RID: 1093
	public float carrydistance;

	// Token: 0x04000446 RID: 1094
	public float drag;

	// Token: 0x04000447 RID: 1095
	public float maxdist;

	// Token: 0x04000448 RID: 1096
	[HideInInspector]
	public bool laststate;

	// Token: 0x04000449 RID: 1097
	[HideInInspector]
	public float origlayer;

	// Token: 0x0400044A RID: 1098
	public int holsteredweapon;

	// Token: 0x0400044B RID: 1099
	[HideInInspector]
	public MyInputManager inputmanager;

	// Token: 0x0400044C RID: 1100
	[HideInInspector]
	public SelectionScript select;

	// Token: 0x0400044D RID: 1101
	[HideInInspector]
	public GameObject wep;

	// Token: 0x0400044E RID: 1102
	[HideInInspector]
	public GameObject plr;

	// Token: 0x0400044F RID: 1103
	[HideInInspector]
	public TextMeshProUGUI namelabel;

	// Token: 0x04000450 RID: 1104
	[HideInInspector]
	public TextMeshProUGUI namelabelshadow;
}
