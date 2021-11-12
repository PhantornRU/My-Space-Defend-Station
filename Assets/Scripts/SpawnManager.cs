using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] targetPrefabs;
    private GameManager gameManager;

    public float borderPosition = 10.0f;
    private int direction;

    [SerializeField] private float startDelay = 1.0f;
    [SerializeField] private float spawnInterval = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        InvokeRepeating("SpawnRandomTarget", startDelay, spawnInterval);
    }

    private void Update()
    {

    }

    public Vector2 RandomStart()
    {

        Vector2 randomPosition;

        //Debug.Log("Проводим операцию");
        direction = Random.Range(0, 4);

        switch (direction)
        {

            case 0:
                return randomPosition = new Vector2(borderPosition, 0);
                Debug.Log("Выбрано направление " + direction + "по координатам: [" + randomPosition.x + "," + randomPosition.y + "];");
                break;
            case 1:
                return randomPosition = new Vector2(-borderPosition, 0);
                Debug.Log("Выбрано направление " + direction + "по координатам: [" + randomPosition.x + "," + randomPosition.y + "];");
                break;
            case 2:
                return randomPosition = new Vector2(0, borderPosition);
                Debug.Log("Выбрано направление " + direction + "по координатам: [" + randomPosition.x + "," + randomPosition.y + "];");
                break;
            case 3:
                return randomPosition = new Vector2(0, -borderPosition);
                Debug.Log("Выбрано направление " + direction + "по координатам: [" + randomPosition.x + "," + randomPosition.y + "];");
                break;
            default:
                Debug.Log("Выбран DEFAULT");
                return randomPosition = new Vector2(borderPosition, borderPosition);
                Debug.Log("Выбрано направление " + direction + "по координатам: [" + randomPosition.x + "," + randomPosition.y + "];");
                break;
        }
    }

    private void SpawnRandomTarget()
    {
        int index = Random.Range(0, targetPrefabs.Length);
        targetPrefabs[index].transform.localEulerAngles = new Vector3(0, 0, -90);

        Instantiate(targetPrefabs[index],
            RandomStart(),
            targetPrefabs[index].transform.rotation);

        //gameManager.Counter(); //подсчитываем
    }
}
