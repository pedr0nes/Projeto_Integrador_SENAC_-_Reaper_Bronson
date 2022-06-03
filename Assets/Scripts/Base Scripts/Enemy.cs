using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Enemy Class derives from the Character Class
 * It is the base class for all the enemy characters in game
 * This class substates are:
        - Bat
        - Ghost
        - Vampire
        - Zombie
 */

public class Enemy : Character   
{
    #region Variables
    //Reference to the sprite renderer
    [SerializeField] public SpriteRenderer spriteRenderer;

    //Variable to keep track of the current health
    public int CurrentHealth { get; protected set; }

    #endregion

    #region Methods

    //Method responsible for causing damage to the Enemy
    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        StartCoroutine(FlashSprite());
    }

    //Coroutine that causes a flash effect to the object's sprite when needed
    public IEnumerator FlashSprite()
    {
        //if(CurrentHealth > 0)
        //{
            int flashTurns = 0;
            while (flashTurns < 5)
            {
                Debug.Log("PISCA");
                spriteRenderer.material.SetFloat("_FlashAmount", 1f);
                yield return new WaitForSeconds(0.1f);
                spriteRenderer.material.SetFloat("_FlashAmount", 0f);
                yield return new WaitForSeconds(0.1f);

                flashTurns++;
            }
            
       // }
        yield return null;
    }
    #endregion

    //Unity MonoBehaviour Methods
    #region Unity Callback Methods
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
    #endregion
}
