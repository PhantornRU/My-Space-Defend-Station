using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public bool canShoot = true;
    //public int amountToShoot = 1;

    public int timeToShoot;
    public float horizontalSpeed;
    public float timeDifferentMoved;

    public int typeRotate = 0;
    /* 
    0 - статичное
    1 - контролирование игрока и следование
    2 - вращение слева направо
    3 - вращение справа налео
    */

    private void Start()
    {
        //смещаем чтобы не было стыка текстур
        transform.Translate(0, 0, 0.01f);
    }

    private void Update()
    {
        switch (typeRotate)
        {
            case 0:
                break;
            case 1:
                RotateToPlayer();
                break;
            case 2:
                RotateHorizontal(true);
                break;
            case 3:
                RotateHorizontal(false);
                break;
        }
    }

    public void gunShoot()
    {
        if (canShoot)
        {
            canShoot = false;
            StartCoroutine(ShootOne());
        }
    }

    protected void RotateToPlayer()
    {
        PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();

        //поворот в сторону игрока
        transform.right = (Vector2)(player.transform.position - transform.position) * Time.deltaTime;

        if (Mathf.Abs(transform.rotation.x) > 0 || Mathf.Abs(transform.rotation.y) > 0)
        {   //убираем дерганность
            transform.eulerAngles = new Vector3(0, 0, transform.rotation.z);
        }
    }
    private float timeSaved;

    private bool canRotate = true;

    protected void RotateHorizontal(bool isLeftToRight)
    {
        if (canRotate)
        {
            if (horizontalSpeed > 0 && isLeftToRight)
            {
                horizontalSpeed *= -1;
            }

            StartCoroutine(HorizontalRotated());
        }
    }

    public GameObject projectilePrefab; //устанавливаем каким снар€дом стрел€ет

    IEnumerator ShootOne() //выстрел
    {
        GameObject pooledProjectile = ProjectileEnemyPools.SharedInstance.GetPooledObject();
        if (pooledProjectile != null)
        {
            pooledProjectile.SetActive(true); //активируем
            pooledProjectile.transform.position = new Vector2(transform.position.x, transform.position.y); //позиционируем   
            pooledProjectile.transform.right = transform.right; //разворачиваем
        }

        yield return new WaitForSeconds(timeToShoot);

        canShoot = true;
    }

    IEnumerator HorizontalRotated() //цикл вращение в разные стороны в заданный таймер
    {
        canRotate = false;

        //Debug.Log("[" + Time.time + "] «апущен таймер:" + intTimeDebug);
        while (Time.time < (timeDifferentMoved / 4) + timeSaved)
        {   //ѕроход таймером от 0 до 1/4 части
            transform.Rotate(0, 0, horizontalSpeed * Time.deltaTime);
            yield return null; //new WaitForSeconds(.1f);
        }

        while (Time.time < (timeDifferentMoved / 2 + timeDifferentMoved / 4) + timeSaved)
        {   //ѕроход таймером от 1/4 до 3/4 части
            transform.Rotate(0, 0, -horizontalSpeed * Time.deltaTime);
            yield return null; //new WaitForSeconds(.1f);
        }

        while (Time.time < timeDifferentMoved + timeSaved)
        {   //ѕроход таймером от 3/4 до 4/4 части
            transform.Rotate(0, 0, horizontalSpeed * Time.deltaTime);
            yield return null; //new WaitForSeconds(.1f);
        }

        timeSaved = Time.time;

        canRotate = true;
    }
}
