using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player; //we cannot make a new player, or get or set a new player, the player is the player, and that is why I had to take them from the scene.
    private float counter; //changes in update, getters and setters not needed 
    private float directionChangeTime = 3f; //getters and setters included, although not currently used as this value is not changed anywhere (yet) 
    private float enemyvelocity = 2f; //getters and setters included, although not currently used as this value is not changed anywhere (yet)
    private UnityEngine.Vector2 movementdirection; //getters and setters included, used. 
    private UnityEngine.Vector2 movementperSecond; //calculated in the script based off variables that have getters and setters, so no need to get or set this.
    private bool detectedplayer; //getters and setters included, used in script 


    //PRIVATE METHODS 
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("player"); 
        counter = 0f;
        CalcMovementDir(); 
    }

    private void CalcMovementDir()
    {
        //calculate a new movement vector with a magnitude of one, and later multiply this movement vector with the velocity of the enemy
        if (UnityEngine.Vector2.Distance(player.transform.position, this.transform.position) < 5)
        {
            if (player.transform.position.x - this.transform.position.x > 1 & player.transform.position.x > this.transform.position.x)
            {
                SetMoveDir(UnityEngine.Vector2.right);
            }
            else if (this.transform.position.x - player.transform.position.x > 1 &  this.transform.position.x > player.transform.position.x)
            {
                SetMoveDir(UnityEngine.Vector2.left);
            }
            else if (this.transform.position.y < player.transform.position.y)
            {
                SetMoveDir(UnityEngine.Vector2.up);
            }
            else if (this.transform.position.y > player.transform.position.y)
            {
                SetMoveDir(UnityEngine.Vector2.down);
            }
        }
        else
        {
            int dir = UnityEngine.Random.Range(0, 4);
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
                default:
                    Debug.Log("Error: Error Code 1, Enemy Move Speed Could Not be Properly Assigned.");
                    break;

            }
        }
            movementperSecond = movementdirection * enemyvelocity;
        
    }

    private void Update()
    {
        if (UnityEngine.Vector2.Distance(player.transform.position, this.transform.position) < 5)
        {
            SetPlayerDetected(true);
            CalcMovementDir();
        }
        SetPlayerDetected(false);
        counter += Time.deltaTime;
        if (counter > directionChangeTime)
        {
          counter = 0;
          CalcMovementDir();
        }
        
            transform.position = new UnityEngine.Vector2(transform.position.x + (movementperSecond.x * Time.deltaTime), transform.position.y + (movementperSecond.y * Time.deltaTime));
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tile")) {
            CalcMovementDir(); 
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

    public void SetMoveDir(UnityEngine.Vector2 newmovedir) {
        movementdirection = newmovedir; 
    }
}
