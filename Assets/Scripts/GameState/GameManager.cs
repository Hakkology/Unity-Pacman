using System.Collections;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}
    public Ghost[] ghostRef;
    public Pacman pacmanRef;
    public Transform pelletsRef;

    public int GhostMultiplier { get; private set; } = 1;
    public int Score { get; private set; }
    public int Lives { get; private set; }
    private IGameState currentState;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

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

    public void SetScore(int score)
    {
        Score = score;
        HUDManager.OnScoreChanged(Score);
    }

    public void SetLives(int lives)
    {
        Lives = lives;
        HUDManager.OnLifeChanged(Lives);
    }

    public void GhostEaten(Ghost ghost)
    {
        SetScore(Score + ghost.points * GhostMultiplier);
        GhostMultiplier++;
    }

    public void PacmanEaten()
    {
        pacmanRef.gameObject.SetActive(false);
        SetLives(Lives - 1);

        if (Lives > 0)
            TransitionToState(new NewRoundState(this));
        else
            TransitionToState(new GameOverState(this));
    }

    public void PelletEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false);
        SetScore(Score + pellet.Points);

        if (!HasPellets())
            TransitionToState(new NewRoundState(this));
    }

    public void PowerPelletEaten(PowerPellet powerPellet)
    {
        for (int i = 0; i < ghostRef.Length; i++)
            ghostRef[i].ghostFrightened.Enable(powerPellet.duration);
    
        PelletEaten(powerPellet);
        CancelInvoke();
        Invoke(nameof(ResetGhostMultiplier), powerPellet.duration);
    }

    private bool HasPellets()
    {
        foreach (Transform pellet in pelletsRef)
        {
            if (pellet.gameObject.activeSelf)
                return true;
        }
        return false;

        //return pelletsRef.Cast<Transform>().Any(p => p.gameObject.activeSelf);
    }

    public void ResetGhostMultiplier()
    {
        GhostMultiplier = 1;
    }
}
