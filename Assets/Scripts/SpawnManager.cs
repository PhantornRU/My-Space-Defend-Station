using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private GameManager gameManager;
    public GameObject[] targetPrefabs;

    private int direction;
    private Vector2 min, max;

    [SerializeField] private float startDelay = 1.0f;
    [SerializeField] private float spawnInterval = 2.0f;



    private void Start()
    {
        // границы екрана
        min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)) * 1.1f;
        max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)) * 1.1f;

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        //timerTest(0.5f);
    }

    public void StartSpawn()
    {
        InvokeRepeating("SpawnRandomTarget", startDelay, spawnInterval / gameManager.difficultyRate);
    }

    public void StopSpawn()
    {
        CancelInvoke("SpawnRandomTarget");
    }

    private int GetChanceIndex()
    {   //получаем случайное значение из массива объектов с выделенными для них значениями
        int indexCheck = 0;

        int[] chances = new int[targetPrefabs.Length];

        for (int i = 0; i < chances.Length; i++)
        {
            chances[i] = targetPrefabs[i].GetComponent<Enemy>().spawnRate0to100; ;
        }

        int chance = Random.Range(0, 100) + 1;

        for (int index = 0; index < chances.Length; index++)
        {
            var ch = chances[index];
            if (chance <= ch)
            {
                indexCheck = index;
            }
        }

        if (indexCheck > 0)
        {
            return indexCheck;
        }
        else
        {
            return 0;
        }
    }

    private void SpawnRandomTarget()
    {
        int index = GetChanceIndex();

        targetPrefabs[index].transform.localEulerAngles = new Vector3(0, 0, -90);

        GameObject obj = (GameObject)Instantiate(targetPrefabs[index], RandomStart(), targetPrefabs[index].transform.rotation);
        obj.transform.SetParent(this.transform); // ставим как дочерний объект к Spawn Manager

        Debug.Log("Спавн " + obj.name + " [" + Mathf.Round(obj.transform.position.x) + ';' + Mathf.Round(obj.transform.position.y) + "]; ");
    }

    public Vector2 RandomStart()
    {

        Vector2 randomPosition;

        //Debug.Log("Проводим операцию");
        direction = Random.Range(0, 4);

        switch (direction)
        {

            case 0:
                return randomPosition = new Vector2(max.x, Random.Range(min.y, max.y));
                //break;
            case 1:
                return randomPosition = new Vector2(min.x, Random.Range(min.y, max.y));
                //break;
            case 2:
                return randomPosition = new Vector2(Random.Range(min.x, max.x), max.y);
                //break;
            case 3:
                return randomPosition = new Vector2(Random.Range(min.x, max.x), min.y);
                //break;
            default:
                Debug.Log("Выбран DEFAULT");
                return randomPosition = new Vector2(max.x, max.y);
                //break;
        }
        //Debug.Log("Выбрано направление " + direction + "по координатам: [" + randomPosition.x + "," + randomPosition.y + "];");
    }


    private float timeLeftTest = 0;
    private void timerTest(float timeSecondsTest)
    {
        timeLeftTest -= Time.deltaTime;
        if (timeLeftTest < 0)
        {
            //тестовое значение
            int testIndex = GetChanceIndex();
            Debug.Log("Значение = " + testIndex);

            //сбрасываем
            timeLeftTest = timeSecondsTest;
        }
    }
}
