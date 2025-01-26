using TMPro;
using UnityEngine;

public class FailTextBehaviour : MonoBehaviour
{
    void Start (){
        Destroy (gameObject, 3f);
    }
    void Update()
    {
        GetComponent<RectTransform>().anchoredPosition += new Vector2 (0, Time.deltaTime * 100f);
        Color e = GetComponent <TMP_Text> ().color;

        float a = e.a;
        a -= Time.deltaTime * 10f;
        GetComponent <TMP_Text> ().color = new Color (e.r, e.g, e.b, e.a - Time.deltaTime * 2f);
    }
}
