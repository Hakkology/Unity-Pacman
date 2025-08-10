using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public int points = 200;

    public CharacterMovementController characterMovementController { get; private set; }

    public GhostHome ghostHome { get; private set; }
    public GhostScatter ghostScatter { get; private set; }
    public GhostChase ghostChase { get; private set; }
    public GhostFrightened ghostFrightened { get; private set; }

    public GhostBehaviour ghostIlkBehaviour;
    public Transform Pacman;

    private void Awake()
    {
        characterMovementController = GetComponent<CharacterMovementController>();
        ghostHome = GetComponent<GhostHome>();
        ghostScatter = GetComponent<GhostScatter>();
        ghostFrightened = GetComponent<GhostFrightened>();
        ghostChase = GetComponent<GhostChase>();
    }
    void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        characterMovementController.ResetState();
        gameObject.SetActive(true);

        ghostFrightened.Disable();
        ghostChase.Disable();
        ghostScatter.Enable();

        if (ghostHome != ghostIlkBehaviour)
            ghostHome.Disable();

        if (ghostIlkBehaviour != null)
            ghostIlkBehaviour.Enable();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (ghostFrightened.enabled)
                GameManager.Instance.GhostEaten(this);
            else
                GameManager.Instance.PacmanEaten();
        }
    }


}
