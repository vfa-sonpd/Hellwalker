using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200001D RID: 29
[Serializable]
public class CreateFileNamesScript : MonoBehaviour
{
	// Token: 0x060000CB RID: 203 RVA: 0x0000C334 File Offset: 0x0000A534
	public virtual void Start()
	{
		this.updatebuttons();
		this.setscrollbar();
	}

	// Token: 0x060000CC RID: 204 RVA: 0x0000C344 File Offset: 0x0000A544
	public virtual void Update()
	{
	}

	// Token: 0x060000CD RID: 205 RVA: 0x0000C348 File Offset: 0x0000A548
	public virtual void updatebuttons()
	{
		this.deletebuttons();
		this.findquicksave();
		this.createbuttons();
	}

	// Token: 0x060000CE RID: 206 RVA: 0x0000C35C File Offset: 0x0000A55C
	public virtual void setscrollbar()
	{
		((Scrollbar)this.scrollbar.GetComponent(typeof(Scrollbar))).value = (float)1;
	}

	// Token: 0x060000CF RID: 207 RVA: 0x0000C380 File Offset: 0x0000A580
	public virtual void deletebuttons()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("FileButtonTag");
		for (int i = 0; i < array.Length; i++)
		{
			UnityEngine.Object.Destroy(array[i]);
		}
	}

	// Token: 0x060000D0 RID: 208 RVA: 0x0000C3B8 File Offset: 0x0000A5B8
	public virtual void findquicksave()
	{
		string[] files = Directory.GetFiles(Application.dataPath + "/../saves/");
		for (int i = 0; i < files.Length; i++)
		{
			string fileName = Path.GetFileName(files[i]);
			if (fileName == "quicksave")
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.mybutton);
				gameObject.name = fileName + "_savebutton";
				((RectTransform)gameObject.GetComponent(typeof(RectTransform))).anchoredPosition3D = new Vector3((float)0, (float)0, (float)0);
				((Text)((FileButtonScript)gameObject.GetComponent(typeof(FileButtonScript))).mytext.GetComponent(typeof(Text))).text = fileName;
				gameObject.transform.SetParent(this.filecontent.transform, false);
			}
		}
	}

	// Token: 0x060000D1 RID: 209 RVA: 0x0000C49C File Offset: 0x0000A69C
	public virtual void createbuttons()
	{
		string[] files = Directory.GetFiles(Application.dataPath + "/../saves/");
		for (int i = 0; i < files.Length; i++)
		{
			string fileName = Path.GetFileName(files[i]);
			if (fileName != "quicksave")
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.mybutton);
				gameObject.name = fileName + "_savebutton";
				((RectTransform)gameObject.GetComponent(typeof(RectTransform))).anchoredPosition3D = new Vector3((float)0, (float)0, (float)0);
				((Text)((FileButtonScript)gameObject.GetComponent(typeof(FileButtonScript))).mytext.GetComponent(typeof(Text))).text = fileName;
				gameObject.transform.SetParent(this.filecontent.transform, false);
			}
		}
	}

	// Token: 0x060000D2 RID: 210 RVA: 0x0000C580 File Offset: 0x0000A780
	public virtual void previewimage(string mytext)
	{
		string text = Application.dataPath + "/../saveimages/" + mytext;
		if (ES2.Exists(text))
		{
			Texture2D texture2D = ES2.LoadImage(text);
			Sprite sprite = Sprite.Create(texture2D, new Rect((float)0, (float)0, (float)texture2D.width, (float)texture2D.height), new Vector2(0.5f, 0.5f));
			if (GameObject.Find("FilePreviewImage"))
			{
				((Image)GameObject.Find("FilePreviewImage").GetComponent(typeof(Image))).sprite = sprite;
			}
		}
		else if (GameObject.Find("FilePreviewImage"))
		{
			((Image)GameObject.Find("FilePreviewImage").GetComponent(typeof(Image))).sprite = this.noimagespr;
		}
	}

	// Token: 0x060000D3 RID: 211 RVA: 0x0000C65C File Offset: 0x0000A85C
	public virtual void Main()
	{
	}

	// Token: 0x040001AA RID: 426
	public GameObject mybutton;

	// Token: 0x040001AB RID: 427
	public GameObject filecontent;

	// Token: 0x040001AC RID: 428
	public GameObject scrollbar;

	// Token: 0x040001AD RID: 429
	public Sprite noimagespr;
}
