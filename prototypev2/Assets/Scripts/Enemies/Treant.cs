using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treant : MonoBehaviour
{
    private GameObject currentroom;
    private GameObject player;
    public enum Phase { Idle, Attacking }
    private Phase currentPhase = Phase.Idle;
    private float phaseStartTime;

    private float idleLength = 2f;

    private float idleTimer = 0;
    private Animator animator;
    public GameObject grenade;
    private Vector3 movementDirection;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        phaseStartTime = Time.time;
        animator = gameObject.GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Detected Trigger Collision " + collision.gameObject.name);
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
        if (currentPhase == Phase.Idle)
        {
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
    private void UpdatePhase()
    {
        Phase previousphase = currentPhase;
        switch (currentPhase)
        {
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
    private void ApplyPhase()
    {
        switch (currentPhase)
        {
            case Phase.Idle:
                ChangeMoveDir(Vector3.zero);
                return;
            case Phase.Attacking:
                int grenadeXOffset = Random.Range(1, 5);
                int grenadeYOffset = Random.Range(1, 5);
                int operation = Random.Range(0, 4);
                switch (operation) {
                    case 0: 
                        Instantiate(grenade, new Vector3(this.transform.position.x + grenadeXOffset, this.transform.position.y + grenadeYOffset, this.transform.position.z), Quaternion.identity);
                        break;
                    case 1:
                        Instantiate(grenade, new Vector3(this.transform.position.x + grenadeXOffset, this.transform.position.y - grenadeYOffset, this.transform.position.z), Quaternion.identity);
                        break;
                    case 2:
                        Instantiate(grenade, new Vector3(this.transform.position.x - grenadeXOffset, this.transform.position.y + grenadeYOffset, this.transform.position.z), Quaternion.identity);
                        break;
                    case 3:
                        Instantiate(grenade, new Vector3(this.transform.position.x - grenadeXOffset, this.transform.position.y - grenadeYOffset, this.transform.position.z), Quaternion.identity);
                        break; 
                }
                idleTimer = 0;
                currentPhase = Phase.Idle;
                break;
        }
    }
    private void ChangeSpriteDirection()
    {
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
