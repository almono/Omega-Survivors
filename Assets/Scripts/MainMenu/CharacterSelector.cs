using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelector : MonoBehaviour
{
    public static CharacterSelector instance;

    public Transform characterSelectorDisplay;
    public List<PlayerCharacterSO> availableCharacters;
    public PlayerCharacterSO selectedCharacter;
    public GameObject characterOption;
    public TMP_Text[] statsInfo;
    public Button selectButton;

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

    private void Start()
    {
        // ensure default character is set to first option
        /*selectedCharacter = availableCharacters[0];
        GameManager.instance.SetPlayerCharacter(selectedCharacter);
        UpdateStatsInfo(selectedCharacter);*/

        selectButton.interactable = false;

        foreach (PlayerCharacterSO character in availableCharacters)
        {
            GameObject newCharacterOption = Instantiate(characterOption, transform.position, Quaternion.identity, characterSelectorDisplay);
            newCharacterOption.SetActive(true);

            CharacterOptionBtn characterConfig = newCharacterOption.GetComponent<CharacterOptionBtn>();
            characterConfig.playerCharacter = character;
            characterConfig.characterName.text = character.GetName();
            characterConfig.characterImage = character.GetPlayerIcon();

            Image characterIcon = GetComponentInChildren<Image>();
            characterIcon = character.GetPlayerIcon();
        }
    }

    public void UpdateStatsInfo(PlayerCharacterSO selectedCharacter)
    {
        statsInfo[0].text = selectedCharacter.moveSpeed.ToString();
        statsInfo[1].text = selectedCharacter.health.ToString();
        statsInfo[2].text = selectedCharacter.pickupRange.ToString();
        statsInfo[3].text = selectedCharacter.critChance.ToString();
        statsInfo[4].text = selectedCharacter.critMultiplier.ToString();
        statsInfo[5].text = selectedCharacter.maxWeapons.ToString();
        selectButton.interactable = true;
    }
}
