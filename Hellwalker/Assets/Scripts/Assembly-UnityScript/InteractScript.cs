using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Boo.Lang;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityScript.Lang;

// Token: 0x0200005A RID: 90
[Serializable]
public class InteractScript : MonoBehaviour
{
	// Token: 0x06000250 RID: 592 RVA: 0x00015D0C File Offset: 0x00013F0C
	public virtual void Start()
	{
		this.inputmanager = Essential.Instance.inputManager;
        this.cross = GameObject.Find("Crosshair");
		this.carry = (PickUpScriptV2)this.GetComponent(typeof(PickUpScriptV2));
		this.toggleuiscript = (ToggleUIScript)GameObject.Find("HUDObjects").GetComponent(typeof(ToggleUIScript));
	}

	// Token: 0x06000251 RID: 593 RVA: 0x00015D8C File Offset: 0x00013F8C
	public virtual void Update()
	{
		this.displayname();
		this.dointeract();
	}

	// Token: 0x06000252 RID: 594 RVA: 0x00015D9C File Offset: 0x00013F9C
	public virtual void displayname()
	{
		TextMeshProUGUI textMeshProUGUI = (TextMeshProUGUI)GameObject.Find("NameLabelText").GetComponent(typeof(TextMeshProUGUI));
		RaycastHit raycastHit = default(RaycastHit);
		((Image)this.cross.GetComponent(typeof(Image))).enabled = true;
		textMeshProUGUI.text = string.Empty;
		if (Physics.Raycast(this.transform.position, this.transform.forward, out raycastHit, this.userange, this.interactlayers))
		{
			NameDisplayScript nameDisplayScript = (NameDisplayScript)raycastHit.transform.gameObject.GetComponent(typeof(NameDisplayScript));
			if (nameDisplayScript)
			{
				textMeshProUGUI.text = nameDisplayScript.myname;
				if (textMeshProUGUI.text != string.Empty)
				{
					((Image)this.cross.GetComponent(typeof(Image))).enabled = false;
				}
			}
		}
		if (this.carry.obihave)
		{
			((Image)this.cross.GetComponent(typeof(Image))).enabled = false;
		}
		if (!this.toggleuiscript.currentlyvisible)
		{
			((Image)this.cross.GetComponent(typeof(Image))).enabled = false;
		}
	}

	// Token: 0x06000253 RID: 595 RVA: 0x00015F00 File Offset: 0x00014100
	public virtual void dointeract()
	{
		RaycastHit raycastHit = default(RaycastHit);
		if (this.inputmanager.GetKeyInput("use", 1) && Physics.Raycast(this.transform.position, this.transform.forward, out raycastHit, this.userange, this.interactlayers))
		{
			if (raycastHit.transform.gameObject.tag == "AltarTag")
			{
				this.altarinteract(raycastHit.transform.gameObject);
			}
			if (raycastHit.transform.gameObject.tag == "TableLightTag")
			{
				this.lightinteract(raycastHit.transform.gameObject);
			}
			if (raycastHit.transform.gameObject.tag == "BottleTag")
			{
				this.bottleinteract(raycastHit.transform.gameObject);
			}
			if (raycastHit.transform.gameObject.tag == "CanTag")
			{
				this.caninteract(raycastHit.transform.gameObject);
			}
			if ((SinkScript)raycastHit.transform.gameObject.GetComponent(typeof(SinkScript)))
			{
				((SinkScript)raycastHit.transform.gameObject.GetComponent(typeof(SinkScript))).turnonwater();
			}
			if ((ShowerScript)raycastHit.transform.gameObject.GetComponent(typeof(ShowerScript)))
			{
				((ShowerScript)raycastHit.transform.gameObject.GetComponent(typeof(ShowerScript))).turnonwater();
			}
			if (raycastHit.transform.gameObject.tag == "BedTag")
			{
				this.bedinteract(raycastHit.transform.gameObject);
			}
			if (raycastHit.transform.gameObject.tag == "ButtonTag")
			{
				this.buttoninteract(raycastHit.transform.gameObject);
			}
			if (raycastHit.transform.gameObject.tag == "EndButtonTag")
			{
				this.buttoninteract(raycastHit.transform.gameObject);
				ButtonScript buttonScript = (ButtonScript)raycastHit.transform.gameObject.GetComponent(typeof(ButtonScript));
				if (buttonScript.altleveltoload == -1)
				{
					//this.StartCoroutine(this.endlevel(Application.loadedLevel, -1));
				}
				else
				{
					//this.StartCoroutine(this.endlevel(buttonScript.altleveltoload - 1, buttonScript.altleveltoload));
				}
			}
			if (raycastHit.transform.gameObject.tag == "TreasureChestTag")
			{
				this.treasureget(raycastHit.transform.gameObject);
			}
			if (raycastHit.transform.gameObject.tag == "CupboardTag")
			{
				this.cupboardget(raycastHit.transform.gameObject);
			}
			if (raycastHit.transform.gameObject.tag == "FireWoodTag")
			{
				this.fireinteract(raycastHit.transform.gameObject);
			}
			if (raycastHit.transform.gameObject.tag == "NormalDoorTag")
			{
				this.normaldoorinteract(raycastHit.transform.gameObject);
			}
			if (raycastHit.transform.gameObject.tag == "ToiletTag")
			{
				this.flushtoilet(raycastHit.transform.gameObject);
			}
			if (raycastHit.transform.gameObject.tag == "CookedViscera")
			{
				this.eatviscera(raycastHit.transform.gameObject);
			}
			if (raycastHit.transform.gameObject.tag == "GravePlotTag")
			{
				this.diggrave(raycastHit.transform.gameObject);
			}
			if (raycastHit.transform.gameObject.tag == "LotionTag")
			{
				this.uselotion(raycastHit.transform.gameObject);
			}
			if ((PlaySoundOnUseSCript)raycastHit.transform.gameObject.GetComponent(typeof(PlaySoundOnUseSCript)))
			{
				this.playsound(raycastHit.transform.gameObject);
			}
			if ((InteractText)raycastHit.transform.gameObject.GetComponent(typeof(InteractText)))
			{
				((TextMeshProUGUI)GameObject.Find("TutorialMessageText").GetComponent(typeof(TextMeshProUGUI))).text = ((InteractText)raycastHit.transform.gameObject.GetComponent(typeof(InteractText))).mytext;
				((ClearMessageAfterTime)GameObject.Find("TutorialMessageText").GetComponent(typeof(ClearMessageAfterTime))).timer = (float)3;
			}
		}
	}

