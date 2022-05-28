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

    public new class UxmlFactory : UxmlFactory<UIGameManager, UxmlTraits> { }

    public UIGameManager()
    {
        this.RegisterCallback<GeometryChangedEvent>(OnGeometryChange);
    }

    void OnGeometryChange(GeometryChangedEvent evt)
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.Paused += ShowResumeScreen;
            GameManager.Instance.Ended += ShowScoreScreen;
            GameManager.Instance.Gaming += ShowGameScreen;
        }

        optionScreen = this.Q("UIOption");
        scoreScreen = this.Q("UIEndGame");
        gameScreen = this.Q("UIGame");
        resumeScreen = this.Q("UIPause");


        resumeScreen?.Q("resume").RegisterCallback<ClickEvent>(evt => ShowOptionScreen());
        resumeScreen?.Q("option").RegisterCallback<ClickEvent>(evt => ShowOptionScreen());
        resumeScreen?.Q("quitToMenu").RegisterCallback<ClickEvent>(evt => GameManager.Instance.UpdateGameState(GameState.Menu));
        resumeScreen?.Q("quitToDesktop").RegisterCallback<ClickEvent>(evt => Application.Quit());

        optionScreen?.Q("back")?.RegisterCallback<ClickEvent>(ev => ShowResumeScreen());

        ShowGameScreen();
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
