using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipZone : MonoBehaviour
{   

    public GameObject hintBox;
    public Text hintText;
    public string hint;
    private bool alreadyShownTip = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            if(!alreadyShownTip){
                alreadyShownTip = true;
                StartCoroutine(ShowTip());
            }
        }
    }

    IEnumerator ShowTip(){
        hintText.text = hint;
        hintBox.SetActive(true);
        yield return new WaitForSeconds(5f);
        hintBox.SetActive(false);
    }

}