	// Token: 0x06000254 RID: 596 RVA: 0x00016420 File Offset: 0x00014620
	public virtual void uselotion(GameObject hit)
	{
		((TextMeshProUGUI)GameObject.Find("TutorialMessageText").GetComponent(typeof(TextMeshProUGUI))).text = "NO HOSE FOR ME!";
		((ClearMessageAfterTime)GameObject.Find("TutorialMessageText").GetComponent(typeof(ClearMessageAfterTime))).timer = (float)3;
		UnityEngine.Object.Destroy(hit);
	}

	// Token: 0x06000255 RID: 597 RVA: 0x00016480 File Offset: 0x00014680
	public virtual void diggrave(GameObject hit)
	{
		((DestructibleObjectScript)hit.GetComponent(typeof(DestructibleObjectScript))).myhealth = (float)0;
	}

	// Token: 0x06000256 RID: 598 RVA: 0x000164A0 File Offset: 0x000146A0
	public virtual void eatviscera(GameObject hit)
	{
		PlayerHealthManagement playerHealthManagement = (PlayerHealthManagement)GameObject.Find("Player").GetComponent(typeof(PlayerHealthManagement));
		if (playerHealthManagement.myhealth < (float)100)
		{
			playerHealthManagement.myhealth += (float)25;
			if (playerHealthManagement.myhealth > (float)100)
			{
				playerHealthManagement.myhealth = (float)100;
			}
			((TextMeshProUGUI)GameObject.Find("MessageText").GetComponent(typeof(TextMeshProUGUI))).text = "+25 Health";
			((ClearMessageAfterTime)GameObject.Find("MessageText").GetComponent(typeof(ClearMessageAfterTime))).timer = ((ClearMessageAfterTime)GameObject.Find("MessageText").GetComponent(typeof(ClearMessageAfterTime))).defaulttime;
			UnityEngine.Object.Destroy(hit.transform.gameObject);
			((AudioSource)GameObject.Find("DrinkSound").GetComponent(typeof(AudioSource))).Play();
		}
	}

	// Token: 0x06000257 RID: 599 RVA: 0x000165A4 File Offset: 0x000147A4
	public virtual void flushtoilet(GameObject hit)
	{
		if (((PlaySoundOnUseSCript)hit.GetComponent(typeof(PlaySoundOnUseSCript))).waittimer >= ((PlaySoundOnUseSCript)hit.GetComponent(typeof(PlaySoundOnUseSCript))).waittime)
		{
			((Animator)hit.GetComponent(typeof(Animator))).SetTrigger("ToiletTrigger");
			((ToiletBowlScript)((ToiletScript)hit.GetComponent(typeof(ToiletScript))).mybowltrigger.GetComponent(typeof(ToiletBowlScript))).flushobjects = true;
		}
	}

