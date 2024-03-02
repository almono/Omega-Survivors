using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private bool gameIsActive;
    public float gameTimer, waitToShowEndScreen = 1f;
    public PlayerCharacterSO selectedPlayerCharacter;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this);
    }

    void Start()
    {
        gameIsActive = true;
    }


    void Update()
    {
        if(gameIsActive && UIController.instance)
        {
            gameTimer += Time.deltaTime;
            UIController.instance.UpdateTimerText(gameTimer);
        }
    }

    public void EndLevel()
    {
        gameIsActive = false;
        StartCoroutine(EndLevelCo());
    }

    public void SetPlayerCharacter(PlayerCharacterSO playerCharacter)
    {
        selectedPlayerCharacter = playerCharacter;
    }

    public PlayerCharacterSO GetPlayerCharacter()
    {
        return selectedPlayerCharacter;
    }

    IEnumerator EndLevelCo()
    {
        yield return new WaitForSeconds(waitToShowEndScreen);

        float minutes = Mathf.FloorToInt(gameTimer / 60f);
        float seconds = Mathf.FloorToInt(gameTimer % 60);
        UIController.instance.endTimeText.text = "Time: " + minutes.ToString("00") + " mins " + seconds.ToString("00") + " secs";
        UIController.instance.levelEndScreen.SetActive(true);
    }
}
