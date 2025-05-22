using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    PlayerStats player;
    CircleCollider2D playerCollector;
    public float pullSpeed;
    private List<GameObject> collectibles = new List<GameObject>();

    private void Start()
    {
        player = FindAnyObjectByType<PlayerStats>();
        playerCollector = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        /** Update the collider radius based on player stats */
        playerCollector.radius = player.currentPickupRadius;

        /** Loop through all object that are currently being collected */
        for (int i = collectibles.Count - 1; i >= 0; i--)
        {
            GameObject collectibleObject = collectibles[i];
            if (collectibleObject != null )
            {
                /** Move the collectible towards the player */
                Vector2 direction = (player.transform.position - collectibleObject.transform.position).normalized;
                collectibleObject.transform.position += (Vector3)direction * pullSpeed * Time.deltaTime;
            }
            else
            {
                /** Remove collectible from the list once it has been collected (this happens in the Pickup script). */
                collectibles.RemoveAt(i);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /** Check if the object has the ICollectible interface. */
        if (collision.gameObject.TryGetComponent(out iCollectible collectible))
        {
            /** Add item to list of items to collect when they reach the player. */
            collectibles.Add(collision.gameObject);
        }
    }
}
