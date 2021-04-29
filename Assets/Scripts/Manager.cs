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

public class Manager : MonoBehaviour
{
    public AudioController _AudioController;
    public PlayerController player;
    public GameState currentState;
    public GameObject gameplayUI;
    public GameObject pauseMenu;
    public TMP_Text altf4;
    public Image hpBar;
    public Fade _Fade;
    public int drugLevel = 0;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        _AudioController = FindObjectOfType(typeof(AudioController)) as AudioController;
        _AudioController.ChangeMusic(_AudioController.music1);
        _Fade.gameObject.SetActive(true);
        gameplayUI.SetActive(false);
        altf4.gameObject.SetActive(false);
        pauseMenu.SetActive(false);
    }

    private void Update()
    {
        if (currentState == GameState.HOME)
        {
            gameplayUI.SetActive(true);
            altf4.gameObject.SetActive(true);
            UpdateUI();
        }
        else
        {
            gameplayUI.SetActive(false);
            altf4.gameObject.SetActive(false);
        }

        if(currentState != GameState.HOME)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !pauseMenu.activeSelf)
            {
                pauseMenu.SetActive(true);
                currentState = GameState.PAUSE;
            }
            else if(Input.GetKeyDown(KeyCode.Escape) && pauseMenu.activeSelf)
            {
                pauseMenu.SetActive(false);
                currentState = GameState.GAMEPLAY;
            }
        }
    }

    void UpdateUI()
    {
        hpBar.fillAmount = player.currentHp / player.maxHp;
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

    public void ResetLevel()
    {
        StartCoroutine("DelayResetScene");
    }

    public void NextScene()
    {
        StartCoroutine("DelayNextScene");
    }

    IEnumerator DelayNextScene()
    {
        _Fade.FadeIn();
        yield return new WaitUntil(() => _Fade.isFadeCompleted);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        yield return new WaitForSeconds(0.4f);
        _Fade.FadeOut();

        if(player == null)
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
}
