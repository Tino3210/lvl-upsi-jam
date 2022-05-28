using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int Score { get; set; }
    public int Life { get; set; }

    public string TimeText { get; set; }
    private string oldText;
    private float time;

    public Sprite emptyHeart;
    
    public static ScoreManager Instance;
    
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
    }

    void Start()
    {
        Score = 0;
        Life = 3;
    }

    void Update()
    {
        // chrono
        time += Time.deltaTime;

        TimeText = string.Format("{0:00}:{1:00}", time / 60, time % 60);
        if (TimeText != oldText)
        {
            oldText = TimeText;
            this.OnTimeChanged(new EventArgs());
        }
        if(Life == 0)
        {
            GameManager.Instance.UpdateGameState(GameState.End);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            LoseLife();
        }
    }

    public void AddScore(int value)
    {
        Score += value;
        this.OnScoreChanged(new EventArgs());
    }

    public void LoseLife()
    {
        Life -= 1;
        this.OnLifeChanged(new EventArgs());
    }

    public event EventHandler TimeChanged;

    protected virtual void OnTimeChanged(EventArgs e)
    {
        EventHandler handler = TimeChanged;
        handler?.Invoke(this, e);
    }

    public event EventHandler ScoreChanged;

    protected virtual void OnScoreChanged(EventArgs e)
    {
        EventHandler handler = ScoreChanged;
        handler?.Invoke(this, e);
    }

    public event EventHandler LifeChanged;

    protected virtual void OnLifeChanged(EventArgs e)
    {
        EventHandler handler = LifeChanged;
        handler?.Invoke(this, e);
    }
}
