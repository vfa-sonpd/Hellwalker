using System;
using UnityEngine;

// Token: 0x0200007C RID: 124
[Serializable]
public class MusicNodeGHOSTScript : MonoBehaviour
{
	// Token: 0x06000307 RID: 775 RVA: 0x0001AE4C File Offset: 0x0001904C
	public virtual void Start()
	{
	}

	// Token: 0x06000308 RID: 776 RVA: 0x0001AE50 File Offset: 0x00019050
	public virtual void Update()
	{
	}

	// Token: 0x06000309 RID: 777 RVA: 0x0001AE54 File Offset: 0x00019054
	public virtual void OnTriggerEnter(Collider hit)
	{
		if (hit.transform.gameObject.name == "Player" && this.mynode != null)
		{
			((MusicNodeScript)this.mynode.GetComponent(typeof(MusicNodeScript))).trackstuff();
		}
	}

	// Token: 0x0600030A RID: 778 RVA: 0x0001AEB0 File Offset: 0x000190B0
	public virtual void Main()
	{
	}

	// Token: 0x04000361 RID: 865
	public GameObject mynode;
}
