using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MyInitBlockPosition : MonoBehaviour
{
    public Vector3 blockPosition1 = new Vector3(0.549f, 1.3f, -1.345f);
    public Quaternion blockRotatoin1 = Quaternion.Euler(-83.369f, 185.838f, -10.511f);

    public Vector3 blockPosition2 = new Vector3(0.897f, 1.298f, -1.365f);
    public Quaternion blockRotatoin2 = Quaternion.Euler(-90.00001f, 0f, -181.849f);
    
    public Vector3 blockPosition3 = new Vector3(1.222f, 1.283f, -1.342f);
    public Quaternion blockRotatoin3 = Quaternion.Euler(0f, -87.826f, 0f);

    public Vector3 blockPosition4 = new Vector3(0.746f, 1.653f, -1.199f);
    public Quaternion blockRotatoin4 = Quaternion.Euler(0f, 93.69401f, 0f);

    public Vector3 blockPosition5 = new Vector3(0.94f, 1.64f, -1.191f);
    public Quaternion blockRotatoin5 = Quaternion.Euler(0f, 0f, -93.97201f);

    public Vector3 blockPosition6 = new Vector3(1.231f, 1.636f, -1.221f);
    public Quaternion blockRotatoin6 = Quaternion.Euler(0f, 92.313f, 0f);

    public Vector3 blockPosition7 = new Vector3(0.615f, 0.964f, -1.183f);
    public Quaternion blockRotatoin7 = Quaternion.Euler(2.963f, 89.75401f, 180.153f);

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
