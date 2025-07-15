using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghostRef;
    public PacmanController pacmanRef;
    public Transform pelletsRef;

    public int Score { get; private set; }
    public int Lives { get; private set; }

    private IGameState currentState;

    void Start()
    {
        TransitionToState(new NewGameState(this));
    }

    void Update()
    {
        currentState?.Update();
    }

    public void TransitionToState(IGameState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }

    public void SetScore(int score) => Score = score;
    public void SetLives(int lives) => Lives = lives;
    public void GhostEaten(Ghost ghost) => SetScore(Score + ghost.points);

    public void PacmanEaten()
    {
        pacmanRef.gameObject.SetActive(false);
        SetLives(Lives - 1);

        if (Lives > 0)
            TransitionToState(new NewRoundState(this));
        else
            TransitionToState(new GameOverState(this));
    }
}
