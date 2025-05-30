using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;

    /** Current Stats */
    [HideInInspector]
    public float currentMoveSpeed;
    [HideInInspector]
    public float currentHealth;
    [HideInInspector]
    public float currentDamage;

    public float despawnDistance = 150f;
    Transform player;

	public void Start()
    {
        player = FindAnyObjectByType<PlayerStats>().transform;
    }

	private void Update()
	{
		if ( Vector2.Distance(transform.position, player.position) >= despawnDistance )
		{
            ReturnEnemy();
		}
	}

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

	private void OnDestroy()
	{
        EnemySpawner es = FindAnyObjectByType<EnemySpawner>();
        if (es != null)
		{
            es.OnEnemyKilled();
		}
	}

    void ReturnEnemy()
    {
        EnemySpawner es = FindAnyObjectByType<EnemySpawner>();
        if (es != null)
        {
            Vector3 newPosition = es.GetRandomSpawnPosition();
            transform.position = player.position + newPosition;
        }
    }
}