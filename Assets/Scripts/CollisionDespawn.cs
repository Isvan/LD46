using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDespawn : MonoBehaviour
{
    public string Tag;
    public float interval;
    public bool destroyTagObject;
    private void OnCollisionEnter(Collision collision)
    {
  
        if (collision.gameObject.CompareTag(Tag) && destroyTagObject == false)
        {
            Destroy(gameObject, interval);
        }else if(destroyTagObject == true)
        {
            Destroy(collision.gameObject, interval);
        }
    }
}
