using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000090 RID: 144
[Serializable]
public class PlayerSaveScript : MonoBehaviour
{
	// Token: 0x0600038F RID: 911 RVA: 0x000224B4 File Offset: 0x000206B4
	public virtual void Start()
	{
		this.sav = (SaveManagerScript)GameObject.Find("SaveManager").GetComponent(typeof(SaveManagerScript));
		this.origcoors = this.transform.position;
	}

	// Token: 0x06000390 RID: 912 RVA: 0x000224F8 File Offset: 0x000206F8
	public virtual void Update()
	{
		string rhs = string.Empty;
		if (this.sav.dosave || this.sav.doload)
		{
			rhs = "?tag=" + this.transform.gameObject.name + this.origcoors.ToString();
		}
		SelectionScript selectionScript = null;
		PersistScript persistScript = null;
		if (this.sav.dosave || this.sav.doload)
		{
			selectionScript = (SelectionScript)GameObject.Find("WeaponAnimator").GetComponent(typeof(SelectionScript));
			persistScript = (PersistScript)GameObject.Find("PERSIST").GetComponent(typeof(PersistScript));
		}
		if (this.sav.dosave)
		{
			ES2.Save<Transform>(this.transform, this.sav.filename + rhs + "tr4n5orm");
			ES2.Save<float>(((PlayerHealthManagement)this.GetComponent(typeof(PlayerHealthManagement))).myhealth, this.sav.filename + rhs + "h34Lth");
			ES2.Save<float>(((PlayerHealthManagement)this.GetComponent(typeof(PlayerHealthManagement))).myarmor, this.sav.filename + rhs + "4rmor");
			ES2.Save<float>(((MyMouseLook)GameObject.Find("MainCamera").GetComponent(typeof(MyMouseLook))).xRotation, this.sav.filename + rhs + "m0u2eX");
			ES2.Save<float>(((MyMouseLook)GameObject.Find("MainCamera").GetComponent(typeof(MyMouseLook))).yRotation, this.sav.filename + rhs + "m0u2eY");
			ES2.Save<int>(selectionScript.selectedweapon, this.sav.filename + rhs + "s3l3ct3dw3p0n");
			ES2.Save<int>(this.sav.boolToInt(selectionScript.permduals), this.sav.filename + rhs + "du4lpist0ls");
			ES2.Save<int>(this.sav.boolToInt(selectionScript.permshotguns), this.sav.filename + rhs + "du4lsh0tgun2");
			ES2.Save<int>(this.sav.boolToInt(selectionScript.permdaikatana), this.sav.filename + rhs + "d4ik4t4n4");
			ES2.Save<int>(this.sav.boolToInt(selectionScript.haveredkey), this.sav.filename + rhs + "r3dk3y");
			ES2.Save<int>(this.sav.boolToInt(selectionScript.havebluekey), this.sav.filename + rhs + "blu3k3y");
			ES2.Save<int>(this.sav.boolToInt(selectionScript.haveyellowkey), this.sav.filename + rhs + "y3ll0wk3y");
			ES2.Save<float>(((ScreenSizeScript)this.GetComponent(typeof(ScreenSizeScript))).currentsize, this.sav.filename + rhs + "scr33ns1z3");
			ES2.Save<int>(((ScreenSizeScript)this.GetComponent(typeof(ScreenSizeScript))).huddetail, this.sav.filename + rhs + "hudd3t41l");
			ES2.Save<float>(((PlayerHealthManagement)this.GetComponent(typeof(PlayerHealthManagement))).drunkness, this.sav.filename + rhs + "bl4m31t0nt3halc0h0l");
			ES2.Save<float>(((PlayerHealthManagement)this.GetComponent(typeof(PlayerHealthManagement))).drunkentimer, this.sav.filename + rhs + "alc0h0lt13m");
			ES2.Save<int>(this.sav.boolToInt(persistScript.pacifistaward), this.sav.filename + rhs + "4w4rdpacifist");
			ES2.Save<int>(this.sav.boolToInt(persistScript.ninjaaward), this.sav.filename + rhs + "4w4rdninja");
			ES2.Save<int>(this.sav.boolToInt(persistScript.completionistaward), this.sav.filename + rhs + "4w4rdcompletionist");
			ES2.Save<int>(this.sav.boolToInt(persistScript.lowtechaward), this.sav.filename + rhs + "4w4rdlowtech");
			ES2.Save<float>(persistScript.totalhours, this.sav.filename + rhs + "hours");
			ES2.Save<float>(persistScript.totalminutes, this.sav.filename + rhs + "minutes");
			ES2.Save<float>(persistScript.totalseconds, this.sav.filename + rhs + "seconds");
			ES2.Save<int>(this.sav.boolToInt(((StatScript)GameObject.Find("StatObject").GetComponent(typeof(StatScript))).brokenflashlight), this.sav.filename + rhs + "nolight");
			ES2.Save<int>(this.sav.boolToInt(((SelectionScript)GameObject.Find("WeaponAnimator").GetComponent(typeof(SelectionScript))).ignorelava), this.sav.filename + rhs + "lavasuit");
			for (int i = 0; i < selectionScript.weaponinventory.Length; i++)
			{
				ES2.Save<int>(this.sav.boolToInt(selectionScript.weaponinventory[i]), this.sav.filename + rhs + "w3p0n" + i.ToString());
				ES2.Save<float>(selectionScript.ammoinventory[i], this.sav.filename + rhs + "amm0" + i.ToString());
			}
		}
		if (this.sav.doload)
		{
			ES2.Load<Transform>(this.sav.filename + rhs + "tr4n5orm", this.transform);
			((PlayerHealthManagement)this.GetComponent(typeof(PlayerHealthManagement))).myhealth = ES2.Load<float>(this.sav.filename + rhs + "h34Lth");
			((PlayerHealthManagement)this.GetComponent(typeof(PlayerHealthManagement))).myarmor = ES2.Load<float>(this.sav.filename + rhs + "4rmor");
			((MyMouseLook)GameObject.Find("MainCamera").GetComponent(typeof(MyMouseLook))).xRotation = ES2.Load<float>(this.sav.filename + rhs + "m0u2eX");
			((MyMouseLook)GameObject.Find("MainCamera").GetComponent(typeof(MyMouseLook))).yRotation = ES2.Load<float>(this.sav.filename + rhs + "m0u2eY");
			selectionScript.selectedweapon = ES2.Load<int>(this.sav.filename + rhs + "s3l3ct3dw3p0n");
			selectionScript.weapontogetto = ES2.Load<int>(this.sav.filename + rhs + "s3l3ct3dw3p0n");
			selectionScript.permduals = this.sav.intToBool(ES2.Load<int>(this.sav.filename + rhs + "du4lpist0ls"));
			selectionScript.havedualpistols = this.sav.intToBool(ES2.Load<int>(this.sav.filename + rhs + "du4lpist0ls"));
			selectionScript.permshotguns = this.sav.intToBool(ES2.Load<int>(this.sav.filename + rhs + "du4lsh0tgun2"));
			selectionScript.havedualshotguns = this.sav.intToBool(ES2.Load<int>(this.sav.filename + rhs + "du4lsh0tgun2"));
			selectionScript.permdaikatana = this.sav.intToBool(ES2.Load<int>(this.sav.filename + rhs + "d4ik4t4n4"));
			selectionScript.havedaikatana = this.sav.intToBool(ES2.Load<int>(this.sav.filename + rhs + "d4ik4t4n4"));
			selectionScript.haveredkey = this.sav.intToBool(ES2.Load<int>(this.sav.filename + rhs + "r3dk3y"));
			int num = ES2.Load<int>(this.sav.filename + rhs + "r3dk3y");
			Color color = ((Image)GameObject.Find("RedIndicator").GetComponent(typeof(Image))).color;
			float num2 = color.a = (float)num;
			Color color2 = ((Image)GameObject.Find("RedIndicator").GetComponent(typeof(Image))).color = color;
			selectionScript.havebluekey = this.sav.intToBool(ES2.Load<int>(this.sav.filename + rhs + "blu3k3y"));
			int num3 = ES2.Load<int>(this.sav.filename + rhs + "blu3k3y");
			Color color3 = ((Image)GameObject.Find("BlueIndicator").GetComponent(typeof(Image))).color;
			float num4 = color3.a = (float)num3;
			Color color4 = ((Image)GameObject.Find("BlueIndicator").GetComponent(typeof(Image))).color = color3;
			selectionScript.haveyellowkey = this.sav.intToBool(ES2.Load<int>(this.sav.filename + rhs + "y3ll0wk3y"));
			int num5 = ES2.Load<int>(this.sav.filename + rhs + "y3ll0wk3y");
			Color color5 = ((Image)GameObject.Find("YellowIndicator").GetComponent(typeof(Image))).color;
			float num6 = color5.a = (float)num5;
			Color color6 = ((Image)GameObject.Find("YellowIndicator").GetComponent(typeof(Image))).color = color5;
			((ScreenSizeScript)this.GetComponent(typeof(ScreenSizeScript))).currentsize = ES2.Load<float>(this.sav.filename + rhs + "scr33ns1z3");
			((ScreenSizeScript)this.GetComponent(typeof(ScreenSizeScript))).huddetail = ES2.Load<int>(this.sav.filename + rhs + "hudd3t41l");
			((ScreenSizeScript)this.GetComponent(typeof(ScreenSizeScript))).sethudstuff();
			((PlayerHealthManagement)this.GetComponent(typeof(PlayerHealthManagement))).drunkness = ES2.Load<float>(this.sav.filename + rhs + "bl4m31t0nt3halc0h0l");
			((PlayerHealthManagement)this.GetComponent(typeof(PlayerHealthManagement))).drunkentimer = ES2.Load<float>(this.sav.filename + rhs + "alc0h0lt13m");
			persistScript.pacifistaward = this.sav.intToBool(ES2.Load<int>(this.sav.filename + rhs + "4w4rdpacifist"));
			persistScript.ninjaaward = this.sav.intToBool(ES2.Load<int>(this.sav.filename + rhs + "4w4rdninja"));
			persistScript.completionistaward = this.sav.intToBool(ES2.Load<int>(this.sav.filename + rhs + "4w4rdcompletionist"));
			persistScript.lowtechaward = this.sav.intToBool(ES2.Load<int>(this.sav.filename + rhs + "4w4rdlowtech"));
			persistScript.totalhours = ES2.Load<float>(this.sav.filename + rhs + "hours");
			persistScript.totalminutes = ES2.Load<float>(this.sav.filename + rhs + "minutes");
			persistScript.totalseconds = ES2.Load<float>(this.sav.filename + rhs + "seconds");
			((StatScript)GameObject.Find("StatObject").GetComponent(typeof(StatScript))).brokenflashlight = this.sav.intToBool(ES2.Load<int>(this.sav.filename + rhs + "nolight"));
			((SelectionScript)GameObject.Find("WeaponAnimator").GetComponent(typeof(SelectionScript))).ignorelava = this.sav.intToBool(ES2.Load<int>(this.sav.filename + rhs + "lavasuit"));
			for (int i = 0; i < selectionScript.weaponinventory.Length; i++)
			{
				selectionScript.weaponinventory[i] = this.sav.intToBool(ES2.Load<int>(this.sav.filename + rhs + "w3p0n" + i.ToString()));
				selectionScript.ammoinventory[i] = ES2.Load<float>(this.sav.filename + rhs + "amm0" + i.ToString());
			}
		}
	}

	// Token: 0x06000391 RID: 913 RVA: 0x000233B0 File Offset: 0x000215B0
	public virtual void Main()
	{
	}

	// Token: 0x04000474 RID: 1140
	public SaveManagerScript sav;

	// Token: 0x04000475 RID: 1141
	public Vector3 origcoors;
}
