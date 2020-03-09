using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000098 RID: 152
[Serializable]
public class SaveFileHighlightSCript : MonoBehaviour
{
	// Token: 0x060003B2 RID: 946 RVA: 0x00023C1C File Offset: 0x00021E1C
	public virtual void Start()
	{
		this.filenametext = (InputField)GameObject.Find("FilenameText").GetComponent(typeof(InputField));
		this.rend = (Image)this.GetComponent(typeof(Image));
	}

	// Token: 0x060003B3 RID: 947 RVA: 0x00023C68 File Offset: 0x00021E68
	public virtual void Update()
	{
		GameObject exists = GameObject.Find(this.filenametext.text + "_savebutton");
		if (exists)
		{
			this.followthis = exists;
		}
		if (this.followthis != null)
		{
			this.transform.position = this.followthis.transform.position;
			this.rend.enabled = true;
		}
		else
		{
			this.rend.enabled = false;
		}
	}

	// Token: 0x060003B4 RID: 948 RVA: 0x00023CEC File Offset: 0x00021EEC
	public virtual void Main()
	{
	}

	// Token: 0x04000498 RID: 1176
	public GameObject followthis;

	// Token: 0x04000499 RID: 1177
	public InputField filenametext;

	// Token: 0x0400049A RID: 1178
	public Image rend;
}
