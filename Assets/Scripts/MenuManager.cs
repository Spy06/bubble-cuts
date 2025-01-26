using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;
    void Awake (){
        Time.timeScale = 1f;
        if (instance == null)instance = this;
        else Destroy (this);

        foreach (Animator anim in anims){
            anim.speed = 0f;
        }

        Settings.volume = 1;
    }

    public bool credits;
    public Image[] images;
    public TMP_Text[] texts;
    public Button[] buttons;
    public Image l;
    public Animator[] anims;

    void Update (){
        foreach (Button btn in buttons){
            btn.interactable = !credits;
        }
        if (credits){
            foreach (Image img in images){
                float a = Mathf.Lerp (img.color.a, 0, Time.deltaTime * 5f);
                img.color = new Color (img.color.r, img.color.g, img.color.b, a);
            }

            foreach (TMP_Text tex in texts){
                float a = Mathf.Lerp (tex.color.a, 0, Time.deltaTime * 5f);
                tex.color = new Color (tex.color.r, tex.color.g, tex.color.b, a);
            }

            float c = Mathf.Lerp (l.color.a, .4f, Time.deltaTime * 5f);
            l.color = new Color (l.color.r, l.color.g, l.color.b, c);
        } else {
            foreach (Image img in images){
                float a = Mathf.Lerp (img.color.a, 1, Time.deltaTime * 5f);
                img.color = new Color (img.color.r, img.color.g, img.color.b, a);
            }

            foreach (TMP_Text tex in texts){
                float a = Mathf.Lerp (tex.color.a, 1, Time.deltaTime * 5f);
                tex.color = new Color (tex.color.r, tex.color.g, tex.color.b, a);
            }

            float c = Mathf.Lerp (l.color.a, 0, Time.deltaTime * 5f);
            l.color = new Color (l.color.r, l.color.g, l.color.b, c);
        }
    }

    public void PlayGame (){
        foreach (Animator anim in anims){
            anim.speed = 1;
            anim.SetTrigger ("playGame");
        }
        StartCoroutine (ChangeLevel ());
    }

    public IEnumerator ChangeLevel (){
        yield return new WaitForSeconds (1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
