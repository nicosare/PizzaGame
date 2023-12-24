using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class Security : MonoBehaviour
{
    [SerializeField] private Transform wayPointsParent;
    [SerializeField] private float radiusOfView;
    [SerializeField] private float detectionDistance;
    [Range(0, 360)]
    [SerializeField] private float angleOfView;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstructionMask;
    [SerializeField] private Material material;
    [SerializeField] private bool randomPatrolling;
    public bool CanFind;
    private Transform player;
    private Moving moving;
    private int currentWayPointIndex;
    private void Awake()
    {
        CanFind= true;
        moving = GetComponent<Moving>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private bool IsInView()
    {
        var directionToTarget = (player.position - transform.position).normalized;
        var distanceToTarget = Vector3.Distance(transform.position, player.position);
        if (Physics.Raycast(transform.position, directionToTarget, distanceToTarget, targetMask))
        {
            if (Vector3.Angle(transform.forward, directionToTarget) <= angleOfView / 2)
            {
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
        return false;
    }

    private bool IsInDetection()
    {
        var distanceToPlayer = Vector3.Distance(player.position, transform.position);
        return distanceToPlayer <= detectionDistance && MathF.Abs(transform.position.y - player.position.y) <= 1;
    }

    private void Update()
    {

        if ((IsInDetection() || IsInView()) && CanFind)
        {
            material.color = Color.red;
            agent.speed = 6;
            moving.MoveTo(player.position);
        }
        else if (moving.IsCome)
        {
            material.color = Color.green;
            agent.speed = 3;
            if (randomPatrolling)
                RandomPatrolling();
            else
                WayPointPatrolling();
        }
        DrawViewState();
    }

    private void RandomPatrolling()
    {
        moving.MoveTo(wayPointsParent.GetChilds().Shuffle().First().position);
    }

    private void WayPointPatrolling()
    {
        moving.MoveTo(wayPointsParent.GetChild(currentWayPointIndex).position);
        currentWayPointIndex++;
        if (currentWayPointIndex == wayPointsParent.childCount)
            currentWayPointIndex = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
            GameManager.Instance.NightToDay();
    }

    private void DrawViewState()
    {
        for (var i = angleOfView / 2; i >= 0; i--)
        {
            Vector3 right = transform.position + Quaternion.Euler(new Vector3(0, i, 0)) * (transform.forward * radiusOfView);
            Vector3 left = transform.position + Quaternion.Euler(-new Vector3(0, i, 0)) * (transform.forward * radiusOfView);
            Debug.DrawLine(transform.position, right, Color.yellow);
            Debug.DrawLine(transform.position, left, Color.yellow);
        }
        for (var i = 0; i < 360; i++)
            Debug.DrawLine(transform.position,
                transform.position + Quaternion.Euler(new Vector3(0, angleOfView + i, 0)) * (transform.forward * detectionDistance), Color.blue);
    }
}
