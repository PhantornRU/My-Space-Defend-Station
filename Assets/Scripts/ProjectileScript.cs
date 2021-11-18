using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float speed = 2.0f;
    private int border = 10;
    public bool isEnemy;

    // Update is called once per frame
    void Update()
    {
        //projectileRb.AddForce(Vector2.right * speed * Time.deltaTime, ForceMode2D.Impulse);
        transform.Translate(speed * Time.deltaTime, 0, 0);   
        if (Mathf.Abs(transform.position.x) > border || Mathf.Abs(transform.position.y) > border)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Projectile"))
        {
            collision.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
