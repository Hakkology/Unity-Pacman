using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaritaNode : MonoBehaviour
{
    public LayerMask ObstacleLayer;
    public List<Vector2> secilebilirYonler { get; private set; }
    void Start()
    {
        secilebilirYonler = new List<Vector2>();
        SecilebilirYonleriBelirle(new List<Vector2> { Vector2.down, Vector2.up, Vector2.left, Vector2.right });
    }

    private void SecilebilirYonleriBelirle(List<Vector2> vector2s)
    {
        foreach (var yon in vector2s)
        {
            RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.5f, 0.0f, yon, 1.0f, ObstacleLayer);

            if (hit.collider == null)
                secilebilirYonler.Add(yon);
        }
    }
}
