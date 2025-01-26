using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameCircle : MonoBehaviour
{
    public void BackToMenu (){
        SceneManager.UnloadSceneAsync (2);
        SceneManager.LoadSceneAsync (0);
    }
}
