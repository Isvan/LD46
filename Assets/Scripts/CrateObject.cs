using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateObject : PhysicsObject
{
    private Vector3 staticRotation;

    // Start is called before the first frame update
    void Start()
    {
        staticRotation = this.transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        this.transform.eulerAngles = staticRotation;
    }
}
