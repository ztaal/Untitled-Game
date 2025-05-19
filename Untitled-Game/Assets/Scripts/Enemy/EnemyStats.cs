using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;

    /** Current Stats */
    float currentMoveSpeed;
    float currentHealth;
    float currentDamage;

    void Awake()
	{
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth = enemyData.MaxHealth;
        currentDamage = enemyData.Damage;
    }

    public void TakeDamage(float dmg)
	{
        currentHealth -= dmg;

        if ( currentHealth <= 0)
		{
            Kill();
		}
	}

    public void Kill()
	{
        Destroy(gameObject);
	}

    private void OnCollisionStay2D(Collision2D collision)
    {
        if ( collision.gameObject.CompareTag("Player") )
        {
            PlayerStats player = collision.gameObject.GetComponent<PlayerStats>();
            player.TakeDamage(currentDamage);
        }
    }
}