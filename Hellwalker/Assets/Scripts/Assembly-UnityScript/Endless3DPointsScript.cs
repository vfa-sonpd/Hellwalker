using System;
using UnityEngine;

// Token: 0x02000039 RID: 57
[Serializable]
public class Endless3DPointsScript : MonoBehaviour
{
	// Token: 0x06000153 RID: 339 RVA: 0x0000E90C File Offset: 0x0000CB0C
	public virtual void Start()
	{
		this.plr = GameObject.Find("MainCamera");
		this.mytext = (TextMesh)this.GetComponent(typeof(TextMesh));
		float num = this.mypoints / this.maxpoints;
		if (num > (float)1)
		{
			num = (float)1;
		}
		this.mytext.color = this.grad.Evaluate(num);
	}

	// Token: 0x06000154 RID: 340 RVA: 0x0000E974 File Offset: 0x0000CB74
	public virtual void Update()
	{
		this.transform.LookAt(this.plr.transform);
		this.risespeed -= Time.deltaTime * this.risedecayspeed;
		if (this.risespeed < (float)0)
		{
			this.risespeed = (float)0;
		}
		float y = this.transform.position.y + Time.deltaTime * this.risespeed;
		Vector3 position = this.transform.position;
		float num = position.y = y;
		Vector3 vector = this.transform.position = position;
		float a = this.mytext.color.a - Time.deltaTime * this.fadespeed;
		Color color = this.mytext.color;
		float num2 = color.a = a;
		Color color2 = this.mytext.color = color;
		if (this.mytext.color.a < (float)0)
		{
			UnityEngine.Object.Destroy(this.transform.gameObject);
		}
	}

	// Token: 0x06000155 RID: 341 RVA: 0x0000EA94 File Offset: 0x0000CC94
	public virtual void Main()
	{
	}

	// Token: 0x04000221 RID: 545
	public float risespeed;

	// Token: 0x04000222 RID: 546
	public float risedecayspeed;

	// Token: 0x04000223 RID: 547
	public float fadespeed;

	// Token: 0x04000224 RID: 548
	public Gradient grad;

	// Token: 0x04000225 RID: 549
	public float maxpoints;

	// Token: 0x04000226 RID: 550
	[HideInInspector]
	public TextMesh mytext;

	// Token: 0x04000227 RID: 551
	[HideInInspector]
	public GameObject plr;

	// Token: 0x04000228 RID: 552
	[HideInInspector]
	public float mypoints;
}