	// Token: 0x06000258 RID: 600 RVA: 0x00016640 File Offset: 0x00014840
	public virtual void playsound(GameObject hit)
	{
		((PlaySoundOnUseSCript)hit.GetComponent(typeof(PlaySoundOnUseSCript))).playsound();
	}

	// Token: 0x06000259 RID: 601 RVA: 0x0001665C File Offset: 0x0001485C
	public virtual void normaldoorinteract(GameObject hit)
	{
		if (((NormalDoorScript)hit.GetComponent(typeof(NormalDoorScript))).targetrotation == (float)0)
		{
			((NormalDoorScript)hit.GetComponent(typeof(NormalDoorScript))).targetrotation = ((NormalDoorScript)hit.GetComponent(typeof(NormalDoorScript))).openrotation;
			((NameDisplayScript)hit.GetComponent(typeof(NameDisplayScript))).myname = "Close";
			((AudioSource)hit.GetComponent(typeof(AudioSource))).Play();
		}
		else
		{
			((NormalDoorScript)hit.GetComponent(typeof(NormalDoorScript))).targetrotation = (float)0;
			((NameDisplayScript)hit.GetComponent(typeof(NameDisplayScript))).myname = "Open";
		}
	}

	// Token: 0x0600025A RID: 602 RVA: 0x00016738 File Offset: 0x00014938
	public virtual void cupboardget(GameObject hit)
	{
		((CupboardScript)hit.transform.gameObject.GetComponent(typeof(CupboardScript))).currentstate = !((CupboardScript)hit.transform.gameObject.GetComponent(typeof(CupboardScript))).currentstate;
		if (((CupboardScript)hit.transform.gameObject.GetComponent(typeof(CupboardScript))).currentstate)
		{
			((Animator)((CupboardScript)hit.transform.gameObject.GetComponent(typeof(CupboardScript))).mybase.GetComponent(typeof(Animator))).SetTrigger("OpenTrigger");
			((AudioSource)((CupboardScript)hit.transform.gameObject.GetComponent(typeof(CupboardScript))).mybase.GetComponent(typeof(AudioSource))).Play();
			((NameDisplayScript)hit.GetComponent(typeof(NameDisplayScript))).myname = "Close";
		}
		else
		{
			((Animator)((CupboardScript)hit.transform.gameObject.GetComponent(typeof(CupboardScript))).mybase.GetComponent(typeof(Animator))).SetTrigger("CloseTrigger");
			((AudioSource)((CupboardScript)hit.transform.gameObject.GetComponent(typeof(CupboardScript))).mybase.GetComponent(typeof(AudioSource))).Play();
			((NameDisplayScript)hit.GetComponent(typeof(NameDisplayScript))).myname = "Open";
		}
	}

	// Token: 0x0600025B RID: 603 RVA: 0x000168FC File Offset: 0x00014AFC
	public virtual void fireinteract(GameObject hit)
	{
		GameObject gameObject = hit.transform.gameObject;
		((DestructibleObjectScript)gameObject.GetComponent(typeof(DestructibleObjectScript))).onfire = true;
		gameObject.tag = "Untagged";
		((NameDisplayScript)gameObject.GetComponent(typeof(NameDisplayScript))).myname = string.Empty;
	}

	// Token: 0x0600025C RID: 604 RVA: 0x0001695C File Offset: 0x00014B5C
	public virtual void treasureget(GameObject hit)
	{
		((Animator)hit.transform.gameObject.GetComponent(typeof(Animator))).SetTrigger("OpenTrigger");
		UnityEngine.Object.Instantiate<GameObject>(((TreasureChestScript)hit.transform.gameObject.GetComponent(typeof(TreasureChestScript))).myitem, this.transform.position, Quaternion.identity);
		((TreasureChestScript)hit.transform.gameObject.GetComponent(typeof(TreasureChestScript))).alreadyused = 1;
		((AudioSource)hit.transform.gameObject.GetComponent(typeof(AudioSource))).Play();
		hit.tag = "Untagged";
		((NameDisplayScript)hit.GetComponent(typeof(NameDisplayScript))).myname = string.Empty;
	}

	// Token: 0x0600025D RID: 605 RVA: 0x00016A40 File Offset: 0x00014C40
	public virtual void buttoninteract(GameObject hit)
	{
		ButtonScript buttonScript = (ButtonScript)hit.GetComponent(typeof(ButtonScript));
		if (!buttonScript.istrigger)
		{
			buttonScript.dopress();
		}
	}

