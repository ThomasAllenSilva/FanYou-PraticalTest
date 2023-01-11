using System;

using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event Action OnGamePaused;

    public event Action OnGameResumed;

    public event Action OnGameStarted;

    public static GameManager Instance { get; private set; }

    public GameColorsManager GameColors { get; private set; }

    public ColorPanelManager ColorPanelManager { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);

            return;
        }

        Instance = this;

        GameColors = GetComponentInChildren<GameColorsManager>();

        ColorPanelManager = GetComponentInChildren<ColorPanelManager>();
    }

    public void ResumeGame()
    {
        OnGameResumed?.Invoke();
    }

    public void PauseGame()
    {
        OnGamePaused?.Invoke();
    }

    public void StartGame()
    {
        OnGameStarted?.Invoke();
    }
}
