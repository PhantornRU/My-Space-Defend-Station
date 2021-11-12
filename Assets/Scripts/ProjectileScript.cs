using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [SerializeField] private float speed = 0.05f;
    private int border = 10;
    //Rigidbody2D projectileRb;

    // Start is called before the first frame update
    void Start()
    {
        //projectileRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //projectileRb.AddForce(Vector2.right * speed * Time.deltaTime, ForceMode2D.Impulse);
        transform.Translate(speed, 0, 0);   
        if (Mathf.Abs(transform.position.x) > border || Mathf.Abs(transform.position.y) > border)
        {
            gameObject.SetActive(false);
        }
    }
}
