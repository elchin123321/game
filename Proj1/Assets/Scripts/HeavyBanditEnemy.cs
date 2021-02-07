using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyBanditEnemy: EmenyAbstract
{

    private float health=1;

    private float timeBtwAttack;
    public float startTimeBtwAttack;
    public float damage;
    private Hero hero;
    // Start is called before the first frame update
    void Start()
    {
        health = 1;
        startTimeBtwAttack = 1.5f;
        timeBtwAttack = startTimeBtwAttack;
        hero = FindObjectOfType<Hero>();
        damage = 0.33f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    override public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        Debug.Log(health);
    }
    
    public void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if(timeBtwAttack <= 0)
            {
                OnEnemyAttack();
            }
            else
            {
                timeBtwAttack -= Time.deltaTime;
            }
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            timeBtwAttack = startTimeBtwAttack;
        }
        }

    public void OnEnemyAttack()
    {
        hero.TakeDamage(damage);
        timeBtwAttack = startTimeBtwAttack;
    }
}
