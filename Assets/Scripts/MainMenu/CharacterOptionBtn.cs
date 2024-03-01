using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterOptionBtn : MonoBehaviour
{
    public PlayerCharacterSO playerCharacter;
    public TMP_Text characterName;
    public Image characterImage;

    public void SetSelectedCharacter()
    {
        CharacterSelector.instance.selectedCharacter = playerCharacter;
        GameManager.instance.SetPlayerCharacter(playerCharacter);
    }
}
