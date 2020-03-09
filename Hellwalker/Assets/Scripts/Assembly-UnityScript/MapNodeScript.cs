using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000077 RID: 119
[Serializable]
public class MapNodeScript : MonoBehaviour
{
	// Token: 0x060002E0 RID: 736 RVA: 0x0001A2B0 File Offset: 0x000184B0
	public virtual void Start()
	{
		this.panel = GameObject.Find("StatsPanel");
		this.levelname = GameObject.Find("LevelName");
		this.kills = GameObject.Find("BestKills");
		this.secrets = GameObject.Find("BestSecrets");
		this.time = GameObject.Find("BestTime");
		this.awards = GameObject.Find("Awards");
		string lhs = Application.dataPath + "/../config/scores" + "?tag=" + this.mylevel.ToString();
		if (!ES2.Exists(lhs + "levelbeaten"))
		{
			this.transform.gameObject.active = false;
		}
	}

	// Token: 0x060002E1 RID: 737 RVA: 0x0001A370 File Offset: 0x00018570
	public virtual void Update()
	{
	}

	// Token: 0x060002E2 RID: 738 RVA: 0x0001A374 File Offset: 0x00018574
	public virtual void clickity()
	{
		PersistScript persistScript = (PersistScript)GameObject.Find("PERSIST").GetComponent(typeof(PersistScript));
		persistScript.leveltoload = this.mylevel;
		persistScript.reseteverything();
		((LoadoutStuffScript)GameObject.Find("Stuff and stuff").GetComponent(typeof(LoadoutStuffScript))).assigninventory();
		Application.LoadLevel("LoadingScene");
	}

	// Token: 0x060002E3 RID: 739 RVA: 0x0001A3E0 File Offset: 0x000185E0
	public virtual void OnGUI()
	{
		Vector2 vector = default(Vector2);
		vector.x = ((RectTransform)GameObject.Find("Canvas").GetComponent(typeof(RectTransform))).rect.width;
		vector.y = ((RectTransform)GameObject.Find("Canvas").GetComponent(typeof(RectTransform))).rect.height;
		Vector2 vector2 = default(Vector2);
		vector2.x = (float)Screen.width * this.originalposition.x / (float)1920;
		vector2.y = (float)Screen.height * this.originalposition.y / (float)1080;
		float x = vector2.x;
		Vector3 position = this.transform.position;
		float num = position.x = x;
		Vector3 vector3 = this.transform.position = position;
		float y = vector2.y;
		Vector3 position2 = this.transform.position;
		float num2 = position2.y = y;
		Vector3 vector4 = this.transform.position = position2;
	}

	// Token: 0x060002E4 RID: 740 RVA: 0x0001A518 File Offset: 0x00018718
	public virtual void mouseover()
	{
		string lhs = Application.dataPath + "/../config/scores" + "?tag=" + this.mylevel.ToString();
		((CanvasGroup)this.panel.GetComponent(typeof(CanvasGroup))).alpha = (float)1;
		if (ES2.Exists(lhs + "levelbeaten"))
		{
			((Text)this.levelname.GetComponent(typeof(Text))).text = ES2.Load<string>(lhs + "name");
			((Text)this.kills.GetComponent(typeof(Text))).text = ES2.Load<int>(lhs + "kills").ToString() + "/" + ES2.Load<int>(lhs + "startingenemies").ToString();
			((Text)this.secrets.GetComponent(typeof(Text))).text = ES2.Load<int>(lhs + "secrets").ToString() + "/" + ES2.Load<int>(lhs + "startingsecrets").ToString();
			((Text)this.time.GetComponent(typeof(Text))).text = ES2.Load<float>(lhs + "minutes").ToString("00") + "." + ES2.Load<float>(lhs + "seconds").ToString("00.00");
		}
		((Text)this.awards.GetComponent(typeof(Text))).text = string.Empty;
		if (ES2.Exists(lhs + "pacifist"))
		{
			((Text)this.awards.GetComponent(typeof(Text))).text = ((Text)this.awards.GetComponent(typeof(Text))).text + "PACIFIST \n";
		}
		if (ES2.Exists(lhs + "ninja"))
		{
			((Text)this.awards.GetComponent(typeof(Text))).text = ((Text)this.awards.GetComponent(typeof(Text))).text + "UNTOUCHABLE \n";
		}
		if (ES2.Exists(lhs + "completionist"))
		{
			((Text)this.awards.GetComponent(typeof(Text))).text = ((Text)this.awards.GetComponent(typeof(Text))).text + "COMPLETIONIST \n";
		}
		if (ES2.Exists(lhs + "lowtech"))
		{
			((Text)this.awards.GetComponent(typeof(Text))).text = ((Text)this.awards.GetComponent(typeof(Text))).text + "LOW TECH \n";
		}
	}

	// Token: 0x060002E5 RID: 741 RVA: 0x0001A874 File Offset: 0x00018A74
	public virtual void mouseexit()
	{
		((CanvasGroup)this.panel.GetComponent(typeof(CanvasGroup))).alpha = (float)0;
	}

	// Token: 0x060002E6 RID: 742 RVA: 0x0001A898 File Offset: 0x00018A98
	public virtual void Main()
	{
	}

	// Token: 0x0400034E RID: 846
	public int mylevel;

	// Token: 0x0400034F RID: 847
	public string myname;

	// Token: 0x04000350 RID: 848
	public float fadetime;

	// Token: 0x04000351 RID: 849
	public Vector2 originalposition;

	// Token: 0x04000352 RID: 850
	[HideInInspector]
	public GameObject panel;

	// Token: 0x04000353 RID: 851
	[HideInInspector]
	public GameObject levelname;

	// Token: 0x04000354 RID: 852
	[HideInInspector]
	public GameObject kills;

	// Token: 0x04000355 RID: 853
	[HideInInspector]
	public GameObject secrets;

	// Token: 0x04000356 RID: 854
	[HideInInspector]
	public GameObject time;

	// Token: 0x04000357 RID: 855
	[HideInInspector]
	public GameObject awards;
}
