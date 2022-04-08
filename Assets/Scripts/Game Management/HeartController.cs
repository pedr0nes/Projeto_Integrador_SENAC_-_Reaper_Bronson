using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartController : MonoBehaviour
{
    public GameManager m_GameManager;
    [SerializeField] private GameObject heart0;
    [SerializeField] private GameObject heart1;
    [SerializeField] private GameObject heart2;
    [SerializeField] private GameObject heart3;
    [SerializeField] private GameObject heart4;

    private void Awake()
    {
        
        heart0 = gameObject.transform.GetChild(0).gameObject;
        heart1 = gameObject.transform.GetChild(1).gameObject;
        heart2 = gameObject.transform.GetChild(2).gameObject;
        heart3 = gameObject.transform.GetChild(3).gameObject;
        heart4 = gameObject.transform.GetChild(4).gameObject;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //m_GameManager = FindObjectOfType<GameManager>();
        if (m_GameManager.CurrentNumberOfHearts == 5)
        {
            heart0.SetActive(true);
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(true);
            heart4.SetActive(true);
        }
        else if (m_GameManager.CurrentNumberOfHearts == 4)
        {
            heart0.SetActive(true);
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(true);
            heart4.SetActive(false);
        }

        else if (m_GameManager.CurrentNumberOfHearts == 3)
        {
            heart0.SetActive(true);
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(false);
            heart4.SetActive(false);
        }
        else if(m_GameManager.CurrentNumberOfHearts == 2)
        {
            heart0.SetActive(true);
            heart1.SetActive(true);
            heart2.SetActive(false);
            heart3.SetActive(false);
            heart4.SetActive(false);
        }
        else if (m_GameManager.CurrentNumberOfHearts == 1)
        {
            heart0.SetActive(true);
            heart1.SetActive(false);
            heart2.SetActive(false);
            heart3.SetActive(false);
            heart4.SetActive(false);
        }
        else if (m_GameManager.CurrentNumberOfHearts <= 0)
        {
            heart0.SetActive(false);
            heart1.SetActive(false);
            heart2.SetActive(false);
            heart3.SetActive(false);
            heart4.SetActive(false);
        }
    }
}
