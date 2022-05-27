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
        gameScreen.style.display = DisplayStyle.None;
        scoreScreen.style.display = DisplayStyle.None;
        optionScreen.style.display = DisplayStyle.Flex;
        resumeScreen.style.display = DisplayStyle.None;
    }

    public void ShowScoreScreen()
    {
        gameScreen.style.display = DisplayStyle.None;
        scoreScreen.style.display = DisplayStyle.Flex;
        optionScreen.style.display = DisplayStyle.None;
        resumeScreen.style.display = DisplayStyle.None;
    }

    public void ShowGameScreen()
    {
        gameScreen.style.display = DisplayStyle.Flex;
        scoreScreen.style.display = DisplayStyle.None;
        optionScreen.style.display = DisplayStyle.None;
        resumeScreen.style.display = DisplayStyle.None;
    }

    public void ShowResumeScreen()
    {
        gameScreen.style.display = DisplayStyle.None;
        scoreScreen.style.display = DisplayStyle.None;
        optionScreen.style.display = DisplayStyle.None;
        resumeScreen.style.display = DisplayStyle.Flex;
    }
    
    public void HideAllScreens()
    {
        gameScreen.style.display = DisplayStyle.None;
        scoreScreen.style.display = DisplayStyle.None;
        optionScreen.style.display = DisplayStyle.None;
        resumeScreen.style.display = DisplayStyle.None;
    }
}
