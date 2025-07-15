using System.Collections.Generic;
using UnityEngine;

// hangi yön açık?
public class MapNode : MonoBehaviour
{
    public LayerMask ObstacleLayer;
    public List<Vector2> availableDirections { get; private set; }

    void Start()
    {
        availableDirections = new List<Vector2>();
        CheckAvailableDirections(new List<Vector2>{Vector2.down, Vector2.up, Vector2.left, Vector2.right});
    }

    void CheckAvailableDirections(List<Vector2> _movableDirections)
    {
        foreach (var direction in _movableDirections)
        {
            RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.5f, 0.0f, direction, 1.0f, ObstacleLayer);

            if (hit.collider == null)
                availableDirections.Add(direction);
        }

    }
}