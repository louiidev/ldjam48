using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private GameController _GameController;
    public Slider musicSlider;
    public Slider fxSlider;

    public void Start()
    {
        _GameController = FindObjectOfType(typeof(GameController)) as GameController;
    }

    public void StartGame()
    {
        _GameController.NextScene();
    }

    public void UpdateSlider()
    {
        musicSlider.value = _GameController._AudioController.music.volume;
        fxSlider.value = _GameController._AudioController.fx.volume;
    }

    public void UpdateVolume()
    {
        _GameController._AudioController.music.volume = musicSlider.value;
        _GameController._AudioController.fx.volume = fxSlider.value;
    }

    public void DesativarMusic()
    {
        _GameController._AudioController.music.mute = !_GameController._AudioController.music.mute;

    }

    public void DesativarFX()
    {
        _GameController._AudioController.fx.mute = !_GameController._AudioController.fx.mute;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
