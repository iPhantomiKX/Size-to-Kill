using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float damageDuration = 0.3f;
    public GameObject damage;

    private float damageTimer = 0.0f;

    private void Update()
    {
        if (damageTimer <= 0.0f)
        {
            damage.SetActive(false);
            damageTimer = 0.0f;
        }
        else
            damageTimer -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Projectile")
        {
            damage.SetActive(true);
            damageTimer = damageDuration;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (gameObject.GetComponent<PlayerController>() && collision.gameObject.tag == "Enemy")
        {
            damage.SetActive(true);
            damageTimer = damageDuration;
        }
    }
}
