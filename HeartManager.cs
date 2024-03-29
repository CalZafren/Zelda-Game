using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HeartManager : MonoBehaviour
{

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;
    public FloatValue heartContainers;
    public FloatValue playerCurrentHealth;

    // Start is called before the first frame update
    void Start()
    {
        InitHearts();
    }
    

    public void InitHearts(){
        for (int i = 0; i < heartContainers.initialValue; i++)
        {
            hearts[i].sprite = fullHeart;
            hearts[i].gameObject.SetActive(true);
        }
    }

    public void UpdateHearts(){
        float tempHealth = (playerCurrentHealth.runtimeValue)/2;

        for (int i = 0; i < heartContainers.initialValue; i++)
        {
            if(i <= tempHealth-1){
                //full heart
                hearts[i].sprite = fullHeart;
            }else if(i >= tempHealth){
                //empty heart
                hearts[i].sprite = emptyHeart;
            }else{
                //half heart
                hearts[i].sprite = halfHeart;
            }
        }
    }

}
