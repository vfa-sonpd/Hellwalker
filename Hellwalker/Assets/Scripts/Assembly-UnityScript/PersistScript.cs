using System;
using UnityEngine;

// Token: 0x02000086 RID: 134
[Serializable]
public class PersistScript : MonoBehaviour
{
	// Token: 0x0600034F RID: 847 RVA: 0x0001F660 File Offset: 0x0001D860
	public PersistScript()
	{
		this.lastfilename = "none";
		this.playerhealth = (float)100;
		this.playerarmor = (float)25;
	}

	// Token: 0x06000350 RID: 848 RVA: 0x0001F688 File Offset: 0x0001D888
	public virtual void Start()
	{
		UnityEngine.Object.DontDestroyOnLoad(this.transform.gameObject);
		this.weaponinventory = new bool[10];
		this.ammoinventory = new float[10];
		this.reseteverything();
	}

	// Token: 0x06000351 RID: 849 RVA: 0x0001F6C8 File Offset: 0x0001D8C8
	public virtual void Update()
	{
	}

	// Token: 0x06000352 RID: 850 RVA: 0x0001F6CC File Offset: 0x0001D8CC
	public virtual void reseteverything()
	{
		for (int i = 0; i < 10; i++)
		{
			this.weaponinventory[i] = false;
			this.ammoinventory[i] = (float)0;
		}
		this.weaponinventory[0] = true;
		this.weaponinventory[9] = true;
		this.ammoinventory[0] = (float)1;
		this.ammoinventory[9] = (float)1;
		this.selectedweapon = 1;
		int num = 0;
		this.havedualshotguns = false;
		this.havedualpistols = false;
		this.havedaikatana = false;
		this.permduals = false;
		this.permshotguns = false;
		this.permdaikatana = false;
		this.totalhours = (float)0;
		this.totalminutes = (float)0;
		this.totalseconds = (float)0;
		this.playerhealth = (float)100;
		this.playerarmor = (float)25;
		this.screensize = (float)1;
		this.huddetail = 1;
		this.lastfilename = "none";
		this.savedthislevel = false;
	}

	// Token: 0x06000353 RID: 851 RVA: 0x0001F7C8 File Offset: 0x0001D9C8
	public virtual void resetweapons()
	{
		for (int i = 0; i < 10; i++)
		{
			this.weaponinventory[i] = false;
			this.ammoinventory[i] = (float)0;
		}
		this.weaponinventory[0] = true;
		this.weaponinventory[9] = true;
		this.ammoinventory[0] = (float)1;
		this.ammoinventory[9] = (float)1;
		this.selectedweapon = 1;
		this.havedualshotguns = false;
		this.havedualpistols = false;
		this.havedaikatana = false;
		this.permduals = false;
		this.permshotguns = false;
		this.permdaikatana = false;
	}

	// Token: 0x06000354 RID: 852 RVA: 0x0001F870 File Offset: 0x0001DA70
	public virtual bool intToBool(int i)
	{
		return i != 0;
	}

	// Token: 0x06000355 RID: 853 RVA: 0x0001F884 File Offset: 0x0001DA84
	public virtual void Main()
	{
	}

	// Token: 0x04000410 RID: 1040
	public bool pixelfilter;

	// Token: 0x04000411 RID: 1041
	public bool colorfilter;

	// Token: 0x04000412 RID: 1042
	public bool BFilter;

	// Token: 0x04000413 RID: 1043
	public bool bloom;

	// Token: 0x04000414 RID: 1044
	public bool flares;

	// Token: 0x04000415 RID: 1045
	public bool disablebuck;

	// Token: 0x04000416 RID: 1046
	public bool weaponbob;

	// Token: 0x04000417 RID: 1047
	public bool alwaysrun;

	// Token: 0x04000418 RID: 1048
	public bool hardcore;

	// Token: 0x04000419 RID: 1049
	public string lastfilename;

	// Token: 0x0400041A RID: 1050
	public bool savedthislevel;

	// Token: 0x0400041B RID: 1051
	public bool domenusound;

	// Token: 0x0400041C RID: 1052
	public bool textnext;

	// Token: 0x0400041D RID: 1053
	public int killsfromlastlevel;

	// Token: 0x0400041E RID: 1054
	public float minutesfromlastlevel;

	// Token: 0x0400041F RID: 1055
	public float secondsfromlastlevel;

	// Token: 0x04000420 RID: 1056
	public float totalhours;

	// Token: 0x04000421 RID: 1057
	public float totalminutes;

	// Token: 0x04000422 RID: 1058
	public float totalseconds;

	// Token: 0x04000423 RID: 1059
	public int secretsfromlastlevel;

	// Token: 0x04000424 RID: 1060
	public int startingsecretsfromlastlevel;

	// Token: 0x04000425 RID: 1061
	public int startingenemiesfromlastlevel;

	// Token: 0x04000426 RID: 1062
	public int lastlevelnumber;

	// Token: 0x04000427 RID: 1063
	public string lastlevelname;

	// Token: 0x04000428 RID: 1064
	public string lastlevelnumbers;

	// Token: 0x04000429 RID: 1065
	public int markthiscomplete;

	// Token: 0x0400042A RID: 1066
	public string partime;

	// Token: 0x0400042B RID: 1067
	public bool[] weaponinventory;

	// Token: 0x0400042C RID: 1068
	public float[] ammoinventory;

	// Token: 0x0400042D RID: 1069
	public int selectedweapon;

	// Token: 0x0400042E RID: 1070
	public bool havedualshotguns;

	// Token: 0x0400042F RID: 1071
	public bool havedualpistols;

	// Token: 0x04000430 RID: 1072
	public bool havedaikatana;

	// Token: 0x04000431 RID: 1073
	public bool permduals;

	// Token: 0x04000432 RID: 1074
	public bool permshotguns;

	// Token: 0x04000433 RID: 1075
	public bool permdaikatana;

	// Token: 0x04000434 RID: 1076
	public float screensize;

	// Token: 0x04000435 RID: 1077
	public int huddetail;

	// Token: 0x04000436 RID: 1078
	public float playerhealth;

	// Token: 0x04000437 RID: 1079
	public float playerarmor;

	// Token: 0x04000438 RID: 1080
	public int leveltoload;

	// Token: 0x04000439 RID: 1081
	public bool pacifistaward;

	// Token: 0x0400043A RID: 1082
	public bool lowtechaward;

	// Token: 0x0400043B RID: 1083
	public bool completionistaward;

	// Token: 0x0400043C RID: 1084
	public bool ninjaaward;

	// Token: 0x0400043D RID: 1085
	public Sprite screenshotimage;

	// Token: 0x0400043E RID: 1086
	public float mavolume;

	// Token: 0x0400043F RID: 1087
	public float muvolume;
}
