using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public enum GameState
{
    HOME, GAMEPLAY, PAUSE
}

public class GameController : MonoBehaviour
{
    public AudioController _AudioController;
    public PlayerController player;
    public GameState currentState;
    public GameObject gameplayUI;
    public GameObject pauseMenu;
    public Slider musicSlider;
    public Slider fxSlider;
    public TMP_Text altf4;
    public Image hpBar;
    public Fade _Fade;
    public int drugLevel = 0;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        LoadAudio();
    }

    private void Start()
    {
        _AudioController = FindObjectOfType(typeof(AudioController)) as AudioController;
        _AudioController.ChangeMusic(_AudioController.music1);
        _Fade.gameObject.SetActive(true);
        gameplayUI.SetActive(false);
        hpBar.transform.parent.gameObject.SetActive(false);
        altf4.gameObject.SetActive(false);
        pauseMenu.SetActive(false);
        ChangeFadeColor(Color.black);
    }

    private void Update()
    {
        if (currentState == GameState.GAMEPLAY && !gameplayUI.activeSelf)
        {
            SetUIGameplay(true);
        }
        else if (currentState == GameState.HOME && gameplayUI.activeSelf)
        {
            SetUIGameplay(false);
        }

        if (currentState != GameState.HOME)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !pauseMenu.activeSelf)
            {
                pauseMenu.SetActive(true);
                currentState = GameState.PAUSE;
                SetTime(0);
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.activeSelf)
            {
                pauseMenu.SetActive(false);
                currentState = GameState.GAMEPLAY;
                SetTime(1);
            }
        }
    }

    void SetUIGameplay(bool isActive)
    {
        gameplayUI.SetActive(isActive);
        hpBar.transform.parent.gameObject.SetActive(isActive);
        altf4.gameObject.SetActive(isActive);
    }

    public void UpdateUI()
    {
        hpBar.fillAmount = (float)player.currentHp / (float)player.maxHp;
    }

    public void OnPickup(GameObject item)
    {
        drugLevel += 1;
        if (drugLevel >= 2)
        {
            _AudioController.ChangeMusic(_AudioController.music2);
        }

        Destroy(item);
        NextScene();
    }

    public void BackToMainMenu()
    {
        ChangeFadeColor(Color.black);
        StartCoroutine("BackToMenu");
    }

    public void ResetLevel()
    {
        ChangeFadeColor(Color.white);
        StartCoroutine("DelayResetScene");
    }

    public void NextScene()
    {
        player.currentHp = player.maxHp;
        UpdateUI();
        ChangeFadeColor(Color.white);
        StartCoroutine("DelayNextScene");
    }

    public void ChangeFadeColor(Color newColor)
    {
        _Fade.GetComponent<Image>().color = newColor;
    }

    public void SetTime(int time)
    {
        switch (time)
        {
            case 0:
                currentState = GameState.PAUSE;
                break;

            case 1:
                currentState = GameState.GAMEPLAY;
                break;
        }
        Time.timeScale = time;
    }
    IEnumerator BackToMenu()
    {
        _Fade.FadeIn();
        yield return new WaitUntil(() => _Fade.isFadeCompleted);
        SceneManager.LoadScene(0);
        yield return new WaitForSeconds(0.4f);
        _Fade.FadeOut();
    }

    IEnumerator DelayNextScene()
    {
        _Fade.FadeIn();
        yield return new WaitUntil(() => _Fade.isFadeCompleted);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        yield return new WaitForSeconds(0.4f);
        _Fade.FadeOut();

        if (player == null)
        {
            player = FindObjectOfType(typeof(PlayerController)) as PlayerController;
        }
    }

    IEnumerator DelayResetScene()
    {
        _Fade.FadeIn();
        yield return new WaitUntil(() => _Fade.isFadeCompleted);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        yield return new WaitForSeconds(0.4f);
        _Fade.FadeOut();

        if (player == null)
        {
            player = FindObjectOfType(typeof(PlayerController)) as PlayerController;
        }
    }

    #region AUDIO
    public void UpdateSlider()
    {
        musicSlider.value = _AudioController.music.volume;
        fxSlider.value = _AudioController.fx.volume;
    }

    public void UpdateVolume()
    {
        _AudioController.music.volume = musicSlider.value;
        _AudioController.fx.volume = fxSlider.value;
    }

    public void DesativarMusic()
    {
        _AudioController.music.mute = !_AudioController.music.mute;
    }

    public void DesativarFX()
    {
        _AudioController.fx.mute = !_AudioController.fx.mute;
    }

    public void SaveAudio()
    {
        PlayerPrefs.SetFloat("music", _AudioController.music.volume);
        PlayerPrefs.SetFloat("fx", _AudioController.fx.volume);
    }

    public void LoadAudio()
    {
        _AudioController.music.volume = PlayerPrefs.GetFloat("music");
        _AudioController.fx.volume = PlayerPrefs.GetFloat("fx");
    }

    #endregion
}
