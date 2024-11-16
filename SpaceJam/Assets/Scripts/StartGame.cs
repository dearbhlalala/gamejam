using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
   public void LoadLevel(int index)
   {
    Debug.Log("Loading Game...");
    SceneManager.LoadScene(index);
    }

     public void QuitGame()
    {
        Debug.Log("Exiting Game...");
        Application.Quit();
    }
}
