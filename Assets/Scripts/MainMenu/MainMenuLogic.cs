using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuLogic : MonoBehaviour
{
    [SerializeField] private GameObject optionsGO;
    [SerializeField] private GameObject creditsGO;
    private SceneLoader sceneLoader;

    public void StartButton()
    {
        print("Start Test");
        sceneLoader.LoadScene();
    }

    public void OptionsButton()
    {
        print("Options Test");
    }
    public void CreditsButton()
    {
        print("Credits Test");
    }
    public void ExitButton()
    {
        print("Exit Test");
        Application.Quit();
    }

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }
}
