using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
    [SerializeField] private Transform areaPoint1;
    [SerializeField] private Transform areaPoint2;
    [SerializeField] private LayerMask playerLayer;
    
    public GameManager m_GameManager;
    private Collider2D isPlayerNearby;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_GameManager = FindObjectOfType<GameManager>();

        isPlayerNearby = Physics2D.OverlapArea(areaPoint1.position, areaPoint2.position, playerLayer);
        Debug.DrawLine(areaPoint2.position, areaPoint1.position);

        if(isPlayerNearby)
        {
            m_GameManager.PlayerWon = true;
            m_GameManager.LoadFinalScene();
        }


    }
}
