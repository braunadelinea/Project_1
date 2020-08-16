using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject currentroom;
    private GameObject player;

    public enum Phase { Idle, Wander, Chase, Attack};
    private Phase currentPhase = Phase.Idle;
    private float phaseStartTime;

    private const float idleLength = 1;

    private const float wanderLength = 2;

    private const float startChaseDistance = 3;
    private const float stopChaseDistance = 4;

    private const float startAttackDistance = 1.2f;
    private const float stopAttackDistance = 2;
    private const float attackInterval = 1;
    private float lastAttackTime;

    private float speed = 0.05f;
    private Vector3 movementDirection;

    //PRIVATE METHODS 
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        phaseStartTime = Time.time;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(currentroom != null)
        {
            return;
        }
        if (collision.gameObject.CompareTag("Room")) {
            currentroom = collision.gameObject;
            Debug.Log("Entering New Room");
        }
    }
    private void FixedUpdate()
    {
        if (currentroom == GameObject.FindGameObjectWithTag("player").GetComponent<PlayerManager>().currentroom)
        {
            UpdatePhase();
            ApplyPhase();
        }
    }

    private void UpdatePhase()
    {
        float distanceToPlayer = Vector2.Distance(player.transform.position, this.transform.position);
        Phase previousPhase = currentPhase;
        //switch to correct phase
        switch (currentPhase)
        {
            //if currently idle: if in chase range, start chasing, else if time to wander, switch to wander
            case Phase.Idle:
                if (distanceToPlayer < startChaseDistance)
                {
                    currentPhase = Phase.Chase;
                }
                else if (Time.time >= phaseStartTime + idleLength)
                {
                    currentPhase = Phase.Wander;
                }
                break;
            //if currently wandering: if in chase range, start chasing, else if time to idle, switch to idle
            case Phase.Wander:
                if (distanceToPlayer < startChaseDistance)
                {
                    currentPhase = Phase.Chase;
                }
                else if (Time.time >= phaseStartTime + wanderLength)
                {
                    currentPhase = Phase.Idle;
                }
                break;
            //if currently chasing, then if out of chase range, switch to idle phase
            case Phase.Chase:
                if (distanceToPlayer > stopChaseDistance)
                {
                    currentPhase = Phase.Idle;
                }
                else if (distanceToPlayer < startAttackDistance)
                {
                    currentPhase = Phase.Attack;
                    lastAttackTime = 0;
                }
                break;
            //if currently attacking, then if out of range, go back to idle
            case Phase.Attack:
                if (distanceToPlayer > stopAttackDistance)
                {
                    currentPhase = Phase.Idle;
                }
                break;
        }
        if (previousPhase != currentPhase)
        {
            phaseStartTime = Time.time;
            Debug.Log("Phase changed from " + previousPhase + " to " + currentPhase);
        }
    }

    private void ApplyPhase()
    {
        //depending on what the current phase is, set things accordingly
        switch (currentPhase)
        {
            //if idle, leave update loop
            case Phase.Idle:
                ChangeMoveDir(Vector3.zero);
                return;
            //if wandering then if a direction has not been picked yet, set a random direction, then move
            case Phase.Wander:
                if (movementDirection == Vector3.zero)
                {
                    System.Random rand = new System.Random();
                    ChangeMoveDir(new Vector3((float)rand.NextDouble() - 0.5f, (float)rand.NextDouble() - 0.5f).normalized);
                }
                ApplyMovement();
                break;
            //if chasing, recalculate direction towards player and move
            case Phase.Chase:
                ChangeMoveDir((player.transform.position - this.transform.position).normalized);
                ApplyMovement();
                break;
            //if attacking, decrease health at the correct interval
            case Phase.Attack:
                if (Time.time > lastAttackTime + attackInterval)
                {
                    //play attack animation
                    player.GetComponent<PlayerManager>().DecreaseHealth(1);
                    lastAttackTime = Time.time;
                }
                break;
        }
    }

    private void ApplyMovement()
    {
        if (movementDirection == Vector3.zero)
        {
            return;
        }
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position + movementDirection * speed, 0.5f);
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
        transform.position += movementDirection * speed;
    }

    private void ChangeMoveDir(Vector2 newDirection)
    {
        movementDirection = newDirection;
        //TODO: update animation
    }
}
