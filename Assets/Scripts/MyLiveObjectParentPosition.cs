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
            transform.localPosition = new Vector3(-0.08f, 0.04f, 0.0f);
        }
        else if (gameObject.name == "ak_content_1")
        {
            transform.localPosition = new Vector3(-0.03f, -0.03f, 0.02f);
        }
        else if(gameObject.name == "ak_content_2")
        {
            transform.localPosition = new Vector3(0.03f, 0.02f, -0.002f);
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