	// Token: 0x0600025E RID: 606 RVA: 0x00016A74 File Offset: 0x00014C74
	public virtual void bedinteract(GameObject hit)
	{
		hit.tag = "Untagged";
		((NameDisplayScript)hit.GetComponent(typeof(NameDisplayScript))).myname = string.Empty;
		PlayerHealthManagement playerHealthManagement = (PlayerHealthManagement)GameObject.Find("Player").GetComponent(typeof(PlayerHealthManagement));
		playerHealthManagement.myarmor += (float)25;
		((TextMeshProUGUI)GameObject.Find("TutorialMessageText").GetComponent(typeof(TextMeshProUGUI))).text = "YOU FEEL RESTED (+25 MORALE).";
		((ClearMessageAfterTime)GameObject.Find("TutorialMessageText").GetComponent(typeof(ClearMessageAfterTime))).timer = (float)3;
		if (playerHealthManagement.myarmor > (float)100)
		{
			playerHealthManagement.myarmor = (float)100;
		}
		int num = 2;
		Color color = ((Image)GameObject.Find("BlackFade").GetComponent(typeof(Image))).color;
		float num2 = color.a = (float)num;
		Color color2 = ((Image)GameObject.Find("BlackFade").GetComponent(typeof(Image))).color = color;
		((AudioSource)hit.GetComponent(typeof(AudioSource))).Play();
	}

