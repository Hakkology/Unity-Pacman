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
        gameManager.StartCoroutine(NewGameRoutine());
    }

    private IEnumerator NewGameRoutine()
    {
        yield return new WaitForSeconds(1);

        gameManager.SetScore(0);
        gameManager.SetLives(3);
        
        yield return new WaitForSeconds(1);

        SetPelletsActive(true);
        SetGhostsActive(true);
        SetPacmanActive(true);
    }

    public override void Update() { }
    public override void Exit() { }
}
