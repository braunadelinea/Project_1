using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class IceDino : MonoBehaviour
{
    private GameObject currentroom;
    private GameObject player;
    public enum Phase {Idle, Attacking}
    private Phase currentPhase = Phase.Idle;
    private float phaseStartTime;

    private float idleLength = 2f;

    private float idleTimer = 0; 
    private Animator animator;
    public GameObject iceShard;
    private Vector3 movementDirection; 

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        phaseStartTime = Time.time;
        animator = gameObject.GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentroom != null)
        {
            return;
        }
        else
        {
            currentroom = collision.gameObject;
            Debug.Log("Room not found for entity " + gameObject.name + " assigning to current detected.");
        }
    }
    private void Update()
    {
        Debug.Log(currentPhase); 
        if (currentPhase == Phase.Idle) {
            idleTimer += Time.deltaTime;
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
        Phase previousphase = currentPhase;
        switch (currentPhase) {
            case Phase.Idle:
                if (idleTimer > idleLength)
                {
                    currentPhase = Phase.Attacking;
                    idleTimer = 0; 
                    animator.SetTrigger("Start Attack");
                }
                break;
            case Phase.Attacking:
               currentPhase = Phase.Idle;
               animator.SetTrigger("Start Idle"); 
               break; 
        }
        if (previousphase != currentPhase)
        {
            phaseStartTime = Time.time;
            Debug.Log("Phase changed from " + previousphase + " to " + currentPhase);
        }
    }
    private void ApplyPhase() {
        switch (currentPhase)
        {
            case Phase.Idle:
                ChangeMoveDir(Vector3.zero);
                return;
            case Phase.Attacking:
                GameObject instanceShard = Instantiate(iceShard, this.transform.position, Quaternion.identity);
                int shardDirection = UnityEngine.Random.Range(0, 3);
                switch (shardDirection)
                {
                    case 0:
                        instanceShard.GetComponent<Rigidbody2D>().velocity = Vector2.left;
                        break;
                    case 1:
                        instanceShard.GetComponent<Rigidbody2D>().velocity = Vector2.right;
                        break;
                    case 2:
                        instanceShard.GetComponent<Rigidbody2D>().velocity = Vector2.up;
                        break;
                    case 3:
                        instanceShard.GetComponent<Rigidbody2D>().velocity = Vector2.down;
                        break;
                    default:
                        Debug.Log("Error applying appropriate forces to ice shard.");
                        break;
                }
                idleTimer = 0;
                currentPhase = Phase.Idle;
                break;
        }
    }
    private void ChangeSpriteDirection() {
            if (movementDirection.x > 0)
            {
                Vector3 temp = transform.localScale;
                temp.x = 1;
                gameObject.transform.localScale = temp;
            }
            else if (movementDirection.x < 0)
            {
                Vector3 temp = transform.localScale;
                temp.x = -1;
                gameObject.transform.localScale = temp;
            }
            if (currentPhase == Phase.Attacking)
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
        movementDirection = newDirection;
    }
}
