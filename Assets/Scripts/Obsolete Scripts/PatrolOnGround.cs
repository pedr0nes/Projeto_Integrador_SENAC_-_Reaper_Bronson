using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolOnGround : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float detectionDistance;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsEnemy;

    private Rigidbody2D m_rigidbody2D;
    private bool isMovingLeft = true;
    private Vector2 direction;
    public Transform groundDetection;

    private void Awake()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        direction = Vector2.left;
    }


    // Update is called once per frame
    void Update()
    {
        m_rigidbody2D.velocity = (direction * speed * Time.deltaTime);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, direction, detectionDistance, whatIsGround);
        RaycastHit2D enemyInfo = Physics2D.Raycast(groundDetection.position, direction, detectionDistance, whatIsEnemy);



        if ((groundInfo.collider == true || enemyInfo.collider == true) && enemyInfo.collider != gameObject.GetComponent<Collider2D>())
        {

            if (isMovingLeft)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                
                isMovingLeft = false;
                direction = Vector2.right;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                
                isMovingLeft = true;
                direction = Vector2.left;
            }
        }

        

    }
}
