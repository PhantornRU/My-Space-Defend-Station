using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWarrior : Enemy
{
    public bool isShootTwice = false;
    [SerializeField] private bool canShoot = true;

    void Update()
    {
        RotateToPlayer();

        HorizontalMove();

        if (canShoot)
        {
            canShoot = false;
            if (!isShootTwice)
            {
                StartCoroutine(ShootOne());
            }
            else
            {
                StartCoroutine(ShootTwice());
            }
        }
    }

    public float timeToShoot;

    public GameObject projectilePrefab;

    IEnumerator ShootOne()
    {
        GameObject pooledProjectile = ProjectileEnemyPools.SharedInstance.GetPooledObject();
        if (pooledProjectile != null)
        {
            pooledProjectile.SetActive(true); //активируем
            pooledProjectile.transform.position = new Vector2(transform.position.x, transform.position.y); //позиционируем   
            pooledProjectile.transform.right = transform.right; //разворачиваем                                                                                                                       //pooledProjectile.transform.localEulerAngles = gunObject.transform.localEulerAngles;
        }

        yield return new WaitForSeconds(timeToShoot);

        canShoot = true;
    }

    IEnumerator ShootTwice()
    {
        GameObject pooledProjectile = ProjectileEnemyPools.SharedInstance.GetPooledObject();
        if (pooledProjectile != null)
        {
            pooledProjectile.SetActive(true); //активируем
            pooledProjectile.transform.right = transform.right; //разворачиваем
            pooledProjectile.transform.position = new Vector2(transform.position.x - transform.localScale.x, transform.position.y); //позиционируем                                                                                                                          //pooledProjectile.transform.localEulerAngles = gunObject.transform.localEulerAngles;
        }

        //pooledProjectile = GameManager.SharedInstance.GetPooledObject();
        //if (pooledProjectile != null)
        //{
        //    pooledProjectile.SetActive(true); //активируем
        //    pooledProjectile.transform.right = transform.right; //разворачиваем
        //    pooledProjectile.transform.position = new Vector2(transform.position.x, transform.position.y); //позиционируем                                                                                                                          //pooledProjectile.transform.localEulerAngles = gunObject.transform.localEulerAngles;
        //}

        yield return new WaitForSeconds(timeToShoot);

        canShoot = true;
    }
}
