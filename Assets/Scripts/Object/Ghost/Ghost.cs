using UnityEngine;

public class Ghost : MonoBehaviour
{
    public Rigidbody2D ghostRigidbody { get; private set; }
    public CharacterMovementController ghostMovementController { get; private set; }

    public Transform target;
    public int points = 200;
    public bool isHome = true;

    public Transform insideTransform;
    public Transform outsideTransform;

    public SpriteRenderer body;
    public SpriteRenderer eyes;
    public SpriteRenderer blue;
    public SpriteRenderer white;

    public float chaseDuration = 0f;
    public float scatterDuration = 0f;
    public float frightenedDuration = 6f;
    public float homeDuration = 5f;
    public float homeExitSegmentDuration = 0.5f;

    public GhostStateMachine stateMachine;

    GhostChaseState chaseState;
    GhostScatterState scatterState;
    GhostFrightenedState frightenedState;
    GhostHomeState homeState;

    bool isFrightened;

    void Awake()
    {
        ghostRigidbody = GetComponent<Rigidbody2D>();
        ghostMovementController = GetComponent<CharacterMovementController>();

        stateMachine = new GhostStateMachine(this);

        chaseState      = new GhostChaseState(chaseDuration);
        scatterState    = new GhostScatterState(scatterDuration);
        frightenedState = new GhostFrightenedState(frightenedDuration, body, eyes, blue, white);
        homeState       = new GhostHomeState(insideTransform, outsideTransform, homeDuration, homeExitSegmentDuration);
    }

    void Start() => ResetState();

    void Update() => stateMachine.Update();
    void OnTriggerEnter2D(Collider2D other) => stateMachine.OnTriggerEnter2D(other);

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (isFrightened)
                GameManager.Instance.GhostEaten(this);
            else
                GameManager.Instance.PacmanEaten();
        }

        stateMachine.OnCollisionEnter2D(collision);
    }

    public void ResetState()
    {
        ghostMovementController.ResetState();
        gameObject.SetActive(true);

        body.enabled = true;
        eyes.enabled = true;
        blue.enabled = false;
        white.enabled = false;

        ghostRigidbody.isKinematic = false;

        if (isHome)
            SetHome();
        else
            SetScatter(scatterDuration);
    }

    public void SetChase(float duration = -1f)
    {
        if (duration >= 0f) chaseState.SetDuration(duration);
        isFrightened = false;
        stateMachine.ChangeState(chaseState);
    }

    public void SetScatter(float duration = -1f)
    {
        if (duration >= 0f) scatterState.SetDuration(duration);
        isFrightened = false;
        stateMachine.ChangeState(scatterState);
    }

    public void SetFrightened(float duration = -1f)
    {
        if (duration >= 0f) frightenedState.SetDuration(duration);
        isFrightened = true;
        stateMachine.ChangeState(frightenedState);
    }

    public void SetHome(float exitSegmentDuration = -1f)
    {
        if (exitSegmentDuration >= 0f) homeState.SetDuration(exitSegmentDuration);
        isFrightened = false;
        stateMachine.ChangeState(homeState);
    }
}
