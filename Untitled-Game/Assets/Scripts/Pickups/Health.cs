using UnityEngine;

public class Health : Pickup
{
    public int healthToRestore;

    override public void Collect()
    {
        PlayerStats player = FindAnyObjectByType<PlayerStats>();
        player.RestoreHealth(healthToRestore);
    }
}