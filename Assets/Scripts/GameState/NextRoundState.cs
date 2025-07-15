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
        SetGhostsActive(false);

        yield return new WaitForSeconds(1f);

        SetPelletsActive(true);
        
        gameManager.TransitionToState(new NewRoundState(gameManager));
    }

    public override void Update() { }

    public override void Exit() { }
}
