using UnityEngine;

public class GameOverState : GameStateBase
{
    public GameOverState(GameManager manager) : base(manager) { }

    public override void Enter()
    {
        SetGhostsActive(false);
        SetPacmanActive(false);
    }

    public override void Update()
    {
        if (Input.anyKeyDown)
            _manager.StateDegistir(new NewGameState(_manager));
    }

    public override void Exit() { }
}