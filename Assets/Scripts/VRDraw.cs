﻿using UnityEngine;
using System.Collections.Generic;
using DilmerGames.Enums;
using DilmerGames.Core.Utilities;
using Photon.Pun;

namespace DilmerGames
{
    public class VRDraw : MonoBehaviourPunCallbacks, IPunObservable
    {   
        private int current_index = 0;
        private Vector3 trackPosition;
        private int numCapVectices;
        private Vector3 linePosition;
        private Vector3 cameraPosition;


        [SerializeField]
        private ControlHand controlHand = ControlHand.NoSet;

        private GameObject objectToTrackMovement;

        private Vector3 prevPointDistance = Vector3.zero;

        [SerializeField, Range(0, 1.0f)]
        private float minDistanceBeforeNewPoint = 0.2f;

        [SerializeField, Range(0, 1.0f)]
        private float minDrawingPressure = 0.8f;

        [SerializeField, Range(0, 1.0f)]
        private float lineDefaultWidth = 0.010f;

        private int positionCount = 0; // 2 by default

        private List<LineRenderer> lines = new List<LineRenderer>();

        private LineRenderer currentLineRender;

        [SerializeField]
        private Color defaultColor = Color.white;

        public GameObject editorObjectToTrackMovement;

        [SerializeField]
        private bool allowEditorControls = true;

        [SerializeField]
        private VRControllerOptions vrControllerOptions;
        
        public VRControllerOptions VRControllerOptions => vrControllerOptions;
        
        void Awake() 
        {
        }

        void Start()
        {
            if (gameObject.name == "VRDrawLeft(Clone)")
            {
                var trackObject = GameObject.Find("OculusTouchForQuestAndRiftS_Left");
                objectToTrackMovement = trackObject;
            }
            if (gameObject.name == "VRDrawRight(Clone)")
            {
                var trackObject = GameObject.Find("OculusTouchForQuestAndRiftS_Right");
                objectToTrackMovement = trackObject;
            }

            AddNewLineRenderer();
        }

        void AddNewLineRenderer()
        {
            var pv = GetComponent<PhotonView>();
            pv.RequestOwnership();

            positionCount = 0;
            GameObject go = new GameObject($"LineRenderer_{controlHand.ToString()}_{lines.Count}");
            go.transform.parent = objectToTrackMovement.transform.parent;
            go.transform.position = objectToTrackMovement.transform.position;
            LineRenderer goLineRenderer = go.AddComponent<LineRenderer>();
            goLineRenderer.startWidth = lineDefaultWidth;
            goLineRenderer.endWidth = lineDefaultWidth;
            goLineRenderer.useWorldSpace = true;
            goLineRenderer.material = MaterialUtils.CreateMaterial(defaultColor, $"Material_{controlHand.ToString()}_{lines.Count}");
            goLineRenderer.positionCount = 1;
            goLineRenderer.numCapVertices = 90;
            goLineRenderer.SetPosition(0, objectToTrackMovement.transform.position);

            // send position
            // TCPControllerClient.Instance.AddNewLine(objectToTrackMovement.transform.position);

            currentLineRender = goLineRenderer;
            lines.Add(goLineRenderer);
            current_index++;
            positionCount = 1;
            numCapVectices = 90;
            trackPosition = objectToTrackMovement.transform.position;
        }

