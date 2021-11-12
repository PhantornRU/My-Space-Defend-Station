using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//private float horizontalInput;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float border = 1.0f; 
    public int damage = 1;

    public GameObject gunObject;
    public GameObject projectilePrefab;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
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

            GameObject pooledProjectile = GameManager.SharedInstance.GetPooledObject();
            if (pooledProjectile != null)
            {
                pooledProjectile.SetActive(true); // activate it
                pooledProjectile.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.1f); // position it at player
                pooledProjectile.transform.localEulerAngles = gunObject.transform.localEulerAngles;
            }
        }
    }
}
