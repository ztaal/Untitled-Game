using UnityEngine;

public class BoltController : WeaponController
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedBolt = Instantiate(prefab);
        spawnedBolt.transform.position = transform.position; // Assign the position to be the same as this object which is paraented to the player
        spawnedBolt.GetComponent<BoltBehaviour>().DirectionChecker(pm.lastMovedVector);
    }
}
