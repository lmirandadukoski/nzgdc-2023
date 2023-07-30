using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    public CharacterData[] SelectableCharacters { get => m_SelectableCharacters; }

    [SerializeField] private CharacterData[] m_SelectableCharacters;
    [SerializeField] private Transform m_CharacterSpawnPoint;

    private GameObject m_SelectedCharacter;

    public void SwapSelectedCharacterPrefab(GameObject characterPrefab)
    {
        if(m_SelectedCharacter != null)
        {
            Destroy(m_SelectedCharacter);
        }

        m_SelectedCharacter = Instantiate(characterPrefab, m_CharacterSpawnPoint);
    }
}
