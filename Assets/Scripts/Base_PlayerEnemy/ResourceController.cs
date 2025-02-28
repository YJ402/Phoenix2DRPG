using System;
using UnityEngine;

public class ResourceController : MonoBehaviour
{
    [SerializeField] int trapDamage=50;
    [SerializeField] private float healthChangeDelay = .5f;

    private BaseController baseController;
    private StatHandler statHandler;
    private AnimationHandler animationHandler;
    PlayerController playerController;
    private float timeSinceLastChange = float.MaxValue;
    public int CurrentHealth { get; set; }
    public int MaxHealth => statHandler.MaxHealth;            //원래 healt는 최대체력 의미하는것이라 MaxHealth로 변경

    public AudioClip damageClip;

    private Action<float, float> OnChangeHealth;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        statHandler = GetComponent<StatHandler>();
        animationHandler = GetComponent<AnimationHandler>();
        baseController = GetComponent<BaseController>();
        CurrentHealth = MaxHealth;
    }

    private void Start()
    {
    }

    private void Update()
    {
        if (timeSinceLastChange < healthChangeDelay)
        {
            timeSinceLastChange += Time.deltaTime;
            if (timeSinceLastChange >= healthChangeDelay)
            {
                animationHandler.InvincibilityEnd();
            }
        }
        
        //playerController.UpdateHpSlider(CurrentHealth/MaxHealth);
    }

    public bool ChangeHealth(int change)
    {
        if (change == 0 || timeSinceLastChange < healthChangeDelay)
        {
            return false;
        }
        if(change<=0)
            timeSinceLastChange = 0f;
        CurrentHealth += change;
        CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth;
        CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth;

        OnChangeHealth?.Invoke(CurrentHealth, MaxHealth);
        playerController?.UpdateHpSlider((float)CurrentHealth / MaxHealth);

        if (change < 0)
        {
            animationHandler.Damage();

            //if (damageClip) ;
                //SoundManager.PlayClip(damageClip); // 나중에 추가
        }

        if (CurrentHealth <= 0f)
        {
            Death();
        }

        return true;
    }

    private void Death()
    {
        baseController.Death();
    }

    public void AddHealthChangeEvent(Action<float, float> action)
    {
        OnChangeHealth += action;
    }

    public void RemoveHealthChangeEvent(Action<float, float> action)
    {
        OnChangeHealth -= action;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            ChangeHealth(-trapDamage);
        }
    }
}