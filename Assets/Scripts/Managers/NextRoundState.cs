using System.Collections;
using UnityEngine;

public class NextRoundState : GameStateBase
{
    public NextRoundState(GameManager manager) : base(manager) { }

    public override void Enter()
    {
        _manager.StartCoroutine(NextRoundRoutine());
    }

    private IEnumerator NextRoundRoutine()
    {
        SetPacmanActive(false);
        SetGhostsActive(false);

        yield return new WaitForSeconds(1f);

        SetPelletsActive(true);

        _manager.StateDegistir(new NewRoundState(_manager));
    }

    public override void Update() { }

    public override void Exit() { }
}