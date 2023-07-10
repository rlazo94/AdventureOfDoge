using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesScript : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private Animator m_animator;
    public GameObject CoinModel;
    public Transform coinTransform;
    public HealthbarScript healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        m_animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        //Damage
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        //Animation
        m_animator.SetTrigger("Hurt");
        Debug.Log("Hit");
        if (currentHealth <= 0)
        {

            Die();
        }
    }


    void Die()
    {
        m_animator.SetBool("IsDead",true);
        Destroy(gameObject,3f);

        if (currentHealth == 0)
        { 
        DropCoin();
        }
    }

    void DropCoin()
    {
        // Positon of the enery
        Vector3 position = coinTransform.position;
        // Coin Drop
        GameObject coin = Instantiate(CoinModel, position + new Vector3(0.0f, 1.0f, 0.0f), Quaternion.identity);       
        coin.SetActive(true);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
