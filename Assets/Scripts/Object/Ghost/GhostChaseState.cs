using UnityEngine;

public class GhostChaseState : GhostBehaviourBase, IGhostState
{
    public GhostChaseState(float duration = 0f) : base(duration) { }

    public override void Enter(Ghost ghost)
    {
        base.Enter(ghost);
    }

    public override void OnTriggerEnter2D(Ghost ghost, Collider2D collision)
    {
        MapNode node = collision.GetComponent<MapNode>();
        if (node != null && !(ghost.stateMachine.Current is GhostFrightenedState))
        {
            Vector2 direction = Vector2.zero;
            float minDistance = float.MaxValue;

            foreach (Vector2 availableDirection in node.availableDirections)
            {
                Vector3 newPosition = ghost.transform.position + new Vector3(availableDirection.x, availableDirection.y, 0.0f);
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

    public override void OnCollisionEnter2D(Ghost ghost, Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            GameManager.Instance.PacmanEaten();
        }
    }

    protected override void OnDurationComplete(Ghost ghost)
    {
        ghost.SetScatter();
    }
}
