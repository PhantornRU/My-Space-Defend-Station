using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPointScript : MonoBehaviour
{
    public ShieldBarrierScript[] shieldBarriers;

    public float startDelay = 0.25f;
    public float intervalDelay = 0.1f;
    private float increaseBonusStartDelay = 10.0f;

    public float sizeValueIncrease = 0.1f;
    public float sizeValueDecrease = 0.01f;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        //устанавливаем z уровень
        //transform.position.z = 0.15f;

        //сразу запускаем понижение
        if (gameManager.isGameActive)
        {
            InvokeRepeating("DecreaseShield", startDelay * increaseBonusStartDelay / gameManager.difficultyRate, intervalDelay / gameManager.difficultyRate);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Projectile"))
        {
            InvokeRepeating("IncreaseShield", startDelay * gameManager.difficultyRate, intervalDelay * gameManager.difficultyRate);
            CancelInvoke("DecreaseShield");

            if (isAllShieldsOffline == true)
            {
                ActivateShields();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Projectile"))
        {
            InvokeRepeating("DecreaseShield", startDelay * increaseBonusStartDelay / gameManager.difficultyRate, intervalDelay / gameManager.difficultyRate);
            CancelInvoke("IncreaseShield");
        }
    }

    private bool isAllShieldsOffline = false;

    private void DecreaseShield()
    {   //уменьшаем щиты
        Debug.Log("Произошло понижение");
        isAllShieldsOffline = true;

        foreach (ShieldBarrierScript shield in shieldBarriers)
        {
            shield.DecreaseShield(sizeValueDecrease);

            if (shield.transform.localScale.x < shield.minSize.x && shield.transform.localScale.y < shield.minSize.y)
            {
                isAllShieldsOffline = false;
                DeactivateShields();
            }
        }

        if (isAllShieldsOffline)
        {   //отключаем таймер над неиспользуемыми щитами
            CancelInvoke("DecreaseShield");
        }
    }
    private void IncreaseShield()
    {   //увеличиваем щиты
        Debug.Log("Произошло повышение");
        foreach (ShieldBarrierScript shield in shieldBarriers)
        {
            shield.IncreaseShield(sizeValueIncrease);
        }
    }

    private void ActivateShields()
    {
        Debug.Log("Щиты " + name + " активированы");
        foreach (ShieldBarrierScript shield in shieldBarriers)
        {
            if (!shield.isActiveAndEnabled)
            {
                shield.gameObject.SetActive(true);
                shield.IncreaseShield(sizeValueIncrease * 2);
            }
        }
        isAllShieldsOffline = false;
    }
    private void DeactivateShields()
    {
        Debug.Log("Щиты " + name + " деактивированы");
        foreach (ShieldBarrierScript shield in shieldBarriers)
        {
            shield.gameObject.SetActive(false);
        }
    }
}
