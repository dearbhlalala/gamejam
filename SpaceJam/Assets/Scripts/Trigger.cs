using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Trigger : MonoBehaviour
{

    public GameObject dialoguePanel;

        public TMP_Text dialogueText;

    public string[] dialogue;

    private int index = 0;




    public bool playerIsClose;





    void Start()

    {

        dialogueText.text = "";

    }



    // Update is called once per frame

    void Update()

    {

        if (playerIsClose)

        {

            if (!dialoguePanel.activeInHierarchy)

            {

                dialoguePanel.SetActive(true);


            }

            else if (dialogueText.text == dialogue[index])

            {

                NextLine();

            }



        }

        if (Input.GetKeyDown(KeyCode.Q) && dialoguePanel.activeInHierarchy)

        {

            RemoveText();

        }

    }



    public void RemoveText()

    {

        dialogueText.text = "";

        index = 0;

        dialoguePanel.SetActive(false);

    }



    public void NextLine()

    {

        if (index < dialogue.Length - 1)

        {

            index++;

            dialogueText.text = "";


        }

        else

        {

            RemoveText();

        }

    }



    private void OnTriggerEnter2D(Collider2D other)

    

       

        {

            playerIsClose = true;

        }

    



    private void OnTriggerExit2D(Collider2D other)

    

        

        {

            playerIsClose = false;

            RemoveText();

        }

    

}

