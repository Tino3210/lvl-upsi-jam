using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class UIGameManager : VisualElement
{
    VisualElement optionScreen;
    VisualElement scoreScreen;
    VisualElement gameScreen;
    VisualElement resumeScreen;

    Label time;
    Label scoreInGame;
    Label scoreEnd;

    SliderInt sliderMainVolume;
    SliderInt sliderInteface;
    SliderInt sliderMusic;
    SliderInt sliderEffect;

    IMGUIContainer[] hearts;

    public new class UxmlFactory : UxmlFactory<UIGameManager, UxmlTraits> { }

    public UIGameManager()
    {
        hearts = new IMGUIContainer[3];
        this.RegisterCallback<GeometryChangedEvent>(OnGeometryChange);
    }

    void OnGeometryChange(GeometryChangedEvent evt)
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.Paused += ShowResumeScreen;
            GameManager.Instance.Ended += ShowScoreScreen;
            GameManager.Instance.Gaming += ShowGameScreen;

            ScoreManager.Instance.TimeChanged += UpdatedTime;
            ScoreManager.Instance.ScoreChanged += UpdatedScore;
            ScoreManager.Instance.LifeChanged += UpdateLife;
        }

        optionScreen = this.Q("UIOption");
        scoreScreen = this.Q("UIEndGame");
        gameScreen = this.Q("UIGame");
        resumeScreen = this.Q("UIPause");

        sliderMainVolume = optionScreen.Q<SliderInt>("mainVolume");
        sliderInteface = optionScreen.Q<SliderInt>("interfaceSound");
        sliderMusic = optionScreen.Q<SliderInt>("musicVolume");
        sliderEffect = optionScreen.Q<SliderInt>("effectVolume");

        if (sliderMainVolume != null)
        {
            sliderMainVolume.RegisterValueChangedCallback(ChangeMainVolume);
            sliderMainVolume.value = (int)(AudioManager.Instance.volumeMain * 100);
        }

        if (sliderInteface != null)
        {
            sliderInteface.RegisterValueChangedCallback(ChangeInterfaceVolume);
            sliderInteface.value = (int)(AudioManager.Instance.volumeInterface * 100);
        }
        if (sliderMusic != null)
        {
            sliderMusic.RegisterValueChangedCallback(ChangeMusicVolume);
            sliderMusic.value = (int)(AudioManager.Instance.volumeMusic * 100);
        }

        if (sliderEffect != null)
        {
            sliderEffect.RegisterValueChangedCallback(ChangeEffectVolume);
            sliderEffect.value = (int)(AudioManager.Instance.volumeEffect * 100);
        }

        time = gameScreen?.Q("time") as Label;
        scoreInGame = gameScreen?.Q("score") as Label;
        scoreEnd = scoreScreen?.Q("score") as Label;

        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i] = gameScreen?.Q("heart" + i) as IMGUIContainer;
        }

        resumeScreen?.Q("resume").RegisterCallback<ClickEvent>(evt => GameManager.Instance.UpdateGameState(GameState.Game));
        resumeScreen?.Q("option").RegisterCallback<ClickEvent>(evt => ShowOptionScreen());
        resumeScreen?.Q("quitToMenu").RegisterCallback<ClickEvent>(evt => GameManager.Instance.UpdateGameState(GameState.Menu));
        resumeScreen?.Q("quitToDesktop").RegisterCallback<ClickEvent>(evt => Application.Quit());

        scoreScreen?.Q("menu").RegisterCallback<ClickEvent>(evt => GameManager.Instance.UpdateGameState(GameState.Menu));

        optionScreen?.Q("back")?.RegisterCallback<ClickEvent>(ev => ShowResumeScreen());

        ShowGameScreen();
    }
    private void ChangeMainVolume(ChangeEvent<int> evt)
    {
        AudioManager.Instance.volumeMain = evt.newValue / 100f;
    }

    private void ChangeInterfaceVolume(ChangeEvent<int> evt)
    {
        AudioManager.Instance.volumeInterface = evt.newValue / 100f;
    }

    private void ChangeMusicVolume(ChangeEvent<int> evt)
    {
        AudioManager.Instance.volumeMusic = evt.newValue / 100f;
        AudioManager.Instance.ChangeMusicVolume();
    }

    private void ChangeEffectVolume(ChangeEvent<int> evt)
    {
        AudioManager.Instance.volumeEffect = evt.newValue / 100f;
    }

    public void UpdatedTime(object sender, EventArgs evt)
    {
        time.text = ScoreManager.Instance.TimeText;
    }

    public void UpdatedScore(object sender, EventArgs evt)
    {
        scoreInGame.text = "Score\n" + ScoreManager.Instance.Score;
        scoreEnd.text = ScoreManager.Instance.Score + " points";

    }
    public void UpdateLife(object sender, EventArgs evt)
    {
        if (ScoreManager.Instance.Life >= 0)
        {
            hearts[ScoreManager.Instance.Life].style.backgroundImage = new StyleBackground(ScoreManager.Instance.emptyHeart);
        }
    }    

    public void ShowResumeScreen(object sender, EventArgs evt)
    {
        ShowResumeScreen();
    }

    public void ShowScoreScreen(object sender, EventArgs evt)
    {
        ShowScoreScreen();
    }
    public void ShowGameScreen(object sender, EventArgs evt)
    {
        ShowGameScreen();
    }

    public void ShowOptionScreen()
    {
        // if all not null
        if (optionScreen != null && scoreScreen != null && gameScreen != null && resumeScreen != null)
        {
            // hide all
            scoreScreen.style.display = DisplayStyle.None;
            gameScreen.style.display = DisplayStyle.None;
            resumeScreen.style.display = DisplayStyle.None;

            // show option
            optionScreen.style.display = DisplayStyle.Flex;
        }
    }

    public void ShowScoreScreen()
    {
        // if all not null
        if (optionScreen != null && scoreScreen != null && gameScreen != null && resumeScreen != null)
        {
            // hide all
            optionScreen.style.display = DisplayStyle.None;
            gameScreen.style.display = DisplayStyle.None;
            resumeScreen.style.display = DisplayStyle.None;

            // show score
            scoreScreen.style.display = DisplayStyle.Flex;
        }
    }

    public void ShowGameScreen()
    {
        // if all not null
        if (optionScreen != null && scoreScreen != null && gameScreen != null && resumeScreen != null)
        {
            // hide all
            optionScreen.style.display = DisplayStyle.None;
            scoreScreen.style.display = DisplayStyle.None;
            resumeScreen.style.display = DisplayStyle.None;

            // show game
            gameScreen.style.display = DisplayStyle.Flex;
        }
    }

    public void ShowResumeScreen()
    {
        // if all not null
        if (optionScreen != null && scoreScreen != null && gameScreen != null && resumeScreen != null)
        {
            // hide all
            optionScreen.style.display = DisplayStyle.None;
            scoreScreen.style.display = DisplayStyle.None;
            gameScreen.style.display = DisplayStyle.None;

            // show resume
            resumeScreen.style.display = DisplayStyle.Flex;
        }
    }
    
    public void HideAllScreens()
    {
        // if all not null
        if (optionScreen != null && scoreScreen != null && gameScreen != null && resumeScreen != null)
        {
            // hide all
            optionScreen.style.display = DisplayStyle.None;
            scoreScreen.style.display = DisplayStyle.None;
            gameScreen.style.display = DisplayStyle.None;
            resumeScreen.style.display = DisplayStyle.None;
        }
    }
}
