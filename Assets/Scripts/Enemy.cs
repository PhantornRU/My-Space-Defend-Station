using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    public float speed;
    public float horizontalSpeed;
    public float timeDifferentMoved;

    public int scoreForDestroy;
    public int spawnRate0to100;

    private GameManager gameManager;
    private PlayerController player;
    private Rigidbody2D enemyRb;

    private float scaleX;
    private float scaleY;

    public bool isDifferentHorizontalMoved;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        enemyRb = GetComponent<Rigidbody2D>();

        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.1f); //������ �� Z �������

        scaleX = transform.localScale.x;
        scaleY = transform.localScale.x;
    }

    private bool checkDifferentMoved = false;

    // Update is called once per frame
    void Update()
    {
        RotateToPlayer();

        HorizontalMove();
    }

    protected void RotateToPlayer()
    {
        //������� � ������� ������
        transform.right = (Vector2)(player.transform.position - transform.position) * Time.deltaTime;

        if (Mathf.Abs(transform.rotation.x) > 0 || Mathf.Abs(transform.rotation.y) > 0)
        {   //������� �����������
            transform.eulerAngles = new Vector3(0, 0, transform.rotation.z);
        }
    }

    protected void HorizontalMove()
    {
        //�������������� ��������
        if (isDifferentHorizontalMoved)
        {   //�������� � ������ �������
            if (!checkDifferentMoved)
            {
                StartCoroutine(DifferentHorizontalMoved());
            }
        }
        else
        {   //�������� � ���� �������
            transform.Translate(speed * Time.deltaTime, horizontalSpeed * Time.deltaTime, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.name + " ���������� � " + name);
        if (collision.gameObject.CompareTag("Projectile"))
        {
            health -= player.damage;

            StartCoroutine(TakeDamage());

            Debug.Log(name + " ������� " + player.damage + " �����, ������� ��������: " + health);

            if (health <= 0)
            {
                gameManager.UpdateScore(scoreForDestroy);

                Debug.Log(name + " ��������� ");

                Destroy(gameObject);
            }

            collision.gameObject.SetActive(false);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        pushOffInTrigger(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        pushOffInTrigger(collision);
    }

    void pushOffInTrigger(Collider2D collision)
    {   //����������� ���� �� �����
        if (collision.gameObject != this.gameObject && collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log(name + " ����������� �� " + collision.name);
            Vector2 awayFromTear = (collision.gameObject.transform.position - transform.position).normalized;
            transform.Translate(speed * awayFromTear.x * Time.deltaTime, horizontalSpeed * awayFromTear.y * Time.deltaTime, 0);
        }
    }

    private float scaleChange = 0.01f;
    private float scaleChangeBorder = 0.25f;

    IEnumerator TakeDamage()
    {
        while (transform.localScale.x > scaleX - scaleChangeBorder)
        {   //���������
            transform.localScale *= 1 - scaleChange;
            yield return null;
        }

        while (transform.localScale.x < scaleX + scaleChangeBorder)
        {   //�����������
            transform.localScale *= 1 + scaleChange;
            yield return null;
        }

        while (transform.localScale.x > scaleX)
        {   //���������� � �������� �����
            transform.localScale *= 1 - scaleChange;
            yield return null;
        }

        transform.localScale = new Vector2(scaleX, scaleY);
    }

    private float timeSaved;

    IEnumerator DifferentHorizontalMoved()
    {
        checkDifferentMoved = true;

        //Debug.Log("[" + Time.time + "] ������� ������:" + intTimeDebug);
        while (Time.time < (timeDifferentMoved / 2) + timeSaved)
        {   //�������� ����
            float checkPos = transform.position.y + horizontalSpeed * Time.deltaTime;
            if (checkPos >= -0.05f && checkPos <= 0.05f) //�������� ����� �� ���������
            {
                transform.Translate(0, 0.1f, 0);
            }

            transform.Translate(speed * Time.deltaTime, horizontalSpeed * Time.deltaTime, 0);
            yield return null; //new WaitForSeconds(.1f);
        }
        while (Time.time < timeDifferentMoved + timeSaved)
        {   //�������� �����
            float checkPos = transform.position.y - horizontalSpeed * Time.deltaTime;
            if (checkPos >= -0.05f && checkPos <= 0.05f) //�������� ����� �� ���������
            {
                transform.Translate(0, -0.1f, 0);
            }

            transform.Translate(speed * Time.deltaTime, -horizontalSpeed * Time.deltaTime, 0);
            yield return null; //new WaitForSeconds(.1f);
        }
        timeSaved = Time.time;

        checkDifferentMoved = false;
    }
}
