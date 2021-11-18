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
            pooledProjectile.SetActive(true); //����������
            pooledProjectile.transform.position = new Vector2(transform.position.x, transform.position.y); //�������������   
            pooledProjectile.transform.right = transform.right; //�������������                                                                                                                       //pooledProjectile.transform.localEulerAngles = gunObject.transform.localEulerAngles;
        }

        yield return new WaitForSeconds(timeToShoot);

        canShoot = true;
    }

    IEnumerator ShootTwice()
    {
        GameObject pooledProjectile = ProjectileEnemyPools.SharedInstance.GetPooledObject();
        if (pooledProjectile != null)
        {
            pooledProjectile.SetActive(true); //����������
            pooledProjectile.transform.right = transform.right; //�������������
            pooledProjectile.transform.position = new Vector2(transform.position.x - transform.localScale.x, transform.position.y); //�������������                                                                                                                          //pooledProjectile.transform.localEulerAngles = gunObject.transform.localEulerAngles;
        }

        //pooledProjectile = GameManager.SharedInstance.GetPooledObject();
        //if (pooledProjectile != null)
        //{
        //    pooledProjectile.SetActive(true); //����������
        //    pooledProjectile.transform.right = transform.right; //�������������
        //    pooledProjectile.transform.position = new Vector2(transform.position.x, transform.position.y); //�������������                                                                                                                          //pooledProjectile.transform.localEulerAngles = gunObject.transform.localEulerAngles;
        //}

        yield return new WaitForSeconds(timeToShoot);

        canShoot = true;
    }
}
