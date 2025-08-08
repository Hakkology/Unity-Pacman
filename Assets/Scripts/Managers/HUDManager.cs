using UnityEngine;
using TMPro;
using System;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI lifeText;
    public static Action<int> OnScoreChanged;
    public static Action<int> OnLifeChanged;

    private void OnEnable()
    {
        OnScoreChanged += UpdateScore;
        OnLifeChanged += UpdateLife;
    }

    private void OnDisable()
    {
        OnScoreChanged -= UpdateScore;
        OnLifeChanged -= UpdateLife;
    }

    private void Start()
    {
        ResetUI();
    }

    public void ResetUI()
    {
        UpdateScore(0);
        UpdateLife(3);
    }

    private void UpdateScore(int newScore) => scoreText.text = $"Score: {newScore}";
    private void UpdateLife(int newLife) => lifeText.text = $"Life: {newLife}";
}