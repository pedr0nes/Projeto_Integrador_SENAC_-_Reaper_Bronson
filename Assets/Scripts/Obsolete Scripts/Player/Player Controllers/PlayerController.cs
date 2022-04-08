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
    [SerializeField] private int maxPlayerLifes;
    private int currentPlayerHearts;


    [SerializeField] private Transform feetPos;
    [SerializeField] private Transform headPos;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsTrap;
    [SerializeField] private float attackRadius;
    [SerializeField] private float checkRadius;

    [Header("Events")]
    [Space]
    public UnityEvent OnLandEvent;
    [Space]

    [SerializeField] private PlayerSFX playerSFXManager;
    private Rigidbody2D m_Rigidbody2D;
    private ShootingMode m_ShootingMode;
    private GameManager m_GameManager;
    private Animator m_Animator;

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
    private bool lostAllHearts;
    private bool lostAllLives;


    private void Awake()
    {
        m_GameManager = FindObjectOfType<GameManager>();
    }

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_ShootingMode = GetComponent<ShootingMode>();
        m_ShootingMode.enabled = false;
        currentPlayerHearts = maxPlayerHearts;
        m_GameManager.CurrentNumberOfHearts = maxPlayerHearts;

        lostAllLives = false;
        lostAllHearts = false;

    }

    void Update()
    {


        m_GameManager = FindObjectOfType<GameManager>();
        m_GameManager.CurrentNumberOfHearts = currentPlayerHearts;
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

        LifeManager();

        //Ataque
        //Attack();
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

        TrapDamage();


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
            playerSFXManager.PlayJumpSFX();
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

    public void Attack()
    {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                EnemyController enemyController = enemy.GetComponent<EnemyController>();
                playerSFXManager.PlayWeaponHitSFX();
                enemyController.TakeDamage(scytheDamage);

            }

    }

    public void TakeDamage(int damage)
    {
        playerSFXManager.PlayTakeDamageSFX();
        m_Animator.SetTrigger("TakeDamage");
        currentPlayerHearts -= damage;
    }


    private void LifeManager()
    {
        if (m_GameManager.CurrentNumberOfLifes <= 0)
        {
            lostAllLives = true;
 
            CallKillPlayerAnimation();

        }
        else
        {
            if (currentPlayerHearts <= 0)
            {
                CallDeathSound();
                CallKillPlayerAnimation();

            }
        }
    }

    private void TrapDamage()
    {
        if(GetComponent<Collider2D>().IsTouchingLayers(whatIsTrap))
        {
            TakeDamage(5);
        }
    }


    private void CallKillPlayerAnimation()
    {
        m_Rigidbody2D.velocity = Vector3.zero;
        m_Rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
        gameObject.GetComponent<Collider2D>().enabled = false;
        m_Animator.SetBool("LostAllHearts", true);

    }

    private void CallDeathSound()
    {
        if(!lostAllHearts)
        {
            playerSFXManager.PlayDeathSFX();
            lostAllHearts = true;
        }
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
            playerSFXManager.PlayDrawGunSFX();
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

    public int MaxPlayerLifes
    {
        get
        {
            return maxPlayerLifes;
        }
    }

    public int CurrentPlayerHearts
    {
        get
        {
            return currentPlayerHearts;
        }
        set
        {
            currentPlayerHearts = value;
        }
    }

    public bool LostAllLives
    {
        get
        {
            return lostAllLives;
        }
    }

}
