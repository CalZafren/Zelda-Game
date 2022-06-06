using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState{
    walk,
    attack,
    interact,
    stagger
}

public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;
    private Rigidbody2D myRigidbody;
    [HideInInspector]
    public Vector3 change;
    private Animator animator;
    public float speed;
    public FloatValue currentHealth;
    public SignalSender PlayerHealthSignal;

    // Start is called before the first frame update
    void Start()
    {
        currentState = PlayerState.walk;
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
    }

    // Update is called once per frame
    void Update()
    {

        //Gets Input
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");

        if(Input.GetButtonDown("Attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger){
            StartCoroutine(Attack());
        }else if(currentState == PlayerState.walk){
            UpdateAnimationsAndMove();    
        }
    }

    void UpdateAnimationsAndMove(){
        
        if(change != Vector3.zero){
            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
        }else{
            animator.SetBool("moving", false);
        }
    }

    void MoveCharacter(){
        change = change.normalized;
        myRigidbody.MovePosition(transform.position + change * speed * Time.fixedDeltaTime);
    }
    
    private IEnumerator Attack(){
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.33f);
        currentState = PlayerState.walk;
    }

    public void Knock(Rigidbody2D myRigidBody, Transform knockObject, float knockTime, float thrust, float damage){
        if(currentHealth.runtimeValue > 0){
            StartCoroutine(KnockBack(myRigidBody, knockObject, knockTime, thrust));
            TakeDamage(damage);
        }   
    }

    private IEnumerator KnockBack(Rigidbody2D myRigidBody, Transform knockObject, float knockTime, float thrust){
        if(myRigidBody != null && currentState != PlayerState.stagger){
            //Sets player state to stagger
            currentState =  PlayerState.stagger;
            //Handles the knockback
            Vector2 forceDirection = transform.position - knockObject.transform.position;
            Vector2 force = forceDirection.normalized * thrust;
            myRigidBody.velocity = force;
            //Waits for a specified amount of time, then sets player state back to walk and removes force
            yield return new WaitForSeconds(knockTime);
            myRigidBody.velocity = Vector2.zero;
            currentState = PlayerState.walk;
        }
    }

    public void TakeDamage(float damage){
        currentHealth.runtimeValue -= damage;
        PlayerHealthSignal.Raise();

        if(currentHealth.runtimeValue <= 0){
            Die();
        }
    }

    private void Die(){
        this.gameObject.SetActive(false);
    }
}
