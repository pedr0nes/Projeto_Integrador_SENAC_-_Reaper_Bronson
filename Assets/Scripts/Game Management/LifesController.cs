using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifesController : MonoBehaviour
{
    public GameManager m_GameManager;
    [SerializeField] private Text numberOfLifesText;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        //m_GameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        m_GameManager = FindObjectOfType<GameManager>();
        numberOfLifesText.text = m_GameManager.CurrentNumberOfLifes.ToString();
        Debug.Log("Vidas atraves do controlador de vidas :" + m_GameManager.CurrentNumberOfLifes);
    }
}
