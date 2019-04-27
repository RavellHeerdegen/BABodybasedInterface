using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public bool isCollidingWithHand;

    public bool getIsColliding()
    {
        return this.isCollidingWithHand;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.transform.tag == "RightHand")
        {
            isCollidingWithHand = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.transform.tag == "RightHand")
        {
            isCollidingWithHand = false;
        }
    }
}
