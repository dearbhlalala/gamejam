using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroDialogueNextScene : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.LoadScene("orbtest", LoadSceneMode.Single);
    }
    }
   
