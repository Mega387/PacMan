using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPurple : MonoBehaviour
{
    public Node currentNode;
    public float speed = 8f;

    private Node targetNode;
    private Vector3 targetPosition;
    private bool isMoving = false;
    private bool Rand = false;

    private void Start()
    {
        ChooseRandomTarget();
    }

    private void Update()
    {
        if (!isMoving)
        {
            ChooseRandomTarget();
        }
        else
        {
            MoveToTarget();
        }
    }

    private void ChooseRandomTarget()
    {
        if (Rand == true)
        {
            targetNode = currentNode.neighbors[1];
            targetPosition = targetNode.transform.position;
            isMoving = true;
            Rand = false;
        }
        else
        {
            if (currentNode.neighbors.Count > 0)
            {
                targetNode = currentNode.neighbors[Random.Range(0, currentNode.neighbors.Count)];
                targetPosition = targetNode.transform.position;
                isMoving = true;
            }
            Rand = true;
        }
    }


    private void MoveToTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPosition) < 0.01f)
        {
            transform.position = targetPosition;
            currentNode = targetNode;
            isMoving = false;
        }
    }
}