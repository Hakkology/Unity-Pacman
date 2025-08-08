using System.Collections;
using UnityEngine;

public class GhostFrightenedState : GhostBehaviourBase, IGhostState
{
    private readonly SpriteRenderer body;
    private readonly SpriteRenderer eyes;
    private readonly SpriteRenderer blue;
    private readonly SpriteRenderer white;

    private bool eaten;
    private Coroutine flashRoutine;

    public GhostFrightenedState(
        float duration,
        SpriteRenderer body,
        SpriteRenderer eyes,
        SpriteRenderer blue,
        SpriteRenderer white
    ) : base(duration)
    {
        this.body = body;
        this.eyes = eyes;
        this.blue = blue;
        this.white = white;
    }

    public override void Enter(Ghost ghost)
    {
        base.Enter(ghost);

        if (body)  body.enabled  = false;
        if (eyes)  eyes.enabled  = false;
        if (blue)  blue.enabled  = true;
        if (white) white.enabled = false;

        ghost.ghostMovementController.charSpeedMultiplier = 0.5f;
        eaten = false;

        if (behaviourDuration > 0f)
        {
            if (flashRoutine != null) ghost.StopCoroutine(flashRoutine);
            flashRoutine = ghost.StartCoroutine(FlashAfter(ghost, behaviourDuration * 0.5f));
        }
    }

    public override void Exit(Ghost ghost)
    {
        ghost.ghostMovementController.charSpeedMultiplier = 1f;
        eaten = false;

        if (flashRoutine != null)
        {
            ghost.StopCoroutine(flashRoutine);
            flashRoutine = null;
        }

        if (body)  body.enabled  = true;
        if (eyes)  eyes.enabled  = true;
        if (blue)  blue.enabled  = false;
        if (white) white.enabled = false;

        base.Exit(ghost);
    }

    public override void OnCollisionEnter2D(Ghost ghost, Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (!eaten)
                Eaten(ghost);
        }
    }

    public override void OnTriggerEnter2D(Ghost ghost, Collider2D collider)
    {
        var node = collider.GetComponent<MapNode>();
        if (node == null) return;

        Vector2 direction = Vector2.zero;
        float maxDistance = float.MinValue;

        foreach (Vector2 availableDirection in node.availableDirections)
        {
            Vector3 newPosition = ghost.transform.position + new Vector3(availableDirection.x, availableDirection.y, 0.0f);
            float distance = (ghost.target.position - newPosition).sqrMagnitude;

            if (distance > maxDistance)
            {
                direction = availableDirection;
                maxDistance = distance;
            }
        }

        ghost.ghostMovementController.SetDirection(direction);
    }

    protected override void OnDurationComplete(Ghost ghost)
    {
        ghost.SetChase();
    }

    private IEnumerator FlashAfter(Ghost ghost, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (!eaten)
        {
            if (blue)  blue.enabled  = false;
            if (white) white.enabled = true;

            var anim = white ? white.GetComponent<CharacterAnimatorController>() : null;
            if (anim) anim.CharRestartAnimation();
        }
    }

    private void Eaten(Ghost ghost)
    {
        eaten = true;

        Vector3 position = ghost.insideTransform != null ? ghost.insideTransform.position : ghost.transform.position;
        position.z = ghost.transform.position.z;
        ghost.transform.position = position;

        if (body)  body.enabled  = false;
        if (eyes)  eyes.enabled  = true;
        if (blue)  blue.enabled  = false;
        if (white) white.enabled = false;

        ghost.SetHome(behaviourDuration);
    }
}
