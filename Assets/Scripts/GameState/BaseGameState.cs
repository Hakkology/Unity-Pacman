using UnityEngine;

public abstract class GameStateBase : IGameState
{
    protected GameManager gameManager;

    public GameStateBase(GameManager manager)
    {
        gameManager = manager;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();

    protected void SetGhostsActive(bool active)
    {
        if (active)
        {
            foreach (Ghost ghost in gameManager.ghostRef)
            {
                ghost.ResetState();
            }
        }
        else
        {
            foreach (Ghost ghost in gameManager.ghostRef)
            {
                ghost.gameObject.SetActive(false);
            }
        }
    }

    protected void SetPacmanActive(bool active)
    {
        if (active)
        {
            gameManager.pacmanRef.ResetState();
        }
        else
        {
            gameManager.pacmanRef.gameObject.SetActive(false);
        }
    }

    protected void SetPelletsActive(bool active)
    {
        foreach (Transform pellet in gameManager.pelletsRef)
            pellet.gameObject.SetActive(active);
    }
}