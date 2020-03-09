using System;
using UnityEngine;

// Token: 0x02000078 RID: 120
[Serializable]
public class MenuButtonsScript : MonoBehaviour
{
	// Token: 0x060002E8 RID: 744 RVA: 0x0001A8A4 File Offset: 0x00018AA4
	public virtual void Start()
	{
	}

	// Token: 0x060002E9 RID: 745 RVA: 0x0001A8A8 File Offset: 0x00018AA8
	public virtual void Update()
	{
	}

	// Token: 0x060002EA RID: 746 RVA: 0x0001A8AC File Offset: 0x00018AAC
	public virtual void continuepress()
	{
		((AudioSource)GameObject.Find("MenuSound").GetComponent(typeof(AudioSource))).Play();
		Application.LoadLevel("A1M2");
	}

	// Token: 0x060002EB RID: 747 RVA: 0x0001A8DC File Offset: 0x00018ADC
	public virtual void endlesspress()
	{
		((AudioSource)GameObject.Find("MenuSound").GetComponent(typeof(AudioSource))).Play();
		Application.LoadLevel("QuakeconEndlessMode");
	}

	// Token: 0x060002EC RID: 748 RVA: 0x0001A90C File Offset: 0x00018B0C
	public virtual void exitpress()
	{
		Application.Quit();
	}

	// Token: 0x060002ED RID: 749 RVA: 0x0001A914 File Offset: 0x00018B14
	public virtual void settingspress()
	{
		this.main.active = false;
		this.settings.active = true;
	}

	// Token: 0x060002EE RID: 750 RVA: 0x0001A930 File Offset: 0x00018B30
	public virtual void LowResPress()
	{
		PersistScript persistScript = (PersistScript)GameObject.Find("PERSIST").GetComponent(typeof(PersistScript));
		persistScript.pixelfilter = !persistScript.pixelfilter;
		PlayerPrefs.SetInt("pixelfilter", this.boolToInt(persistScript.pixelfilter));
	}

	// Token: 0x060002EF RID: 751 RVA: 0x0001A984 File Offset: 0x00018B84
	public virtual void ColorPress()
	{
		PersistScript persistScript = (PersistScript)GameObject.Find("PERSIST").GetComponent(typeof(PersistScript));
		persistScript.colorfilter = !persistScript.colorfilter;
		PlayerPrefs.SetInt("colorfilter", this.boolToInt(persistScript.colorfilter));
	}

	// Token: 0x060002F0 RID: 752 RVA: 0x0001A9D8 File Offset: 0x00018BD8
	public virtual void BilinearPress()
	{
		PersistScript persistScript = (PersistScript)GameObject.Find("PERSIST").GetComponent(typeof(PersistScript));
		persistScript.BFilter = !persistScript.BFilter;
		PlayerPrefs.SetInt("bfilter", this.boolToInt(persistScript.BFilter));
	}

	// Token: 0x060002F1 RID: 753 RVA: 0x0001AA2C File Offset: 0x00018C2C
	public virtual void BloomPress()
	{
		PersistScript persistScript = (PersistScript)GameObject.Find("PERSIST").GetComponent(typeof(PersistScript));
		persistScript.bloom = !persistScript.bloom;
		PlayerPrefs.SetInt("bloom", this.boolToInt(persistScript.bloom));
	}

	// Token: 0x060002F2 RID: 754 RVA: 0x0001AA80 File Offset: 0x00018C80
	public virtual void FlarePress()
	{
		PersistScript persistScript = (PersistScript)GameObject.Find("PERSIST").GetComponent(typeof(PersistScript));
		persistScript.flares = !persistScript.flares;
		PlayerPrefs.SetInt("flares", this.boolToInt(persistScript.flares));
	}

	// Token: 0x060002F3 RID: 755 RVA: 0x0001AAD4 File Offset: 0x00018CD4
	public virtual void BuckPress()
	{
		PersistScript persistScript = (PersistScript)GameObject.Find("PERSIST").GetComponent(typeof(PersistScript));
		persistScript.disablebuck = !persistScript.disablebuck;
		PlayerPrefs.SetInt("buck", this.boolToInt(persistScript.disablebuck));
	}

	// Token: 0x060002F4 RID: 756 RVA: 0x0001AB28 File Offset: 0x00018D28
	public virtual void BobPress()
	{
		PersistScript persistScript = (PersistScript)GameObject.Find("PERSIST").GetComponent(typeof(PersistScript));
		persistScript.weaponbob = !persistScript.weaponbob;
		PlayerPrefs.SetInt("bob", this.boolToInt(persistScript.weaponbob));
	}

	// Token: 0x060002F5 RID: 757 RVA: 0x0001AB7C File Offset: 0x00018D7C
	public virtual void RunPress()
	{
		PersistScript persistScript = (PersistScript)GameObject.Find("PERSIST").GetComponent(typeof(PersistScript));
		persistScript.alwaysrun = !persistScript.alwaysrun;
		PlayerPrefs.SetInt("alwaysrun", this.boolToInt(persistScript.alwaysrun));
	}

	// Token: 0x060002F6 RID: 758 RVA: 0x0001ABD0 File Offset: 0x00018DD0
	public virtual void backsettingspress()
	{
		this.main.active = true;
		this.title.active = true;
		this.settings.active = false;
	}

	// Token: 0x060002F7 RID: 759 RVA: 0x0001AC04 File Offset: 0x00018E04
	public virtual int boolToInt(bool b)
	{
		return (!b) ? 0 : 1;
	}

	// Token: 0x060002F8 RID: 760 RVA: 0x0001AC18 File Offset: 0x00018E18
	public virtual void Main()
	{
	}

	// Token: 0x04000358 RID: 856
	public GameObject main;

	// Token: 0x04000359 RID: 857
	public GameObject title;

	// Token: 0x0400035A RID: 858
	public GameObject settings;
}
