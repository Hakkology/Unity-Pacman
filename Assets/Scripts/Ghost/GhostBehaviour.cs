using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBehaviour : MonoBehaviour
{
    public Ghost ghost { get; private set; }
    public float behaviourDuration;

    private void Awake()
    {
        ghost = GetComponent<Ghost>();
        //enabled = false;
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
        CancelInvoke();
        enabled = false;
    }

}
