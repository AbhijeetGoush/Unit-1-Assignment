using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    Rigidbody2D rb;
    bool touchingplatform;
    public float health;
    private Animator anim;
    public bool attack;
    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        touchingplatform = false;
        health = 3;
        anim = GetComponent<Animator>();
        attack = false;
    }

    // Update is called once per frame
    async void Update()
    {
        Vector2 vel = rb.velocity;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            vel.x = 7;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            vel.x = -7;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && (touchingplatform == true))
        {
            vel.y = 7;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow)) 
        {
            await Task.Delay(50);
            vel.x = 0;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            await Task.Delay(50);
            vel.x = 0;
        }
        if(health <= 0)
        {
            anim.SetBool("Die", true);
            await Task.Delay(1000);
            Destroy(this.gameObject);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            attack = true;
            anim.SetBool("Attack", true);
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            anim.SetBool("Attack", false);
        }
        
        rb.velocity = vel;
    
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "platform")
        {
            touchingplatform = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "platform")
        {
            touchingplatform = false;
        }
    }

    private async void OnCollisionEnter2D(Collision2D collision)
    {
        while (collision.gameObject.tag == "Enemy")
        {
            health -= 1;
            await Task.Delay(3000);
        }

        
    }

    
}
