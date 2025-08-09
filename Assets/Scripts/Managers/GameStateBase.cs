using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameStateBase : IGameState
{
    protected GameManager _manager;
    public GameStateBase(GameManager manager)
    {
        _manager = manager;
    }

    public abstract void Enter();
    public abstract void Exit();
    public abstract void Update();

    protected void SetGhostsActive(bool active)
    {
        foreach (Ghost ghost in _manager.ghostRef)
            ghost.gameObject.SetActive(active);
    }

    protected void SetPacmanActive(bool active)
    {
        if (_manager.pacmanRef != null)
            _manager.pacmanRef.gameObject.SetActive(active);
    }

    protected void SetPelletsActive(bool active)
    {
        foreach (Transform pellet in _manager.pelletRef)
            pellet.gameObject.SetActive(active);
    }
}
