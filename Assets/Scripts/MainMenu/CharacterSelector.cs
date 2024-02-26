using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    public static CharacterSelector instance;

    public Transform characterSelectorDisplay;
    public List<PlayerCharacterSO> availableCharacters;
    public PlayerCharacterSO selectedCharacter;
    public GameObject characterOption;

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
        selectedCharacter = availableCharacters[0];

        foreach(PlayerCharacterSO character in availableCharacters)
        {
            GameObject newCharacterOption = Instantiate(characterOption, transform.position, Quaternion.identity, characterSelectorDisplay);

            CharacterOptionBtn characterConfig = newCharacterOption.GetComponent<CharacterOptionBtn>();
            characterConfig.playerCharacter = character;
            characterConfig.characterName.text = character.GetName();
            characterConfig.characterImage = character.GetPlayerIcon();
        }
    }
}
