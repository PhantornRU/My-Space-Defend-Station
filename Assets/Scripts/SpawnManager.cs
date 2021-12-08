using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private GameManager gameManager;
    public GameObject[] targetPrefabs;

    public float borderPosition = 10.0f;
    private int direction;

    [SerializeField] private float startDelay = 1.0f;
    [SerializeField] private float spawnInterval = 2.0f;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void StartSpawn()
    {
        InvokeRepeating("SpawnRandomTarget", startDelay, spawnInterval / gameManager.difficultyRate);
    }

    public void StopSpawn()
    {
        CancelInvoke("SpawnRandomTarget");
    }

    private void SpawnRandomTarget()
    {
        int index = Random.Range(0, targetPrefabs.Length);
        targetPrefabs[index].transform.localEulerAngles = new Vector3(0, 0, -90);

        GameObject obj = (GameObject)Instantiate(targetPrefabs[index], RandomStart(), targetPrefabs[index].transform.rotation);
        obj.transform.SetParent(this.transform); // ставим как дочерний объект к Spawn Manager
    }

    public Vector2 RandomStart()
    {

        Vector2 randomPosition;

        //Debug.Log("Проводим операцию");
        direction = Random.Range(0, 4);

        switch (direction)
        {

            case 0:
                return randomPosition = new Vector2(borderPosition * 2, 0);
                //break;
            case 1:
                return randomPosition = new Vector2(-borderPosition * 2, 0);
                //break;
            case 2:
                return randomPosition = new Vector2(0, borderPosition);
                //break;
            case 3:
                return randomPosition = new Vector2(0, -borderPosition);
                //break;
            default:
                Debug.Log("Выбран DEFAULT");
                return randomPosition = new Vector2(borderPosition, borderPosition);
                //break;
        }
        //Debug.Log("Выбрано направление " + direction + "по координатам: [" + randomPosition.x + "," + randomPosition.y + "];");
    }
}
