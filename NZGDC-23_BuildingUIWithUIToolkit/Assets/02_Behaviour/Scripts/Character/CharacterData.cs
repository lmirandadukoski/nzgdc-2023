using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "NZGDC23Demo/Character Data", fileName = "[NAME]_CharacterData")]
public class CharacterData : ScriptableObject
{
    //Some basic character properties
    public Sprite CharacterIcon { get => m_CharacterIcon; }
    public GameObject CharacterPrefab { get => m_Prefab; }
    public string CharacterName { get => m_CharacterName; }
    public int CharacterAttack { get => m_CharacterAttack; }
    public int CharacterHealth { get => m_CharacterHealth; }

    [SerializeField] private Sprite m_CharacterIcon;
    [SerializeField] private GameObject m_Prefab;
    [SerializeField] private string m_CharacterName;
    [SerializeField] private int m_CharacterAttack;
    [SerializeField] private int m_CharacterHealth;
}
