using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactPointsController : MonoBehaviour
{
    public bool isHaveGround = true; //������� �� ���� �� ���� �����������
    private int countGrounds = 0; //���� ��������� ��� ��������� �����������

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.name + " ����� � " + name);

        if (collision.gameObject.CompareTag("Ground"))
        {
            countGrounds++;
            //Debug.Log(name + ": ���������� CountGrounds = " + countGrounds);

            isHaveGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.name + " ����� �� " + name);

        if (collision.gameObject.CompareTag("Ground"))
        {
            countGrounds--;
            //Debug.Log(name + ": ������� CountGrounds = " + countGrounds);

            if (countGrounds == 0)
            {
                isHaveGround = false;
            }
        }
    }
}
