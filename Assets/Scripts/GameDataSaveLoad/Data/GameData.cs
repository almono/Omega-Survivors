using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public Achievement[] achievements; // saved achievements

    public GameData(Achievement[] achievementsList)
    {
        achievements = achievementsList; // get default achievements by reading achievements assigned to manager
    }
}
