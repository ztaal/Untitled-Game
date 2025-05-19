using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public CharacterScriptableObject characterData;

    // Current Stats
    float currentHealth;
    float currentRecovery;
    float currentMoveSpeed;
    float currentMight;
    float currentProjectileSpeed;

    // I-Frames
    [Header("I_Frames")]
    public float invincibilityDuration;
    float invincibilityTimer;
    bool isInvincible;

    // Experience 
    [Header("Experience/Level")]
    public int experience = 0;
    public int level = 1;
    public int experienceCap;

    // Class for level ranges
    [System.Serializable]
    public class LevelRange
    {
        public int startLevel;
        public int endLevel;
        public int experienceCapIncrease;
    }

    public List<LevelRange> levelRanges;
    private void Awake()
    {
        currentHealth = characterData.MaxHealth;
        currentRecovery = characterData.Recovery;
        currentMoveSpeed = characterData.MoveSpeed;
        currentMight = characterData.Might;
        currentProjectileSpeed = characterData.ProjectileSpeed;
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
}
