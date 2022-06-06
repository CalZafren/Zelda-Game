using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{

    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;
    public bool playerInRange;
    public GameObject hintBox;
    public Text hintText;
    public string hint;
    public bool alreadyShowedHint = false;
    
    void Update(){
        //Checks to see if player is in range and presses input
        if(Input.GetKeyDown(KeyCode.Space) && playerInRange){
            if(dialogBox.activeInHierarchy){
                dialogBox.SetActive(false);
                if(!alreadyShowedHint){
                    hintBox.SetActive(true);
                }
            }else{
                dialogText.text = dialog;
                dialogBox.SetActive(true);
                hintBox.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            playerInRange = true;
            //Sets hint active
            hintText.text = hint;
            hintBox.SetActive(true);
            alreadyShowedHint = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        if(other.CompareTag("Player")){
            playerInRange = false;
            dialogBox.SetActive(false);
            hintBox.SetActive(false);
        }
    }
}
