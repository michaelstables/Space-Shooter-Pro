using UnityEngine;

public class ReferenceManager : MonoBehaviour
{
    public SpawnManager spawnManager;
    public GameManager gameManager;
    public UIManager uiManager;
    public AudioManager audioManager;
    public ScoreManager scoreManager;
    public Player player;

    private void Awake()
    {
        spawnManager = FindObjectOfType<SpawnManager>();
        if (spawnManager == null) { Debug.LogError("No Spawn Manager Reference"); }

        uiManager = FindObjectOfType<UIManager>();
        if (uiManager == null) { Debug.LogError("No UI Manager Reference"); }

        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null) { Debug.LogError("No Game Manager Reference"); }

        audioManager = FindObjectOfType<AudioManager>();
        if (audioManager == null) { Debug.LogError("No Audio Manager Reference"); }

        scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager == null) { Debug.LogError("No Score Manager Reference"); }

        player = FindObjectOfType<Player>();
        if (player == null) { Debug.LogError("No Reference"); }
    }
}