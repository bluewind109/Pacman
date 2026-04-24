using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] private LayerMask obstacleLayer;

    public List<Vector2> AvailableDirections { get; private set; }

    void Start()
    {
        AvailableDirections = new List<Vector2>();

        CheckAvailableDirections(Vector2.up);
        CheckAvailableDirections(Vector2.down);
        CheckAvailableDirections(Vector2.left);
        CheckAvailableDirections(Vector2.right);
    }

    private void CheckAvailableDirections(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(
            this.transform.position,
            Vector2.one * 0.5f,
            0.0f,
            direction,
            1.0f,
            obstacleLayer
        );

        if (!hit.collider)
        {
            AvailableDirections.Add(direction);
        }
    }
}
