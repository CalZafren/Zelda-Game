using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomMove : MonoBehaviour
{

    public Vector2 newMaxPosition;
    public Vector2 newMinPosition;
    public Vector3 playerChange;
    private CameraMovement cam;
    public GameObject titleCard;
    public Text titleCardText;
    public bool needTitleCard;
    public string placeName;

    void Start(){
        cam = Camera.main.GetComponent<CameraMovement>();
    }

    private void OnTriggerEnter2D(Collider2D other){
        //Check to see if collider is the player
        if(other.CompareTag("Player") && !other.isTrigger){
            cam.minPosition = newMinPosition;
            cam.maxPosition = newMaxPosition;
            other.transform.position += playerChange;
            if(needTitleCard){
                StartCoroutine(ShowPlaceName());
            }
        }
    }

    private IEnumerator ShowPlaceName(){
        titleCardText.text = placeName;
        titleCard.SetActive(true);
        yield return new WaitForSeconds(3f);
        titleCard.SetActive(false);
    }
}
