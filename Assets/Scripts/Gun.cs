using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public BulletBehavior bullet;
    public GameObject muzzle;

    public bool debugDraw = false;

    public void Fire()
    {
        GameObject goBullet = Instantiate(bullet.gameObject, muzzle.transform.position, muzzle.transform.rotation);

        Rigidbody2D rb = goBullet.GetComponent<Rigidbody2D>();
        rb.AddForce(muzzle.transform.right * bullet.speed, ForceMode2D.Impulse);
    }

    private void OnDrawGizmos()
    {
        if(debugDraw)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(muzzle.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }
}
