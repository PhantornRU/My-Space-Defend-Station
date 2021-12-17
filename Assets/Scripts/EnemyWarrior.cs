using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWarrior : Enemy
{
    //public bool isShootTwice = false;

    public EnemyGun[] guns;

    public int timeToShoot = 1;
    public float horizontalSpeedGun = 30.0f;
    public float timeDifferentMovedGun = 4;
    public bool isShootTogether = true;
    public float shootDelay = 0.5f;
    //public EnemyGun[] guns;

    [SerializeField] int numGun = 0;

    private void Awake()
    {
        foreach (EnemyGun gun in guns)
        {
            //gun.transform.right = transform.right;
            gun.timeToShoot = timeToShoot;
            gun.horizontalSpeed = horizontalSpeedGun;
            gun.timeDifferentMoved = timeDifferentMovedGun;
            numGun++;
        }
    }
    
    void Update()
    {
        RotateToPlayer();

        HorizontalMove();

        if (isShootTogether)
        {   //стрельба одновременно
            foreach (EnemyGun gun in guns)
            {
                if (gun.canShoot)
                {
                    gun.gunShoot();
                }
            }
        }
        else
        {   //стрельба очередью
            if (canLineShoot)
            {
                StartCoroutine(lineShoot());
            }
        }
    }

    private bool canLineShoot = true;
    private float timeGunSaved;

    IEnumerator lineShoot() //цикл вращение в разные стороны в заданный таймер
    {
        canLineShoot = false;

        for (int i = numGun; i > 0; i--)
        {
            //Debug.Log("[" + Time.time + "] Запущен таймер:" + intTimeDebug);

            while (Time.time < (timeDifferentMoved / i ) + timeGunSaved)
            {
                if (guns[i - 1].canShoot)
                {
                    //Debug.Log("Высстрел #" + i);
                    guns[i - 1].gunShoot();
                }

                yield return null;
            }
        }

        timeGunSaved = Time.time;

        canLineShoot = true;
    }
}
