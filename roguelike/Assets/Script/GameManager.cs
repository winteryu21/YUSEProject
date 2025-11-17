using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    
    public static GameManager Instance { get; private set; } // Singleton instance
    
    private void Awake()
    {
        Init();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void Init()
    {
        //Debug.Log("Init");

        //Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        Application.targetFrameRate = 60;


        //Debug.Log("Application Initialization Complete.");
    }

    public void Shutdown()
    {
        Application.Quit();
    }
}