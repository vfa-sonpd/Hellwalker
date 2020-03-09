using System;
using System.Collections;
using Boo.Lang.Runtime;
using UnityEngine;
using UnityScript.Lang;

// Token: 0x02000074 RID: 116
[Serializable]
public class MainMenuEnableController : MonoBehaviour
{
	// Token: 0x060002D4 RID: 724 RVA: 0x0001A134 File Offset: 0x00018334
	public virtual void Start()
	{
		IEnumerator enumerator = UnityRuntimeServices.GetEnumerator(GameObject.Find("Canvas").transform);
		while (enumerator.MoveNext())
		{
			object obj = enumerator.Current;
			object obj3;
			object obj2 = obj3 = obj;
			if (!(obj2 is Transform))
			{
				obj3 = RuntimeServices.Coerce(obj2, typeof(Transform));
			}
			Transform transform = (Transform)obj3;
			transform.gameObject.active = false;
			UnityRuntimeServices.Update(enumerator, transform);
		}
		this.mainmenu.active = true;
		this.eventcontroller.active = true;
		this.blackimage.active = true;
	}

	// Token: 0x060002D5 RID: 725 RVA: 0x0001A1C4 File Offset: 0x000183C4
	public virtual void Update()
	{
	}

	// Token: 0x060002D6 RID: 726 RVA: 0x0001A1C8 File Offset: 0x000183C8
	public virtual void Main()
	{
	}

	// Token: 0x0400034B RID: 843
	public GameObject mainmenu;

	// Token: 0x0400034C RID: 844
	public GameObject eventcontroller;

	// Token: 0x0400034D RID: 845
	public GameObject blackimage;
}
