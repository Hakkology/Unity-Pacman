using UnityEngine;

public class GhostFrightened : GhostBehaviour
{
    public SpriteRenderer body;
    public SpriteRenderer eyes;
    public SpriteRenderer blue;
    public SpriteRenderer white;

    public bool eaten { get; private set; }

    public override void Enable(float duration)
    {
        base.Enable(duration);
        body.enabled = false;
        eyes.enabled = false;
        blue.enabled = true;
        white.enabled = false;

        Invoke(nameof(Flash), duration / 2.0f);
    }

    void OnEnable()
    {
        ghost.ghostMovementController.charSpeedMultiplier = .5f;
        eaten = false;
    }

    void OnDisable()
    {
        ghost.ghostMovementController.charSpeedMultiplier = 1f;
        eaten = false;
    }

    public override void Disable()
    {
        base.Disable();
        body.enabled = true;
        eyes.enabled = true;
        blue.enabled = false;
        white.enabled = false;
    }

    private void Flash()
    {
        if (!eaten)
        {
            blue.enabled = false;
            white.enabled = true;
            white.GetComponent<CharacterAnimatorController>().CharRestartAnimation();
        }
        else
        {
            // body
        }
    }

    private void Eaten()
    {
        eaten = true;

        Vector3 position = ghost.ghostHome.insideTransform.position;
        position.z = ghost.transform.position.z;
        ghost.transform.position = position;

        ghost.ghostHome.Enable(behaviourDuration);

        body.enabled = false;
        eyes.enabled = true;
        blue.enabled = false;
        white.enabled = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (enabled)
                Eaten();
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        MapNode node = collision.GetComponent<MapNode>();
        if (node != null && enabled)
        {
            Vector2 direction = Vector2.zero;
            float maxDistance = float.MinValue;

            foreach (Vector2 availableDirection in node.availableDirections)
            {
                Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y, 0.0f);
                float distance = (ghost.target.position - newPosition).sqrMagnitude;

                if (distance > maxDistance)
                {
                    direction = availableDirection;
                    maxDistance = distance;
                }
            }

            ghost.ghostMovementController.SetDirection(direction);
        }
    }
}