using System.Collections;
using UnityEngine;

public class GhostHomeState : GhostBehaviourBase, IGhostState
{
    private readonly Transform insideTransform;
    private readonly Transform outsideTransform;
    private float exitSegmentDuration;  
    
    private Coroutine exitRoutine;
    public GhostHomeState(Transform insideTransform, Transform outsideTransform, float stayDuration, float exitSegmentDuration)
        : base(stayDuration)
    {
        this.insideTransform = insideTransform;
        this.outsideTransform = outsideTransform;
        this.exitSegmentDuration = exitSegmentDuration;
    }

    public void SetDurations(float stayDuration, float exitSegmentDuration)
    {
        SetDuration(stayDuration);             // base timer
        this.exitSegmentDuration = exitSegmentDuration;
    }

    public override void Enter(Ghost ghost)
    {
        base.Enter(ghost);
        if (exitRoutine != null)
        {
            ghost.StopCoroutine(exitRoutine);
            exitRoutine = null;
        }
    }

    public override void Exit(Ghost ghost)
    {
        if (exitRoutine != null)
        {
            ghost.StopCoroutine(exitRoutine);
            exitRoutine = null;
        }
    }

    protected override void OnDurationComplete(Ghost ghost)
    {
        if (ghost.gameObject.activeSelf && exitRoutine == null)
            exitRoutine = ghost.StartCoroutine(ExitTransition(ghost));
    }

    public override void OnCollisionEnter2D(Ghost ghost, Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
            ghost.ghostMovementController.SetDirection(-ghost.ghostMovementController.charCurrentDirection);
    }

    public override void OnTriggerEnter2D(Ghost ghost, Collider2D collider) { }

    private IEnumerator ExitTransition(Ghost ghost)
    {
        ghost.ghostMovementController.SetDirection(Vector2.up, true);
        ghost.ghostRigidbody.isKinematic = true;
        ghost.ghostMovementController.enabled = false;

        Vector3 position = ghost.transform.position;
        float seg = exitSegmentDuration > 0f ? exitSegmentDuration : 0.5f;
        float elapsed = 0f;

        while (elapsed < seg)
        {
            Vector3 newPosition = Vector3.Lerp(position, insideTransform.position, elapsed / seg);
            newPosition.z = position.z;
            ghost.transform.position = newPosition;
            elapsed += Time.deltaTime;
            yield return null;
        }

        elapsed = 0f;

        while (elapsed < seg)
        {
            Vector3 newPosition = Vector3.Lerp(insideTransform.position, outsideTransform.position, elapsed / seg);
            newPosition.z = position.z;
            ghost.transform.position = newPosition;
            elapsed += Time.deltaTime;
            yield return null;
        }

        ghost.ghostMovementController.SetDirection(new Vector2(Random.value < 0.5f ? -1.0f : 1.0f, 0.0f), true);
        ghost.ghostRigidbody.isKinematic = false;
        ghost.ghostMovementController.enabled = true;

        exitRoutine = null;
        ghost.SetScatter(); 
    }
}
