using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    CharacterScriptableObject characterData;

    /** Current Stats */
    [HideInInspector]
    public float currentHealth;
    [HideInInspector]
    public float currentRecovery;
    [HideInInspector]
    public float currentMoveSpeed;
    [HideInInspector]
    public float currentMight;
    [HideInInspector]
    public float currentProjectileSpeed;
    [HideInInspector]
    public float currentPickupRadius;

    /** I-Frames */
    [Header("I_Frames")]
    public float invincibilityDuration;
    float invincibilityTimer;
    bool isInvincible;

    /** Experience */
    [Header("Experience/Level")]
    public int experience = 0;
    public int level = 1;
    public int experienceCap;

    /** Class for level ranges */
    [System.Serializable]
    public class LevelRange
    {
        public int startLevel;
        public int endLevel;
        public int experienceCapIncrease;
    }

    public List<LevelRange> levelRanges;

    /** Inventory */
    InventoryManager inventory;
    public int weaponIndex;
    public int passiveItemIndex;

    private void Awake()
    {
        /** Get Character Data */
        characterData = CharacterSelector.GetData();
        CharacterSelector.instance.DestroySingleton();

        /** Set Inventory. */
        inventory = GetComponent<InventoryManager>();

        /** Set Variables */
        currentHealth = characterData.MaxHealth;
        currentRecovery = characterData.Recovery;
        currentMoveSpeed = characterData.MoveSpeed;
        currentMight = characterData.Might;
        currentProjectileSpeed = characterData.ProjectileSpeed;
        currentPickupRadius = characterData.PickupRadius;

        /** Set Starting Weapon */
        EquipWeapon(characterData.StartingWeapon);
    }

    void Start()
    {
        experienceCap = levelRanges[0].experienceCapIncrease;
    }

    void Update()
    {
        /** Update I-Frames Timer */
        if ( invincibilityTimer > 0 )
        {
            invincibilityTimer -= Time.deltaTime;
        } else if ( isInvincible ) {
            isInvincible = false;
        }

        /** Regen Health */
        recover();
    }

    public void IncreaseExperience(int amount)
    {
        experience += amount;
        LevelUpChecker();
    }

    void LevelUpChecker()
    {
        if ( experience >= experienceCap )
        {
            level++;
            experience -= experienceCap;

            int experienceCapIncrease = 0;
            foreach ( LevelRange range in levelRanges )
            {
                if ( level >= range.startLevel && level <= range.endLevel )
                {
                    experienceCapIncrease = range.experienceCapIncrease;
                    break;
                }
            }
            experienceCap += experienceCapIncrease;
        }
    }
    public void RestoreHealth(float amount)
    {
        if ( currentHealth < characterData.MaxHealth )
        {
            /** Heal */
            currentHealth += amount;

            if ( currentHealth > characterData.MaxHealth )
            {
                currentHealth = characterData.MaxHealth;
            }
        }
    }

    void recover()
    {
        if ( currentHealth < characterData.MaxHealth )
        {
            currentHealth += currentRecovery * Time.deltaTime;
            if ( currentHealth > characterData.MaxHealth )
            {
                currentHealth = characterData.MaxHealth;
            }
        }
    }

    public void TakeDamage(float dmg)
    {
        /** Check for I-Frames. */
        if ( !isInvincible )
        {
            /** Take damage */
            currentHealth -= dmg;

            /** Start I-Frames */
            invincibilityTimer = invincibilityDuration;
            isInvincible = true;

            /** Kill if health gets too low */
            if ( currentHealth <= 0 )
            {
                Kill();
            }
        }
    }

    public void Kill()
    {
        /** \todo add death here later. */
        Debug.Log("YOU DIED");
    }

    public void EquipWeapon(GameObject weapon)
    {
        /** Check if the maximum number of weapons have already been equiped. */
        if ( weaponIndex >= inventory.weaponSlots.Count - 1 )
        {
            Debug.Log("Inventory is full, cannot add anymore weapons.");
            return;
        }

        GameObject newWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
        newWeapon.transform.SetParent(transform);
        inventory.AddWeapon(weaponIndex, newWeapon.GetComponent<WeaponController>());
        weaponIndex++;
    }
    public void EquipPassiveItem(GameObject passiveItem)
    {
        /** Check if the maximum number of passive items have already been equiped. */
        if (passiveItemIndex >= inventory.passiveItemsSlots.Count - 1)
        {
            Debug.Log("Inventory is full, cannot add anymore passive items.");
            return;
        }

        GameObject newPassiveItem = Instantiate(passiveItem, transform.position, Quaternion.identity);
        newPassiveItem.transform.SetParent(transform);
        inventory.AddPassiveItem(weaponIndex, newPassiveItem.GetComponent<PassiveItem>());
        passiveItemIndex++;
    }
}
