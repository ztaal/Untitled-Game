using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /** Check if the object has the ICollectible interface. */
        if ( collision.gameObject.TryGetComponent(out iCollectible collectible))
        {
            /** Collect the item. */
            collectible.Collect();
        }
    }
}
