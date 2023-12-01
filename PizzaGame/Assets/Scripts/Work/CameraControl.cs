using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private float leftBorder;
    [SerializeField] private float rightBorder;
    [SerializeField] private float topBorder;
    [SerializeField] private float bottomBorder;

    [SerializeField] private Transform leftPanel;
    [SerializeField] private Transform rightPanel;
    [SerializeField] private Transform topPanel;
    [SerializeField] private Transform bottomPanel;

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
        if (SystemInfo.deviceType != DeviceType.Handheld)
            DesktopMoving();
        else
            MobileMoving();
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
        leftBorder = Mathf.Lerp(0, -7f, normalizedZ);
        rightBorder = Mathf.Lerp(0, 7f, normalizedZ);
        bottomBorder = Mathf.Lerp(1, -7f, normalizedZ);
        topBorder = Mathf.Lerp(12f, 7f, normalizedZ);
        CheckBorders();

    }

    private void MoveTo(float xDirection, float yDirection)
    {
        transform.Translate(xDirection, yDirection, 0);
        CheckBorders();

    }
}
