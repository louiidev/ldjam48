using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private Manager _Manager;
    public void Start()
    {
        _Manager = Manager.Instance;
    }

    public void StartGame()
    {
        _Manager.NextScene();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
