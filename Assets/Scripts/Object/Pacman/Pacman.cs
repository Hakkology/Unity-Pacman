using UnityEngine;

public class Pacman : MonoBehaviour
{
    CircleCollider2D pacmanCircleCollider;
    Rigidbody2D pacmanRigidbody;
    CharacterMovementController pacmanMovementController;
    CharacterAnimatorController pacmanAnimatorController;

    void Awake()
    {
        pacmanCircleCollider = GetComponent<CircleCollider2D>();
        pacmanRigidbody = GetComponent<Rigidbody2D>();
        pacmanMovementController = GetComponent<CharacterMovementController>();
        pacmanAnimatorController = GetComponent<CharacterAnimatorController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            pacmanMovementController.SetDirection(Vector2.up);
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            pacmanMovementController.SetDirection(Vector2.down);
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            pacmanMovementController.SetDirection(Vector2.left);
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            pacmanMovementController.SetDirection(Vector2.right);
        }

        //y ile x değeriyle verilen vektörün baktığı açıyı alır.
        //float angle = Mathf.Atan2(pmc.pacmanCurrentDirection.y, pmc.pacmanCurrentDirection.x);
        //istenilen aksta dönme yapar.
        //transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }

    void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        pacmanMovementController.ResetState();
        gameObject.SetActive(true);
    }
}