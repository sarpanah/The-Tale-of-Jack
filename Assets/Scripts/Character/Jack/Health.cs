using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Health : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    public int health;
    private bool isAlive;
    public event Action OnTakeDamage;
    public event Action OnDie;

    //TODO ADD FLOATING DAMAGE TEXT LATER
    //UIManager uIManager;

    void Awake()
    {
        health = maxHealth;
        isAlive = true;
        //uIManager = FindObjectOfType<UIManager>();
    }

    private void Update()
    {
       
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        OnTakeDamage?.Invoke();
        //uIManager.characterDamaged.Invoke(gameObject, damage);
        if (health <= 0)
        {
            Die();
        }
    }

    public void restoreHP(int HP)
    {
        health += HP;
        if (health >= maxHealth)
        {
            health = maxHealth;
        }
    }

    void Die()
    {
        OnDie?.Invoke();

        isAlive = false;
        Debug.Log("Died");
        //health = maxHealth;

    }

}

