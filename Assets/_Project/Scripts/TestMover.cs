using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMover : MonoBehaviour
{
    public Rigidbody2D myRigid;

    void Update()
    {
        if(myRigid != null)
        {
            myRigid.velocity = Vector2.right * 3;
        }
        else
        {
            transform.position += Vector3.right * Time.deltaTime;
        }
    }
}
