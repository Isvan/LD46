using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDespawn : MonoBehaviour
{
    public string Tag;
    public float interval;
    public bool destroyTagObject;
    public bool destroySelf;
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag(Tag))
        {
            if (destroyTagObject == true)
            {

                Destroy(collision.gameObject, interval);
            }


            if (destroySelf == true)
            {
                Destroy(gameObject, interval);
            }
        }
    }
}
