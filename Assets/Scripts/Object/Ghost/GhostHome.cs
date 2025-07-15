using System.Collections;
using UnityEngine;

public class GhostHome : GhostBehaviour
{
    public Transform insideTransform;
    public Transform outsideTransform;

    void OnEnable()
    {
        StopAllCoroutines();
    }

    void OnDisable()
    {
        if (gameObject.activeInHierarchy)
            StartCoroutine(ExitTransition());
    }

    private IEnumerator ExitTransition()
    {
        ghost.ghostMovementController.SetDirection(Vector2.up, true);
        ghost.ghostRigidbody.isKinematic = true;
        ghost.ghostMovementController.enabled = false;

        Vector3 position = transform.position;

        float duration = .5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            Vector3 newPosition = Vector3.Lerp(position, insideTransform.position, elapsed / duration);
            newPosition.z = position.z;
            ghost.transform.position = newPosition;
            elapsed += Time.deltaTime;
            yield return null;
        }

        elapsed = 0;

        while (elapsed < duration)
        {
            Vector3 newPosition = Vector3.Lerp(insideTransform.position, outsideTransform.position, elapsed / duration);
            newPosition.z = position.z;
            ghost.transform.position = newPosition;
            elapsed += Time.deltaTime;
            yield return null;
        }

        ghost.ghostMovementController.SetDirection(new Vector2(Random.value < 0.5f ? -1.0f : 1.0f, 0.0f), true);
        ghost.ghostRigidbody.isKinematic = false;
        ghost.ghostMovementController.enabled = true;
    }
}