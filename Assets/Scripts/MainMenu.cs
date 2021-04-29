using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private Manager _Manager;
    public Slider musicSlider;
    public Slider fxSlider;

    public void Start()
    {
        _Manager = FindObjectOfType(typeof(Manager)) as Manager;
    }

    public void StartGame()
    {
        _Manager.NextScene();
    }

    public void UpdateSlider()
    {
        musicSlider.value = _Manager._AudioController.music.volume;
        fxSlider.value = _Manager._AudioController.fx.volume;
    }

    public void UpdateVolume()
    {
        _Manager._AudioController.music.volume = musicSlider.value;
        _Manager._AudioController.fx.volume = fxSlider.value;
    }

    public void DesativarMusic()
    {
        _Manager._AudioController.music.mute = !_Manager._AudioController.music.mute;

    }

    public void DesativarFX()
    {
        _Manager._AudioController.fx.mute = !_Manager._AudioController.fx.mute;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
