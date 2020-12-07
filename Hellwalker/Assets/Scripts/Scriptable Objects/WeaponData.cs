using UnityEngine;

public enum WeaponType
{
    Pistol = 2,
 
    Shotgun = 3,

    MachineGun = 4,

    HuntingRifle = 6,
}

[CreateAssetMenu(fileName = "WeaponData", menuName = "Weapons/Weapon Data", order = 1)]
public class WeaponData : ScriptableObject
{
    // What kind of weapon is this ?
    public WeaponType weaponType;

    // Maximum ammo this weapon can have
    public int maxAmmo;

    // Is this weapon picked up by player ?
    public bool pickedUp;

    // Is this weapon equipped ?
    public bool equipped;

    // Tracer prefabs
    public GameObject tracerobject;

    // Token: 0x04000052 RID: 82
    public LayerMask hitlayers;

    // Token: 0x040000B2 RID: 178
    public GameObject bloodarcs;

    // Token: 0x040000B3 RID: 179
    public Color defaultparticlecolor;

    // Token: 0x040000B4 RID: 180
    public AudioClip defaulthitsound;

    // Token: 0x04000049 RID: 73
    public bool spawndust;

    // Token: 0x04000048 RID: 72
    public bool spawndecals;

    // Token: 0x040000B5 RID: 181
    public Color ragdollbloodcolor;

    // Token: 0x040000AF RID: 175
    public GameObject hitparticles;

    // Token: 0x04000054 RID: 84
    public LayerMask bloodsplatlayers;

    // References to prefabs
    public GameObject bulletholes;
    public GameObject dynamicbloodsplats;

    public float traceroffset;

    public MyInputManager inputmanager;


    public float FireSpeed;
    public float WeaponSpeed;
    public float AttackFrame;
    public float Buck;
    public float Inaccuracy;
    public float Damage;
    public int Pellet = 1;

    public bool pauseafterthrowing;
    public bool doattack;
    public bool didattack;

    public GameObject weaponPrefab;

    public GameObject weaponObject;

    public void Equip(Transform transformParent)
    {
        if(weaponObject)
        {
            weaponObject.SetActive(true);
            weaponObject.GetComponent<Weapon>().Reset();
            return;
        }
        // Create weapon prefab, as child of this transform
        weaponObject = Instantiate(weaponPrefab, transformParent);
    }

    public void Unequip()
    {
        if (weaponObject)
        {
            weaponObject.SetActive(false);
        }
    }

}
