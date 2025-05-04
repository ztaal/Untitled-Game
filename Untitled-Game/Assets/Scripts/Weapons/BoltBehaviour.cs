using UnityEngine;

public class BoltBehaviour : ProjectileWeaponBehaviour
{
    BoltController bc;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        bc = FindObjectOfType<BoltController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * bc.speed * Time.deltaTime;
    }
}
