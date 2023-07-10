using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;
    public CharacterController characterController;
    public int muenze;
    private Animator m_animator;
    private bool triggerEnter = false;

    public Transform shopeingang;
    public Transform shopausgang;

    public HealthbarScript healthBar;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        m_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && triggerEnter == true)
        {
            muenze++;
            SoundEffectScript.PlaySound("coin");
            GameObject.Find("Deko_6_Kiste").SetActive(false);
            triggerEnter = false;
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        m_animator.SetTrigger("Hit");

        if (currentHealth <= 0)
        {
            Die();
        }
    }


    void Die()
    {
        if (currentHealth == 0)
        {
            m_animator.SetBool("IsDead", true);          
        }
    }

    void OnTriggerEnter(Collider target)
    {
        switch (target.tag)
        {
            case "ShopEingang":
                SetPosition(shopeingang.position);
                break;
            case "EnemySlime":
                TakeDamage(10);
                break;
            case "ShopAusgang":
                SetPosition(shopausgang.position);
                break;
            case "Muenze":
                muenze++;
                SoundEffectScript.PlaySound("coin");
                break;
            case "Chest":          
                triggerEnter = true;
                break;
            case "Trap":
                TakeDamage(20);
                break;
            default:
                break;
        }
    }

    void SetPosition(Vector3 NewPosition)
    {
        characterController.enabled = false;
        transform.position = NewPosition;
        characterController.enabled = true;
    }
}
