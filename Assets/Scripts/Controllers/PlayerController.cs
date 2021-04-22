using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float speedInAir;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpTime;
    [SerializeField] private float scytheDamage;
    [SerializeField] private int maxPlayerHearts;
    private int currentPlayerHearts;

    [SerializeField] private Transform feetPos;
    [SerializeField] private Transform headPos;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float attackRadius;
    [SerializeField] private float checkRadius;

    [Header("Events")]
    [Space]
    public UnityEvent OnLandEvent;

    private Rigidbody2D m_Rigidbody2D;
    private ShootingMode m_ShootingMode;
    

    private float moveInput;
    private bool jumpButtonDown;
    private bool jumpButtonHold;
    private bool jumpButtonUp;
    private bool attackButtonDown;

    private float jumpTimeCounter;
    private bool isGrounded;
    private bool isTouchingCeiling;
    private bool isJumping;
    private bool facingRight = true;
    
    

    void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_ShootingMode = GetComponent<ShootingMode>();
        m_ShootingMode.enabled = false;
        currentPlayerHearts = maxPlayerHearts;
        
    }

    void Update()
    {

        //Pega Inputs
        moveInput = Input.GetAxisRaw("Horizontal");
        jumpButtonDown = Input.GetButtonDown("Jump");
        jumpButtonHold = Input.GetButton("Jump");
        jumpButtonUp = Input.GetButtonUp("Jump");
        attackButtonDown = Input.GetButtonDown("Fire1");

        //Muda o personagem de lado conforme o input
        if(!facingRight && moveInput > 0)
        {
            FlipSprite();
        }
        else if(facingRight && moveInput < 0)
        {
            FlipSprite();
        }

        //Pulo
        Jump();

        //Ataque
        Attack();
    }

    private void FixedUpdate()
    {
        //Checa se está no chão ou não. Se está terminando o pulo, encerra a animação de pulo.
        bool wasGrounded = isGrounded;
        isGrounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(feetPos.position, checkRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                isGrounded = true;
                if (!wasGrounded)
                {
                    OnLandEvent.Invoke();
                }
            }
        }

        //Checa se bateu no teto
        isTouchingCeiling = Physics2D.OverlapCircle(headPos.position, checkRadius, whatIsGround);
        
        //Mover
        Move();
    }

    //Métodos

    private void Move()
    {
        if (isGrounded)
        {
            m_Rigidbody2D.velocity = new Vector2(moveInput * speed, m_Rigidbody2D.velocity.y);
        }
        else
        {
            m_Rigidbody2D.velocity = new Vector2(moveInput * speedInAir, m_Rigidbody2D.velocity.y);
        }
    }

    private void Jump()
    {
        //Pula se recebeu input e está no chão
        if (isGrounded && jumpButtonDown)
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            m_Rigidbody2D.velocity = Vector2.up * jumpForce;
        }
        
        //Pula mais alto se o botão do pulo for pressionado por mais tempo
        if (jumpButtonHold && isJumping)
        {
            //Faz uma checagem pra não continuar pulando se bater a cabeça no teto
            if (jumpTimeCounter > 0 && !isTouchingCeiling)
            {
                m_Rigidbody2D.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        //Para de pular se o botão de pulo é solto
        if (jumpButtonUp)
        {
            isJumping = false;
        }

    }

    private void FlipSprite()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void Attack()
    {
        if(attackButtonDown)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                EnemyController enemyController = enemy.GetComponent<EnemyController>();
                enemyController.TakeDamage(scytheDamage);
                Debug.Log("Reaper Bronson hit " + enemy.name);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentPlayerHearts -= damage;
        Debug.Log("Reaper Bronson got hit!");
        Debug.Log("Remaining hearts = " + currentPlayerHearts);
    }

    //Mostra o raio do ataque físico
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Item"))
        {
            m_ShootingMode.enabled = true;
            Destroy(collision.gameObject);
        }
    }


    //Get / Sets
    public float MoveInput
    {
        get
        {
            return moveInput;
        }
    }

    public bool JumpButtonDown
    {
        get
        {
            return jumpButtonDown;
        }
    }

    public bool IsGrounded
    {
        get
        {
            return isGrounded;
        }
    }

    public bool IsJumping
    {
        get
        {
            return isJumping;
        }
    }

    public bool AttackButtonDown
    {
        get
        {
            return attackButtonDown;
        }
    }
}
