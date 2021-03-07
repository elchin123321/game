using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class HeavyBanditEnemy: EmenyAbstract
{
    [SerializeField] private EnemyHealthBarLineRenderer healthBar;
    private float health=1;
    private Hero hero;
    private Animator anim;
    private AudioSource attackSound;
    private float timeBtwAttack;
    public float startTimeBtwAttack;
    private bool attack=false;
    public float damage;
    public float speed;
    public float attackRange;
    private float seeRange;
    private float rangeBtwHero;
    private bool isDead = false;

  
    // Start is called before the first frame update
    void Start()
    {
        attackSound = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        health = 1;
        timeBtwAttack = 0;
        hero = FindObjectOfType<Hero>();
        seeRange = 6.0f;
    }

    // Update is called once per frame
    void Update()
    {
        rangeBtwHero = System.Math.Abs(hero.transform.position.x - transform.position.x);
        if (rangeBtwHero < seeRange && rangeBtwHero >= attackRange && !isDead) Pursuit();
        else anim.SetBool("isRun", false);
        
    }

    override public void TakeDamage(float damage)
    {
        if (!isDead)
        {
            health -= damage;
            healthBar.RedceHealthBar(damage);
        }
        if(!attack && !isDead)anim.SetTrigger("hit");
        if (health <= 0 && !isDead)
        {
            isDead = true;
            anim.SetTrigger("die");
            Destroy(gameObject, 1f);
        }
        Debug.Log(health);
    }
    
    public void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if(!attack && timeBtwAttack<=0 && !isDead)
            {
                attack = true;
                anim.SetTrigger("attack");
                
            }
            else
            {
                timeBtwAttack -= Time.deltaTime;
            }
        }
    }
    

    //Вызывается во время анимации
    public void OnEnemyAttack()
    {
        if (rangeBtwHero <= attackRange+0.15)
        {
            hero.TakeDamage(damage);
            
        }
        attackSound.Play();
        timeBtwAttack = startTimeBtwAttack;
        attack = false;

    }
    //Вызывается в конце анимации
   

    public void Pursuit()
    {
        if (hero.transform.position.x - transform.position.x > 0.0f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        if (!attack)
        {
            anim.SetBool("inBattle", true);
            anim.SetBool("isRun", true);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(hero.transform.position.x,transform.position.y), speed * Time.deltaTime);
        }
    }

    
}
