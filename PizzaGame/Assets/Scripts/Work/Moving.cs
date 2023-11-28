using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Moving : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    private NavMeshAgent meshAgent;
    public Vector3 TargetPosition;
    public bool IsCome = true;

    void Awake()
    {
        meshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (meshAgent.velocity == Vector3.zero && TargetPosition != Vector3.zero)
            RotateTo(TargetPosition);
    }

    public void MoveTo(Vector3 position)
    {
        IsCome = false;
        TargetPosition = position;
        meshAgent.destination = TargetPosition;
    }

    public void RotateTo(Vector3 targetPosition)
    {
        var directionToTarget = targetPosition - transform.position;
        directionToTarget.y = 0f;
        if (directionToTarget == Vector3.zero)
        {
            IsCome = true;
            return;
        }
        var targetRotation = Quaternion.LookRotation(directionToTarget);

        if (Quaternion.Angle(transform.rotation, targetRotation) == 0)
        {
            IsCome = true;
            TargetPosition = Vector3.zero;
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}