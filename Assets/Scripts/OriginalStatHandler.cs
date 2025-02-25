using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginalStatHandler : MonoBehaviour
{
    [Range(1, 9999)][SerializeField] private int health = 1000; //�ִ� 100���� 9999, �⺻ 100���� 1000���� ����
    public int Health
    {
        get => health;
        set => health = Mathf.Clamp(value, 0, 9999);
    }

    [Range(1f, 20f)][SerializeField] private float speed = 5;  //�⺻ 3���� 5�� ����
    public float Speed
    {
        get => speed;
        set => speed = Mathf.Clamp(value, 0, 40);
    }
    //{                                                             ���ǵ忡 ���� ���� �ִϸ��̼� �ӵ�����,  �Ѵ� ���밡���ҵ���
    //    get { return speed; }
    //    set 
    //    { 
    //        speed = value;
    //        animationHandler.ChangeMovingSpeed(Speed/5);
    //    }
    //}
}
