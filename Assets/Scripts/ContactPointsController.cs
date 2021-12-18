using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactPointsController : MonoBehaviour
{
    public bool isHaveGround = true; //имеется ли хотя бы одна поверхность
    private int countGrounds = 0; //счет имеющейся под триггером поверхности

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.name + " вошел в " + name);

        if (collision.gameObject.CompareTag("Ground"))
        {
            countGrounds++;
            //Debug.Log(name + ": Прибавлено CountGrounds = " + countGrounds);

            isHaveGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.name + " вышел из " + name);

        if (collision.gameObject.CompareTag("Ground"))
        {
            countGrounds--;
            //Debug.Log(name + ": Вычтено CountGrounds = " + countGrounds);

            if (countGrounds == 0)
            {
                isHaveGround = false;
            }
        }
    }
}
