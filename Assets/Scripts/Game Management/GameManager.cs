using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    PlayerInput inputManager;
    InputAction reset;

    bool isGameOver = false;

    private void Awake()
    {
        GetRefrances();
        reset.performed += OnRestart;
    }

    private void GetRefrances()
    {
        inputManager = GetComponent<PlayerInput>();
        if (inputManager == null) { Debug.LogError("Missing Input Manager Refrance"); }

        reset = inputManager.actions["Restart Level"];
        if (reset == null) { Debug.LogError("Missing Input Action For Reset"); }

    }

    private void OnDestroy()
    {
        reset.performed -= OnRestart;
    }

    void OnRestart(InputAction.CallbackContext context)
    {
        if (isGameOver) { SceneManager.LoadScene("Game"); } 
    }

    public void GameOver()
    {
        isGameOver = true;
    }
}