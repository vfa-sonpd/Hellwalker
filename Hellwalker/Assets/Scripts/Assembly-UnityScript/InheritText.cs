using System;
using TMPro;
using UnityEngine;

// Token: 0x02000059 RID: 89
[Serializable]
public class InheritText : MonoBehaviour
{
	// Token: 0x0600024C RID: 588 RVA: 0x00015CA0 File Offset: 0x00013EA0
	public virtual void Start()
	{
		this.mytext = (TextMeshProUGUI)this.GetComponent(typeof(TextMeshProUGUI));
		this.theirtext = (TextMeshProUGUI)this.inheritfrom.GetComponent(typeof(TextMeshProUGUI));
	}

	// Token: 0x0600024D RID: 589 RVA: 0x00015CE8 File Offset: 0x00013EE8
	public virtual void Update()
	{
		this.mytext.text = this.theirtext.text;
	}

	// Token: 0x0600024E RID: 590 RVA: 0x00015D00 File Offset: 0x00013F00
	public virtual void Main()
	{
	}

	// Token: 0x040002B8 RID: 696
	public GameObject inheritfrom;

	// Token: 0x040002B9 RID: 697
	[HideInInspector]
	public TextMeshProUGUI mytext;

	// Token: 0x040002BA RID: 698
	[HideInInspector]
	public TextMeshProUGUI theirtext;
}
