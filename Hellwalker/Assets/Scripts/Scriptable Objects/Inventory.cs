using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InventoryStatus
{
    NONE,
    ERROR,
    WEAPON_NOT_IN_INVENTORY,
    WEAPON_IS_IN_INVENTORY,
}

public class InventoryMessage
{
    public InventoryStatus status;
    private WeaponData weaponData;

    public InventoryMessage(InventoryStatus status, WeaponData weaponData = null)
    {
        this.status = status;
        this.weaponData = weaponData;
    }

    public WeaponData GetWeapon()
    {
        return weaponData;
    }
}

[CreateAssetMenu(fileName = "Inventory", menuName = "Weapons/Inventory", order = 1)]
public class Inventory : ScriptableObject
{
    public WeaponData[] weapons;

    public InventoryMessage GetWeapon(WeaponType desiredWeapon)
    {
        // Get the correct weapon data in inventory
        foreach (WeaponData weapon in weapons)
        {
            // If weapon is marked as picked up, start the euip process
            if (weapon.weaponType == desiredWeapon && weapon.pickedUp)
            {
                return new InventoryMessage(InventoryStatus.WEAPON_IS_IN_INVENTORY,weapon);
            }
        }

        return new InventoryMessage(InventoryStatus.WEAPON_NOT_IN_INVENTORY, null);
    }
}
