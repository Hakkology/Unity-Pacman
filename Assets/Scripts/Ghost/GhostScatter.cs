using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Rastgele hareket algoritmasý
/// </summary>
public class GhostScatter : GhostBehaviour
{
    private void OnEnable()
    {
        Debug.Log("Dagilma modu baþladý");
    }

    private void OnDisable()
    {
        ghost.ghostChase.Enable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HaritaNode node = collision.GetComponent<HaritaNode>();

        if (node != null && enabled && !ghost.ghostFrightened.enabled)
        {
            int index = Random.Range(0, node.secilebilirYonler.Count);

            if (node.secilebilirYonler[index] 
                == -ghost.characterMovementController.mevcutDirection 
                && node.secilebilirYonler.Count > 1)
            {
                index++;
                if (index >= node.secilebilirYonler.Count)
                {
                    index = 0;
                }
            }

            ghost.characterMovementController.SetDirection(node.secilebilirYonler[index]);
        }
    }
}
