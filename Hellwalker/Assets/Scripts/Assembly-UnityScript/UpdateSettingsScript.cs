using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000C7 RID: 199
[Serializable]
public class UpdateSettingsScript : MonoBehaviour
{
	// Token: 0x0600049E RID: 1182 RVA: 0x0002A554 File Offset: 0x00028754
	public virtual void Start()
	{
	}

	// Token: 0x0600049F RID: 1183 RVA: 0x0002A558 File Offset: 0x00028758
	public virtual void Update()
	{
		PersistScript persistScript = (PersistScript)GameObject.Find("PERSIST").GetComponent(typeof(PersistScript));
		((Text)GameObject.Find("PixelToggle").GetComponent(typeof(Text))).text = "Low-Res Effect: " + persistScript.pixelfilter;
		((Text)GameObject.Find("ColorToggle").GetComponent(typeof(Text))).text = "Color Filter: " + persistScript.colorfilter;
		((Text)GameObject.Find("BilinearFilterToggle").GetComponent(typeof(Text))).text = "Bilinear Filtering: " + persistScript.BFilter;
		((Text)GameObject.Find("BloomToggle").GetComponent(typeof(Text))).text = "Light Bloom: " + persistScript.bloom;
		((Text)GameObject.Find("FlareToggle").GetComponent(typeof(Text))).text = "Light Flares: " + persistScript.flares;
		((Text)GameObject.Find("BuckToggle").GetComponent(typeof(Text))).text = "Camera Animations: " + !persistScript.disablebuck;
		((Text)GameObject.Find("BobToggle").GetComponent(typeof(Text))).text = "Weapon Sway: " + persistScript.weaponbob;
		((Text)GameObject.Find("RunToggle").GetComponent(typeof(Text))).text = "Always Run: " + persistScript.alwaysrun;
	}

	// Token: 0x060004A0 RID: 1184 RVA: 0x0002A748 File Offset: 0x00028948
	public virtual void Main()
	{
	}
}
