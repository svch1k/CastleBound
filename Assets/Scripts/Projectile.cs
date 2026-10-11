using UnityEngine;

public class Projectile : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Player" && collision.gameObject.tag != "Arrow")
        {
            Destroy(gameObject);
        }
    }

}
