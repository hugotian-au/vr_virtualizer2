using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyLiveObjectParentPosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.name == "ak_content_0")
        {
            transform.localPosition = new Vector3(-0.03f, -0.15f, 0.03f);
        }
        else if (gameObject.name == "ak_content_1")
        {
            transform.localPosition = new Vector3(-0.03f, -0.152f, 0.02f);
        }
        else if(gameObject.name == "ak_content_2")
        {
            transform.localPosition = new Vector3(-0.039f, -0.133f, -0.002f);
            transform.localRotation = Quaternion.Euler(0.009000001f, 0.433f, 0.317f);
}


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
