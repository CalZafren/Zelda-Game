using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyState{
    idle,
    walk,
    attack,
    stagger
}

public class Enemy : MonoBehaviour
{
    public EnemyState currentState;
    public FloatValue maxHealth;
    private float health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;

    private void Awake(){
        health = maxHealth.initialValue;
    }

    public void ChangeState(EnemyState newState){
        if(newState != currentState){
            currentState = newState;
        }
    }

    public void Knock(Rigidbody2D myRigidBody, Transform knockObject, float knockTime, float thrust, float damage){
        StartCoroutine(KnockBack(myRigidBody, knockObject, knockTime, thrust));
        //Damage the enemy
        TakeDamage(damage);
    }

    private IEnumerator KnockBack(Rigidbody2D myRigidBody, Transform knockObject, float knockTime, float thrust){
        if(myRigidBody != null && currentState != EnemyState.stagger){
            //Sets enemy state to stagger
            currentState =  EnemyState.stagger;
            //Handles the knockback
            Vector2 forceDirection = transform.position - knockObject.transform.position;
            Vector2 force = forceDirection.normalized * thrust;
            myRigidBody.velocity = force;
            //Waits for a specified amount of time, then sets enemy state back to idle and removes force
            yield return new WaitForSeconds(knockTime);
            myRigidBody.velocity = Vector2.zero;
            currentState = EnemyState.idle;
        }
    }

    private void TakeDamage(float damage){
        health -= damage;
        if(health <= 0){
            Die();
        }

    }

    private void Die(){
        this.gameObject.SetActive(false);
    }
}
