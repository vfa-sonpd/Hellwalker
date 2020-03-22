using com.ootii.Messages;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200008F RID: 143
[Serializable]
public class PlayerPickupScript : MonoBehaviour
{
    // Token: 0x0600037F RID: 895 RVA: 0x000208CC File Offset: 0x0001EACC
    //public virtual void OnTriggerStay(Collider hit)
    //{
    //	if (!Physics.Raycast(this.transform.position, (hit.transform.position - this.transform.position).normalized, Vector3.Distance(this.transform.position, hit.transform.position), this.pickupblockinglayers))
    //	{
    //		GameObject gameObject = hit.transform.gameObject;
    //		//if (gameObject.tag == "Weapon Pickup")
    //		//{
    //		//	this.pickupweapon(gameObject);
    //		//}
    //		if (gameObject.tag == "Health Pickup")
    //		{
    //			this.pickuphealth(gameObject);
    //		}
    //		if (gameObject.tag == "HolyHealthTag")
    //		{
    //			this.pickupholyhealth(gameObject);
    //		}
    //		if (gameObject.tag == "SwordTag")
    //		{
    //			this.PickUpSword(gameObject);
    //		}
    //		if (gameObject.tag == "WeaponEnhanceTag")
    //		{
    //			this.pickupenhance(gameObject);
    //		}
    //		if (gameObject.tag == "RedKeyTag")
    //		{
    //			this.pickupredkey(gameObject);
    //		}
    //		if (gameObject.tag == "BlueKeyTag")
    //		{
    //			this.pickupbluekey(gameObject);
    //		}
    //		if (gameObject.tag == "YellowKeyTag")
    //		{
    //			this.pickupyellowkey(gameObject);
    //		}
    //		if (gameObject.tag == "SuperShotgunTag")
    //		{
    //			this.pickupweapon(gameObject);
    //		}
    //		if (gameObject.tag == "Weapon Speed Pickup")
    //		{
    //			this.weaponspeedpickup(gameObject);
    //		}
    //		if (gameObject.tag == "ClimbingPickupTag")
    //		{
    //			this.climbpickup(gameObject);
    //		}
    //		if (gameObject.tag == "Superhot Powerup")
    //		{
    //			this.superhotpickup(gameObject);
    //		}
    //		if (gameObject.tag == "FlashlightPickupTag")
    //		{
    //			this.flashlightpickup(gameObject);
    //		}
    //		if (gameObject.tag == "LavaSuitPickup")
    //		{
    //			this.lavasuitpickup(gameObject);
    //		}
    //	}
    //}

    SelectionScript selectionScript; // Reference to SelectionScript

    private void Awake()
    {
        selectionScript = (SelectionScript)GameObject.Find("WeaponAnimator").GetComponent(typeof(SelectionScript));

        MessageDispatcher.AddListener(GameEvent.pickUpWeapon, OnPickupWeapon);
    }

