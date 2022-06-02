using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    UIManager uiManager;

    [SerializeField] int score = 0;

    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
        if (uiManager == null) { Debug.LogError("No UI Manager Referance"); }
    }

    public void IncrementScore()
    {
        score += Random.Range(5, 21);
        uiManager.UpdateScoreText(score);
    }
}