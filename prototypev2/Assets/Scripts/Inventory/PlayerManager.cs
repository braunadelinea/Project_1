using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    //---- MEMBER VARIABLES ----//

    private int currentHealth = 3;
    private int maxHealth;
    public GameObject currentroom;
    private int balance;

    private Inventory inventory;
    public enum Weapon { Sword, Bow };
    private Weapon equipedWeapon;

    private float playerSpeed;
    private GameObject currentItemCollision;

    //---- METHODS ----//

    void Start()
    {
        currentHealth = 5;
        maxHealth = 5;
        balance = 10;
        inventory = gameObject.GetComponent<Inventory>();
        equipedWeapon = Weapon.Sword;
        playerSpeed = 0.1f;
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

    private void ApplyMovement()
    {
        Vector3 newPosition = this.transform.position;
        Vector3 movementVector = Vector3.zero;
        if (Input.GetKey(KeyCode.A))
        {
            movementVector.x -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movementVector.x += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movementVector.y -= 1;
        }
        if (Input.GetKey(KeyCode.W))
        {
            movementVector.y += 1;
        }
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Room"))
        {
            currentroom = collision.gameObject;
        }
        else if (collision.CompareTag("Item"))
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
    }
    public void DecreaseHealth(int decreaseAmount) {

        currentHealth -= decreaseAmount; 
        Debug.Log("Taking damage, current health is now " + currentHealth);
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
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
}
