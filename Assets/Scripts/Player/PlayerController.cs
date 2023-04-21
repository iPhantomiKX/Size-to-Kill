using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float rotationSpeed = 5.0f;
    private PlayerMovement playerMovement;
    public Gun gun;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = gameObject.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        Aim();
        Move();
        if (Input.GetMouseButtonDown(0))
            Shoot();
    }

    public void Aim()
    {
        // Get the screen position of the mouse cursor
        Vector2 cursorPos = Input.mousePosition;

        // Convert the cursor position to world space
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(cursorPos);

        // Calculate the angle between the player's current facing direction and the direction towards the cursor
        float angle = Vector2.SignedAngle(gun.transform.right, worldPos - gun.transform.position);

        // Rotate the player towards the cursor over time
        gun.transform.Rotate(0, 0, angle);
    }

    public void Shoot()
    {
        if (gun != null)
        {
            gun.Fire();
            ScaleDown(0.05f);   //scale with health
        }
    }

    private void Move()
    {
        playerMovement.Move();
    }

    public void ScaleDown(float offset)
    {
        Vector3 newScale = new Vector3(transform.localScale.x - offset, transform.localScale.y - offset, transform.localScale.z);
        if (transform.localScale.x < 0.0f || transform.localScale.y < 0.0f)
            //game over, but for now stick to setting it to 0
            newScale.x = newScale.y = 0.0f;

        transform.localScale = newScale;
    }
}