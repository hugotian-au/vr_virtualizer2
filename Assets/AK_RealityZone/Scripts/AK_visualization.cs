﻿/**
 * Copyright (c) 2019 Hisham Bedri
 * Copyright (c) 2019-2020 James Hobin
 *
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */
//point cloud for the azure kinect, heavily influenced by the Zed pointcloud shader
//written by Hisham Bedri, Reality Lab, 2019

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AK_visualization : MonoBehaviour {

    public Texture2D colorTex;
    public Texture2D depthTex;
    public Texture2D XYMap;

    //public Material AK_pointCloudMat;
    public Shader AK_pointCloudShader;
    private Material matDefault;
    // Map from camera instance id to pointcloud shader material
    // Note that each camera gets its own material due to UNITY_MATRIX_VP
    // being computed per-material instead of per-render
    public Dictionary<int, Material> matForCamera = new Dictionary<int, Material>();

    public struct cameraInfoStruct
    {
        public int serial;

        public Matrix4x4 color_extrinsics;

        public float color_cx;
        public float color_cy;
        public float color_fx;
        public float color_fy;
        public float color_k1;
        public float color_k2;
        public float color_k3;
        public float color_k4;
        public float color_k5;
        public float color_k6;
        public float color_codx;
        public float color_cody;
        public float color_p2;
        public float color_p1;
        public float color_metric_radius;

    }

    public cameraInfoStruct cameraInfo;

    private Vector3 registeredPosition0 = new Vector3(1.7f, 2.69f, 1.20f);
    private Vector3 registeredRotation0 = new Vector3(149f, 32.5f, 0.56f);

    private Vector3 registeredPosition1 = new Vector3(-1.6495f, 2.595078f, 1.244509f);
    private Vector3 registeredRotation1 = new Vector3(153.0f, -52.6f, 0.6f);

    private Vector3 registeredPosition2 = new Vector3(1.57f, 2.68f, -0.93f);
    private Vector3 registeredRotation2 = new Vector3(31f, -36f, -178f);

    // Use this for initialization
    void Start()
    {
        matDefault = new Material(AK_pointCloudShader);
        foreach (Camera cam in Camera.allCameras)
        {
            Debug.Log("Hello from camera: " + cam.GetInstanceID());
            matForCamera.Add(cam.GetInstanceID(), matDefault); // cam.GetInstanceID(), new Material(AK_pointCloudShader));
        }
        //mat = new Material(Shader.Find("Standard"));

        if (cameraInfo.serial == 668194612)
        {
            // The serial number of the camera in the corner above the door
            regPos = registeredPosition0;
            regRotation = registeredRotation0;
        }
        else if (cameraInfo.serial == 457594512)
        {
            // The serial number of the camera in the corner above desk
            regPos = registeredPosition1;
            regRotation = registeredRotation1;
        }
        else if (cameraInfo.serial == 676294612)
        {
            // The serial number of the camera in the corner above the bookshelf
            regPos = registeredPosition2;
            regRotation = registeredRotation2;
        }
        Quaternion registeredRotation = Quaternion.Euler(regRotation.x, regRotation.y, regRotation.z);
        transform.localPosition = regPos;
        transform.localRotation = registeredRotation;
    }

    RenderTexture resized_depth_tex = null;
    RenderTexture resized_distortion_tex = null;

    public float size = 0.08f;
    public bool enhanced_depth_sampling = false;
    public float multiplier = 1.0f;

    public Vector3 regPos;
    public Vector3 regRotation;
    public bool enable_manual_adjust = false;

    // Update is called once per frame
    void Update () {
        if (enhanced_depth_sampling)
        {
            RenderTexture previous_rt = RenderTexture.active;
            if (resized_depth_tex == null || (resized_depth_tex.width != ((int)((float)depthTex.width * multiplier))))
            {
                resized_depth_tex = new RenderTexture(((int)((float)depthTex.width * multiplier)), ((int)((float)depthTex.height * multiplier)), 0, RenderTextureFormat.RFloat);
                resized_depth_tex.filterMode = FilterMode.Point;
            }
            if (resized_distortion_tex == null || (resized_distortion_tex.width != ((int)((float)depthTex.width * multiplier))))
            {
                resized_distortion_tex = new RenderTexture(((int)((float)depthTex.width * multiplier)), ((int)((float)depthTex.height * multiplier)), 0, RenderTextureFormat.RGFloat);
                resized_distortion_tex.filterMode = FilterMode.Point;
            }
            RenderTexture.active = resized_depth_tex;
            // Copy your texture ref to the render texture
            Graphics.Blit(depthTex, resized_depth_tex);

            RenderTexture.active = resized_distortion_tex;
            // Copy your texture ref to the render texture
            Graphics.Blit(XYMap, resized_distortion_tex);

            foreach (Material mat in matForCamera.Values)
            {
                mat.SetTexture("_DepthTex", resized_depth_tex);
                mat.SetTexture("_DistortionMapTex", resized_distortion_tex);
            }
        }
        else
        {
            foreach (Material mat in matForCamera.Values)
            {
                mat.SetTexture("_DepthTex", depthTex);
                mat.SetTexture("_DistortionMapTex", XYMap);
            }
        }


        foreach (Material mat in matForCamera.Values)
        {
            mat.SetFloat("_Size", size);
            mat.SetTexture("_ColorTex", colorTex);





            mat.SetMatrix("_color_extrinsics", cameraInfo.color_extrinsics);
            mat.SetFloat("_color_cx", cameraInfo.color_cx);
            mat.SetFloat("_color_cy", cameraInfo.color_cy);
            mat.SetFloat("_color_fx", cameraInfo.color_fx);
            mat.SetFloat("_color_fy", cameraInfo.color_fy);
            mat.SetFloat("_color_k1", cameraInfo.color_k1);
            mat.SetFloat("_color_k2", cameraInfo.color_k2);
            mat.SetFloat("_color_k3", cameraInfo.color_k3);
            mat.SetFloat("_color_k4", cameraInfo.color_k4);
            mat.SetFloat("_color_k5", cameraInfo.color_k5);
            mat.SetFloat("_color_k6", cameraInfo.color_k6);
            //Debug.Log(gameObject + " setting size: " + size);
            //Debug.Log(gameObject + " setting ks: " + cameraInfo.color_k1 + " " + cameraInfo.color_k2 + " " + cameraInfo.color_k3 + " " + cameraInfo.color_k4 + " " + cameraInfo.color_k5 + " " + cameraInfo.color_k6);
            mat.SetFloat("_color_codx", cameraInfo.color_codx);
            mat.SetFloat("_color_cody", cameraInfo.color_cody);
            mat.SetFloat("_color_p1", cameraInfo.color_p1);
            mat.SetFloat("_color_p2", cameraInfo.color_p2);
            mat.SetFloat("_color_metric_radius", cameraInfo.color_metric_radius);
        }

        if (!enable_manual_adjust)
        {
            Quaternion registeredRotation = Quaternion.Euler(regRotation.x, regRotation.y, regRotation.z);
            transform.localPosition = regPos;
            transform.localRotation = registeredRotation;
        }
    }

    void OnRenderObject()
    {
        if ((Camera.current.cullingMask & (1 << gameObject.layer)) == 0)
        {
            Debug.Log("Would have been culled");
            // return;
        }
        Material mat = matDefault;

        if (matForCamera.ContainsKey(Camera.current.GetInstanceID()))
        {
            mat = matForCamera[Camera.current.GetInstanceID()];
        }

        //Debug.Log("assigning mat stuff!");
        //Debug.Log("color texture size: " + colorTex.width + " " + colorTex.height);

        //mat.SetTexture("_MainTex", colorTex);

        int numberPoints = 0;
        if (enhanced_depth_sampling)
        {
            numberPoints = resized_distortion_tex.width * resized_distortion_tex.height;
        }
        else
        {
            numberPoints = depthTex.width * depthTex.height;
        }


        //int numberPoints = 100;
        mat.SetMatrix("_Position", transform.localToWorldMatrix);
        mat.SetPass(0);
        Graphics.DrawProceduralNow(MeshTopology.Points, numberPoints, 1);
    }
}
