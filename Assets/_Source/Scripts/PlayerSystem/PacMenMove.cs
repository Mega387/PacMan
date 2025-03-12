using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacMenMove : MonoBehaviour
{
    public Node currentNode;
    public float speed = 4f;

    private Node targetNode;
    private Vector3 targetPosition;
    private bool isMoving = false;
    private Vector2 lastDirection = Vector2.right;

    private void Update()
    {
        if (!isMoving)
        {
            CheckInputAndMove();
        }
        else
        {
            MoveToTarget();
        }
    }

    private void CheckInputAndMove()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            TryMove(Vector2.up);
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            TryMove(Vector2.down);
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            TryMove(Vector2.left);
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            TryMove(Vector2.right);
    }

    private void TryMove(Vector2 direction)
    {
        Node bestNode = null;
        float bestDistance = float.MaxValue;

        foreach (Node neighbor in currentNode.neighbors)
        {
            Vector2 toNeighbor = (neighbor.transform.position - transform.position).normalized;

            if (Vector2.Dot(toNeighbor, direction) > 0.9f)
            {
                float distance = Vector2.Distance(transform.position, neighbor.transform.position);
                if (distance < bestDistance)
                {
                    bestDistance = distance;
                    bestNode = neighbor;
                }
            }
        }

        if (bestNode != null)
        {
            targetNode = bestNode;
            targetPosition = targetNode.transform.position;
            isMoving = true;
            lastDirection = direction;
            UpdateRotation(direction);
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

    private void UpdateRotation(Vector2 direction)
    {
        float angle = 0f;

        if (direction == Vector2.up) angle = 90f;
        else if (direction == Vector2.down) angle = -90f;
        else if (direction == Vector2.left) angle = 180f;
        else if (direction == Vector2.right) angle = 0f;

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}