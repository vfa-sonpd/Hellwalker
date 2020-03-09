using System;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

// Token: 0x02000014 RID: 20
[Serializable]
public class BlurDecrease : MonoBehaviour
{
	// Token: 0x0600008F RID: 143 RVA: 0x0000ACC4 File Offset: 0x00008EC4
	public virtual void Start()
	{
	}

	// Token: 0x06000090 RID: 144 RVA: 0x0000ACC8 File Offset: 0x00008EC8
	public virtual void Update()
	{
		((MotionBlur)this.GetComponent(typeof(MotionBlur))).blurAmount = ((MotionBlur)this.GetComponent(typeof(MotionBlur))).blurAmount - Time.deltaTime * 2.5f;
	}

	// Token: 0x06000091 RID: 145 RVA: 0x0000AD18 File Offset: 0x00008F18
	public virtual void Main()
	{
	}
}
