using System.Collections;
using UnityEngine;

public class NewGameState : GameStateBase
{
    public NewGameState(GameManager manager) : base(manager)
    {

    }

    public override void Enter()
    {
        gameManager.ResetGhostMultiplier();
        NewGame();
    }

    private void NewGame()
    {
        SetPelletsActive(true);
        SetGhostsActive(true);
        SetPacmanActive(true);
        gameManager.SetScore(0);
        gameManager.SetLives(3);
    }

    public override void Update() { }
    public override void Exit() { }
}
