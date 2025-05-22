using UnityEngine;

public class Pickup : MonoBehaviour, iCollectible
{
    virtual public void Collect() { }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Collect();
            Destroy(gameObject);
        }
    }
}