        void Update()
        {
    #if !UNITY_EDITOR
            // primary left controller
            if(controlHand == ControlHand.Left && OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > minDrawingPressure)
            {
                //VRStats.Instance.firstText.text = $"Axis1D.PrimaryIndexTrigger: {OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger)}";
                UpdateLine();
            }
            else if(controlHand == ControlHand.Left && OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
            {
                //VRStats.Instance.secondText.text = $"Button.PrimaryIndexTrigger: {Time.deltaTime}";
                AddNewLineRenderer();
            }

            // secondary right controller
            if(controlHand == ControlHand.Right && OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > minDrawingPressure)
            {
                //VRStats.Instance.firstText.text = $"Axis1D.SecondaryIndexTrigger: {OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger)}";
                UpdateLine();
            }
            else if(controlHand == ControlHand.Right && OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger))
            {
                //VRStats.Instance.secondText.text = $"Button.SecondaryIndexTrigger: {Time.deltaTime}";
                AddNewLineRenderer();
            }

    #endif

    #if UNITY_EDITOR
            // if(!allowEditorControls) return;

            // left controller
            if(controlHand == ControlHand.Left && Input.GetKey(KeyCode.K))
            {
                // VRStats.Instance.firstText.text = $"Input.GetKey(KeyCode.K) {Input.GetKey(KeyCode.K)}";
                UpdateLine();
            }
            else if(controlHand == ControlHand.Left && Input.GetKeyUp(KeyCode.K))
            {
                // VRStats.Instance.secondText.text = $"Input.GetKeyUp(KeyCode.K) {Input.GetKeyUp(KeyCode.K)}";
                AddNewLineRenderer();
            }

            // right controller
            if(controlHand == ControlHand.Right && Input.GetKey(KeyCode.L))
            {
                // VRStats.Instance.firstText.text = $"Input.GetKey(KeyCode.L): {Input.GetKey(KeyCode.L)}";
                UpdateLine();
            }
            else if(controlHand == ControlHand.Right && Input.GetKeyUp(KeyCode.L))
            {
                // VRStats.Instance.secondText.text = $"Input.GetKeyUp(KeyCode.L): {Input.GetKeyUp(KeyCode.L)}";
                AddNewLineRenderer();
            }
    #endif

        }

        void UpdateLine()
        {
            if(prevPointDistance == null)
            {
                prevPointDistance = objectToTrackMovement.transform.position;
            }

            if(prevPointDistance != null && Mathf.Abs(Vector3.Distance(prevPointDistance, objectToTrackMovement.transform.position)) >= minDistanceBeforeNewPoint)
            {
                Vector3 dir = (objectToTrackMovement.transform.position - Camera.main.transform.position).normalized;
                prevPointDistance = objectToTrackMovement.transform.position;
                AddPoint(prevPointDistance, dir);
            }
        }

        void AddPoint(Vector3 position, Vector3 direction)
        {
            currentLineRender.SetPosition(positionCount, position);
            positionCount++;
            currentLineRender.positionCount = positionCount + 1;
            currentLineRender.SetPosition(positionCount, position);

            // send position
            // TCPControllerClient.Instance.UpdateLine(position);
            linePosition = position;


        }

        public void UpdateLineWidth(float newValue)
        {
            currentLineRender.startWidth = newValue;
            currentLineRender.endWidth = newValue;
            lineDefaultWidth = newValue;
        }

        public void UpdateLineColor(Color color)
        {
            // in case we haven't drawn anything
            if(currentLineRender.positionCount == 1)
            {
                currentLineRender.material.color = color;
                currentLineRender.material.EnableKeyword("_EMISSION");
                currentLineRender.material.SetColor("_EmissionColor", color);
            }
            defaultColor = color;
        }

        public void UpdateLineMinDistance(float newValue)
        {
            minDistanceBeforeNewPoint = newValue;
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(current_index);
                stream.SendNext(trackPosition);
                stream.SendNext(lineDefaultWidth);
                stream.SendNext(positionCount);
                stream.SendNext(numCapVectices);
                stream.SendNext(linePosition);
                // stream.SendNext(defaultColor);
                stream.SendNext(minDistanceBeforeNewPoint);

            }
            else
            {
                current_index = (int)stream.ReceiveNext();
                trackPosition = (Vector3)stream.ReceiveNext();
                lineDefaultWidth = (float)stream.ReceiveNext();
                positionCount = (int)stream.ReceiveNext();
                numCapVectices = (int)stream.ReceiveNext();
                linePosition = (Vector3)stream.ReceiveNext();
                // defaultColor = (Color)stream.ReceiveNext();
                minDistanceBeforeNewPoint = (float)stream.ReceiveNext();
            }
        }
    }
}