using UnityEngine;

public class GhostScatterState : GhostBehaviourBase, IGhostState
{
    private readonly bool avoidBacktrack;

    /// <param name="duration">0 = sınırsız</param>
    /// <param name="avoidBacktrack">Geldiği yönün tersine dönmeyi engelle</param>
    public GhostScatterState(float duration = 0f, bool avoidBacktrack = true) : base(duration)
    {
        this.avoidBacktrack = avoidBacktrack;
    }

    public override void Enter(Ghost ghost)
    {
        base.Enter(ghost);
        // İstersen hız/görsel ayarı burada
        // ghost.ghostMovementController.charSpeedMultiplier = 1f;
    }

    public override void OnTriggerEnter2D(Ghost ghost, Collider2D collider)
    {
        var node = collider.GetComponent<MapNode>();
        if (node == null) return;

        // Rastgele bir index seç
        int count = node.availableDirections.Count;
        if (count == 0) return;

        int index = Random.Range(0, count);

        // Backtrack'i engelle: seçilen yön, mevcut yönün tersi ise ve alternatif varsa kaydır
        var current = ghost.ghostMovementController.charCurrentDirection;
        if (avoidBacktrack && count > 1 && node.availableDirections[index] == -current)
        {
            index = (index + 1) % count;
        }

        ghost.ghostMovementController.SetDirection(node.availableDirections[index]);
    }

    public override void OnCollisionEnter2D(Ghost ghost, Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            // Frightened değilse Pacman ölür (frightened akışı FrightenedState içinde yönetilir)
            GameManager.Instance.PacmanEaten();
        }
    }

    protected override void OnDurationComplete(Ghost ghost)
    {
        // Scatter süresi bitince Chase'e geç
        ghost.SetChase();
    }
}
