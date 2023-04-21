using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float speed;
    public float timer = 3.0f;
    public float knockbackForce = 100.0f;
    public ParticleSystem onHitEffect;
    
    Rigidbody2D enemyRB;
    float elapsedTime = 0f;
    float knockbackDuration = 0.1f;

    private void Update()
    {
        if(timer <= 0.0f)
            Destroy(gameObject);
        else
            timer -= Time.deltaTime;

        if(enemyRB != null)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > knockbackDuration)
            {
                enemyRB.velocity = Vector3.zero;
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if its an enemy, do knock back
        if (collision.gameObject.tag == "Enemy")
        {
            enemyRB = collision.gameObject.GetComponent<Rigidbody2D>();
            if(enemyRB != null)
            {
                enemyRB.AddForce(transform.right * knockbackForce, ForceMode2D.Impulse);
                elapsedTime = 0f;
            }
            //deal damage to the enemy
            GetComponent<SpriteRenderer>().enabled = false;
        }
        else if(collision.gameObject.tag != "Player")
            //do particle effect, then destroy it
            GetComponent<SpriteRenderer>().enabled = false;

        //spawn at where the bullet hits
        Instantiate(onHitEffect, transform.position, onHitEffect.transform.rotation);
    }
}
