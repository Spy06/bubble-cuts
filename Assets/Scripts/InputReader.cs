using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//GANTI PAKE COLLISION, BUKAN TIMER
public class InputReader : MonoBehaviour, ITickHandler
{
    public static InputReader instance;
    void Awake (){
        if (instance == null)instance = this;
        else Destroy (this);
    }

    public float tick, scale = 0;
    public Transform safeZone, cubePref, failTextPref, canvasParent;
    public List <BeatItem> currentBeatItem = new List<BeatItem> ();
    public Bubble bubble1, bubble2;
    private bool usingBubble1 = true, usingBubble2;
    int failCounter;
    float maxTick;
    public string[] successDictionary = new string[] {
        "NICE", "GOOD JOB", "UWWOGGHH", "BEUH", "ANJAY"
    };
    public string[] failDictionary = new string[] {
        "FAIL", "MISSED!", "SKILL ISSUE", "NOOB"
    };
    private float maxGameLength;
    private float gameLength;
    public float[] startLength = new float []{
        9f};

    private int musicID;

    public TMP_Text tex;
    public float a;
    public GameObject gameOverScreen;

    public bool GameStarted (){
        return gameLength > startLength [musicID];
    }

    void Start (){
        Application.targetFrameRate = 60;
        musicID = GameManager.instance.Randomize ();
        Core.tickHandler = this;
        GameManager.instance.bgm.Stop ();
        GameManager.instance.bgm.Play ();
        maxGameLength = GameManager.instance.bgm.clip.length;
    }

    void Update()
    {
        gameLength = GameManager.instance.bgm.time;
        tex.text = gameLength.ToString ();
        a = gameLength > startLength [musicID] ? 1 : 0;
        if (!Core.running)return;
        
        GetInput ();
        BubbleLogic ();

        if (failCounter > 8){
            GameManager.instance.GameOver (false);
        }

        if (failCounter < 8 && gameLength >= maxGameLength){
            GameManager.instance.GameOver (true);
        }

        if (Input.GetKeyDown (KeyCode.Escape))Application.Quit ();
    }

    public void Restart (){
        failCounter = 0;
        maxTick = 0;
        tick = 0;
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag ("WalkingCube");
        foreach (GameObject a in gameObjects)
            Destroy (a);
        scale = 0f;
        GameManager.instance.Restart ();
    }

    private void BubbleLogic (){
        if (usingBubble1){
            bubble2.activated = true;
            bubble1.activated = false;
        } else if (usingBubble2) {
            bubble1.activated = true;
            bubble2.activated = false;
        }
    }

    public void ReadInput (BeatItem beatItem, BeatItem caller = null){
        if (gameLength < startLength [musicID])return;
        if (beatItem == null){
            for (int i = 0; i < currentBeatItem.Count; i++){
                if (currentBeatItem [i] == caller){
                    currentBeatItem.Remove (currentBeatItem[i]);
                    break;
                }
            }
        }
        else currentBeatItem.Add (beatItem);
    }

    private void GetInput (){
        tick += Time.deltaTime * 100;
        if (gameLength < startLength [musicID])return;
        if (Input.GetKeyDown (KeyCode.Space)){
            if (currentBeatItem.Count <= 0){
                scale -= scale > 0 ? 1 : 0;
                Dimana ();
            }
            else {
                if (currentBeatItem.Count > 0){
                    Success ();
                    scale += UnityEngine.Random.Range (1,2);
                    if (usingBubble1){
                        bubble1.SetSprite (true);
                        bubble2.SetSprite (false);
                    } else if (usingBubble2){
                        bubble2.SetSprite (true);
                        bubble1.SetSprite (false);
                    }
                    if (scale > 17){
                        ReleaseBubble ();
                        scale = 0f;
                    }
                    for (int i = 0; i < currentBeatItem.Count; i++){
                        currentBeatItem [i]?.Destroy (true);
                        currentBeatItem.Remove (currentBeatItem [i]);
                    }
                }
            }
        }
    }

    void Dimana (){
        if (gameLength < startLength [musicID])return;
        failCounter++;
        RectTransform e = Instantiate (failTextPref, canvasParent).GetComponent <RectTransform>();
        e.anchoredPosition = new Vector2 (UnityEngine.Random.Range (-600, -750), e.anchoredPosition.y);
        string text = "Mana JIR";
        e.GetComponent <TMP_Text>().text = text;
    }

    private void Success (){
        if (gameLength < startLength [musicID])return;
        RectTransform e = Instantiate (failTextPref, canvasParent).GetComponent <RectTransform>();
        e.anchoredPosition = new Vector2 (UnityEngine.Random.Range (-100, 100), e.anchoredPosition.y);
        string text = successDictionary [UnityEngine.Random.Range (0, successDictionary.Length)];
        e.GetComponent <TMP_Text>().text = text;
        e.GetComponent <TMP_Text>().color = Color.green;
    }

    public void Fail (){
        if (gameLength < startLength [musicID])return;
        failCounter++;
        RectTransform e = Instantiate (failTextPref, canvasParent).GetComponent <RectTransform>();
        e.anchoredPosition = new Vector2 (UnityEngine.Random.Range (600, 750), e.anchoredPosition.y);
        string text = failDictionary [UnityEngine.Random.Range (0, failDictionary.Length)];
        e.GetComponent <TMP_Text>().text = text;
    }

    private void ReleaseBubble (){
        if (usingBubble1){
            usingBubble2 = true;
            usingBubble1 = false;
        } else if (usingBubble2){
            usingBubble1 = true;
            usingBubble2 = false;
        }
    }

    void SetCubes (float maxTick){
        if (gameLength < startLength [musicID])return;
        GameObject e = Instantiate (cubePref.gameObject, new Vector3 ((-maxTick / 100) * 24, 0), Quaternion.identity, safeZone.transform.parent);
        e.GetComponent <SpriteRenderer>().color = Color.white;
        e.transform.localPosition = new Vector3 ((-maxTick / 100), 0);
    }

    public void Tick (){
        if (!Core.running)return;
        maxTick = maxTick == 0 ? tick : maxTick;
        tick = 0;
        SetCubes (maxTick);
    }

    public void GameOver (){
        canvasParent.gameObject.SetActive (false);
        GameManager.instance.QuickGameOver ();
    }
}
