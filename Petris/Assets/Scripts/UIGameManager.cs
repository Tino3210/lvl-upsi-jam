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
        optionScreen = this.Q("UIOption");
        scoreScreen = this.Q("UIEndGame");
        gameScreen = this.Q("UIGame");
        resumeScreen = this.Q("UIPause");

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
