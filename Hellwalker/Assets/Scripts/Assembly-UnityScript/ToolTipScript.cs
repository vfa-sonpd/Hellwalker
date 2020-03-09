using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000C0 RID: 192
[Serializable]
public class ToolTipScript : MonoBehaviour
{
	// Token: 0x06000479 RID: 1145 RVA: 0x00029E40 File Offset: 0x00028040
	public virtual void Awake()
	{
	}

	// Token: 0x0600047A RID: 1146 RVA: 0x00029E44 File Offset: 0x00028044
	public virtual void Start()
	{
		this.mytext = this.newLineReplace(this.mytext);
	}

	// Token: 0x0600047B RID: 1147 RVA: 0x00029E58 File Offset: 0x00028058
	public virtual void Update()
	{
	}

	// Token: 0x0600047C RID: 1148 RVA: 0x00029E5C File Offset: 0x0002805C
	public virtual void showtip()
	{
		this.tipback.active = true;
		this.tipback.transform.parent = this.transform.parent;
		this.tipback.transform.position = this.transform.position;
		((Text)this.tiptext.GetComponent(typeof(Text))).text = this.mytext;
	}

	// Token: 0x0600047D RID: 1149 RVA: 0x00029ED0 File Offset: 0x000280D0
	public virtual void hidetip()
	{
		this.tipback.active = false;
		this.tipback.transform.parent = null;
	}

	// Token: 0x0600047E RID: 1150 RVA: 0x00029EF0 File Offset: 0x000280F0
	public virtual void OnGUI()
	{
		this.meeses = Event.current.mousePosition;
	}

	// Token: 0x0600047F RID: 1151 RVA: 0x00029F08 File Offset: 0x00028108
	public virtual string newLineReplace(string InText)
	{
		bool flag = true;
		while (flag)
		{
			int num = InText.IndexOf("\\n");
			if (num == -1)
			{
				flag = false;
			}
			else
			{
				InText = InText.Remove(num, 2);
				InText = InText.Insert(num, "\n");
			}
		}
		return InText;
	}

	// Token: 0x06000480 RID: 1152 RVA: 0x00029F60 File Offset: 0x00028160
	public virtual void Main()
	{
	}

	// Token: 0x0400057B RID: 1403
	public GameObject tipback;

	// Token: 0x0400057C RID: 1404
	public GameObject tiptext;

	// Token: 0x0400057D RID: 1405
	[HideInInspector]
	public Vector3 meeses;

	// Token: 0x0400057E RID: 1406
	public string mytext;
}
