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

        foreach (EnemyGun gun in guns)
        {
            if (gun.canShoot)
            {
                gun.gunShoot();
            }
        }
    }
    

}
