using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StateHandler : MonoBehaviour
{
    [SerializeField] private int health = 1000;
    public int Health
    {
        get { return health; }
        set
        {
            if(value > maxHealth)
            {
                health = maxHealth;
            }
        }
    }
    [SerializeField] private int maxHealth = 1000;
    public int MaxHealth
    {
        get { return maxHealth; }
        set
        {
            int delta = value - maxHealth;
            if (delta > 0)
                Health += delta;
            else Health = Health;

            if (value <= 0)
                maxHealth = 1;
            if (value > 9999)
                maxHealth = 9999;
        }
    }

    [SerializeField] private float speed;
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }
    private void Awake()
    {
        Speed = 5;
    }

}
