using UnityEngine;

public class Bubble : MonoBehaviour
{
    public bool activated, popped;
    public SpriteRenderer spriteRenderer;
    public GameObject poppedBubble;
    public Rigidbody2D rb;
    public Sprite [] sprites;

    void Update (){
        if (popped)GetComponent <SpriteRenderer>().enabled = InputReader.instance.GameStarted ();
        if (activated){
            if (collided)return;
            GetComponent <Collider2D>().enabled = true;
            rb.AddForce (Vector2.right * 2f, ForceMode2D.Impulse);
        } else {
            if (!popped){
                collided = false;
                spriteRenderer.enabled = true;
                rb.linearVelocity = Vector2.zero;
                transform.localPosition = Vector3.zero;
            }
        }
    }

    public void SetSprite (bool processed){
        if (spriteRenderer != null)
            spriteRenderer.sprite = sprites [processed ? (int)InputReader.instance.scale : 0];
    }

    bool collided;

    public void Pop (){
        GetComponent <Collider2D>().enabled = false;
        GameObject instantiatedBubble = Instantiate (poppedBubble, transform.position, transform.rotation);
        spriteRenderer.sprite = null;
        collided = true;
    }

    public void Destroy (){
        Destroy (gameObject);
    }
}