using UnityEngine;

public abstract class GhostBehaviourBase : IGhostState
{
    protected float behaviourDuration;
    protected float timer;
    protected bool hasDuration;

    protected GhostBehaviourBase(float duration = 0f)
    {
        behaviourDuration = duration;
        hasDuration = duration > 0f;
    }

    public virtual void Enter(Ghost ghost) { timer = 0f; }
    public virtual void Update(Ghost ghost)
    {
        if (hasDuration)
        {
            timer += Time.deltaTime;
            if (timer >= behaviourDuration)
                OnDurationComplete(ghost);
        }
    }
    public virtual void Exit(Ghost ghost) { }

    public virtual void OnTriggerEnter2D(Ghost ghost, Collider2D c) { }
    public virtual void OnCollisionEnter2D(Ghost ghost, Collision2D c) { }

    protected virtual void OnDurationComplete(Ghost ghost) { /* override edersin */ }

    public void SetDuration(float duration)
    {
        behaviourDuration = duration;
        hasDuration = duration > 0f;
    }
}