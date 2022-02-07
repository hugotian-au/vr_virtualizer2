
using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace start
{
    public class VirtualizerScenePlayer : NetworkBehaviour
    {
        public NetworkVariableVector3 Position = new NetworkVariableVector3(new NetworkVariableSettings
        {
            WritePermission = NetworkVariablePermission.Everyone,
            ReadPermission = NetworkVariablePermission.Everyone
        });
        public NetworkVariableQuaternion Rotation = new NetworkVariableQuaternion(new NetworkVariableSettings
        {
            WritePermission = NetworkVariablePermission.Everyone,
            ReadPermission = NetworkVariablePermission.Everyone
        });

        private Vector3 prevPosition = new Vector3(0.0f, 0.0f, 0.0f);
        private Vector3 prevFinalPos = new Vector3(0.0f, 0.0f, 0.0f);
        private void Awake()
        {
            /*
            NetworkObject netObject = GetComponent<NetworkObject>();
            netObject.CheckObjectVisibility = ((clientId) => {
                // return true to show the object, return false to hide it
                if (IsLocalPlayer)
                {
                    // Only show the object to players that are within 5 meters. Note that this has to be rechecked by your own code
                    // If you want it to update as the client and objects distance change.
                    // This callback is usually only called once per client
                    print("This is local player");
                    return false;
                }
                else
                {
                    // Dont show this object
                    return true;
                }
            });
            */
        }

        public override void NetworkStart()
        {
            GameObject rso = GameObject.Find("RemoteScene");
            if (rso != null)
            {
                if (IsLocalPlayer)
                {
                    GameObject go = this.gameObject.transform.GetChild(0).gameObject;
                    if (go != null)
                    {
                        go.SetActive(false);
                    }
                   
                    this.gameObject.transform.parent = GameObject.Find("OVRCameraRig").transform;
                    this.gameObject.transform.localScale = new Vector3(0, 0, 0);
                }
                else
                {
                    GameObject go = this.gameObject.transform.GetChild(1).gameObject; ;
                    if (go != null)
                    {
                        go.SetActive(false);
                    }
                }
            }
            // Move();
        }



        [ServerRpc(RequireOwnership = false)]
        void GetRemotePlayerPositionServerRpc(ServerRpcParams rpcParams = default)
        {
            print("Get remote player's position from AR player!");
            Position.Value = transform.localPosition;
            print("Position.Value of VR player is: " + Position.Value);
        }

        [ClientRpc]
        void GetLocalPlayerPositionClientRpc(ClientRpcParams rpcParams = default)
        {
            // Don't need to implement at server side
        }

        [ServerRpc(RequireOwnership = false)]
        void GetRemotePlayerRotationServerRpc(ServerRpcParams rpcParams = default)
        {
            Rotation.Value = transform.localRotation;
        }

        [ClientRpc]
        void GetLocalPlayerRotationClientRpc(ClientRpcParams rpcParams = default)
        {
            // Don't need to implement at server side
        }

        void Update()
        {
            // transform.position = Position.Value;
            Animator anim;
            
            if (IsLocalPlayer)
            {
                Transform trans = GameObject.Find("OVRCameraRig").transform;
                transform.localPosition = new Vector3(trans.localPosition.x, 0, trans.localPosition.z);
                transform.localRotation = trans.localRotation;
            }
            else
            {
                GameObject remotePlayer = this.gameObject.transform.GetChild(0).gameObject;
                anim = remotePlayer.GetComponent(typeof(Animator)) as Animator;

                GetLocalPlayerPositionClientRpc();
                print("Position.Value of AR player is: " + Position.Value);
                // transform.Translate(Vector3.forward * 0.5);
                // transform.Translate(Position.Value);
                //transform.Translate(Vector3.forward * 0.5);
                transform.localPosition = Position.Value;
                // GetLocalPlayerRotationClientRpc();
                // transform.localRotation = Rotation.Value;
                Vector3 diffFinalPos = prevFinalPos - Position.Value;
                if (diffFinalPos != Vector3.zero)
                {
                    transform.LookAt(Position.Value);
                    prevFinalPos = Position.Value;
                }

                Vector3 diff = transform.localPosition - prevPosition;
                // anim.SetFloat("VerticalMov", Input.GetAxis("Vertical"));
                if (diff.x > 0.1f || diff.z > 0.1f || diff.x < -0.1f || diff.z < -0.1f)
                {
                    transform.LookAt(Position.Value);
                    anim.SetFloat("VerticalMov", 0.2f);
                }
                else
                {
                    anim.SetFloat("VerticalMov", 0.0f);
                }
                // anim.SetFloat("HorizontalMov", Input.GetAxis("Horizontal"));

                prevPosition = Position.Value;
            }
        }
    }
}