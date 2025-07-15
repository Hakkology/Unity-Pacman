using UnityEngine;

public class GameOverState : GameStateBase
{
    public GameOverState(GameManager manager) : base(manager) {}

    public override void Enter()
    {
        SetGhostsActive(false);
        //SetPacmanActive(false);
    }

    public override void Update()
    {
        if (Input.anyKeyDown)
            gameManager.TransitionToState(new NewGameState(gameManager));
    }

    public override void Exit() { }
}