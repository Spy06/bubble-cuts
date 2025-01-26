using Unity.VisualScripting;
using UnityEngine;

public class BeatItem : MonoBehaviour
{
    bool inTrigger, exitTrigger, destroyed;

    public SpriteRenderer spriteRenderer;
    void Start (){
        spriteRenderer.color = new Color (1,1,1,InputReader.instance.a);
    }

    void Update()
    {
        if (!destroyed)transform.localPosition += new Vector3 (Time.deltaTime, 0);

        if (destroyed){
            Color e = spriteRenderer.color;

            float a = e.a;
            a -= Time.deltaTime * 10f;
            spriteRenderer.color = new Color (e.r, e.g, e.b, e.a - Time.deltaTime * 2f);
        }

        if (transform.position.x > 8f && !destroyed){
            Destroy (false);
        }
    }

    public void Destroy (bool finished){
        destroyed = true;
        if (!finished)
            InputReader.instance.Fail ();
    }

    void OnTriggerEnter2D (Collider2D other){
        if (other.CompareTag ("SafeZone")){
            InputReader.instance.ReadInput (this);
            inTrigger = true;
        }
    }

    void OnTriggerExit2D (Collider2D other){
        if (other.CompareTag ("SafeZone"))
            InputReader.instance.ReadInput (null, this);
    }
}