using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Zoom : MonoBehaviour
{
    public float zoomMin;
    public float zoomMax;
    public float mobileSensetivity;
    public float desktopSensetivity;


    private Touch touchA;
    private Touch touchB;
    private Vector2 touchADirection;
    private Vector2 touchBDirection;
    private float distanceBtwTouchPositions;
    private float distanceBtwTouchDirections;
    private float zoom;
    private CameraControl cameraControl;
    public bool CanMove;

    private void Awake()
    {
        cameraControl = GetComponent<CameraControl>();
    }

    private void Update()
    {
        if (CanMove)
        {
            if (SystemInfo.deviceType != DeviceType.Handheld)
                DesktopControl();
            else
                MobileControl();
        }
    }

    private void DesktopControl()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            var newZ = transform.position.z + Input.mouseScrollDelta.y * desktopSensetivity;
            newZ = Mathf.Clamp(newZ, zoomMin, zoomMax);
            transform.position = new Vector3(transform.position.x, transform.position.y, newZ);

            cameraControl.SetBorder();
        }
    }

    private void MobileControl()
    {
        if (Input.touchCount == 2)
        {

            touchA = Input.GetTouch(0);
            touchB = Input.GetTouch(1);
            touchADirection = touchA.position - touchA.deltaPosition;
            touchBDirection = touchB.position - touchB.deltaPosition;

            distanceBtwTouchPositions = Vector2.Distance(touchA.position, touchB.position);
            distanceBtwTouchDirections = Vector2.Distance(touchADirection, touchBDirection);

            zoom = distanceBtwTouchDirections - distanceBtwTouchPositions;

            var currentZoom = transform.position.z - zoom * mobileSensetivity;

            transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(currentZoom, zoomMin, zoomMax));

            cameraControl.SetBorder();
        }
    }
}
