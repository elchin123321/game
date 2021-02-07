using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    [SerializeField] float speed = 4.0f;
    [SerializeField] float jumpForce = 4f;
    public Image bar;
    public Text nameBar;
    public Text goalBar;
    

    private Rigidbody2D rg2D;
    private SpriteRenderer sprite;
    private bool grounded = false;

    private float health;
    
    // Start is called before the first frame update
    void Start()
    {
        rg2D = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        health = 1;
        nameBar.text = DataHolder.HeroName;
    }

    // Update is called once per frame
    void Update()
    {
        bar.fillAmount = health;
        if (Input.GetButton("Horizontal")) Run();
        if (Input.GetButtonDown("Jump")&&grounded) Jump();
    }
    
    private void Run()
    {
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
        if(direction.x < 0.0f)
        {
            transform.localScale = new Vector3(-1f,1f,1f);
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
    private void Jump()
    {
        rg2D.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            bar.fillAmount = 0;
            Destroy(gameObject);
            goalBar.text = "You lose";
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Ground"))
        {
            grounded = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            grounded = false;
        }
    }

}
