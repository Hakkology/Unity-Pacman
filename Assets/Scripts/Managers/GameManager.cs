using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Ghost[] ghostRef;
    public Pacman pacmanRef;
    public Transform pelletRef;

    public int Score { get; private set; }
    public int Lives { get; private set; }

    private IGameState mevcutState;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        StateDegistir(new NewGameState(this));
    }

    private void Update()
    {
        mevcutState?.Update();
    }

    //private void Start()
    //{
    //    YeniOyun();
    //}

    //private void Update()
    //{
    //    if (Lives <= 0)
    //    {
    //        YeniOyun();
    //    }
    //}

    //private void YeniOyun()
    //{

    //    SetScore(0);
    //    SetLives(3);
    //    YeniRound(yenidenBasliyoruz: true);
    //}

    //private void OyunBitti()
    //{
    //    pacmanRef.gameObject.SetActive(false);
    //    foreach (Ghost ghost in ghostRef)
    //        ghost.gameObject.SetActive(false);
    //}

    //private IEnumerator SureliYeniOyun(bool yenidenBasliyoruz)
    //{
    //    // OYUN DURUMU UI GÖSTER

    //    yield return new WaitForSeconds(2);
    //    YeniRound(yenidenBasliyoruz);
    //}

    //private void YeniRound(bool yenidenBasliyoruz)
    //{
    //    if (yenidenBasliyoruz)
    //    {
    //        foreach (Transform pellet in pelletRef)
    //            pellet.gameObject.SetActive(true);
    //    }

    //    foreach (Ghost ghost in ghostRef)
    //        ghost.gameObject.SetActive(true);
    //    pacmanRef.gameObject.SetActive(true);
    //}

    public void SetScore(int score)
    {
        Score = score;
    }

    public void SetLives(int lives)
    {
        Lives = lives;
    }

    public void GhostEaten(Ghost ghost)
    {
        SetScore(Score + ghost.points);
    }

    public void PacmanEaten()
    {
        pacmanRef.gameObject.SetActive(false);
        Lives--;
        SetLives(Lives);

        if (Lives > 0)
        {
            StateDegistir(new NewRoundState(this));
        }
        else
        {
            StateDegistir(new GameOverState(this));
        }
    }

    public void PelletEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false);
        SetScore(Score + pellet.points);

        if (!HasPellets())
            StateDegistir(new NewRoundState(this));
    }

    public void PowerPelletEaten(PowerPellet pellet)
    {
        for (int i = 0; i < ghostRef.Length; i++)
            ghostRef[i].ghostFrightened.Enable(pellet.duration);

        PelletEaten(pellet);
        CancelInvoke();
        //Invoke(nameof(ResetGhostMultiplier), powerPellet.duration);
    }

    private bool HasPellets()
    {
        foreach (Transform pellet in pelletRef)
        {
            if (pellet.gameObject.activeSelf)
                return true;
        }
        return false;
    }


    public void StateDegistir(IGameState yeniState)
    {
        mevcutState?.Exit();
        mevcutState = yeniState;
        mevcutState.Enter();
    }

}