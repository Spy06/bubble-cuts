using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public int maxHP = 4;
    private int currentHP;
    private bool isDamaged;
    public float damageDelay = 0.3f;
    bool attacking;
    public Player player;
    void Start()
    {
        currentHP = maxHP;
        EnemyManager.instance.SetEnemy (this);
        player = GameObject.FindWithTag ("Player").GetComponent <Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDamaged && !attacking)
            transform.position += new Vector3 (-Time.deltaTime * speed, 0);
        
        if (Vector3.Distance (transform.position, player.transform.position) <= 5){
            if (!attacking)StartCoroutine (Attack ());
        }

        if (currentHP <= 0){
            EnemyManager.instance.currentEnemy = null;
            Destroy (gameObject);
        }
    }

    IEnumerator Attack (){
        yield return new WaitForSeconds (1f);
        player.Damage ();
    }

    void OnCollisionEnter2D (Collision2D other){
        if (other.collider.CompareTag ("Bubble") && other.collider.GetComponent <Bubble>().activated){
            currentHP--;
            StartCoroutine (Damage ());

            other.collider.GetComponent <Bubble>().Pop ();
        }
    }

    /*void OnTriggerStay2D (Collider2D other){
        if (other.CompareTag ("Bubble") && other.GetComponent <Bubble>().activated){
            currentHP--;
            StartCoroutine (Damage ());

            other.GetComponent <Bubble>().Pop ();
        }
    }*/

    IEnumerator Damage (){
        isDamaged = true;
        yield return new WaitForSeconds (damageDelay);
        isDamaged = false;
    }
}
