using UnityEngine;

public class GhostStateMachine
{
    private IGhostState _current;
    private readonly Ghost _owner;

    public GhostStateMachine(Ghost owner) { _owner = owner; }

    public IGhostState Current => _current;

    public void ChangeState(IGhostState next)
    {
        _current?.Exit(_owner);
        _current = next;
        _current?.Enter(_owner);
    }

    public void Update() => _current?.Update(_owner);
    public void OnTriggerEnter2D(Collider2D c) => _current?.OnTriggerEnter2D(_owner, c);
    public void OnCollisionEnter2D(Collision2D c) => _current?.OnCollisionEnter2D(_owner, c);

}