using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("UI Text Fields")]
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI gameoverText;
    [SerializeField] TextMeshProUGUI restartText;

    [Header("UI Image Fields")]
    [SerializeField] Sprite[] livesSprites;
    [SerializeField] Image livesImage;

    private void Awake()
    {
        UISetup();
    }

    private void UISetup()
    {
        livesImage.sprite = livesSprites[3];
        scoreText.text = "SCORE: 0";
        gameoverText.gameObject.SetActive(false);
        restartText.gameObject.SetActive(false);
    }

    public void UpdateScoreText(int score)
    {
        scoreText.text = "SCORE: " + score.ToString();
    }

    public void UpdateLivesImage(int lives)
    {
        if (lives <= livesSprites.Length) { livesImage.sprite = livesSprites[lives]; }
        if (lives <= 0) { GameOverSequence(); }
    }

    private void GameOverSequence()
    {
        StartCoroutine("GameOverFlicker");
        restartText.gameObject.SetActive(true);
    }

    IEnumerator GameOverFlicker()
    {
        while (true)
        {
            gameoverText.gameObject.SetActive(true);
            yield return new WaitForSecondsRealtime(0.8f);
            gameoverText.gameObject.SetActive(false);
            yield return new WaitForSecondsRealtime(0.8f);
        }
    }
}