using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class OrbBehaviour : MeleeWeaponBehaviour
{
    protected PlayerMovement pm;
    public float orbitDistance;
    bool canDealDamage = false;
    float currentCooldown;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        pm = FindFirstObjectByType<PlayerMovement>();
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = pm.transform.position + (transform.position - pm.transform.position).normalized * orbitDistance;
        transform.RotateAround(pm.transform.position, Vector3.forward, currentSpeed * Time.deltaTime);
       
        currentCooldown -= Time.deltaTime;
        if (currentCooldown <= 0f)
        {
            canDealDamage = true;
        }
    }
    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if ( col.CompareTag("Enemy") && canDealDamage == true )
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(currentDamage);

            /** Reset when the weapon can deal damage. */
            canDealDamage = false;
            currentCooldown = weaponData.CooldownDuration;
        }
        else if (col.CompareTag("Prop") && canDealDamage == true )
        {
            if (col.gameObject.TryGetComponent(out BreakableProps breakable) ) {
                breakable.TakeDamage(currentDamage);
            }
        }
    }
}