using UnityEngine;

public class Experience : Pickup
{
    public int experienceGranted;

    override public void Collect()
    {
        PlayerStats player = FindAnyObjectByType<PlayerStats>();
        player.IncreaseExperience(experienceGranted);
    }
}