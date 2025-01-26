using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour
{
    public void NextScene (){
        SceneManager.LoadScene (2);
    }
}
