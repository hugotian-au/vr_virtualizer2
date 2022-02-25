using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MySceneObjectManager : MonoBehaviour
{
    [Tooltip("The prefab to use for representing scene object located in the room")]
    public GameObject sceneObject1;


    // Start is called before the first frame update
    void Start()
    {
        // PhotonNetwork.InstantiateSceneObject(this.sceneObject1.name, new Vector3(0f, 0f, 0f), Quaternion.identity, 0);
    }
}
