using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//private float horizontalInput;

public class PlayerController : MonoBehaviour
{
    public float health = 5.0f;
    public float speed = 5.0f;
    public int damage = 1;

    public GameObject gunObject;
    public GameObject projectilePrefab;

    private float border = 1.5f;

    private GameManager gameManager;

    public bool invulnerability = false;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            //передвижение
            float verticalInput = Input.GetAxis("Vertical") * Time.deltaTime * speed;
            float horizontalInput = Input.GetAxis("Horizontal") * Time.deltaTime * speed;

            if (Mathf.Abs(transform.position.x + horizontalInput) < border && Mathf.Abs(transform.position.y + verticalInput) < border)
            {
                transform.Translate(horizontalInput, verticalInput, 0);
            }

            //управление оружием
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            gunObject.transform.right = ((Vector3)mousePosition - transform.position) * Time.deltaTime;

            //стрельба, выпуск снарядов
            if (Input.GetMouseButtonDown(0))
            {
                GameObject pooledProjectile = ProjectilePools.SharedInstance.GetPooledObject();
                if (pooledProjectile != null)
                {
                    pooledProjectile.SetActive(true); // activate it
                    pooledProjectile.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.1f); // position it at player
                    pooledProjectile.transform.localEulerAngles = gunObject.transform.localEulerAngles;
                }
            }
        }

        //ставим на паузу
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameManager.PauseGame();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name + " столкнулся с " + name);

        if (collision.gameObject.CompareTag("ProjectileEnemy"))
        {
            Debug.Log(name + " получил урон, текущее здоровье: " + health + " хп.");
            if (!invulnerability) health -= 0.25f;
            gameManager.UpdateSliderHealth();

            collision.gameObject.SetActive(false);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log(name + " получил урон от столкновения с врагом, текущее здоровье: " + health + " хп.");
            if (!invulnerability) health -= 0.50f;
            gameManager.UpdateSliderHealth();
            Destroy(collision.gameObject);
        }

        if (health <= 0 && !invulnerability)
        {
            gameManager.GameOver();
        }
    }
}
