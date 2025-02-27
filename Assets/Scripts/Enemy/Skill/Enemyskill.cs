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

    private void ready() // 객체 생성시 바로 실행.
    {
        animator.SetBool(IsReady,true);
        //animator.SetBool(IsBoom,true);
    }

    private void Boom() // 애니메이션 이벤트(ready): 애니메이션 끝나면 Boom 애니메이션, 콜리더 실행
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

    private void ExitSkill2() // 애니메이션 이벤트(Boom): 애니메이션 끝나면 오브젝트 삭제
    {
        Destroy(gameObject);  
    }
}