    void OnPickupWeapon(IMessage message)
    {
        if(message.Data is WeaponPickupData)
        {
            WeaponPickupData data = message.Data as WeaponPickupData;

            try
            {
                selectionScript.weaponinventory[data.weaponcontent] = true;

                if (selectionScript.ammoinventory[data.weaponcontent] < selectionScript.maxammo[data.weaponcontent])
                {
                    selectionScript.ammoinventory[data.weaponcontent] = selectionScript.maxammo[data.weaponcontent];
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
    }

    // Token: 0x06000380 RID: 896 RVA: 0x00020ACC File Offset: 0x0001ECCC
    public virtual void flashlightpickup(GameObject ob)
	{
		((StatScript)GameObject.Find("StatObject").GetComponent(typeof(StatScript))).brokenflashlight = false;
		((TextMeshProUGUI)GameObject.Find("TutorialMessageText").GetComponent(typeof(TextMeshProUGUI))).text = "YOU FOUND A WORKING FLASHLIGHT";
		((ClearMessageAfterTime)GameObject.Find("TutorialMessageText").GetComponent(typeof(ClearMessageAfterTime))).timer = (float)3;
		((AudioSource)this.holyhealthpickupsound.GetComponent(typeof(AudioSource))).Play();
		((Collider)ob.GetComponent(typeof(Collider))).enabled = false;
		UnityEngine.Object.Destroy(ob);
	}

	// Token: 0x06000381 RID: 897 RVA: 0x00020B8C File Offset: 0x0001ED8C
	public virtual void climbpickup(GameObject ob)
	{
		((LadderUseScript)this.GetComponent(typeof(LadderUseScript))).climbanything = true;
		((TextMeshProUGUI)GameObject.Find("MessageText").GetComponent(typeof(TextMeshProUGUI))).text = "You found the Climbing Thing";
		((ClearMessageAfterTime)GameObject.Find("MessageText").GetComponent(typeof(ClearMessageAfterTime))).timer = ((ClearMessageAfterTime)GameObject.Find("MessageText").GetComponent(typeof(ClearMessageAfterTime))).defaulttime;
		((Collider)ob.GetComponent(typeof(Collider))).enabled = false;
		UnityEngine.Object.Destroy(ob);
	}

	// Token: 0x06000382 RID: 898 RVA: 0x00020C44 File Offset: 0x0001EE44
	public virtual void superhotpickup(GameObject ob)
	{
		MyControllerScript myControllerScript = (MyControllerScript)this.GetComponent(typeof(MyControllerScript));
		((TextMeshProUGUI)GameObject.Find("MessageText").GetComponent(typeof(TextMeshProUGUI))).text = "You found the Syrum of Blistering Heat";
		((ClearMessageAfterTime)GameObject.Find("MessageText").GetComponent(typeof(ClearMessageAfterTime))).timer = ((ClearMessageAfterTime)GameObject.Find("MessageText").GetComponent(typeof(ClearMessageAfterTime))).defaulttime;
		((CrosshairSizeScript)GameObject.Find("Crosshair").GetComponent(typeof(CrosshairSizeScript))).plus = 1.5f;
		myControllerScript.superhot = true;
		myControllerScript.superhottimer = (float)15;
		float a = 0.3f;
		Color color = ((Image)GameObject.Find("WeaponSpeedOverlay").GetComponent(typeof(Image))).color;
		float num = color.a = a;
		Color color2 = ((Image)GameObject.Find("WeaponSpeedOverlay").GetComponent(typeof(Image))).color = color;
		((SetImageAlphaZeroAfterTime)GameObject.Find("WeaponSpeedOverlay").GetComponent(typeof(SetImageAlphaZeroAfterTime))).mytime = myControllerScript.superhottimer;
		((AudioSource)this.holyhealthpickupsound.GetComponent(typeof(AudioSource))).Play();
		((Collider)ob.GetComponent(typeof(Collider))).enabled = false;
		UnityEngine.Object.Destroy(ob);
	}

	// Token: 0x06000383 RID: 899 RVA: 0x00020DDC File Offset: 0x0001EFDC
	public virtual void lavasuitpickup(GameObject ob)
	{
		SelectionScript selectionScript = (SelectionScript)GameObject.Find("WeaponAnimator").GetComponent(typeof(SelectionScript));
		((TextMeshProUGUI)GameObject.Find("MessageText").GetComponent(typeof(TextMeshProUGUI))).text = "You found the Biosuit. Lava cannot harm you.";
		((ClearMessageAfterTime)GameObject.Find("MessageText").GetComponent(typeof(ClearMessageAfterTime))).timer = ((ClearMessageAfterTime)GameObject.Find("MessageText").GetComponent(typeof(ClearMessageAfterTime))).defaulttime;
		((CrosshairSizeScript)GameObject.Find("Crosshair").GetComponent(typeof(CrosshairSizeScript))).plus = 1.5f;
		selectionScript.ignorelava = true;
		((AudioSource)this.holyhealthpickupsound.GetComponent(typeof(AudioSource))).Play();
		((Collider)ob.GetComponent(typeof(Collider))).enabled = false;
		UnityEngine.Object.Destroy(ob);
	}

	// Token: 0x06000384 RID: 900 RVA: 0x00020EE8 File Offset: 0x0001F0E8
	public virtual void weaponspeedpickup(GameObject ob)
	{
		AttackScript attackScript = (AttackScript)GameObject.Find("WeaponAnimator").GetComponent(typeof(AttackScript));
		((TextMeshProUGUI)GameObject.Find("MessageText").GetComponent(typeof(TextMeshProUGUI))).text = "You found the Fast Fire Totem";
		((ClearMessageAfterTime)GameObject.Find("MessageText").GetComponent(typeof(ClearMessageAfterTime))).timer = ((ClearMessageAfterTime)GameObject.Find("MessageText").GetComponent(typeof(ClearMessageAfterTime))).defaulttime;
		((CrosshairSizeScript)GameObject.Find("Crosshair").GetComponent(typeof(CrosshairSizeScript))).plus = 1.5f;
		attackScript.firespeed *= (float)2;
		attackScript.firespeedtimer = (float)15;
		((ChangePitchOverTime)GameObject.Find("FastFireSound").GetComponent(typeof(ChangePitchOverTime))).changetime = attackScript.firespeedtimer;
		((DisableSoundAfterTime)GameObject.Find("FastFireSound").GetComponent(typeof(DisableSoundAfterTime))).timer = attackScript.firespeedtimer;
		((DecreaseBarOverTime)GameObject.Find("SpeedPowerupBar").GetComponent(typeof(DecreaseBarOverTime))).time = attackScript.firespeedtimer;
		((AudioSource)GameObject.Find("FastFireSound").GetComponent(typeof(AudioSource))).volume = (float)1;
		((AudioSource)this.holyhealthpickupsound.GetComponent(typeof(AudioSource))).Play();
		((Collider)ob.GetComponent(typeof(Collider))).enabled = false;
		UnityEngine.Object.Destroy(ob);
	}

	// Token: 0x06000385 RID: 901 RVA: 0x000210A4 File Offset: 0x0001F2A4
	public virtual void pickupredkey(GameObject ob)
	{
		SelectionScript selectionScript = (SelectionScript)GameObject.Find("WeaponAnimator").GetComponent(typeof(SelectionScript));
		selectionScript.haveredkey = true;
		float a = 0.55f;
		Color color = ((Image)GameObject.Find("RedIndicator").GetComponent(typeof(Image))).color;
		float num = color.a = a;
		Color color2 = ((Image)GameObject.Find("RedIndicator").GetComponent(typeof(Image))).color = color;
		((TextMeshProUGUI)GameObject.Find("MessageText").GetComponent(typeof(TextMeshProUGUI))).text = "You found the <color=#800000ff>RED</color> Key";
		((ClearMessageAfterTime)GameObject.Find("MessageText").GetComponent(typeof(ClearMessageAfterTime))).timer = ((ClearMessageAfterTime)GameObject.Find("MessageText").GetComponent(typeof(ClearMessageAfterTime))).defaulttime;
		float a2 = 0.8f;
		Color color3 = ((Image)GameObject.Find("DamageOverlay").GetComponent(typeof(Image))).color;
		float num2 = color3.a = a2;
		Color color4 = ((Image)GameObject.Find("DamageOverlay").GetComponent(typeof(Image))).color = color3;
		((CrosshairSizeScript)GameObject.Find("Crosshair").GetComponent(typeof(CrosshairSizeScript))).plus = 1.5f;
		((AudioSource)this.keysound.GetComponent(typeof(AudioSource))).Play();
		((Collider)ob.GetComponent(typeof(Collider))).enabled = false;
		UnityEngine.Object.Destroy(ob);
	}

	// Token: 0x06000386 RID: 902 RVA: 0x0002127C File Offset: 0x0001F47C
	public virtual void pickupbluekey(GameObject ob)
	{
		SelectionScript selectionScript = (SelectionScript)GameObject.Find("WeaponAnimator").GetComponent(typeof(SelectionScript));
		selectionScript.havebluekey = true;
		float a = 0.55f;
		Color color = ((Image)GameObject.Find("BlueIndicator").GetComponent(typeof(Image))).color;
		float num = color.a = a;
		Color color2 = ((Image)GameObject.Find("BlueIndicator").GetComponent(typeof(Image))).color = color;
		((TextMeshProUGUI)GameObject.Find("MessageText").GetComponent(typeof(TextMeshProUGUI))).text = "You found the <color=#000080ff>BLUE</color> Key";
		((ClearMessageAfterTime)GameObject.Find("MessageText").GetComponent(typeof(ClearMessageAfterTime))).timer = ((ClearMessageAfterTime)GameObject.Find("MessageText").GetComponent(typeof(ClearMessageAfterTime))).defaulttime;
		float a2 = 0.8f;
		Color color3 = ((Image)GameObject.Find("GetWeaponOverlay").GetComponent(typeof(Image))).color;
		float num2 = color3.a = a2;
		Color color4 = ((Image)GameObject.Find("GetWeaponOverlay").GetComponent(typeof(Image))).color = color3;
		((CrosshairSizeScript)GameObject.Find("Crosshair").GetComponent(typeof(CrosshairSizeScript))).plus = 1.5f;
		((AudioSource)this.keysound.GetComponent(typeof(AudioSource))).Play();
		((Collider)ob.GetComponent(typeof(Collider))).enabled = false;
		UnityEngine.Object.Destroy(ob);
	}

	// Token: 0x06000387 RID: 903 RVA: 0x00021454 File Offset: 0x0001F654
	public virtual void pickupyellowkey(GameObject ob)
	{
		SelectionScript selectionScript = (SelectionScript)GameObject.Find("WeaponAnimator").GetComponent(typeof(SelectionScript));
		selectionScript.haveyellowkey = true;
		float a = 0.55f;
		Color color = ((Image)GameObject.Find("YellowIndicator").GetComponent(typeof(Image))).color;
		float num = color.a = a;
		Color color2 = ((Image)GameObject.Find("YellowIndicator").GetComponent(typeof(Image))).color = color;
		((TextMeshProUGUI)GameObject.Find("MessageText").GetComponent(typeof(TextMeshProUGUI))).text = "You found the <color=#808000ff>YELLOW</color> Key";
		((ClearMessageAfterTime)GameObject.Find("MessageText").GetComponent(typeof(ClearMessageAfterTime))).timer = ((ClearMessageAfterTime)GameObject.Find("MessageText").GetComponent(typeof(ClearMessageAfterTime))).defaulttime;
		float a2 = 0.8f;
		Color color3 = ((Image)GameObject.Find("GetPowerOverlay").GetComponent(typeof(Image))).color;
		float num2 = color3.a = a2;
		Color color4 = ((Image)GameObject.Find("GetPowerOverlay").GetComponent(typeof(Image))).color = color3;
		((CrosshairSizeScript)GameObject.Find("Crosshair").GetComponent(typeof(CrosshairSizeScript))).plus = 1.5f;
		((AudioSource)this.keysound.GetComponent(typeof(AudioSource))).Play();
		((Collider)ob.GetComponent(typeof(Collider))).enabled = false;
		UnityEngine.Object.Destroy(ob);
	}

	// Token: 0x06000388 RID: 904 RVA: 0x0002162C File Offset: 0x0001F82C
	public virtual void pickuphealth(GameObject ob)
	{
		PlayerHealthManagement playerHealthManagement = (PlayerHealthManagement)this.GetComponent(typeof(PlayerHealthManagement));
		HealthPickupScript healthPickupScript = (HealthPickupScript)ob.GetComponent(typeof(HealthPickupScript));
		float num = playerHealthManagement.myhealth + healthPickupScript.myhealthcontent;
		float num2 = playerHealthManagement.myarmor + healthPickupScript.myarmorcontent;
		float myhealthcontent = healthPickupScript.myhealthcontent;
		float myarmorcontent = healthPickupScript.myarmorcontent;
		if (num > (float)100 && healthPickupScript.myhealthcontent > (float)0 && myhealthcontent <= (float)100 && !healthPickupScript.doublehealth)
		{
			num = (float)100;
		}
		if (num2 > (float)100 && healthPickupScript.myarmorcontent > (float)0 && myhealthcontent <= (float)100 && !healthPickupScript.doublearmor)
		{
			num2 = (float)100;
		}
		if (num > (float)200)
		{
			num = (float)200;
		}
		if (num2 > (float)200)
		{
			num2 = (float)200;
		}
		if (num > playerHealthManagement.myhealth || num2 > playerHealthManagement.myarmor)
		{
			((TextMeshProUGUI)GameObject.Find("MessageText").GetComponent(typeof(TextMeshProUGUI))).text = healthPickupScript.message;
			((ClearMessageAfterTime)GameObject.Find("MessageText").GetComponent(typeof(ClearMessageAfterTime))).timer = ((ClearMessageAfterTime)GameObject.Find("MessageText").GetComponent(typeof(ClearMessageAfterTime))).defaulttime;
			float a = 0.3f;
			Color color = ((Image)GameObject.Find("GetHealthOverlay").GetComponent(typeof(Image))).color;
			float num3 = color.a = a;
			Color color2 = ((Image)GameObject.Find("GetHealthOverlay").GetComponent(typeof(Image))).color = color;
			((CrosshairSizeScript)GameObject.Find("Crosshair").GetComponent(typeof(CrosshairSizeScript))).plus = 1.5f;
			if (healthPickupScript.myarmorcontent > (float)0)
			{
				((AudioSource)this.armorpickupsound.GetComponent(typeof(AudioSource))).Play();
			}
			else
			{
				((AudioSource)this.healthpickupsound.GetComponent(typeof(AudioSource))).Play();
			}
			playerHealthManagement.myhealth = num;
			playerHealthManagement.myarmor = num2;
			((Collider)ob.GetComponent(typeof(Collider))).enabled = false;
			UnityEngine.Object.Destroy(ob);
		}
	}

	// Token: 0x06000389 RID: 905 RVA: 0x000218B0 File Offset: 0x0001FAB0
	public virtual void pickupweapon(GameObject ob)
	{
		WeaponPickupScript weaponPickupScript = (WeaponPickupScript)ob.GetComponent(typeof(WeaponPickupScript));
		SelectionScript selectionScript = (SelectionScript)GameObject.Find("WeaponAnimator").GetComponent(typeof(SelectionScript));
		if (weaponPickupScript.giveweapon)
		{
            // If weapon is a pistol and player is not having 2 pistols already and player already has 1 pistol, activate dual wield pistol
			if (weaponPickupScript.weaponcontent == 1 && !selectionScript.havedualpistols && selectionScript.weaponinventory[1])
			{
				((TextMeshProUGUI)GameObject.Find("MessageText").GetComponent(typeof(TextMeshProUGUI))).text = "You found another Pistol";
				((ClearMessageAfterTime)GameObject.Find("MessageText").GetComponent(typeof(ClearMessageAfterTime))).timer = ((ClearMessageAfterTime)GameObject.Find("MessageText").GetComponent(typeof(ClearMessageAfterTime))).defaulttime;
				float a = 0.3f;
				Color color = ((Image)GameObject.Find("GetWeaponOverlay").GetComponent(typeof(Image))).color;
				float num = color.a = a;
				Color color2 = ((Image)GameObject.Find("GetWeaponOverlay").GetComponent(typeof(Image))).color = color;
				((CrosshairSizeScript)GameObject.Find("Crosshair").GetComponent(typeof(CrosshairSizeScript))).plus = 1.5f;
				selectionScript.havedualpistols = true;
				selectionScript.permduals = true;
				((Animator)GameObject.Find("WeaponAnimator").GetComponent(typeof(Animator))).SetTrigger("StopAll");
				if (selectionScript.selectedweapon != 0 && this.doautoswitch)
				{
					selectionScript.weapontogetto = weaponPickupScript.weaponcontent + 1;
					selectionScript.lastselectedweapon = selectionScript.selectedweapon;
				}
				((AudioSource)this.weaponpickupsound.GetComponent(typeof(AudioSource))).Play();
				((Collider)ob.GetComponent(typeof(Collider))).enabled = false;
				UnityEngine.Object.Destroy(ob);
			}
			if (weaponPickupScript.weaponcontent == 2 && !selectionScript.havedualshotguns && selectionScript.weaponinventory[2])
			{
				((TextMeshProUGUI)GameObject.Find("MessageText").GetComponent(typeof(TextMeshProUGUI))).text = "You found another Shotgun";
				((ClearMessageAfterTime)GameObject.Find("MessageText").GetComponent(typeof(ClearMessageAfterTime))).timer = ((ClearMessageAfterTime)GameObject.Find("MessageText").GetComponent(typeof(ClearMessageAfterTime))).defaulttime;
				float a2 = 0.3f;
				Color color3 = ((Image)GameObject.Find("GetWeaponOverlay").GetComponent(typeof(Image))).color;
				float num2 = color3.a = a2;
				Color color4 = ((Image)GameObject.Find("GetWeaponOverlay").GetComponent(typeof(Image))).color = color3;
				((CrosshairSizeScript)GameObject.Find("Crosshair").GetComponent(typeof(CrosshairSizeScript))).plus = 1.5f;
				selectionScript.havedualshotguns = true;
				selectionScript.permshotguns = true;
				((Animator)GameObject.Find("WeaponAnimator").GetComponent(typeof(Animator))).SetTrigger("StopAll");
				if (selectionScript.selectedweapon != 0 && this.doautoswitch)
				{
					selectionScript.weapontogetto = weaponPickupScript.weaponcontent + 1;
					selectionScript.lastselectedweapon = selectionScript.selectedweapon;
				}
				((AudioSource)this.weaponpickupsound.GetComponent(typeof(AudioSource))).Play();
				((Collider)ob.GetComponent(typeof(Collider))).enabled = false;
				UnityEngine.Object.Destroy(ob);
			}
			if (ob.tag == "Weapon Pickup" && weaponPickupScript.weaponcontent == 3)
			{
				selectionScript.havesingleshotgun = true;
			}
			if (!selectionScript.weaponinventory[weaponPickupScript.weaponcontent])
			{
				if (selectionScript.selectedweapon != 0 && this.doautoswitch)
				{
					selectionScript.weapontogetto = weaponPickupScript.weaponcontent + 1;
					selectionScript.lastselectedweapon = selectionScript.selectedweapon;
				}
				((AudioSource)this.weaponpickupsound.GetComponent(typeof(AudioSource))).Play();
				((TextMeshProUGUI)GameObject.Find("MessageText").GetComponent(typeof(TextMeshProUGUI))).text = weaponPickupScript.message;
				((ClearMessageAfterTime)GameObject.Find("MessageText").GetComponent(typeof(ClearMessageAfterTime))).timer = ((ClearMessageAfterTime)GameObject.Find("MessageText").GetComponent(typeof(ClearMessageAfterTime))).defaulttime;
				float a3 = 0.3f;
				Color color5 = ((Image)GameObject.Find("GetWeaponOverlay").GetComponent(typeof(Image))).color;
				float num3 = color5.a = a3;
				Color color6 = ((Image)GameObject.Find("GetWeaponOverlay").GetComponent(typeof(Image))).color = color5;
				((CrosshairSizeScript)GameObject.Find("Crosshair").GetComponent(typeof(CrosshairSizeScript))).plus = 1.5f;
			}
			selectionScript.weaponinventory[weaponPickupScript.weaponcontent] = true;
		}
		if (selectionScript.ammoinventory[weaponPickupScript.weaponcontent] < selectionScript.maxammo[weaponPickupScript.weaponcontent])
		{
			selectionScript.ammoinventory[weaponPickupScript.weaponcontent] = selectionScript.ammoinventory[weaponPickupScript.weaponcontent] + (float)weaponPickupScript.ammocontent;
			((AudioSource)this.ammopickupsound.GetComponent(typeof(AudioSource))).Play();
			((TextMeshProUGUI)GameObject.Find("MessageText").GetComponent(typeof(TextMeshProUGUI))).text = weaponPickupScript.message;
			((ClearMessageAfterTime)GameObject.Find("MessageText").GetComponent(typeof(ClearMessageAfterTime))).timer = ((ClearMessageAfterTime)GameObject.Find("MessageText").GetComponent(typeof(ClearMessageAfterTime))).defaulttime;
			float a4 = 0.3f;
			Color color7 = ((Image)GameObject.Find("GetWeaponOverlay").GetComponent(typeof(Image))).color;
			float num4 = color7.a = a4;
			Color color8 = ((Image)GameObject.Find("GetWeaponOverlay").GetComponent(typeof(Image))).color = color7;
			((CrosshairSizeScript)GameObject.Find("Crosshair").GetComponent(typeof(CrosshairSizeScript))).plus = 1.5f;
			((Collider)ob.GetComponent(typeof(Collider))).enabled = false;
			UnityEngine.Object.Destroy(ob);
			if (selectionScript.ammoinventory[weaponPickupScript.weaponcontent] > selectionScript.maxammo[weaponPickupScript.weaponcontent])
			{
				selectionScript.ammoinventory[weaponPickupScript.weaponcontent] = selectionScript.maxammo[weaponPickupScript.weaponcontent];
			}
		}
	}

	// Token: 0x0600038A RID: 906 RVA: 0x00022008 File Offset: 0x00020208
	public virtual void PickUpSword(GameObject ob)
	{
		SelectionScript selectionScript = (SelectionScript)GameObject.Find("WeaponAnimator").GetComponent(typeof(SelectionScript));
		selectionScript.permdaikatana = true;
		selectionScript.havedaikatana = true;
		if (this.doautoswitch)
		{
			selectionScript.weapontogetto = 1;
		}
		selectionScript.lastselectedweapon = selectionScript.selectedweapon;
		((TextMeshProUGUI)GameObject.Find("MessageText").GetComponent(typeof(TextMeshProUGUI))).text = "You found the Sword";
		((ClearMessageAfterTime)GameObject.Find("MessageText").GetComponent(typeof(ClearMessageAfterTime))).timer = ((ClearMessageAfterTime)GameObject.Find("MessageText").GetComponent(typeof(ClearMessageAfterTime))).defaulttime;
		float a = 0.3f;
		Color color = ((Image)GameObject.Find("GetWeaponOverlay").GetComponent(typeof(Image))).color;
		float num = color.a = a;
		Color color2 = ((Image)GameObject.Find("GetWeaponOverlay").GetComponent(typeof(Image))).color = color;
		((CrosshairSizeScript)GameObject.Find("Crosshair").GetComponent(typeof(CrosshairSizeScript))).plus = 1.5f;
		((AudioSource)this.weaponpickupsound.GetComponent(typeof(AudioSource))).Play();
		((Animator)GameObject.Find("WeaponAnimator").GetComponent(typeof(Animator))).SetTrigger("StopAll");
		((Collider)ob.GetComponent(typeof(Collider))).enabled = false;
		UnityEngine.Object.Destroy(ob);
	}

	// Token: 0x0600038B RID: 907 RVA: 0x000221C4 File Offset: 0x000203C4
	public virtual void pickupholyhealth(GameObject ob)
	{
		PlayerHealthManagement playerHealthManagement = (PlayerHealthManagement)this.GetComponent(typeof(PlayerHealthManagement));
		playerHealthManagement.myhealth += (float)50;
		if (playerHealthManagement.myhealth > (float)200)
		{
			playerHealthManagement.myhealth = (float)200;
		}
		((TextMeshProUGUI)GameObject.Find("MessageText").GetComponent(typeof(TextMeshProUGUI))).text = "You found the Hallowed Health";
		((ClearMessageAfterTime)GameObject.Find("MessageText").GetComponent(typeof(ClearMessageAfterTime))).timer = ((ClearMessageAfterTime)GameObject.Find("MessageText").GetComponent(typeof(ClearMessageAfterTime))).defaulttime;
		float a = 0.6f;
		Color color = ((Image)GameObject.Find("GetPowerOverlay").GetComponent(typeof(Image))).color;
		float num = color.a = a;
		Color color2 = ((Image)GameObject.Find("GetPowerOverlay").GetComponent(typeof(Image))).color = color;
		((CrosshairSizeScript)GameObject.Find("Crosshair").GetComponent(typeof(CrosshairSizeScript))).plus = 1.5f;
		((AudioSource)this.holyhealthpickupsound.GetComponent(typeof(AudioSource))).Play();
		((Collider)ob.GetComponent(typeof(Collider))).enabled = false;
		UnityEngine.Object.Destroy(ob);
	}

	// Token: 0x0600038D RID: 909 RVA: 0x000224A8 File Offset: 0x000206A8
	public virtual void Main()
	{
	}

	// Token: 0x0400046C RID: 1132
	public GameObject weaponpickupsound;

	// Token: 0x0400046D RID: 1133
	public GameObject healthpickupsound;

	// Token: 0x0400046E RID: 1134
	public GameObject armorpickupsound;

	// Token: 0x0400046F RID: 1135
	public GameObject ammopickupsound;

	// Token: 0x04000470 RID: 1136
	public GameObject holyhealthpickupsound;

	// Token: 0x04000471 RID: 1137
	public GameObject keysound;

	// Token: 0x04000472 RID: 1138
	public LayerMask pickupblockinglayers;

	// Token: 0x04000473 RID: 1139
	public bool doautoswitch;
}
