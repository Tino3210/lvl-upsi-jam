using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Menu,
    LaunchGame,
    Pause,
    Game,
    End
}

public class GameManager : MonoBehaviour
{
    public GameState State { get; set; }

    public static GameManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateGameState(GameState.Menu);
    }

    void Update()
    {
        if (Input.GetKeyDown("escape") && GameManager.Instance.State == GameState.Game)
        {
            if(GameManager.Instance.State == GameState.Game)
            {
                UpdateGameState(GameState.Pause);
            }
            else if (GameManager.Instance.State == GameState.Pause)
            {
                UpdateGameState(GameState.Game);
            }
        }
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.Menu:
                SceneManager.LoadScene("InterfaceScene");
                break;
            case GameState.LaunchGame:
                SceneManager.LoadScene("MasterScene");
                UpdateGameState(GameState.Game);
                break;
            case GameState.Game:
                this.OnGame(new EventArgs());
                break;
            case GameState.End:
                this.OnEnd(new EventArgs());
                break;
            case GameState.Pause:
                this.OnPause(new EventArgs());
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }

    public void StartGame()
    {
        StartCoroutine(StartGameCoroutine());
    }

    private IEnumerator StartGameCoroutine()
    {
        yield return new WaitForSeconds(3f);
        UpdateGameState(GameState.LaunchGame);
    }

    public event EventHandler Paused;

    protected virtual void OnPause(EventArgs e)
    {
        EventHandler handler = Paused;
        handler?.Invoke(this, e);
    }

    public event EventHandler Ended;

    protected virtual void OnEnd(EventArgs e)
    {
        EventHandler handler = Ended;
        handler?.Invoke(this, e);
    }

    public event EventHandler Gaming;

    protected virtual void OnGame(EventArgs e)
    {
        EventHandler handler = Gaming;
        handler?.Invoke(this, e);
    }

}

