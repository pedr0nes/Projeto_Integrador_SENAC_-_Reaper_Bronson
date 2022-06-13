using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Sonic Wave Controler is a Unity MonoBehaviour derived class
// It is responsible for managing the speed and damage of Enemy Bat's wave attacks

public class SonicWaveController : MonoBehaviour
{
    //Variable Declaration
    [SerializeField] private float sonicWaveSpeed;
    [SerializeField] private int sonicWaveDamage;
    [SerializeField] private float lifeTime;
    private Rigidbody2D m_Rigidbody2D;

    void Start()
    {
        //Assigns a reference to the wave's Rigidbody
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        //Sets wave direction and speed 
        m_Rigidbody2D.velocity = transform.right * sonicWaveSpeed;

        //Starts a Coroutine that manages wave active time
        StartCoroutine(WaveLifeSpam());
    }

    //Coroutine that manages wave active time. Destroys the object after some time.
    IEnumerator WaveLifeSpam()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    //Trigger method responsible for causing damage in objects labeled as 'Player'
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.TakeDamage(sonicWaveDamage);
            Destroy(gameObject);
        }
    }


}
