using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostHome : GhostBehaviour
{
    public Transform IcNokta;
    public Transform DisNokta;

    private void OnEnable()
    {
        Debug.Log("EVDEYÝZ EVDE " + ghost.gameObject.name);
        StopAllCoroutines();
    }

    private void OnDisable()
    {
        if (gameObject.activeSelf)
        {
            StartCoroutine(nameof(CikisSureci));
        }
    }

    private IEnumerator CikisSureci()
    {
        ghost.characterMovementController.SetDirection(Vector2.up);
        ghost.characterMovementController._rb.isKinematic = true;
        ghost.characterMovementController.enabled = false;

        Vector3 position = transform.position;

        float sure = .5f;
        float gecenSure = 0f;

        while (gecenSure < sure)
        {
            Vector3 yeniKonum = Vector3.Lerp(position, IcNokta.transform.position, gecenSure/sure);
            yeniKonum.z = position.z;
            ghost.transform.position = yeniKonum;
            gecenSure += Time.deltaTime;
            yield return null;
        }

        gecenSure = 0;

        while (gecenSure < sure)
        {
            Vector3 yeniKonum = Vector3.Lerp(IcNokta.transform.position, DisNokta.transform.position, gecenSure / sure);
            yeniKonum.z = position.z;
            ghost.transform.position = yeniKonum;
            gecenSure += Time.deltaTime;
            yield return null;
        }

        ghost.characterMovementController.SetDirection(new Vector2(Random.value < 0.5f ? -1.0f : 1.0f, 0.0f), true);
        ghost.characterMovementController._rb.isKinematic = false;
        ghost.characterMovementController.enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (enabled && collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            ghost.characterMovementController.SetDirection(
                -ghost.characterMovementController.mevcutDirection);
        }
    }

}
