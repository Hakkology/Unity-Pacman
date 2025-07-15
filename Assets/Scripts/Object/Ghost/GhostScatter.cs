using UnityEngine;

public class GhostScatter : GhostBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        MapNode node = collision.GetComponent<MapNode>();
        if (node != null && !enabled && !ghost.ghostFrightened.enabled)
        {
            int index = Random.Range(0, node.availableDirections.Count);
            if (node.availableDirections[index] == -ghost.ghostMovementController.charCurrentDirection && node.availableDirections.Count > 1)
            {
                index++;
                if (index >= node.availableDirections.Count)
                {
                    index = 0;
                }

            }

            ghost.ghostMovementController.SetDirection(node.availableDirections[index]);
        }
    }
}