using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    public Node currentNode;
    public float speed = 4f;

    private Node targetNode;
    private Vector3 targetPosition;
    private bool isMoving = false;

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
        if (currentNode.neighbors.Count > 0)
        {
            targetNode = currentNode.neighbors[Random.Range(0, currentNode.neighbors.Count)];
            targetPosition = targetNode.transform.position;
            isMoving = true;
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