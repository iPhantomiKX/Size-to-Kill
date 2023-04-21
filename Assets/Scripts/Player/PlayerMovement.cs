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

    // Start is called before the first frame update
    void Start()
    {
        activeMoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    public void Move()
    {
        // Get the horizontal and vertical input axes
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the direction in which to move the GameObject
        direction = new Vector2(horizontalInput, verticalInput).normalized;

        // Apply a force to the GameObject's Rigidbody2D component in the calculated direction
        rb.velocity = direction * activeMoveSpeed;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(dashCooldownCounter <= 0.0f && dashCounter <= 0)
            {
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;
                //spawn the dash vfx
                dashTrailgo = Instantiate(dashTrail.gameObject, transform.position, transform.rotation);
                dashTrailgo.transform.parent = transform;
            }
        }

        if(dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;

            if(dashCounter <= 0)
            {
                activeMoveSpeed = moveSpeed;
                dashCooldownCounter = dashCooldown;
                Destroy(dashTrailgo);
                GetComponent<PlayerController>().Shoot();
            }
        }

        if(dashCooldownCounter > 0)
        {
            dashCooldownCounter -= Time.deltaTime;
        }
    }
}
