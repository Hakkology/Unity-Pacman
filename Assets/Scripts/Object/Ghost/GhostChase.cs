using UnityEngine;

public class GhostChase : GhostBehaviour 
{
    void OnEnable()
    {
        Debug.Log("ghost chase mode.");
    }
    void OnDisable()
    {
        ghost.ghostScatter.Enable();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        MapNode node = collision.GetComponent<MapNode>();
        if (node != null && enabled && !ghost.ghostFrightened.enabled)
        {
            Vector2 direction = Vector2.zero;
            float minDistance = float.MaxValue;

            foreach (Vector2 availableDirection in node.availableDirections)
            {
                Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y, 0.0f);
                float distance = (ghost.target.position - newPosition).sqrMagnitude;

                if (distance < minDistance)
                {
                    direction = availableDirection;
                    minDistance = distance;
                }
            }

            ghost.ghostMovementController.SetDirection(direction);
        }
    }
}