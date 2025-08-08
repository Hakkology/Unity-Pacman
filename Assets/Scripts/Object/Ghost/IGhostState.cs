using UnityEngine;

public interface IGhostState
{
    void Enter(Ghost ghost);
    void Update(Ghost ghost);
    void Exit(Ghost ghost);
    void OnTriggerEnter2D(Ghost ghost, Collider2D collider);
    void OnCollisionEnter2D(Ghost ghost, Collision2D collision);
}