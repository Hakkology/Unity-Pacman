using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostChase : GhostBehaviour
{
    private void OnEnable()
    {
        Debug.Log("GOVALAMA modu baþladý");
    }

    private void OnDisable()
    {
        ghost.ghostScatter.Enable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HaritaNode node = collision.GetComponent<HaritaNode>();

        if (node != null && enabled && !ghost.ghostFrightened.enabled)
        {
            Vector2 _yon = Vector2.zero;
            float _minMesafe = float.MaxValue;

            foreach (var secilenYon in node.secilebilirYonler)
            {
                Vector3 yeniPozisyon = transform.position + new Vector3(secilenYon.x, secilenYon.y, 0.0f);
                float mesafe = (ghost.Pacman.position - yeniPozisyon).sqrMagnitude;

                if (mesafe < _minMesafe)
                {
                    _yon = secilenYon;
                    _minMesafe = mesafe;
                }
            }

            ghost.characterMovementController.SetDirection(_yon);
        }
    }
}
