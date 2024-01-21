using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private bool gameIsActive;
    public float gameTimer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        gameIsActive = true;
    }


    void Update()
    {
        if(gameIsActive)
        {
            gameTimer += Time.deltaTime;
            UIController.instance.UpdateTimerText(gameTimer);
        }
    }
}
