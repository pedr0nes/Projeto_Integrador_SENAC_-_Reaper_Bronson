using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Bullet Controler is a Unity MonoBehaviour derived class
// It is responsible for managing the speed and damage of Player's gun bullets

public class BulletController : MonoBehaviour
{
    //Variable Declaration
    [SerializeField] private float bulletSpeed;
    [SerializeField] private int bulletDamage;
    [SerializeField] private BulletSFX bulletSFXManager;
    private Rigidbody2D m_Rigidbody2D;
    
    void Start()
    {
        //Assigns a reference to the bullet's Rigidbody
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        //Sets bullet direction and speed 
        m_Rigidbody2D.velocity = transform.right * bulletSpeed;
    }

    //Trigger method responsible for causing damage in objects labeled as 'Enemy'
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            bulletSFXManager.PlayBulletHitSFX();
            enemy.TakeDamage(bulletDamage);
            Destroy(gameObject);
        }
    }




}
