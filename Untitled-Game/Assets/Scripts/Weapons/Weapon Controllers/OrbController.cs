using UnityEngine;

public class OrbController : WeaponController
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        GameObject spawnedOrb = Instantiate(weaponData.Prefab);
        spawnedOrb.transform.position = transform.position; // Assign the position to be the same as this object which is paraented to the player
    }

    protected override void Attack()
    {
        base.Attack();
    }
}