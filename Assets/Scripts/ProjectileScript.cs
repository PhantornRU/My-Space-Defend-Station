using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float speed = 2.0f;
    //public bool isEnemy;

    private Vector2 border;

    private void Start()
    {
        // границы екрана
        border = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
    }

    // Update is called once per frame
    void Update()
    {
        //projectileRb.AddForce(Vector2.right * speed * Time.deltaTime, ForceMode2D.Impulse);
        transform.Translate(speed * Time.deltaTime, 0, 0);   
        if (Mathf.Abs(transform.position.x) > border.x || Mathf.Abs(transform.position.y) > border.y)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile") || collision.gameObject.CompareTag("ProjectileEnemy"))
        {
            collision.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{

    //    if (collision.gameObject.CompareTag("Projectile") || collision.gameObject.CompareTag("ProjectileEnemy"))
    //    {
    //        collision.gameObject.SetActive(false);
    //        gameObject.SetActive(false);
    //    }
    //}
}
