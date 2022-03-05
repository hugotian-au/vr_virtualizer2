using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MySceneObjectManager : MonoBehaviourPunCallbacks
{
    [Tooltip("The prefab to use for representing scene object located in the room")]
    public GameObject sceneObject1;


    // Start is called before the first frame update
    void Start()
    {
        // PhotonNetwork.SetMasterClient(PhotonNetwork.MasterClient.GetNext());
        if (!PhotonNetwork.IsMasterClient)
        {
            var sceneobject_name1 = this.sceneObject1.name + "(Clone)";
            var scene_object1 = GameObject.Find(sceneobject_name1);
            if (scene_object1 == null)
            {
                var pv = GetComponent<PhotonView>();
                pv.RPC("RPC_InstantiateSceneObject", RpcTarget.All,
                    this.sceneObject1.name, new Vector3(3f, 5f, 3f), Quaternion.identity);
                // Instantiate(sceneObject1, new Vector3(0, 0, 0), Quaternion.identity);
            }
        }
    }

    [PunRPC]
    void RPC_InstantiateSceneObject(string name, Vector3 position, Quaternion rotation)
    {
        PhotonNetwork.InstantiateSceneObject(name, position, rotation, 0);
    }
}
