using System;
using TMPro;
using UnityEngine;

// Token: 0x020000B9 RID: 185
[Serializable]
public class TextTriggerScript : MonoBehaviour
{
	// Token: 0x06000458 RID: 1112 RVA: 0x0002957C File Offset: 0x0002777C
	public TextTriggerScript()
	{
		this.displaytime = (float)3;
	}

	// Token: 0x06000459 RID: 1113 RVA: 0x0002958C File Offset: 0x0002778C
	public virtual void Start()
	{
	}

	// Token: 0x0600045A RID: 1114 RVA: 0x00029590 File Offset: 0x00027790
	public virtual void Update()
	{
	}

	// Token: 0x0600045B RID: 1115 RVA: 0x00029594 File Offset: 0x00027794
	public virtual void OnTriggerEnter(Collider hit)
	{
		if (hit.transform.gameObject.layer == 10)
		{
			((TextMeshProUGUI)GameObject.Find("TutorialMessageText").GetComponent(typeof(TextMeshProUGUI))).text = this.mytext;
			((ClearMessageAfterTime)GameObject.Find("TutorialMessageText").GetComponent(typeof(ClearMessageAfterTime))).timer = this.displaytime;
			UnityEngine.Object.Destroy(this.transform.gameObject);
		}
	}

	// Token: 0x0600045C RID: 1116 RVA: 0x0002961C File Offset: 0x0002781C
	public virtual void Main()
	{
	}

	// Token: 0x0400055D RID: 1373
	public string mytext;

	// Token: 0x0400055E RID: 1374
	public float displaytime;
}
