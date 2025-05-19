using UnityEngine;

public class BreakableProps : MonoBehaviour
{
    public float health;

    public void TakeDamage(float dmg)
    {
        health -= dmg;

        if ( health <= 0 )
        {
            Kill();
        }
    }

    void Kill()
    {
        Destroy(gameObject);
    }
}
