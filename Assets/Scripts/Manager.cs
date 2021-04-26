using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Manager : MonoBehaviour
{
    public AudioController _AudioController;
    public PlayerController player;
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
        hpBar.gameObject.transform.parent.gameObject.SetActive(false);
        altf4.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (player != null)
        {
            UpdateUI();
            hpBar.gameObject.transform.parent.gameObject.SetActive(true);
            altf4.gameObject.SetActive(false);
        }
        else
        {
            hpBar.gameObject.transform.parent.gameObject.SetActive(false);
            altf4.gameObject.SetActive(false);
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
        _Fade.FadeOut();
    }

    IEnumerator DelayResetScene()
    {
        _Fade.FadeIn();
        yield return new WaitUntil(() => _Fade.isFadeCompleted);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        _Fade.FadeOut();
    }
}
