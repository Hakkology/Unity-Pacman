using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghostRef;
    public PacmanController pacmanRef;
    public Transform pelletsRef;

    public int Score { get; private set; }
    public int Lives { get; private set; }

    void Start()
    {
        NewGame();
    }

    void Update()
    {
        if (Lives <= 0 && Input.anyKeyDown)
        {
            NewGame();
        }
    }

    private void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewRound(true);
    }

    private void GameOver()
    {
        // display UI

        foreach (Ghost ghost in ghostRef)
            ghost.gameObject.SetActive(false);

        pacmanRef.gameObject.SetActive(false);
    }

    private void NewRound(bool restartingGame)
    {
        if (restartingGame)
        {
            foreach (Transform pellet in pelletsRef)
                pellet.gameObject.SetActive(true);
        }

        foreach (Ghost ghost in ghostRef)
            ghost.gameObject.SetActive(true);

        pacmanRef.gameObject.SetActive(true);
    }

    private IEnumerator NewRoundAfterDelay(bool restartingGame)
    {
        yield return new WaitForSeconds(2);
        NewRound(restartingGame);
    }

    private void SetScore(int score)
    {
        Score = score;
    }

    private void SetLives(int lives)
    {
        Lives = lives;
    }

    public void GhostEaten(Ghost ghost)
    {
        SetScore(Score + ghost.points);
        //ghost.gameObject.SetActive(false);
    }

    public void PacmanEaten()
    {
        pacmanRef.gameObject.SetActive(false);

        Lives--;
        SetLives(Lives);

        if (Lives > 0)
            StartCoroutine(NewRoundAfterDelay(false));
        else
            GameOver();
    }
}
