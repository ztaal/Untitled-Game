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

    public float GetCurrentDamage()
    {
        return currentDamage *= FindAnyObjectByType<PlayerStats>().currentMight;
    }

    protected virtual void Start()
    {
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
	{
        if ( col.CompareTag("Enemy") )
		{
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(GetCurrentDamage());
        }
        else if (col.CompareTag("Prop"))
        {
            if (col.gameObject.TryGetComponent(out BreakableProps breakable) ) {
                breakable.TakeDamage(GetCurrentDamage());
            }
        }
    }
}
