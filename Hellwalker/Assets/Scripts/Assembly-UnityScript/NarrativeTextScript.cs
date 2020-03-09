using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000083 RID: 131
[Serializable]
public class NarrativeTextScript : MonoBehaviour
{
	// Token: 0x0600033F RID: 831 RVA: 0x0001F200 File Offset: 0x0001D400
	public virtual void Start()
	{
		this.index = 0;
		this.mytext = this.newLineReplace(this.mytext);
	}

	// Token: 0x06000340 RID: 832 RVA: 0x0001F21C File Offset: 0x0001D41C
	public virtual void Update()
	{
		this.timer += Time.deltaTime;
		if (this.timer > this.time)
		{
			this.timer = (float)0;
			this.index++;
		}
		if (this.index > this.mytext.Length)
		{
			this.index = this.mytext.Length;
		}
		this.currentstring = this.mytext.Substring(0, this.index);
		((Text)this.GetComponent(typeof(Text))).text = this.currentstring;
		if (Input.anyKeyDown)
		{
			if (this.index >= this.mytext.Length)
			{
				if (GameObject.Find("PERSIST"))
				{
					((PersistScript)GameObject.Find("PERSIST").GetComponent(typeof(PersistScript))).domenusound = true;
				}
				Application.LoadLevel("Menu");
			}
			this.index = this.mytext.Length;
			this.currentstring = this.mytext;
		}
	}

	// Token: 0x06000341 RID: 833 RVA: 0x0001F340 File Offset: 0x0001D540
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

	// Token: 0x06000342 RID: 834 RVA: 0x0001F398 File Offset: 0x0001D598
	public virtual void Main()
	{
	}

	// Token: 0x04000400 RID: 1024
	public string mytext;

	// Token: 0x04000401 RID: 1025
	public float time;

	// Token: 0x04000402 RID: 1026
	[HideInInspector]
	public string currentstring;

	// Token: 0x04000403 RID: 1027
	[HideInInspector]
	public int index;

	// Token: 0x04000404 RID: 1028
	[HideInInspector]
	public float timer;
}
