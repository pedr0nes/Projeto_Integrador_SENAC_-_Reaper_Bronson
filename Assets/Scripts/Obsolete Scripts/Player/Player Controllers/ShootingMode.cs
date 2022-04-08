using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShootingMode : MonoBehaviour
{
    [SerializeField] Sprite playerShootingSprite;
    [SerializeField] GameObject rifleBulletPrefab;
    [SerializeField] Transform shootingPoint;
    [SerializeField] private PlayerSFX playerSFXManager;
    [SerializeField] private GameObject rifleCanvasImage;

    private PlayerController m_PlayerController;
    private Rigidbody2D m_Rigidbody;
    private Animator m_Animator;
    private Transform rifle;

    private bool shootingModeAlreadyCalled = false;

    [Header("Events")]
    [Space]
    public UnityEvent OnShootEvent;

    //public Camera m_Camera;

    //public float mouseX;
    //public float mouseY;

    // Start is called before the first frame update
    void Start()
    {
        
        m_PlayerController = GetComponent<PlayerController>();
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
        rifle = transform.Find("Rifle");
        rifleCanvasImage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Avaliar a possibilidade da mira do rilfe ser dada pela posição do mouse
        //mouseX = Input.mousePosition.x;
        //mouseY = Input.mousePosition.y;

        //Debug.Log("X do mouse: " + mouseX);
        //Debug.Log("Y do mouse: " + mouseY);
        //Debug.Log("Posição do Jogador: " + m_Camera.WorldToScreenPoint(transform.position));


        if (m_PlayerController.IsGrounded && Input.GetButton("Fire2"))
        {
            rifleCanvasImage.SetActive(true);
            CallDrawGunSFX();
            m_Animator.enabled = false;
            GetComponent<SpriteRenderer>().sprite = playerShootingSprite;
            rifle.gameObject.SetActive(true);
            m_PlayerController.enabled = false;
            m_Rigidbody.velocity = Vector2.zero;

            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }

        }
        else
        {
            shootingModeAlreadyCalled = false;
            m_PlayerController.enabled = true;
            m_Animator.enabled = true;
            rifle.gameObject.SetActive(false);
            rifleCanvasImage.SetActive(true);
        }
    }


    private void Shoot()
    {
        OnShootEvent.Invoke();
        Instantiate(rifleBulletPrefab, shootingPoint.position, transform.rotation);
        playerSFXManager.PlayGunShotSFX();
    }

    private void CallDrawGunSFX()
    {
        if(!shootingModeAlreadyCalled)
        {
            playerSFXManager.PlayDrawGunSFX();
            shootingModeAlreadyCalled = true;
        }
    }


}
