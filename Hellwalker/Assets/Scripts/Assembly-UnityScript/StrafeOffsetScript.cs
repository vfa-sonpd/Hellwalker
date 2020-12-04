using System;
using UnityEngine;

// Token: 0x020000B4 RID: 180
[Serializable]
public class StrafeOffsetScript : MonoBehaviour
{
	// Token: 0x06000441 RID: 1089 RVA: 0x00028F98 File Offset: 0x00027198
	public virtual void Start()
	{
		this.inputmanager = Essential.Instance.inputManager;
        this.mscript = (MyMouseLook)GameObject.Find("MainCamera").GetComponent(typeof(MyMouseLook));
	}

	// Token: 0x06000442 RID: 1090 RVA: 0x00028FF0 File Offset: 0x000271F0
	public virtual void Update()
	{
		if (this.mscript.dobob)
		{
			this.strafeoffsetgoal /= Time.deltaTime * this.changespeed + (float)1;
			if (this.inputmanager.GetKeyInput("zoom", 0))
			{
				this.strafeoffsetgoal = (float)0;
			}
			if (this.currentstrafeoffset > this.strafeoffsetgoal)
			{
				this.currentstrafeoffset -= Time.deltaTime * this.changespeed;
			}
			if (this.currentstrafeoffset < this.strafeoffsetgoal)
			{
				this.currentstrafeoffset += Time.deltaTime * this.changespeed;
			}
			if (this.inputmanager.GetKeyInput("right", 0))
			{
				this.strafeoffsetgoal = (float)-1 * this.amount;
			}
			else if (this.inputmanager.GetKeyInput("left", 0))
			{
				this.strafeoffsetgoal = (float)1 * this.amount;
			}
			else
			{
				this.strafeoffsetgoal = (float)0;
			}
			float x = this.currentstrafeoffset;
			Vector3 localPosition = this.transform.localPosition;
			float num = localPosition.x = x;
			Vector3 vector = this.transform.localPosition = localPosition;
		}
	}

	// Token: 0x06000443 RID: 1091 RVA: 0x0002912C File Offset: 0x0002732C
	public virtual void Main()
	{
	}

	// Token: 0x0400054A RID: 1354
	public float changespeed;

	// Token: 0x0400054B RID: 1355
	public float amount;

	// Token: 0x0400054C RID: 1356
	[HideInInspector]
	public float strafeoffsetgoal;

	// Token: 0x0400054D RID: 1357
	[HideInInspector]
	public float currentstrafeoffset;

	// Token: 0x0400054E RID: 1358
	[HideInInspector]
	public MyMouseLook mscript;

	// Token: 0x0400054F RID: 1359
	[HideInInspector]
	public MyInputManager inputmanager;
}
