using System;
using UnityEngine;

// Token: 0x02000085 RID: 133
[Serializable]
public class ObjectButtonActivation : MonoBehaviour
{
	// Token: 0x06000349 RID: 841 RVA: 0x0001F5C0 File Offset: 0x0001D7C0
	public virtual void Start()
	{
	}

	// Token: 0x0600034A RID: 842 RVA: 0x0001F5C4 File Offset: 0x0001D7C4
	public virtual void Update()
	{
	}

	// Token: 0x0600034B RID: 843 RVA: 0x0001F5C8 File Offset: 0x0001D7C8
	public virtual void OnTriggerEnter(Collider hit)
	{
		if (this.onenter && this.checkforobjects(hit))
		{
			((ButtonScript)this.GetComponent(typeof(ButtonScript))).buttonstate = true;
		}
	}

	// Token: 0x0600034C RID: 844 RVA: 0x0001F608 File Offset: 0x0001D808
	public virtual void OnTriggerExit(Collider hit)
	{
	}

	// Token: 0x0600034D RID: 845 RVA: 0x0001F60C File Offset: 0x0001D80C
	public virtual bool checkforobjects(Collider hit)
	{
		for (int i = 0; i < this.objects.Length; i++)
		{
			if (hit.transform.gameObject == this.objects[i])
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600034E RID: 846 RVA: 0x0001F65C File Offset: 0x0001D85C
	public virtual void Main()
	{
	}

	// Token: 0x0400040D RID: 1037
	public GameObject[] objects;

	// Token: 0x0400040E RID: 1038
	public bool onenter;

	// Token: 0x0400040F RID: 1039
	public bool onexit;
}
