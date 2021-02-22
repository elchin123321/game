using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.IO;

public class Hero : MonoBehaviour
{

    static public  string savePath;
    public class Data
    {
        public Vector3 pos;
    } Data data;

    public float speed = 4.0f;
    public float jumpForce = 6f;
    public float rollForce = 6f;
    public Image bar;
    public Text nameBar;
    public Text goalBar;
    

    private Rigidbody2D rg2D;
    private SpriteRenderer sprite;
    private Animator anim;
    private int facingDirection = 1;
    private bool grounded = false;
    private bool isBlock = false;
    private bool isRoll = false;
    float inputX;

    public float timeBtwAttack;
    public float startTimeBtwAttack;
    public Transform attackPos;
    public LayerMask enemy;
    public float attackRange;
    public float damage;
    private int currentAttack = 0;


    private float health;

    void OnApplicationQuit()
    {
        data = new Data();
        data.pos = transform.position;
        SaveLoad.SaveJSON(data);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        savePath = Path.Combine(Application.dataPath, "Save.json");
        data = SaveLoad.LoadJson<Data>();
        if (data != null)
        {
            transform.position = data.pos;
        }

        anim = GetComponent<Animator>();
        rg2D = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        health = 1;
        nameBar.text = DataHolder.HeroName;
    }

    // Update is called once per frame
    void Update()
    {
        inputX = Input.GetAxis("Horizontal");
        if (inputX > 0)
        {
            facingDirection = 1;
        }

        else if (inputX < 0)
        {
            facingDirection = -1;
        }
        bar.fillAmount = health;
        if (Input.GetMouseButton(1))
        {
            Block(); 
        }
        else 
        { 
            isBlock = false;
            anim.SetBool("isBlock", false);
        };
        if (Input.GetButton("Horizontal") && !isBlock && !isRoll) 
        {
            Run();
            anim.SetBool("isRunning", true);
        }
        else { anim.SetBool("isRunning", false); }
        if (Input.GetButtonDown("Jump")&& grounded &&! isBlock && !isRoll) Jump();
        if (!grounded && (rg2D.velocity.y<0) && timeBtwAttack==0) anim.SetBool("isFalling", true); else  anim.SetBool("isFalling", false);
        if (Input.GetKeyDown("left shift") && grounded && !isBlock && !isRoll) { Roll(); }
        if (Input.GetMouseButton(0) && !isRoll) Attack();
    }

    
    private void Run()
    {
        
        Vector3 direction = transform.right * inputX;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
        if (direction.x < 0.0f)
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
        anim.SetTrigger("jump");
        rg2D.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }
    public void TakeDamage(float damage)
    {
        if (!isBlock&&!isRoll)
        { 
            anim.SetTrigger("hit");
            health -= damage;
            if (health <= 0)
            {
                bar.fillAmount = 0;
                anim.SetTrigger("isDie");
                //Destroy(gameObject);
                goalBar.text = "You lose";
            }
        }
        else
        {
            if (!isRoll)
            {
                health -= damage * 0.15f;
            }
            if (health <= 0)
            {
                bar.fillAmount = 0;
                anim.SetTrigger("isDie");
                //Destroy(gameObject);
                goalBar.text = "You lose";
            }
            anim.SetTrigger("blocked");
        }
    }
    public void Block()
    {
        if (grounded) {
            isBlock = true;
            anim.SetBool("isBlock", true);
        }
    }

    public void Attack()
    {

        if (timeBtwAttack <= 0)
        {        
                currentAttack++;
                if (currentAttack > 3) currentAttack = 1;
                anim.SetTrigger("attack" + currentAttack);
                Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);
                for (int i = 0; i < enemies.Length; i++)
                {
                    enemies[i].GetComponent<EmenyAbstract>().TakeDamage(damage);

                }
                timeBtwAttack = startTimeBtwAttack;           

        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }
    public void Roll()
    {
        isRoll = true;
        rg2D.velocity = new Vector2(facingDirection * rollForce, rg2D.velocity.y);
        anim.SetTrigger("roll");
    }
    public void endRoll()
    {
        isRoll = false;
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


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

}
