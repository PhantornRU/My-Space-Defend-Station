using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBarrierScript : MonoBehaviour
{
    //public ShieldPointScript pointParent;

    public bool isShieldActive = true;

    public Vector2 maxSize = new Vector2(4, 4);
    public Vector2 minSize = new Vector2(1, 1);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isShieldActive)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                DecreaseShield(0.5f);
            }

            if (collision.gameObject.CompareTag("ProjectileEnemy"))
            {
                DecreaseShield(0.25f);
                collision.gameObject.SetActive(false);
            }
        }

        if (collision.gameObject.CompareTag("Projectile"))
        {
            IncreaseShield(0.25f);
        }
    }

    public void DecreaseShield(float sizeValueDecrease)
    {   
        //уменьшаем щит
        if (transform.localScale.x > minSize.x)
        {
            transform.localScale -= new Vector3(sizeValueDecrease, 0, 0);
        }
        if (transform.localScale.y > minSize.y)
        {
            transform.localScale -= new Vector3(0, sizeValueDecrease, 0);
        }

        //отключаем щит, если достигли минимальных значений
        if (transform.localScale.x <= minSize.x && transform.localScale.y <= minSize.y)
        {
            isShieldActive = false;
            this.gameObject.SetActive(false);
        }
    }

    public void IncreaseShield(float sizeValueIncrease)
    {
        isShieldActive = true;

        if (transform.localScale.x < maxSize.x)
        {
            transform.localScale += new Vector3(sizeValueIncrease, 0, 0);
        }
        if (transform.localScale.y < maxSize.y)
        {
            transform.localScale += new Vector3(0, sizeValueIncrease, 0);
        }
    }
}
