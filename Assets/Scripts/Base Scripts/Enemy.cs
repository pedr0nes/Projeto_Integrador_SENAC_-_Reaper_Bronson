using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public int CurrentHealth { get; protected set; }
    [SerializeField] public SpriteRenderer spriteRenderer;
    public void TakeDamage(int damage)
    {
        
        CurrentHealth -= damage;
        StartCoroutine(FlashSprite());
    }


    public IEnumerator FlashSprite()
    {
        if(CurrentHealth > 0)
        {
            int flashTurns = 0;
            while (flashTurns < 5)
            {
                spriteRenderer.material.SetFloat("_FlashAmount", 1f);
                yield return new WaitForSeconds(0.1f);
                spriteRenderer.material.SetFloat("_FlashAmount", 0f);
                yield return new WaitForSeconds(0.1f);

                flashTurns++;
            }
            
        }
        yield return null;
    }
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }
}
