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
        foreach (Ghost ghost in gameManager.ghostRef)
            ghost.gameObject.SetActive(active);
    }

    protected void SetPacmanActive(bool active)
    {
        if (gameManager.pacmanRef != null)
            gameManager.pacmanRef.gameObject.SetActive(active);
    }

    protected void SetPelletsActive(bool active)
    {
        foreach (Transform pellet in gameManager.pelletsRef)
            pellet.gameObject.SetActive(active);
    }
}