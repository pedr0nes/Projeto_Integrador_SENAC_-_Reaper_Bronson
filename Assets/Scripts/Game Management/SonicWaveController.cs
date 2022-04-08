﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonicWaveController : MonoBehaviour
{
    [SerializeField] private float sonicWaveSpeed;
    [SerializeField] private int sonicWaveDamage;
    [SerializeField] private float lifeTime;
    private Rigidbody2D m_Rigidbody2D;


    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Rigidbody2D.velocity = transform.right * sonicWaveSpeed;
        StartCoroutine(WaveLifeSpam());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator WaveLifeSpam()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

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