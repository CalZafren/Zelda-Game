using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{

    private Rigidbody2D objectRB;
    private Transform thisObject;
    public float thrust;
    public float knockBackTime;
    public float damage;

    void Start(){
        thisObject = this.transform;
    }

    private void OnTriggerEnter2D(Collider2D other){
        objectRB = other.GetComponent<Rigidbody2D>();

        if(other.CompareTag("Player")){
            //Only runs this if object is not null and player is not staggered
            if(objectRB != null && other.GetComponent<PlayerMovement>().currentState != PlayerState.stagger){
                objectRB.GetComponent<PlayerMovement>().Knock(objectRB, thisObject, knockBackTime, thrust, damage);
            }
        }

        if(other.CompareTag("Enemy") && other.isTrigger){
            if(objectRB != null){
                objectRB.GetComponent<Enemy>().Knock(objectRB, thisObject, knockBackTime, thrust, damage);
            }
        }

        if(other.CompareTag("Breakable") && this.gameObject.CompareTag("Player")){
            other.GetComponent<BreakableObject>().Break();
        }
    }
}
