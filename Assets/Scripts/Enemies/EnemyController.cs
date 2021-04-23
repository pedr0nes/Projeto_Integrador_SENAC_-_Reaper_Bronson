using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float enemyMaxHealth;
    [SerializeField] private int enemyDamage;
    private float enemyCurrentHealth;

    // Start is called before the first frame update
    void Start()
    {
        enemyCurrentHealth = enemyMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void TakeDamage(float damage)
    {
        enemyCurrentHealth -= damage;
        Debug.Log("Enemy Life = " + enemyCurrentHealth);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.TakeDamage(enemyDamage);
        }
    }


}
