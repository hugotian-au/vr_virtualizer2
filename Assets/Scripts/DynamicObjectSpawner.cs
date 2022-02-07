using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class DynamicObjectSpawner : MonoBehaviour
{
    public GameObject myPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("Space is pressed!");
            // if (Input.GetKeyDown(KeyCode.A))
            {
                GameObject go = Instantiate(myPrefab, Vector3.zero, Quaternion.identity);
                go.GetComponent<NetworkObject>().Spawn();
            }
        }
    }
}
