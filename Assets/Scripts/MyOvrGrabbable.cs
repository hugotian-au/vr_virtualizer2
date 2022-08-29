using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MyOvrGrabbable : OVRGrabbable
{
    public GameObject soma_cubes;
    private GameObject timer;


    // Start is called before the first frame update
    void Start()
    {
        soma_cubes = GameObject.Find("condition1_and_condition2");
        timer = GameObject.Find("Timer");
    }

    override public void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        Debug.Log("DAGGASOFT: GRAB BEGIN");
        if (soma_cubes == null)
        {
            return;
        }

        //Your code goes here
        var pv = GetComponent<PhotonView>();
        pv.RequestOwnership();

        //var rb = GetComponent<Rigidbody>();
        //rb.isKinematic = false;
        //rb.detectCollisions = true;
        //rb.WakeUp();
        var script = timer.GetComponent<RecoredDurationTime>();
        script.hasStudyStarts = true;

        base.GrabBegin(hand, grabPoint); //pass attributes down to Super
    }

    override public void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        Debug.Log("DAGGASOFT: GRAB END");
        if (soma_cubes == null)
        {
            return;
        }

        //Your code goes here

        base.GrabEnd(linearVelocity, angularVelocity);
        var rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.detectCollisions = true;
        rb.WakeUp();
    }
}