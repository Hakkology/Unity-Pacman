using UnityEngine;

public class PacmanController : MonoBehaviour
{
    CircleCollider2D cc;
    Rigidbody2D rb;
    PacmanMovementController pmc;
    PacmanAnimatorController pac;

    void Awake()
    {
        cc = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        pmc = GetComponent<PacmanMovementController>();
        pac = GetComponent<PacmanAnimatorController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            pmc.SetDirection(Vector2.up);
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            pmc.SetDirection(Vector2.down);
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            pmc.SetDirection(Vector2.left);
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            pmc.SetDirection(Vector2.right);
        }

        //y ile x değeriyle verilen vektörün baktığı açıyı alır.
        //float angle = Mathf.Atan2(pmc.pacmanCurrentDirection.y, pmc.pacmanCurrentDirection.x);
        //istenilen aksta dönme yapar.
        //transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }
}