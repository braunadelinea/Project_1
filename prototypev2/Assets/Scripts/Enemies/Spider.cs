using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;
using UnityEngine.UIElements;
public class Spider : MonoBehaviour
{
    //SCENE VARIABLES 
    private GameObject currentroom;
    private GameObject player; 

    //STATE MACHINE VARIABLES 
    public enum Phase {Wander, Buildup, Running, Attacking, Stunned};
    private Phase currentphase = Phase.Wander;
    private float phasestartTime;
    
    //MISC VARIABLES 
    private const float wanderLength = 2;
    
    //GRAPHICS VARIABLES 
    private Animator animator;

    //ATTACK VARIABLES 
    float distToPlayer;
    private float attackTimer = 0f; 
    private float stunnedTimer = 0f; 
    private float buildupTimer = 0f;
    private float runningTimer = 0f; 
    private const float startLungeDistance = 4;
    private const float stopLungeDistance = 5; 
    private const float startAttackDistance = 1f;
    private const float stopAttackDistance = 2;
    private const float attackInterval = 1;
    private float lastAttackTime;

    //MOVEMENT VARIABLES
    private float speed = 0.5f;
    private Vector3 movementdir;

    //PRIVATE METHODS
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        phasestartTime = Time.time;
        animator = gameObject.GetComponent<Animator>(); 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentroom != null)
        {
            return;
        }
        else {
            currentroom = collision.gameObject;
            Debug.Log("Room not found for entity " + gameObject.name + " assigning to current detected."); 
        }
    }
    private void Update()
    {
        if (currentphase == Phase.Buildup) {
            buildupTimer += Time.deltaTime; 
        }
        if (currentphase == Phase.Stunned) {
            stunnedTimer += Time.deltaTime; 
        }
        if (currentphase == Phase.Attacking) {
            attackTimer += Time.deltaTime; 
        }
        if (currentphase == Phase.Running) {
            runningTimer += Time.deltaTime; 
        }
    }
    private void FixedUpdate()
    {
        if (currentroom == GameObject.FindGameObjectWithTag("player").GetComponent<PlayerManager>().currentroom && GameObject.Find("Spawner").GetComponent<spawnplayer>().GetSpawnedPlayer())
        {
            UpdatePhase();
            ApplyPhase();
            ChangeSpriteDirection();
        }
    }
    private void UpdatePhase() {
        distToPlayer = Vector2.Distance(player.transform.position, this.transform.position);
        Phase previousphase = currentphase;
        switch (currentphase) {
            //if Wandering, and the player is within range, begin the Lunge sequence. 
            case Phase.Wander:
                if (distToPlayer < startLungeDistance)
                {
                    //Debug.Log("Beginning Buildup"); 
                    currentphase = Phase.Buildup;
                    buildupTimer = 0; 
                    animator.SetTrigger("Start Buildup");
                }
                else if (Time.time >= phasestartTime + wanderLength) {
                    currentphase = Phase.Wander;
                    animator.SetTrigger("Start Wander");  
                }
                break;
            case Phase.Buildup:
                if (buildupTimer > 2) {
                    //Debug.Log("Lunging"); 
                    ChangeMoveDir((player.transform.position - this.transform.position).normalized);
                    currentphase = Phase.Running;
                    buildupTimer = 0;
                    runningTimer = 0; 
                    animator.SetTrigger("Start Running"); 
                }
                break;
            case Phase.Running:
                if (runningTimer > 4) {
                    currentphase = Phase.Wander;
                    runningTimer = 0;
                    animator.SetTrigger("Start Wander");
                }
                break;
            case Phase.Attacking:
                if (distToPlayer > stopAttackDistance) {
                    //Debug.Log("Wandering"); 
                    currentphase = Phase.Wander;
                    animator.SetTrigger("Start Wander"); 
                }
                break;
            case Phase.Stunned:
                if (stunnedTimer > 2) {
                    //Debug.Log("Stunned, Wandering"); 
                    currentphase = Phase.Wander;
                    animator.SetTrigger("Start Wander"); 
                    stunnedTimer = 0; 
                }
                break; 
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Debug.Log("Wall Collision Detected, Stunned");
            currentphase = Phase.Stunned;
            animator.SetTrigger("Start Stunned");
        }
        else if (collision.gameObject.tag == "player") {
            Debug.Log("Player Collision Detected, attacking");
            currentphase = Phase.Attacking;
            animator.SetTrigger("Start Attacking");
        }
    }
    private void ApplyPhase() {
        switch (currentphase) {
            case Phase.Wander:
                Debug.Log("Wandering"); 
                if (movementdir == Vector3.zero)
                {
                    speed = 0.10f; 
                    System.Random rand = new System.Random();
                    ChangeMoveDir(new Vector3((float)rand.NextDouble() - 0.5f, (float)rand.NextDouble() - 0.5f).normalized);
                }
                ApplyMovement();
                break;
            case Phase.Running:
                Debug.Log("Running"); 
                speed = 0.25f; 
                ApplyMovement(); 
                break;
            case Phase.Attacking:
                Debug.Log("Attacking"); 
                if (attackTimer > 1)
                {
                    player.GetComponent<PlayerManager>().DecreaseHealth(1);
                    attackTimer = 0; 
                }
                break;
        }
    }
    private void ApplyMovement() {
        if (movementdir == Vector3.zero)
        {
            return;
        }
        if (Phase.Running != currentphase) 
        {
            Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position + movementdir * speed, 0.5f);
            foreach (Collider2D collider in collisions)
            {
                string tag = collider.gameObject.tag;
                //stop if the enemy is close to a wall
                if (tag == "player" || tag == "Wall")
                {
                    ChangeMoveDir(Vector3.zero);
                    return;
                }
                else if (collider.gameObject != currentroom && tag == "Room")
                {
                    ChangeMoveDir(Vector3.zero);
                    return;
                }
            }
        }
        transform.position += movementdir * speed;
    }
    private void ChangeSpriteDirection() {
        if (movementdir.x > 0)
        {
            Vector3 temp = transform.localScale;
            temp.x = 1;
            gameObject.transform.localScale = temp;
        }
        else if (movementdir.x < 0)
        {
            Vector3 temp = transform.localScale;
            temp.x = -1;
            gameObject.transform.localScale = temp;
        }
        if (currentphase == Phase.Attacking)
        {
            if (gameObject.transform.position.x - player.transform.position.x > 0)
            {
                Vector3 temp = transform.localScale;
                temp.x = -1;
                gameObject.transform.localScale = temp;
            }
            else
            {
                Vector3 temp = transform.localScale;
                temp.x = 1;
                gameObject.transform.localScale = temp;
            }
        }
    }
    private void ChangeMoveDir(Vector2 newDirection)
    {
        movementdir = newDirection;
    }
}
