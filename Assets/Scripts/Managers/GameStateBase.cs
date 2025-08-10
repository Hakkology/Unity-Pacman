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
        if (active)
        {
            foreach (Ghost ghost in _manager.ghostRef)
            {
                ghost.ResetState();
            }
        }
        else
        {
            foreach (Ghost ghost in _manager.ghostRef)
            {
                ghost.gameObject.SetActive(false);
            }
        }
    }

    protected void SetPacmanActive(bool active)
    {
        if (active)
        {
            _manager.pacmanRef.ResetState();
        }
        else
        {
            _manager.pacmanRef.gameObject.SetActive(false);
        }
    }

    protected void SetPelletsActive(bool active)
    {
        foreach (Transform pellet in _manager.pelletRef)
            pellet.gameObject.SetActive(active);
    }
}
