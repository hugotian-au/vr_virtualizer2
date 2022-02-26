﻿using System.Collections;
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
            var pv = GetComponent<PhotonView>();
            pv.RPC("RPC_InstantiateSceneObject", RpcTarget.All,
                this.sceneObject1.name, new Vector3(0f, 0f, 0f), Quaternion.identity);
        }
    }

    [PunRPC]
    void RPC_InstantiateSceneObject(string name, Vector3 position, Quaternion rotation)
    {
        PhotonNetwork.InstantiateSceneObject(name, position, rotation, 0);
    }
}
