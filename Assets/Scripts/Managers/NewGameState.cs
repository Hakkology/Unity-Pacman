using System.Collections;
using UnityEngine;

public class NewGameState : GameStateBase
{
    public NewGameState(GameManager manager) : base(manager)
    {

    }

    public override void Enter()
    {
        //_manager.ResetGhostMultiplier();
        NewGame();
    }

    private void NewGame()
    {
        SetPelletsActive(true);
        SetGhostsActive(true);
        SetPacmanActive(true);
        _manager.SetScore(0);
        _manager.SetLives(3);
    }

    public override void Update() { }
    public override void Exit() { }
}