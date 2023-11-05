using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float Speed;

    public void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved && Input.touchCount < 2)
        {
            Vector2 _touchdeltaPos = Input.GetTouch(0).deltaPosition;

            transform.Translate(-_touchdeltaPos.x * Speed, -_touchdeltaPos.y * Speed, 0);

            transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, -4f, 4f),
            Mathf.Clamp(transform.position.y, 0f, 5f),
            Mathf.Clamp(transform.position.z, -40f, 40f));
        }
    }
}