	// Token: 0x0600025F RID: 607 RVA: 0x00016BBC File Offset: 0x00014DBC
	public virtual void lightinteract(GameObject hit)
	{
		((Light)hit.GetComponent(typeof(Light))).enabled = !((Light)hit.GetComponent(typeof(Light))).enabled;
		if (((Light)hit.GetComponent(typeof(Light))).enabled)
		{
			hit.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color((float)1, (float)1, (float)1));
		}
		else
		{
			hit.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0.1f, 0.1f, 0.1f));
		}
		((AudioSource)hit.GetComponent(typeof(AudioSource))).Play();
	}

	// Token: 0x06000260 RID: 608 RVA: 0x00016C88 File Offset: 0x00014E88
	public virtual void bottleinteract(GameObject hit)
	{
		PlayerHealthManagement playerHealthManagement = (PlayerHealthManagement)GameObject.Find("Player").GetComponent(typeof(PlayerHealthManagement));
		if (playerHealthManagement.myarmor < (float)100)
		{
			playerHealthManagement.myarmor += (float)1;
		}
		playerHealthManagement.drunkness += (float)1;
		if (playerHealthManagement.drunkness < (float)4)
		{
			((TextMeshProUGUI)GameObject.Find("MessageText").GetComponent(typeof(TextMeshProUGUI))).text = "+1 Morale";
			((ClearMessageAfterTime)GameObject.Find("MessageText").GetComponent(typeof(ClearMessageAfterTime))).timer = ((ClearMessageAfterTime)GameObject.Find("MessageText").GetComponent(typeof(ClearMessageAfterTime))).defaulttime;
		}
		else
		{
			((TextMeshProUGUI)GameObject.Find("MessageText").GetComponent(typeof(TextMeshProUGUI))).text = "You feel <color=#99cc00>INTOXICATED</color>";
			((ClearMessageAfterTime)GameObject.Find("MessageText").GetComponent(typeof(ClearMessageAfterTime))).timer = (float)4;
			playerHealthManagement.drunkentimer = (float)30;
		}
		UnityEngine.Object.Destroy(hit.transform.gameObject);
		((AudioSource)GameObject.Find("DrinkSound").GetComponent(typeof(AudioSource))).Play();
	}

	// Token: 0x06000261 RID: 609 RVA: 0x00016DEC File Offset: 0x00014FEC
	public virtual void caninteract(GameObject hit)
	{
		PlayerHealthManagement playerHealthManagement = (PlayerHealthManagement)GameObject.Find("Player").GetComponent(typeof(PlayerHealthManagement));
		if (playerHealthManagement.myhealth < (float)100)
		{
			playerHealthManagement.myhealth += (float)2;
			if (playerHealthManagement.myhealth > (float)100)
			{
				playerHealthManagement.myhealth = (float)100;
			}
			((TextMeshProUGUI)GameObject.Find("MessageText").GetComponent(typeof(TextMeshProUGUI))).text = "+2 Health";
			((ClearMessageAfterTime)GameObject.Find("MessageText").GetComponent(typeof(ClearMessageAfterTime))).timer = ((ClearMessageAfterTime)GameObject.Find("MessageText").GetComponent(typeof(ClearMessageAfterTime))).defaulttime;
		}
		UnityEngine.Object.Destroy(hit.transform.gameObject);
		((AudioSource)GameObject.Find("DrinkSound").GetComponent(typeof(AudioSource))).Play();
	}

	// Token: 0x06000262 RID: 610 RVA: 0x00016EF0 File Offset: 0x000150F0
	public virtual void altarinteract(GameObject hit)
	{
		PlayerHealthManagement playerHealthManagement = (PlayerHealthManagement)GameObject.Find("Player").GetComponent(typeof(PlayerHealthManagement));
		if (playerHealthManagement.myhealth < (float)100)
		{
			playerHealthManagement.myhealth = (float)100;
		}
		if (playerHealthManagement.myarmor < (float)100)
		{
			playerHealthManagement.myarmor = (float)100;
		}
		((TextMeshProUGUI)GameObject.Find("MessageText").GetComponent(typeof(TextMeshProUGUI))).text = "Health and Morale have been recharged.";
		((ClearMessageAfterTime)GameObject.Find("MessageText").GetComponent(typeof(ClearMessageAfterTime))).timer = ((ClearMessageAfterTime)GameObject.Find("MessageText").GetComponent(typeof(ClearMessageAfterTime))).defaulttime;
		hit.tag = "Untagged";
		((NameDisplayScript)hit.GetComponent(typeof(NameDisplayScript))).myname = string.Empty;
		((ParticleSystem)hit.GetComponent(typeof(ParticleSystem))).Play();
		((AudioSource)hit.GetComponent(typeof(AudioSource))).Play();
	}

	// Token: 0x06000263 RID: 611 RVA: 0x00017018 File Offset: 0x00015218
	//public virtual IEnumerator endlevel(int thislevel, int markthiscomplete)
	//{
	//	//return new InteractScript.endlevel437(thislevel, markthiscomplete, this).GetEnumerator();
	//}

	// Token: 0x06000264 RID: 612 RVA: 0x00017028 File Offset: 0x00015228
	public virtual void setscreenstuff()
	{
		PersistScript persistScript = (PersistScript)GameObject.Find("PERSIST").GetComponent(typeof(PersistScript));
		ScreenSizeScript screenSizeScript = (ScreenSizeScript)GameObject.Find("Player").GetComponent(typeof(ScreenSizeScript));
		persistScript.screensize = screenSizeScript.currentsize;
		persistScript.huddetail = screenSizeScript.huddetail;
	}

	// Token: 0x06000265 RID: 613 RVA: 0x0001708C File Offset: 0x0001528C
	public virtual void Main()
	{
	}

	// Token: 0x040002BB RID: 699
	public float userange;

	// Token: 0x040002BC RID: 700
	public LayerMask interactlayers;

	// Token: 0x040002BD RID: 701
	public GameObject nokeysound;

	// Token: 0x040002BE RID: 702
	[HideInInspector]
	public GameObject cross;

	// Token: 0x040002BF RID: 703
	[HideInInspector]
	public PickUpScriptV2 carry;

	// Token: 0x040002C0 RID: 704
	[HideInInspector]
	public ToggleUIScript toggleuiscript;

	// Token: 0x040002C1 RID: 705
	[HideInInspector]
	public MyInputManager inputmanager;

	// Token: 0x0200005B RID: 91
	//[CompilerGenerated]
	//[Serializable]
	//internal sealed class $endlevel$437 : GenericGenerator<WaitForEndOfFrame>
	//{
	//	// Token: 0x06000266 RID: 614 RVA: 0x00017090 File Offset: 0x00015290
	//	public $endlevel$437(int thislevel, int markthiscomplete, InteractScript self_)
	//	{
	//		this.$thislevel$448 = thislevel;
	//		this.$markthiscomplete$449 = markthiscomplete;
	//		this.$self_$450 = self_;
	//	}

	//	// Token: 0x06000267 RID: 615 RVA: 0x000170B0 File Offset: 0x000152B0
	//	public override IEnumerator<WaitForEndOfFrame> GetEnumerator()
	//	{
	//		return new InteractScript.$endlevel$437.$(this.$thislevel$448, this.$markthiscomplete$449, this.$self_$450);
	//	}

	//	// Token: 0x040002C2 RID: 706
	//	internal int $thislevel$448;

	//	// Token: 0x040002C3 RID: 707
	//	internal int $markthiscomplete$449;

	//	// Token: 0x040002C4 RID: 708
	//	internal InteractScript $self_$450;
	//}
}
