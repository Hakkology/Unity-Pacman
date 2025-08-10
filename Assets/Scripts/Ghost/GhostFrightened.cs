using System;
using System.Collections;
using System.Collections.Generic;
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

    public override void Disable()
    {
        base.Disable();
        body.enabled = true;
        eyes.enabled = true;
        blue.enabled = false;
        white.enabled = false;
    }

    private void OnEnable()
    {
        ghost.characterMovementController.speedMultipler = .5f;
        eaten = false;
    }

    private void OnDisable()
    {
        ghost.characterMovementController.speedMultipler = 1f;
        eaten = false;
    }

    private void Flash()
    {
        if (!eaten)
        {
            blue.enabled = false;
            white.enabled = true;
            white.GetComponent<CharacterAnimatorController>().RestartAnimation();
        }
        else
        {
            // NEYSE ÝÞTE 
        }
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
        HaritaNode node = collision.GetComponent<HaritaNode>();
        if (node != null && enabled)
        {
            Vector2 direction = Vector2.zero;
            float maxDistance = float.MinValue;

            foreach (Vector2 availableDirection in node.secilebilirYonler)
            {
                Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y, 0.0f);
                float distance = (ghost.Pacman.position - newPosition).sqrMagnitude;

                if (distance > maxDistance)
                {
                    direction = availableDirection;
                    maxDistance = distance;
                }
            }

            ghost.characterMovementController.SetDirection(direction);
        }
    }

    private void Eaten()
    {
        eaten = true;

        Vector3 position = ghost.ghostHome.IcNokta.position;
        position.z = ghost.transform.position.z;
        ghost.transform.position = position;

        ghost.ghostHome.Enable(behaviourDuration);

        body.enabled = false;
        eyes.enabled = true;
        blue.enabled = false;
        white.enabled = false;
    }
}
