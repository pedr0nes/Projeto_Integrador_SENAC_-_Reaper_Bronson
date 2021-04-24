using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float enemyMaxHealth;
    [SerializeField] private int enemyTouchDamage;
    [SerializeField] private int enemyAttackDamage;
    private float enemyCurrentHealth;
    private Vector2 impulseDirection;
    private Rigidbody2D m_Rigidbody;
    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        enemyCurrentHealth = enemyMaxHealth;
        m_Rigidbody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
        //PUSHBACK não está dando certo por enquanto
        /*if (transform.position.x - player.transform.position.x <= 0)
        {
            impulseDirection = Vector2.left;
        }
        else if (transform.position.x - player.transform.position.x > 0)
        {
            impulseDirection = Vector2.right;
        }*/
    }


    public void TakeDamage(float damage)
    {
        enemyCurrentHealth -= damage;
        //m_Rigidbody.AddForce(impulseDirection * 10, ForceMode2D.Impulse);
        Debug.Log("Enemy Life = " + enemyCurrentHealth);

    }

    public void Attack(PlayerController player)
    {
        player.TakeDamage(enemyAttackDamage);
    }

    public void KillEnemy()
    {
            Destroy(gameObject);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.TakeDamage(enemyTouchDamage);
        }
    }

    public int EnemyAttackDamage
    {
        get
        {
            return enemyAttackDamage;
        }
    }

    public float EnemyCurrentHealth
    {
        get
        {
            return enemyCurrentHealth;
        }
    }


}
