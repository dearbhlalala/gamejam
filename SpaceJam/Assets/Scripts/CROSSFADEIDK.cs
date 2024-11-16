using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CROSSFADEIDK : MonoBehaviour
{
    // Start is called before the first frame update
     public Animator transition;
   public void LoadTheLevel(int index)
   {
    StartCoroutine(LoadNextLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadNextLevel(int levelIndex)
    {
        transition.SetTrigger("START");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(levelIndex);
    }

}
