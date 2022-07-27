using System;

public class GameContext
{
    public event Action OnGameStarted;
    public event Action OnGameEnded;
        
    public float Time { get; set; }
    public void StartGame()
    {
        OnGameStarted?.Invoke();
    }

    public void EndGame()
    {
        OnGameEnded?.Invoke();
    }
}