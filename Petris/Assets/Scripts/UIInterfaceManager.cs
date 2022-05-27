using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIInterfaceManager : VisualElement
{
    VisualElement optionScreen;
    VisualElement mainScreen;

    public new class UxmlFactory : UxmlFactory<UIInterfaceManager, UxmlTraits> { }

    public UIInterfaceManager()
    {
        this.RegisterCallback<GeometryChangedEvent>(OnGeometryChange);
    }

    void OnGeometryChange(GeometryChangedEvent evt)
    {
        optionScreen = this.Q("UIOption");
        mainScreen = this.Q("UIMainMenu");

        mainScreen?.Q("option")?.RegisterCallback<ClickEvent>(ev => ShowOptionScreen());
        optionScreen?.Q("back")?.RegisterCallback<ClickEvent>(ev => ShowMainScreen());

        ShowMainScreen();
    }
    
    public void ShowOptionScreen()
    {
        optionScreen.style.display = DisplayStyle.Flex;
        mainScreen.style.display = DisplayStyle.None;
    }

    public void ShowMainScreen()
    {
        optionScreen.style.display = DisplayStyle.None;
        mainScreen.style.display = DisplayStyle.Flex;
    }
    
    public void HideAllScreen()
    {
        optionScreen.style.display = DisplayStyle.None;
        mainScreen.style.display = DisplayStyle.None;
    }
}
