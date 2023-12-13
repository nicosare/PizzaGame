using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private float speed;

    private float leftBorder;
    private float rightBorder;
    private float topBorder;
    private float bottomBorder;

    [SerializeField] private Transform leftPanel;
    [SerializeField] private Transform rightPanel;
    [SerializeField] private Transform topPanel;
    [SerializeField] private Transform bottomPanel;

    [SerializeField] private float minFurtherX;
    [SerializeField] private float maxFurtherX;
    [SerializeField] private float minNearX;
    [SerializeField] private float maxNearX;
    [SerializeField] private float minFurtherY;
    [SerializeField] private float maxFurtherY;
    [SerializeField] private float minNearY;
    [SerializeField] private float maxNearY;

    private Zoom zoom;

    private void Awake()
    {
        zoom = GetComponent<Zoom>();
    }

    private void Start()
    {
        SetBorder();
    }

    public void Update()
    {
        if (zoom.CanMove)
        {
            if (SystemInfo.deviceType != DeviceType.Handheld)
                DesktopMoving();
            else
                MobileMoving();
        }
    }

    private void DesktopMoving()
    {
        if (Input.mousePosition.x < leftPanel.position.x)
            MoveTo(-speed, 0);
        else if (Input.mousePosition.x > rightPanel.position.x)
            MoveTo(speed, 0);

        if (Input.mousePosition.y > topPanel.position.y)
            MoveTo(0, speed);
        else if (Input.mousePosition.y < bottomPanel.position.y)
            MoveTo(0, -speed);
    }

    private void MobileMoving()
    {
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            var touchdeltaPos = Input.GetTouch(0).deltaPosition;

            MoveTo(-touchdeltaPos.x * speed, -touchdeltaPos.y * speed);
        }
    }

    private void CheckBorders()
    {
        transform.localPosition = new Vector3(
                    Mathf.Clamp(transform.localPosition.x, leftBorder, rightBorder),
                    Mathf.Clamp(transform.localPosition.y, bottomBorder, topBorder),
                    transform.position.z);
    }

    public void SetBorder()
    {
        var normalizedZ = (transform.position.z - zoom.zoomMin) / (zoom.zoomMax - zoom.zoomMin);
        leftBorder = Mathf.Lerp(minFurtherX, minNearX, normalizedZ);
        rightBorder = Mathf.Lerp(maxFurtherX, maxNearX, normalizedZ);
        bottomBorder = Mathf.Lerp(minFurtherY, minNearY, normalizedZ);
        topBorder = Mathf.Lerp(maxFurtherY, maxNearY, normalizedZ);
        CheckBorders();

    }

    private void MoveTo(float xDirection, float yDirection)
    {
        transform.Translate(xDirection, yDirection, 0);
        CheckBorders();

    }
}
