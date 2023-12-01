using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class FloorMove : MonoBehaviour
{
    [SerializeField] float doubleClickTime;
    [SerializeField] private LayerMask ground;
    [SerializeField] private Task task;
    private float lastClickTime;

    private void Update()
    {

        if (!Input.GetMouseButtonDown(0) || !DoubleClick())
            return;

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 100, ground))
        {
            TaskManager.Instance.CreateTask(task, hitInfo.point);
        }
    }

    private bool DoubleClick()
    {
        float timeFromLastClick = Time.time - lastClickTime;
        lastClickTime = Time.time;
        return timeFromLastClick <= doubleClickTime;
    }
}
