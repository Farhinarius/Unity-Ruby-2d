using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    public GameObject projectilePrefab;
    
    public float projectileForce = 100.0f;
    public float speed = 3.0f;

    public int maxHealth = 5;
    public float timeInvincible = 0.5f;
    
    public int health { get { return currentHealth; } }
    int currentHealth;
    
    bool isInvincible;
    float invincibleTimer;
    
    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;
    Vector2 move;
    
    Animator animator;
    Vector2 lookDirection = new Vector2(1,0);

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // Charachter control
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        move = new Vector2(horizontal, vertical);

        // set look direction
        if( !Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f) )
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
        
        // set animation parameters (send animation parameters)
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);
        
        // damage zone code
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
            {
                isInvincible = false;
            }
        }

        // if key pressed then launch projectile
        /* if(Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        } */

        // Fire functionality
        if( Input.GetButtonDown("Fire1") )
        {
            Launch();
        }

        // Interaction with NPC
        if (Input.GetKeyDown(KeyCode.E))
        {
            InteractWithNPC();
        }
    }

    
    void FixedUpdate()
    {
        // change position of charachter rigigbody and also change position of gameobject
        Vector2 position = rigidbody2d.position;
        position += move * speed * Time.deltaTime;  /* ==> position.x += horizontal * speed * Time.deltaTime; position.y += vertical * speed * Time.deltaTime; */
        rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)     // if damage taken
        {            
            animator.SetTrigger("Hit");
            if (isInvincible)
                return;
            
            isInvincible = true;
            invincibleTimer = timeInvincible;
            Debug.Log("Damage taken");
        }
        
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, projectileForce);

        animator.SetTrigger("Launch");
    }

    private void InteractWithNPC()
    {
        RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
        if (hit.collider != null)
        {
            Debug.Log("Raycast has hit the object " + hit.collider.gameObject);
            NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
            if (character != null)
            {
                character.DisplayDialog();
            }
        }
    }
}