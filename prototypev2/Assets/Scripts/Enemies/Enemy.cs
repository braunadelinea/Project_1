using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Numerics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject currentroom; 
    //TO-DO: Enemy attack (basic) [further attacks implemented in alpha or whatever the next phase is] 
    private float attackcooldown = 1; 
    private GameObject player;
    private float counter;
    private float directionChangeTime = 3f;
    private float enemyvelocity = 2f;
    private UnityEngine.Vector2 movementdirection;
    private UnityEngine.Vector2 movementperSecond;
    private bool detectedplayer;
    private bool collisionimmenent;


    //PRIVATE METHODS 
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        counter = 0f;
        CalcMovementDir();
    }

    private void CalcMovementDir() //checks if close to the player, then follows the player and if not, randomly walks around. 
    {
        if (currentroom == GameObject.FindGameObjectWithTag("player").GetComponent<PlayerManager>().currentroom)
        {
            //calculate a new movement vector with a magnitude of one, and later multiply this movement vector with the velocity of the enemy
            if (UnityEngine.Vector2.Distance(player.transform.position, this.transform.position) < 5)
            {
                SetMoveDir((player.transform.position - this.transform.position).normalized);
                if (player.transform.position.x > this.transform.position.x)
                {
                    //set sprite image to the right 
                }
                else if (player.transform.position.x < this.transform.position.x)
                {
                    //set sprite image to the left 
                }
            }
            else
            {
                int dir = UnityEngine.Random.Range(0, 5);
                switch (dir)
                {
                    case 0:
                        SetMoveDir(UnityEngine.Vector2.left);
                        break;
                    case 1:
                        SetMoveDir(UnityEngine.Vector2.up);
                        break;
                    case 2:
                        SetMoveDir(UnityEngine.Vector2.right);
                        break;
                    case 3:
                        SetMoveDir(UnityEngine.Vector2.down);
                        break;
                    case 4:
                        SetMoveDir(UnityEngine.Vector2.zero);
                        break;
                    default:
                        Debug.Log("Error: Error Code 1, Enemy Move Speed Could Not be Properly Assigned.");
                        break;

                }
            }
            movementperSecond = movementdirection * enemyvelocity;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Room")) {
            currentroom = collision.gameObject;
        }
    }
    private void Update()
    {
        if (currentroom == GameObject.FindGameObjectWithTag("player").GetComponent<PlayerManager>().currentroom)
        {
            if (UnityEngine.Vector2.Distance(player.transform.position, this.transform.position) < 5)
            {
                SetPlayerDetected(true);
                CalcMovementDir();
            }
            else
            {
                SetPlayerDetected(false);
            }
            if (GetPlayerDetected() == true)
            {
                Collider2D[] collider = Physics2D.OverlapCircleAll(this.transform.position, 2);
                for (int i = 0; i < collider.Length; i++)
                {
                    if (collider[i].gameObject.CompareTag("player"))
                    {
                        attackcooldown -= Time.deltaTime;
                        if (attackcooldown < 0)
                        {
                            attackcooldown = 1;
                            //play attack animation
                            player.GetComponent<PlayerManager>().DecreaseHealth(1);
                        }
                    }
                    else
                    {
                        attackcooldown = 1;
                    }
                }
            }

            if (GetMoveDir() == UnityEngine.Vector2.left)
            {
                UnityEngine.Vector3 pos = this.transform.position;
                pos.x -= 1f;
                Collider2D[] collider = Physics2D.OverlapCircleAll(pos, 1f);
                for (int i = 0; i < collider.Length; i++)
                {
                    if (collider[i].gameObject.CompareTag("Tile"))
                    {
                        SetColImm(true);
                    }
                }
            }
            else if (GetMoveDir() == UnityEngine.Vector2.right)
            {
                UnityEngine.Vector3 pos = this.transform.position;
                pos.x += 1f;
                Collider2D[] collider = Physics2D.OverlapCircleAll(pos, 1f);
                for (int i = 0; i < collider.Length; i++)
                {
                    if (collider[i].gameObject.CompareTag("Tile"))
                    {
                        SetColImm(true);
                    }
                }
            }
            else if (GetMoveDir() == UnityEngine.Vector2.up)
            {
                UnityEngine.Vector3 pos = this.transform.position;
                pos.y += 1;
                Collider2D[] collider = Physics2D.OverlapCircleAll(pos, 1f);
                for (int i = 0; i < collider.Length; i++)
                {
                    if (collider[i].gameObject.CompareTag("Tile"))
                    {
                        SetColImm(true);
                    }
                }
            }
            else if (GetMoveDir() == UnityEngine.Vector2.down)
            {
                UnityEngine.Vector3 pos = this.transform.position;
                pos.y -= 1;
                Collider2D[] collider = Physics2D.OverlapCircleAll(pos, 1f);
                for (int i = 0; i < collider.Length; i++)
                {
                    if (collider[i].gameObject.CompareTag("Tile"))
                    {
                        SetColImm(true);
                    }
                }
            }

            if (GetColImm())
            {
                CalcMovementDir();
                SetColImm(false);
            }

            counter += Time.deltaTime;
            if (counter > directionChangeTime)
            {
                counter = 0;
                CalcMovementDir();
            }

            transform.position = new UnityEngine.Vector2(transform.position.x + (movementperSecond.x * Time.deltaTime), transform.position.y + (movementperSecond.y * Time.deltaTime));
        }
    }

    //NOTE - gizmos for test purposes only 
    private void OnDrawGizmos()
    {
        if (GetMoveDir() == UnityEngine.Vector2.left)
        {
            UnityEngine.Vector3 pos = this.transform.position;
            pos.x -= 1;
            Gizmos.DrawWireSphere(pos, 1f);
        }
        else if (GetMoveDir() == UnityEngine.Vector2.right)
        {
            UnityEngine.Vector3 pos = this.transform.position;
            pos.x += 1;
            Gizmos.DrawWireSphere(pos, 1f);
        }
        else if (GetMoveDir() == UnityEngine.Vector2.down)
        {
            UnityEngine.Vector3 pos = this.transform.position;
            pos.y -= 1;
            Gizmos.DrawWireSphere(pos, 1f);
        }
        else if (GetMoveDir() == UnityEngine.Vector2.up)
        {
            UnityEngine.Vector3 pos = this.transform.position;
            pos.y += 1;
            Gizmos.DrawWireSphere(pos, 1f);
        }
    }
    //GETTER METHODS 

    public float GetDirChangeTime()
    {
        return directionChangeTime;
    }

    public float GetEnemyVelocity()
    {
        return enemyvelocity;
    }

    public bool GetPlayerDetected()
    {
        return detectedplayer;
    }

    public UnityEngine.Vector2 GetMoveDir()
    {
        return movementdirection;
    }

    public bool GetColImm()
    {
        return collisionimmenent;
    }
    //SETTER METHODS 

    public void SetDirChangeTime(float newdirchangetime)
    {
        directionChangeTime = newdirchangetime;
    }

    public void SetEnemyVelocity(float newenemyvelocity)
    {
        enemyvelocity = newenemyvelocity;
    }

    public void SetPlayerDetected(bool newplayerdetected)
    {
        detectedplayer = newplayerdetected;
    }

    public void SetMoveDir(UnityEngine.Vector2 newmovedir)
    {
        movementdirection = newmovedir;
    }
    public void SetColImm(bool newColImm)
    {
        collisionimmenent = newColImm;
    }
}
