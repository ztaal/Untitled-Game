using UnityEngine;

public class MeleeWeaponBehaviour : MonoBehaviour
{
    public WeaponScriptableObject weaponData;

    /** Current Stats */
    protected float currentDamage;
    protected float currentSpeed;
    protected float currentCooldownDuration;


    void Awake()
	{
        currentDamage = weaponData.Damage;
        currentSpeed = weaponData.Speed;
        currentCooldownDuration = weaponData.CooldownDuration;
    }

    protected virtual void Start()
    {
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
	{
        if ( col.CompareTag("Enemy") )
		{
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(currentDamage);
        }
	}
}
