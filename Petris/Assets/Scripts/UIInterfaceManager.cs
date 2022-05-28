using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class UIInterfaceManager : VisualElement
{

    VisualElement optionScreen;
    VisualElement mainScreen;

    SliderInt sliderMainVolume;
    SliderInt sliderInteface;

    private bool alreadyStart;

    public new class UxmlFactory : UxmlFactory<UIInterfaceManager, UxmlTraits> { }

    public UIInterfaceManager()
    {
        alreadyStart = false;
        this.RegisterCallback<GeometryChangedEvent>(OnGeometryChange);
    }

    void OnGeometryChange(GeometryChangedEvent evt)
    {
        optionScreen = this.Q("UIOption");
        mainScreen = this.Q("UIMainMenu");

        ShowMainScreen();

        sliderMainVolume = optionScreen.Q<SliderInt>("mainVolume");
        sliderInteface = optionScreen.Q<SliderInt>("interfaceSound");

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

        mainScreen?.Q("start")?.RegisterCallback<ClickEvent>(ev => StartGame());
        mainScreen?.Q("title")?.RegisterCallback<ClickEvent>(ev => AudioManager.Instance.PlayPetris());
        
        mainScreen?.Q("option")?.RegisterCallback<ClickEvent>(ev => ShowOptionScreen());
        optionScreen?.Q("back")?.RegisterCallback<ClickEvent>(ev => ShowMainScreen());
    }

    private void ChangeMainVolume(ChangeEvent<int> evt)
    {
        AudioManager.Instance.volumeMain = evt.newValue / 100f;
    }

    private void ChangeInterfaceVolume(ChangeEvent<int> evt)
    {
        AudioManager.Instance.volumeInterface = evt.newValue / 100f;
    }

    public IEnumerator DoSomething()
    {
        yield return new WaitForSeconds(5);
        Debug.Log("Done");
    }

    public void StartGame()
    {
        //Play start sound

        if (!alreadyStart)
        {
            alreadyStart = true;
            AudioManager.Instance.PlayStart();

            GameManager.Instance.StartGame();
        }
    }

    public void ShowOptionScreen()
    {
        if (optionScreen != null && mainScreen != null)
        {
            mainScreen.style.display = DisplayStyle.None;
            optionScreen.style.display = DisplayStyle.Flex;
        }
    }

    public void ShowMainScreen()
    {
        if (optionScreen != null && mainScreen != null)
        {
            mainScreen.style.display = DisplayStyle.Flex;
            optionScreen.style.display = DisplayStyle.None;
        }
    }
    
    public void HideAllScreen()
    {
        if (optionScreen != null && mainScreen != null)
        {
            mainScreen.style.display = DisplayStyle.None;
            optionScreen.style.display = DisplayStyle.None;
        }
    }
}
