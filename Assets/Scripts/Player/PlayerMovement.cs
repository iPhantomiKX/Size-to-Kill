using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float dashSpeed;
    public float dashLength = .5f;
    public float dashCooldown = 1f;
    public Rigidbody2D rb;
    public TrailRenderer dashTrail;
    private Vector2 direction;
    private GameObject dashTrailgo;

    public float activeMoveSpeed;
    public float dashCounter;
    public float dashCooldownCounter;

    private bool isDashing = false;

    // Start is called before the first frame update
    void Start()
    {
        activeMoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    public void Move()
    {
        if(!isDashing)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                isDashing = true;
            else
                // Get the horizontal and vertical input axes
                direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        }
        else
        {
            if (dashCooldownCounter <= 0.0f && dashCounter <= 0)
            {
                // Calculate the direction in which to move the GameObject
                direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;
                //spawn the dash vfx
                dashTrailgo = Instantiate(dashTrail.gameObject, transform.position, transform.rotation);
                dashTrailgo.transform.parent = transform;
            }

            if (dashCounter > 0)
            {
                dashCounter -= Time.deltaTime;

                if (dashCounter <= 0)
                {
                    activeMoveSpeed = moveSpeed;
                    dashCooldownCounter = dashCooldown;
                    Destroy(dashTrailgo);
                    GetComponent<PlayerController>().Shoot();
                    isDashing = false;
                }
            }

            if (dashCooldownCounter > 0)
            {
                dashCooldownCounter -= Time.deltaTime;
            }
        }

        // Apply a force to the GameObject's Rigidbody2D component in the calculated direction
        rb.velocity = direction * activeMoveSpeed;
    }
}
