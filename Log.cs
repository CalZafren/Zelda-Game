using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Enemy
{

    private Rigidbody2D rigidBody;
    private Transform target;
    public Transform homePosition;
    public Animator anim;
    public float chaseRadius;
    public float attackRadius;
    private float halfSpeed;
    private float fullSpeed;
    private bool alreadyAttacked = false;

    

    // Start is called before the first frame update
    void Start()
    {
       target = GameObject.FindWithTag("Player").transform;
       rigidBody = GetComponent<Rigidbody2D>();
       anim = GetComponent<Animator>();
       currentState = EnemyState.idle;
       halfSpeed = moveSpeed/4;
       fullSpeed = moveSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();
        DetermineSpeed();
    }

    void CheckDistance(){
        //Only follows the player if player is within chase distance, and stops right before the player gets in attack range
        if((Vector3.Distance(transform.position, target.position) <= chaseRadius) && (Vector3.Distance(transform.position, target.position) >= attackRadius)){
            //Cancels invoke damage call
            CancelInvoke();
            alreadyAttacked = false;
            //chase target
            if((currentState == EnemyState.idle) || (currentState == EnemyState.walk)){
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                ChangeAnim(temp - transform.position);
                rigidBody.MovePosition(temp);
                ChangeState(EnemyState.walk);
                anim.SetBool("wakingUp", true);
            }
        }else if(Vector3.Distance(transform.position, target.position) >= chaseRadius){
                //Cancels invoke damage call
                CancelInvoke();
                alreadyAttacked = false;
                anim.SetBool("wakingUp", false);
                currentState = EnemyState.idle;
                //Only damages the player if the player is still
        }else if(Vector3.Distance(transform.position, target.position) <= attackRadius && (target.GetComponent<PlayerMovement>().change == Vector3.zero)){
            //Constantly damages player if they are in attack rangwe
            if(!alreadyAttacked){
                InvokeRepeating("DamagePlayer", .5f, 1f);
                alreadyAttacked = true;
            }
        }
    }

    void DetermineSpeed(){
        if(target.GetComponent<PlayerMovement>().currentState == PlayerState.stagger){
            moveSpeed = halfSpeed;
        }else{
            moveSpeed = fullSpeed;
        }
    }

    private void ChangeAnim(Vector3 direction){

        //Checks which direction enemy is moving
        if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y)){
            if(direction.x > 0){
                SetAnimFloat(Vector2.right);
            }else if(direction.x < 0){
                SetAnimFloat(Vector2.left);            }
        }else if(Mathf.Abs(direction.x) < Mathf.Abs(direction.y)){
            if(direction.y > 0){
                SetAnimFloat(Vector2.up);
            }else if(direction.y < 0){
                SetAnimFloat(Vector2.down);            }
        }
    }

    private void DamagePlayer(){
        target.GetComponent<PlayerMovement>().TakeDamage(GetComponent<Knockback>().damage);
    }

    private void SetAnimFloat(Vector2 setVector){
        anim.SetFloat("moveX", setVector.x);
        anim.SetFloat("moveY", setVector.y);
    }

    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
