using System.Collections;
using UnityEngine;

public class NewRoundState : GameStateBase
{
    public NewRoundState(GameManager manager) : base(manager) {}

    public override void Enter()
    {
        gameManager.ResetGhostMultiplier();
        gameManager.StartCoroutine(NewRoundRoutine());
    }

    private IEnumerator NewRoundRoutine()
    {
        yield return new WaitForSeconds(1f);

        SetGhostsActive(true);
        SetPacmanActive(true);
    }

    public override void Update() { }

    public override void Exit() { }
}