using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000046 RID: 70
[Serializable]
public class FileButtonScript : MonoBehaviour
{
	// Token: 0x06000197 RID: 407 RVA: 0x000101D8 File Offset: 0x0000E3D8
	public virtual void Start()
	{
		this.fileinput = GameObject.Find("FilenameText");
	}

	// Token: 0x06000198 RID: 408 RVA: 0x000101EC File Offset: 0x0000E3EC
	public virtual void Update()
	{
	}

	// Token: 0x06000199 RID: 409 RVA: 0x000101F0 File Offset: 0x0000E3F0
	public virtual void pressbutton()
	{
		((InputField)this.fileinput.GetComponent(typeof(InputField))).text = ((Text)this.mytext.GetComponent(typeof(Text))).text;
		((CreateFileNamesScript)GameObject.Find("DasMenu").GetComponent(typeof(CreateFileNamesScript))).previewimage(((Text)this.mytext.GetComponent(typeof(Text))).text + ".jpg");
		((CreateFileNamesScript)GameObject.Find("DasMenu").GetComponent(typeof(CreateFileNamesScript))).updatebuttons();
	}

	// Token: 0x0600019A RID: 410 RVA: 0x000102AC File Offset: 0x0000E4AC
	public virtual void Main()
	{
	}

	// Token: 0x04000264 RID: 612
	public GameObject mytext;

	// Token: 0x04000265 RID: 613
	[HideInInspector]
	public GameObject fileinput;
}
