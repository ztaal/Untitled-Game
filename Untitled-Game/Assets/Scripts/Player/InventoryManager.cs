using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public List<WeaponController> weaponSlots = new List<WeaponController>(6);
    public int[] weaponLevels = new int[6];
    public List<Image> weaponUISlots = new List<Image>(6);
    public List<PassiveItem> passiveItemsSlots = new List<PassiveItem>(6);
    public int[] passiveItemsLevels = new int[6];
    public List<Image> passiveItemUISlots = new List<Image>(6);

    public void AddWeapon(int slotIndex, WeaponController weapon)
    {
        weaponSlots[slotIndex] = weapon;
        weaponLevels[slotIndex] = weapon.weaponData.Level;
        weaponUISlots[slotIndex].enabled = true;
        weaponUISlots[slotIndex].sprite = weapon.weaponData.Icon;
    }
    public void AddPassiveItem(int slotIndex, PassiveItem passiveItem)
    {
        passiveItemsSlots[slotIndex] = passiveItem;
        passiveItemsLevels[slotIndex] = passiveItem.passiveItemData.Level;
        passiveItemUISlots[slotIndex].enabled = true;
        passiveItemUISlots[slotIndex].sprite = passiveItem.passiveItemData.Icon;
    }

    public void LevelUpWeapon(int slotIndex)
    {
        if ( weaponSlots.Count > slotIndex )
        {
            /** Get Weapon */
            WeaponController weapon = weaponSlots[slotIndex];

            /** Check if it is possible to level up the weapon. */
            if ( !weapon.weaponData.NextLevelPrefab )
            {
                Debug.LogError("No Next Level Set For " + weapon.name);
                return;
            }

            /** Level up weapon and destroy old weapon */
            GameObject upgradedWeapon = Instantiate(weapon.weaponData.NextLevelPrefab, transform.position, Quaternion.identity);
            upgradedWeapon.transform.SetParent(transform); // Set weapon to be a child of the player
            AddWeapon(slotIndex, upgradedWeapon.GetComponent<WeaponController>());
            Destroy(weapon.gameObject);
            weaponLevels[slotIndex] = upgradedWeapon.GetComponent<WeaponController>().weaponData.Level;
        }
    }

    public void LevelUpPassiveItem(int slotIndex)
    {
        if (passiveItemsSlots.Count > slotIndex)
        {
            /** Get Passive Item */
            PassiveItem passiveItem = passiveItemsSlots[slotIndex];

            /** Check if it is possible to level up the weapon. */
            if (!passiveItem.passiveItemData.NextLevelPrefab)
            {
                Debug.LogError("No Next Level Set For " + passiveItem.name);
                return;
            }

            /** Level up passive item and destroy old passive item */
            GameObject upgradedPassiveItem = Instantiate(passiveItem.passiveItemData.NextLevelPrefab, transform.position, Quaternion.identity);
            upgradedPassiveItem.transform.SetParent(transform); // Set weapon to be a child of the player
            AddPassiveItem(slotIndex, upgradedPassiveItem.GetComponent<PassiveItem>());
            Destroy(passiveItem.gameObject);
            weaponLevels[slotIndex] = upgradedPassiveItem.GetComponent<PassiveItem>().passiveItemData.Level;
        }
    }
}