using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Boo.Lang;
using UnityEngine;

// Token: 0x02000069 RID: 105
[Serializable]
public class LoadingBarScript : MonoBehaviour
{
	// Token: 0x060002A5 RID: 677 RVA: 0x0001927C File Offset: 0x0001747C
	public virtual void Start()
	{
		this.persist = (PersistScript)GameObject.Find("PERSIST").GetComponent(typeof(PersistScript));
		if (this.persist)
		{
			//this.StartCoroutine(this.LoadAsync());
		}
	}

	// Token: 0x060002A6 RID: 678 RVA: 0x000192CC File Offset: 0x000174CC
	public virtual void Update()
	{
		this.timer += Time.deltaTime;
	}

	// Token: 0x060002A7 RID: 679 RVA: 0x000192E0 File Offset: 0x000174E0
	//public virtual IEnumerator LoadAsync()
	//{
	//	return new LoadingBarScript.$LoadAsync$451(this).GetEnumerator();
	//}

	// Token: 0x060002A8 RID: 680 RVA: 0x000192F0 File Offset: 0x000174F0
	public virtual void Main()
	{
	}

	// Token: 0x04000318 RID: 792
	[HideInInspector]
	public PersistScript persist;

	// Token: 0x04000319 RID: 793
	[HideInInspector]
	public float timer;

	// Token: 0x0200006A RID: 106
	//[CompilerGenerated]
	//[Serializable]
	//internal sealed class $LoadAsync$451 : GenericGenerator<object>
	//{
	//	// Token: 0x060002A9 RID: 681 RVA: 0x000192F4 File Offset: 0x000174F4
	//	public $LoadAsync$451(LoadingBarScript self_)
	//	{
	//		this.$self_$455 = self_;
	//	}

	//	// Token: 0x060002AA RID: 682 RVA: 0x00019304 File Offset: 0x00017504
	//	public override IEnumerator<object> GetEnumerator()
	//	{
	//		return new LoadingBarScript.$LoadAsync$451.$(this.$self_$455);
	//	}

	//	// Token: 0x0400031A RID: 794
	//	internal LoadingBarScript $self_$455;
	//}
}
