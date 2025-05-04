using UnityEngine;

public class ProjectileWeaponBehaviour : MonoBehaviour
{
    protected Vector3 direction;
    public float lifeTime;

    protected virtual void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    public void DirectionChecker(Vector3 dir)
    {
        direction = dir;

        float dirX = direction.x;
        float dirY = direction.y;

        Vector3 scale = transform.localScale;
        Vector3 rotation = transform.rotation.eulerAngles;

        if ( dirX < 0 && dirY == 0 ) // left
        {
            scale.x *= -1;
            scale.y *= -1;
        } 
        else if ( dirX == 0 &&  dirY < 0 ) // down
        {
            scale.y *= -1;
            rotation.z = 90f;
        }
        else if (dirX == 0 && dirY > 0) // up
        {
            scale.x *= -1;
            rotation.z = -90f;
        }
        else if (dirX > 0 && dirY > 0) // right up
        {
            rotation.z = 45f;
        }
        else if (dirX > 0 && dirY < 0) // right down
        {
            rotation.z = -45f;
        }
        else if (dirX < 0 && dirY > 0) // left up
        {
            scale.x *= -1;
            scale.y *= -1;
            rotation.z = -225f;
        }
        else if (dirX < 0 && dirY < 0) // left down
        {
            scale.x *= -1;
            scale.y *= -1;
            rotation.z = -315f;
        }

        transform.localScale = scale;
        transform.rotation = Quaternion.Euler(rotation);
    }
}
