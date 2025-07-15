using UnityEngine;

public class GhostBehaviour : MonoBehaviour
{
    public Ghost ghost { get; private set; }
    public float behaviourDuration;

    void Awake()
    {
        ghost = GetComponent<Ghost>();

    }

    void Start()
    {
        enabled = false;
    }

    public virtual void Enable()
    {
        Enable(behaviourDuration);
    }

    public virtual void Enable(float duration)
    {
        enabled = true;
        CancelInvoke();
        Invoke(nameof(Disable), duration);
    }

    public virtual void Disable()
    {
        enabled = false;
        CancelInvoke();
    }
}