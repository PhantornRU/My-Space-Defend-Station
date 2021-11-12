using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    public float speed;
    public float horizontalSpeed;
    public float timeDifferentMoved;

    private PlayerController player;
    //private Rigidbody2D enemyRb;

    private float scaleX;
    private float scaleY;

    public bool isDifferentHorizontalMoved;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        //enemyRb = GetComponent<Rigidbody2D>();

        //поворот в сторону игрока
        transform.right = (Vector2)(player.transform.position - transform.position) * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.1f); //ставим на Z уровень

        scaleX = transform.localScale.x;
        scaleY = transform.localScale.x;
    }

    [SerializeField] private bool checkDifferentMoved = false;

    // Update is called once per frame
    void Update()
    {
        //поворот в сторону игрока
        transform.right = (Vector2)(player.transform.position - transform.position) * Time.deltaTime;

        //горизонтальное движение
        if (isDifferentHorizontalMoved)
        {   //движение в разные стороны
            if (!checkDifferentMoved)
            {
                Debug.Log("Запущена функция таймера");
                StartCoroutine("DifferentHorizontalMoved");
            }
        }
        else
        {   //движение в одну сторону
            transform.Translate(speed * Time.deltaTime, horizontalSpeed * Time.deltaTime, 0);
        }

        if (Mathf.Abs(transform.rotation.x) > 0 || Mathf.Abs(transform.rotation.y) > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, transform.rotation.z);
        }
    }

    [SerializeField] private float timeSaved;
    [SerializeField] private int intTimeDebug = 0;

    IEnumerator DifferentHorizontalMoved()
    {
        intTimeDebug++;
        checkDifferentMoved = true;

        //Debug.Log("[" + Time.time + "] Запущен таймер:" + intTimeDebug);
        while (Time.time < (timeDifferentMoved / 2) + timeSaved)
        {   //движение вниз
            //Debug.Log("[" + Time.time + "] Первый этап: " + intTimeDebug);
            float checkPos = transform.position.y + horizontalSpeed * Time.deltaTime;
            if (checkPos >= -0.05f && checkPos <= 0.05f) //смещение чтобы не застревал
            {
                //Debug.Log("[" + Time.time + "] Смещен: " + intTimeDebug + " на y:" + transform.position.y + " при checkPos = " + checkPos);
                transform.Translate(0, 0.1f, 0);
            }

            transform.Translate(speed * Time.deltaTime, horizontalSpeed * Time.deltaTime, 0);
            yield return null; //new WaitForSeconds(.1f);
        }
        while (Time.time < timeDifferentMoved + timeSaved) 
        {   //движение вверх
            //Debug.Log("[" + Time.time + "] Второй этап: " + intTimeDebug);
            float checkPos = transform.position.y - horizontalSpeed * Time.deltaTime;
            if (checkPos >= -0.05f && checkPos <= 0.05f) //смещение чтобы не застревал
            {
                //Debug.Log("[" + Time.time + "] Смещен: " + intTimeDebug + " на y:" + transform.position.y + " при checkPos = " + checkPos);
                transform.Translate(0, -0.1f, 0); 
            }

            transform.Translate(speed * Time.deltaTime, - horizontalSpeed * Time.deltaTime, 0);
            yield return null; //new WaitForSeconds(.1f);
        }
        //Debug.Log("[" + Time.time + "] Завершение таймера: " + intTimeDebug);
        timeSaved = Time.time;

        checkDifferentMoved = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name + "столкнулся с " + name);

        if (collision.gameObject.CompareTag("Projectile"))
        {
            health -= player.damage;

            StartCoroutine("TakeDamage");

            Debug.Log(name + " получил " + player.damage + " урона, текущее здоровье: " + health);

            if (health <= 0)
            {
                Destroy(gameObject);
                Debug.Log(name + " уничтожен ");
            }

            collision.gameObject.SetActive(false);
        }
    }

    [SerializeField] private float scaleChange = 0.01f;
    [SerializeField] private float scaleChangeBorder = 0.25f;

    IEnumerator TakeDamage()
    {
        while (transform.localScale.x > scaleX - scaleChangeBorder)
        {   //уменьшаем
            transform.localScale *= 1 - scaleChange;
            yield return null;
        }

        //while (Time.time < changeScaleTime)
        while (transform.localScale.x < scaleX + scaleChangeBorder)
        {   //увеличиваем
            transform.localScale *= 1 + scaleChange;
            yield return null;
        }

        while (transform.localScale.x > scaleX)
        {   //возвращаем в исходную форму
            transform.localScale *= 1 - scaleChange;
            yield return null;
        }
    }
}
