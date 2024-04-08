using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockFragment : InanimateObject
{
    public void FragmentAddForce(Vector2 forceValue)
    {
        localRigidbody.AddForce(forceValue);
    }

    public void FragmentSetRotation(float rotationValue)
    {
        localRigidbody.rotation = rotationValue;
    }

    public void FragmentSetVelocity(Vector2 velocityValue)
    {
        localRigidbody.velocity = velocityValue;
    }
}
