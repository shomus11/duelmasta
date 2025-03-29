using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager instance;
    public GameState state = GameState.preparation;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        state = GameState.preparation;
        Invoke("InitGame", 1f);
    }
    public void Endgame(bool win = false)
    {
        state = GameState.endgame;
        GameplayUI.instance.ShowEndPanel(win);
    }
    void InitGame()
    {
        state = GameState.playing;
    }
}
