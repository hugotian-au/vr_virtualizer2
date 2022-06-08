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
            transform.localPosition = new Vector3(0.008f, -0.174f, 0.08f);
        }
        else if (gameObject.name == "ak_content_1")
        {
            transform.localPosition = new Vector3(0.05f, -0.135f, 0.055f);
        }
        else if(gameObject.name == "ak_content_2")
        {
            transform.localPosition = new Vector3(0.012f, -0.133f, -0.044f);
            transform.localRotation = Quaternion.Euler(0.009000001f, 0.433f, 0.317f);
}


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
