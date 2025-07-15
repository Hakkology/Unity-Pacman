using System.Collections;
using UnityEngine;

public class NewGameState : GameStateBase
{
    public NewGameState(GameManager manager) : base(manager)
    {

    }

    public override void Enter()
    {
        gameManager.StartCoroutine(NewGameRoutine());
    }

    private IEnumerator NewGameRoutine()
    {
        yield return new WaitForSeconds(2f);

        SetPelletsActive(true);
        SetGhostsActive(true);
        SetPacmanActive(true);
    }

    public override void Update() { }
    public override void Exit() { }
}
