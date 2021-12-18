using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//private float horizontalInput;

public class PlayerController : MonoBehaviour
{
    public float health = 5.0f;
    public float speed = 7.0f;
    public int damage = 1; //�� ���������� ��������
    public float timeToShoot = 0.25f;

    public GameObject gunObject;
    public GameObject projectilePrefab;

    private Vector2 border;

    private GameManager gameManager;

    public bool invulnerability = false;

    private ContactPointsController contactPointUp;
    private ContactPointsController contactPointDown;
    private ContactPointsController contactPointRight;
    private ContactPointsController contactPointLeft;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        // ������� ������
        border = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        //����� ���������������
        contactPointUp = GameObject.Find("ContactPointUp").GetComponent<ContactPointsController>();
        contactPointDown = GameObject.Find("ContactPointDown").GetComponent<ContactPointsController>();
        contactPointRight = GameObject.Find("ContactPointRight").GetComponent<ContactPointsController>();
        contactPointLeft = GameObject.Find("ContactPointLeft").GetComponent<ContactPointsController>();
    }

    public bool canShoot = false;

    private float timeLeft = 0;

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            //������������
            MovePlayer();

            //���������� �������
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            gunObject.transform.right = ((Vector3)mousePosition - transform.position) * Time.deltaTime;

            //��������, ������ �������� �� �������/������� ����� ������ ����
            if (Input.GetMouseButtonDown(0))
            {
                OneShoot();
                Debug.Log("���");
            }

            if (Input.GetMouseButton(0))
            {
                timeLeft -= Time.deltaTime;
                if (timeLeft < 0)
                {
                    OneShoot();
                    //����������
                    timeLeft = timeToShoot;
                }
            }
        }

        //������ �� �����
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameManager.PauseGame();
        }
    }
    private void timerTest(float timeSecondsTest)
    {
    }

    private void OneShoot()
    {
        GameObject pooledProjectile = ProjectilePools.SharedInstance.GetPooledObject();
        if (pooledProjectile != null)
        {
            pooledProjectile.SetActive(true); // activate it
            pooledProjectile.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.1f); // position it at player
            pooledProjectile.transform.localEulerAngles = gunObject.transform.localEulerAngles;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.name + " ����������� � " + name);

        if (collision.gameObject.CompareTag("ProjectileEnemy"))
        {
            //Debug.Log(name + " ������� ����, ������� ��������: " + health + " ��.");
            if (!invulnerability) health -= 0.25f;
            gameManager.UpdateSliderHealth();

            collision.gameObject.SetActive(false);
        }

        if (health <= 0 && !invulnerability)
        {
            gameManager.GameOver();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.name + " ���������� � " + name);

        if (collision.gameObject.CompareTag("Enemy"))
        {
            //Debug.Log(name + " ������� ���� �� ������������ � ������, ������� ��������: " + health + " ��.");
            if (!invulnerability) health -= 0.50f;
            gameManager.UpdateSliderHealth();
            Destroy(collision.gameObject);
        }

        if (health <= 0 && !invulnerability)
        {
            gameManager.GameOver();
        }
    }

    private float verticalInput, horizontalInput;

    private void MovePlayer()
    {
        //������� �� ������� WASD
        verticalInput = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        horizontalInput = Input.GetAxis("Horizontal") * Time.deltaTime * speed;

        //��������� �� ������� �� �� �� ����� ������ � ����� �� ��������� � ���� �� ������
        if (Mathf.Abs(transform.position.x + horizontalInput) < border.x && Mathf.Abs(transform.position.y + verticalInput) < border.y)
        {
            if (contactPointUp.isHaveGround && verticalInput > 0)
            {
                transform.Translate(0, verticalInput, 0);
            }
            if (contactPointDown.isHaveGround && verticalInput < 0)
            {
                transform.Translate(0, verticalInput, 0);
            }
            if (contactPointRight.isHaveGround && horizontalInput > 0)
            {
                transform.Translate(horizontalInput, 0, 0);
            }
            if (contactPointLeft.isHaveGround && horizontalInput < 0)
            {
                transform.Translate(horizontalInput, 0, 0);
            }
        }

        if (!contactPointUp.isHaveGround && !contactPointDown.isHaveGround
            && !contactPointRight.isHaveGround && !contactPointLeft.isHaveGround)
        {
            contactPointUp.isHaveGround = true;
            contactPointDown.isHaveGround = true;
            contactPointRight.isHaveGround = true;
            contactPointLeft.isHaveGround = true;
        }
    }

}
