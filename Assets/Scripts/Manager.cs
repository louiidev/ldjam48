using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public PlayerController player;
    public Image hpBar;
    public Fade _Fade;
    public int drugLevel = 0;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        _Fade.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (player != null)
        {
            UpdateUI();
        }
    }
    
    void UpdateUI()
    {
        hpBar.fillAmount = player.currentHp / player.maxHp;
    }

    public void OnPickup(GameObject item)
    {
        drugLevel += 1;
        Destroy(item);
        NextScene();
    }

    public void NextScene()
    {
        StartCoroutine("DelayNextScene");   
    }

    IEnumerator DelayNextScene()
    {
        _Fade.FadeIn();
        yield return new WaitUntil(() => _Fade.isFadeCompleted);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
