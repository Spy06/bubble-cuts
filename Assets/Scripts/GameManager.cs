using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public AudioSource bgm;
    public AudioClip[] bgms;
    public GameObject panel;
    public bool gameOver;
    public Image targetText;
    public Sprite win, lose;
    void Awake (){
        if (instance == null)instance = this;
        else Destroy (this);
    }

    void Start (){
        bgm.volume = Settings.volume;
    }

    public int Randomize (){
        int random = Random.Range (0, bgms.Length);
        bgm.clip = bgms [random];
        return random;
    }

    void Update (){
        if (gameOver){
            bgm.volume = Mathf.Lerp (bgm.volume, 0, Time.deltaTime * 10f);
            //Time.timeScale = Mathf.Lerp (Time.timeScale, 0, Time.deltaTime * 10f);
        }
    }
    
    public void Restart (){
        Core.tickHandler = null;
        Core.running = false;
        SceneManager.UnloadSceneAsync (2);
        SceneManager.LoadSceneAsync (2);
        Time.timeScale = 1f;
        //StartCoroutine (RestartGame ());
    }

    public void Menu (){
        Core.tickHandler = null;
        Core.running = false;
    }

    public void GameOver (bool win){
        gameOver = true;
        Core.running = false;
        PopUIMenu (win);
    }

    public void QuickGameOver (){
        gameOver = true;
        Core.running = false;
        PopUIMenu (false);
    }

    void PopUIMenu (bool winning){
        panel.SetActive (true);
        targetText.sprite = winning ? win : lose;
    }

    IEnumerator RestartGame (){
        bgm.Stop ();
        Core.running = false;
        yield return new WaitForSeconds (3);
        Core.running = true;
        bgm.Play ();
    }
}
