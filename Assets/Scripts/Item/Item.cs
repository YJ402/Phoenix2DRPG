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
    //resource 쪽으로 enterTrigger사용, change health 함수
    //잘 작동하는지는 민성님께 검토 받기
    //itemprefab에 들어갈 cs 따로 만들고 거기에 만들어줘야함
    //item controller 생성
}