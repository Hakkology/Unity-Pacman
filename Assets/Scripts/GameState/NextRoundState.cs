using System.Collections;
using UnityEngine;

public class NextRoundState : GameStateBase
{
    public NextRoundState(GameManager manager) : base(manager) {}

    public override void Enter()
    {
        gameManager.StartCoroutine(NextRoundRoutine());
    }

    private IEnumerator NextRoundRoutine()
    {
        SetPacmanActive(false);
        // reset pacman position, will write.
        SetPelletsActive(true);
        yield return new WaitForSeconds(1f);
        gameManager.TransitionToState(new NewRoundState(gameManager));
    }

    public override void Update() { }

    public override void Exit() { }
}
