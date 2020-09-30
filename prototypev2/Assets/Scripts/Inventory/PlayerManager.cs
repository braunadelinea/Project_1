using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    //---- MEMBER VARIABLES ----//

    private int currentHealth = 3;
    private int maxHealth;
    public Vector3 currentroom;
    private int balance;

    private Inventory inventory;
    public enum Weapon { Sword, Bow };
    private Weapon equipedWeapon;

    private float playerSpeed;
    private bool movingRight;
    private bool moving;
    private GameObject currentItemCollision;
    private bool isSpawned;
    public bool IsSpawned
    {
        get
        {
            return isSpawned;
        }
    }

    private Animator animator;

    //---- METHODS ----//

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        currentHealth = 5;
        maxHealth = 5;
        balance = 10;
        inventory = gameObject.GetComponent<Inventory>();
        equipedWeapon = Weapon.Sword;
        playerSpeed = 0.1f;
        movingRight = true;
        moving = false;
        isSpawned = false;
    }

    void Update()
    {
        if (currentItemCollision != null && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Picking up item");
            PickUpItem(currentItemCollision.gameObject.GetComponent<Item>());
        }
    }

    void FixedUpdate()
    {
        if(currentroom == null)
        {
            //don't update until currentroom is set
            return;
        }
        //Vector3 cameraPosition = currentroom.transform.position;
        //cameraPosition.z -= 10;
        ApplyMovement();
    }

    public void SpawnPlayer(float x, float y)
    {
        gameObject.transform.position = new Vector3(x, y);
        currentroom = new Vector3(x, y);
        isSpawned = true;
    }

    private void ApplyMovement()
    {
        Vector3 newPosition = this.transform.position;
        Vector3 movementVector = Vector3.zero;
        bool newMoving = false;
        if (Input.GetKey(KeyCode.A))
        {
            movingRight = false;
            movementVector.x -= 1;
            newMoving = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movingRight = true;
            movementVector.x += 1;
            newMoving = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movementVector.y -= 1;
            newMoving = true;
        }
        if (Input.GetKey(KeyCode.W))
        {
            movementVector.y += 1;
            newMoving = true;
        }
        if (moving == false && newMoving == true)
        {
            animator.SetTrigger("Start Walk");
        }
        else if (moving == true && newMoving == false)
        {
            animator.SetTrigger("Start Idle");
        }
        moving = newMoving;
        movementVector = movementVector.normalized * playerSpeed;
        newPosition += movementVector;
        Collider2D[] collisions = Physics2D.OverlapCircleAll(newPosition, 0.5f);
        foreach (Collider2D collider in collisions)
        {
            string tag = collider.gameObject.tag;
            if (tag == "Wall")
            {
                return;
            }
        }
        transform.position = newPosition;
        UpdateAnimationDirection();
    }

    void UpdateAnimationDirection()
    {
        if (movingRight)
        {
            Vector3 temp = transform.localScale;
            temp.x = 1;
            gameObject.transform.localScale = temp;
        }
        else if (!movingRight)
        {
            Vector3 temp = transform.localScale;
            temp.x = -1;
            gameObject.transform.localScale = temp;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            currentItemCollision = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            currentItemCollision = null;
        }
    }

    //---- PUBLIC METHODS ----//

    public void IncreaseHealth(int increaseAmount) //increases health by the given amount up to max health
    {
        if (currentHealth + increaseAmount > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += increaseAmount;
        }
        RenderHearts();
    }
    public void DecreaseHealth(int decreaseAmount) {

        currentHealth -= decreaseAmount; 
        Debug.Log("Taking damage, current health is now " + currentHealth);
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        RenderHearts();
    }

    public int AddToBalance(int increaseAmount)
    {
        balance += increaseAmount;
        return balance;
    }

    public int SubtractFromBalance(int decreaseAmount)
    {
        if (balance - decreaseAmount < 0)
        {
            return -1;
        }
        balance -= decreaseAmount;
        return balance;
    }

    public Item PickUpItem(Item item)
    {
        Debug.Log("Entered PlayerManager.PickUpItem script");
        if (item.GetType() == Item.ItemType.Heart)
        {
            IncreaseHealth(1);
            return item;
        }
        else if (item.GetType() == Item.ItemType.Coin)
        {
            AddToBalance(1);
            return item;
        }
        else if (item.GetType() == Item.ItemType.Ring || item.GetType() == Item.ItemType.Potion)
        {
            return inventory.PlaceInInventory(item);
        }
        Debug.Log("ERROR: Item does not have any type assigned - cannot be picked up");
        return null;
    }

    public void SetSpeed(float newSpeed)
    {
        playerSpeed = newSpeed;
    }

    //---- PRIVATE METHODS ----//

    private void RenderHearts()
    {
        Image[] icons = GameObject.Find("Hearts").GetComponentsInChildren<Image>();
        for (int i = 1; i <= icons.Length; i++)
        {
            if (i <= currentHealth)
            {
                icons[i - 1].enabled = true;
            }
            else
            {
                icons[i - 1].enabled = false;
            }
        }
    }
}
