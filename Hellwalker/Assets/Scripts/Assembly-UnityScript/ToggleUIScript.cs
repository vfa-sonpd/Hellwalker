using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000BD RID: 189
[Serializable]
public class ToggleUIScript : MonoBehaviour
{
	// Token: 0x0600046A RID: 1130 RVA: 0x00029B9C File Offset: 0x00027D9C
	[HideInInspector]
	public virtual void Start()
	{
		this.currentlyvisible = true;
	}

	// Token: 0x0600046B RID: 1131 RVA: 0x00029BA8 File Offset: 0x00027DA8
	public virtual void Update()
	{
	}

	// Token: 0x0600046C RID: 1132 RVA: 0x00029BAC File Offset: 0x00027DAC
	public virtual void hidestuff(bool a)
	{
		for (int i = 0; i < this.objectstohide.Length; i++)
		{
			if (this.objectstohide[i] != null)
			{
				if ((Image)this.objectstohide[i].GetComponent(typeof(Image)))
				{
					((Image)this.objectstohide[i].GetComponent(typeof(Image))).enabled = a;
				}
				if ((TextMeshProUGUI)this.objectstohide[i].GetComponent(typeof(TextMeshProUGUI)))
				{
					((TextMeshProUGUI)this.objectstohide[i].GetComponent(typeof(TextMeshProUGUI))).enabled = a;
				}
			}
		}
	}

	// Token: 0x0600046D RID: 1133 RVA: 0x00029C78 File Offset: 0x00027E78
	public virtual void hidespecificstuff(int i, bool a)
	{
		if ((Image)this.objectstohide[i].GetComponent(typeof(Image)))
		{
			((Image)this.objectstohide[i].GetComponent(typeof(Image))).enabled = a;
		}
		if ((TextMeshProUGUI)this.objectstohide[i].GetComponent(typeof(TextMeshProUGUI)))
		{
			((TextMeshProUGUI)this.objectstohide[i].GetComponent(typeof(TextMeshProUGUI))).enabled = a;
		}
	}

	// Token: 0x0600046E RID: 1134 RVA: 0x00029D18 File Offset: 0x00027F18
	public virtual void Main()
	{
	}

	// Token: 0x04000573 RID: 1395
	public GameObject[] objectstohide;

	// Token: 0x04000574 RID: 1396
	[HideInInspector]
	public bool currentlyvisible;
}
