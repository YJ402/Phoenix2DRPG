using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemyskill : MonoBehaviour
{
    Collider2D collider;
    ResourceController resourceController;
    Animator animator;
    private static int IsReady = Animator.StringToHash("IsReady");
    private static int IsBoom = Animator.StringToHash("IsBoom");
    private void Awake()
    {
        collider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();    
        collider.enabled = false;
        ready();
    }

    private void ready() // ��ü ������ �ٷ� ����.
    {
        animator.SetBool(IsReady,true);
        //animator.SetBool(IsBoom,true);
    }

    private void Boom() // �ִϸ��̼� �̺�Ʈ(ready): �ִϸ��̼� ������ Boom �ִϸ��̼�, �ݸ��� ����
    {
        collider.enabled =enabled;
        animator.SetBool(IsBoom, true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if(collision.gameObject.layer == 6)
        //{
            resourceController = collision.GetComponent<ResourceController>();
            resourceController.ChangeHealth(-10);
        //}
    }

    private void ExitSkill2() // �ִϸ��̼� �̺�Ʈ(Boom): �ִϸ��̼� ������ ������Ʈ ����
    {
        Destroy(gameObject);  
    }
}
