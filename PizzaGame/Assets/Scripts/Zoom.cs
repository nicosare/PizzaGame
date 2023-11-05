using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Zoom : MonoBehaviour
{
    public float ZoomMin;
    public float ZoomMax;
    public float Sensetivity;

    private Camera _mainCamera;

    private Touch _touchA;
    private Touch _touchB;
    private Vector2 _touchADirection;
    private Vector2 _touchBDirection;
    private float _distanceBtwTouchPositions;
    private float _distanceBtwTouchDirections;
    private float _zoom;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.touchCount == 2)
        {

            _touchA = Input.GetTouch(0);
            _touchB = Input.GetTouch(1);
            _touchADirection = _touchA.position - _touchA.deltaPosition;
            _touchBDirection = _touchB.position - _touchB.deltaPosition;

            _distanceBtwTouchPositions = Vector2.Distance(_touchA.position, _touchB.position);
            _distanceBtwTouchDirections = Vector2.Distance(_touchADirection, _touchBDirection);

            _zoom = _distanceBtwTouchDirections - _distanceBtwTouchPositions;

            var currentZoom = transform.position.z - _zoom * Sensetivity;

            transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(currentZoom, ZoomMin, ZoomMax));
        } 
    }
}
