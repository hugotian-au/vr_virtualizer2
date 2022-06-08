using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MyInitBlockPosition : MonoBehaviour
{
    private Vector3 blockPosition1 = new Vector3(0.5475666f, 1.399917f, -1.067f);
    private Quaternion blockRotatoin1 = Quaternion.Euler(-91f, 59.59f, -62.083f);

    private Vector3 blockPosition2 = new Vector3(0.932f, 1.198f, -1.311f);
    private Quaternion blockRotatoin2 = Quaternion.Euler(-90.00001f, 0f, -181.849f);

    private Vector3 blockPosition3 = new Vector3(1.264f, 1.186f, -1.297f);
    private Quaternion blockRotatoin3 = Quaternion.Euler(0f, -87.826f, 0f);

    private Vector3 blockPosition4 = new Vector3(0.781f, 1.533f, -1.173f);
    private Quaternion blockRotatoin4 = Quaternion.Euler(0f, 93.69401f, 0f);

    private Vector3 blockPosition5 = new Vector3(0.964f, 1.547f, -1.162f);
    private Quaternion blockRotatoin5 = Quaternion.Euler(0f, 0f, -93.97201f);

    private Vector3 blockPosition6 = new Vector3(1.256f, 1.577f, -1.183f);
    private Quaternion blockRotatoin6 = Quaternion.Euler(0f, 92.313f, 0f);

    private Vector3 blockPosition7 = new Vector3(0.609f, 0.826f, -1.285f);
    private Quaternion blockRotatoin7 = Quaternion.Euler(2.228f, 180.2351f, 181.38f);

    private bool updated = false;
    // Start is called before the first frame update
    void Start()
    {
        // var pv = GetComponent<PhotonView>();
        // pv.RequestOwnership();

        if (gameObject.name == "block1(Clone)")
        {
            transform.localPosition = blockPosition1;
            transform.localRotation = blockRotatoin1;
        }
        else if (gameObject.name == "block2(Clone)")
        {
            transform.localPosition = blockPosition2;
            transform.localRotation = blockRotatoin2;
        }
        else if (gameObject.name == "block3(Clone)")
        {
            transform.localPosition = blockPosition3;
            transform.localRotation = blockRotatoin3;
        }
        else if (gameObject.name == "block4(Clone)")
        {
            transform.localPosition = blockPosition4;
            transform.localRotation = blockRotatoin4;
        }
        else if (gameObject.name == "block5(Clone)")
        {
            transform.localPosition = blockPosition5;
            transform.localRotation = blockRotatoin5;
        }
        else if (gameObject.name == "block6(Clone)")
        {
            transform.localPosition = blockPosition6;
            transform.localRotation = blockRotatoin6;
        }
        else if (gameObject.name == "block7(Clone)")
        {
            transform.localPosition = blockPosition7;
            transform.localRotation = blockRotatoin7;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!updated)
        {
            if (gameObject.name == "block1(Clone)")
            {
                transform.localPosition = blockPosition1;
                transform.localRotation = blockRotatoin1;
            }
            else if (gameObject.name == "block2(Clone)")
            {
                transform.localPosition = blockPosition2;
                transform.localRotation = blockRotatoin2;
            }
            else if (gameObject.name == "block3(Clone)")
            {
                transform.localPosition = blockPosition3;
                transform.localRotation = blockRotatoin3;
            }
            else if (gameObject.name == "block4(Clone)")
            {
                transform.localPosition = blockPosition4;
                transform.localRotation = blockRotatoin4;
            }
            else if (gameObject.name == "block5(Clone)")
            {
                transform.localPosition = blockPosition5;
                transform.localRotation = blockRotatoin5;
            }
            else if (gameObject.name == "block6(Clone)")
            {
                transform.localPosition = blockPosition6;
                transform.localRotation = blockRotatoin6;
            }
            else if (gameObject.name == "block7(Clone)")
            {
                transform.localPosition = blockPosition7;
                transform.localRotation = blockRotatoin7;
            }

            updated = true;
        }
    }
}
