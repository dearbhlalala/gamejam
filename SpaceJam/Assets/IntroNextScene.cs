using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroNextScene : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.LoadScene("Dialogue", LoadSceneMode.Single);
    }
    }
   
