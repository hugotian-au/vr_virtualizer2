using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MyOvrGrabbable : OVRGrabbable
{

    override public void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        Debug.Log("DAGGASOFT: GRAB BEGIN");

        //Your code goes here
        var pv = GetComponent<PhotonView>();
        pv.RequestOwnership();

        //var rb = GetComponent<Rigidbody>();
        //rb.isKinematic = false;
        //rb.detectCollisions = true;
        //rb.WakeUp();

        base.GrabBegin(hand, grabPoint); //pass attributes down to Super
    }

    override public void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        Debug.Log("DAGGASOFT: GRAB END");

        //Your code goes here

        base.GrabEnd(linearVelocity, angularVelocity);
        var rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.detectCollisions = true;
        rb.WakeUp();
    }
}