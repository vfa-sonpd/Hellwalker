using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000045 RID: 69
[Serializable]
public class FakeDosLoading : MonoBehaviour
{
	// Token: 0x06000193 RID: 403 RVA: 0x000100A0 File Offset: 0x0000E2A0
	public virtual void Start()
	{
		this.i = 0;
		this.tmr = (float)0;
		Text text = (Text)this.GetComponent(typeof(Text));
		text.text += this.zatext[0];
	}

	// Token: 0x06000194 RID: 404 RVA: 0x000100EC File Offset: 0x0000E2EC
	public virtual void Update()
	{
		this.tmr += Time.deltaTime;
		Text text = null;
		if (this.tmr > this.zatimes[this.i])
		{
			this.i++;
			this.tmr = (float)0;
			text = (Text)this.GetComponent(typeof(Text));
			if (this.i < this.zatext.Length)
			{
				text.text += "\n" + this.zatext[this.i];
			}
		}
		if (this.i >= this.zatext.Length)
		{
			text.text = "loading tunes (this may take awhile)..." + "\n" + "HEADPHONES RECOMMENDED";
			Application.LoadLevel("firstscene");
		}
	}

	// Token: 0x06000195 RID: 405 RVA: 0x000101CC File Offset: 0x0000E3CC
	public virtual void Main()
	{
	}

	// Token: 0x04000260 RID: 608
	public string[] zatext;

	// Token: 0x04000261 RID: 609
	public float[] zatimes;

	// Token: 0x04000262 RID: 610
	[HideInInspector]
	public int i;

	// Token: 0x04000263 RID: 611
	[HideInInspector]
	public float tmr;
}
