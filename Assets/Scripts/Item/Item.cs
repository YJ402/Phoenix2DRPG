using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {

        }
    }
    //resource ������ enterTrigger���, change health �Լ�
    //�� �۵��ϴ����� �μ��Բ� ���� �ޱ�
    //itemprefab�� �� cs ���� ����� �ű⿡ ����������
    //item controller ����
}