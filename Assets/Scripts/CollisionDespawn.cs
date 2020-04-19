using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDespawn : MonoBehaviour
{
    // Start is called before the first frame update
    public string Tag;
    public float interval;
    private void OnCollisionEnter(Collision collision)
    {
        // You probably want a check here to make sure you're hitting a zombie
        // Note that this is not the best method for doing so.
        if (collision.gameObject.tag == Tag)
        {
            Destroy(gameObject, interval);
        }
    }
}
