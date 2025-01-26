using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator animator;

    public void Damage (){
        animator.SetBool ("dead", true);
        StartCoroutine (EnableMenu ());
    }

    IEnumerator EnableMenu (){
        yield return new WaitForSeconds (1f);
        InputReader.instance.GameOver ();
    }
}
