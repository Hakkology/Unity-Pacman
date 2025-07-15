using UnityEngine;

public class Ghost : MonoBehaviour
{
    public Rigidbody2D ghostRigidbody { get; private set; }
    public CharacterMovementController ghostMovementController { get; private set; }
    public GhostHome ghostHome { get; private set; }
    public GhostScatter ghostScatter { get; private set; }
    public GhostChase ghostChase { get; private set; }
    public GhostFrightened ghostFrightened { get; private set; }

    public GhostBehaviour ghostInitialBehaviour;
    public Transform target;

    public int points = 200;

    void Awake()
    {
        ghostRigidbody = GetComponent<Rigidbody2D>();
        ghostMovementController = GetComponent<CharacterMovementController>();
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
        ghostMovementController.ResetState();
        gameObject.SetActive(true);

        ghostFrightened.Disable();
        ghostChase.Disable();
        ghostScatter.Enable();

        if (ghostHome != ghostInitialBehaviour)
            ghostHome.Disable();

        if (ghostInitialBehaviour != null)
            ghostInitialBehaviour.Enable();
    }

    void OnCollisionEnter2D(Collision2D collision)
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