using System;
using TMPro;
using UnityEngine;

// Token: 0x020000C4 RID: 196
[Serializable]
public class TutorialMessageOnObjectDestroy : MonoBehaviour
{
	// Token: 0x06000490 RID: 1168 RVA: 0x0002A308 File Offset: 0x00028508
	public virtual void Start()
	{
	}

	// Token: 0x06000491 RID: 1169 RVA: 0x0002A30C File Offset: 0x0002850C
	public virtual void Update()
	{
		if (!this.checkobjects())
		{
			this.objectsdead = true;
		}
		if (this.objectsdead)
		{
			this.timer += Time.deltaTime;
			if (this.timer >= this.delaytime)
			{
				this.displaytext();
			}
		}
	}

	// Token: 0x06000492 RID: 1170 RVA: 0x0002A360 File Offset: 0x00028560
	public virtual bool checkobjects()
	{
		int i = 0;
		bool result = false;
		while (i < this.objects.Length)
		{
			if (this.objects[i] != null)
			{
				result = true;
			}
			i++;
		}
		return result;
	}

	// Token: 0x06000493 RID: 1171 RVA: 0x0002A3A4 File Offset: 0x000285A4
	public virtual void displaytext()
	{
		((TextMeshProUGUI)GameObject.Find("TutorialMessageText").GetComponent(typeof(TextMeshProUGUI))).text = this.mytext;
		((ClearMessageAfterTime)GameObject.Find("TutorialMessageText").GetComponent(typeof(ClearMessageAfterTime))).timer = this.displaytime;
		UnityEngine.Object.Destroy(this.transform.gameObject);
	}

	// Token: 0x06000494 RID: 1172 RVA: 0x0002A414 File Offset: 0x00028614
	public virtual void Main()
	{
	}

	// Token: 0x0400058A RID: 1418
	public GameObject[] objects;

	// Token: 0x0400058B RID: 1419
	public string mytext;

	// Token: 0x0400058C RID: 1420
	public float displaytime;

	// Token: 0x0400058D RID: 1421
	public float delaytime;

	// Token: 0x0400058E RID: 1422
	[HideInInspector]
	public bool objectsdead;

	// Token: 0x0400058F RID: 1423
	[HideInInspector]
	public float timer;
}
