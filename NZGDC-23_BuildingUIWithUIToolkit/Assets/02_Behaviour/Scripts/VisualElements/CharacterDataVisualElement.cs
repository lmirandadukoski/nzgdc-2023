using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterDataVisualElement : VisualElement
{
    public new class UxmlFactory : UxmlFactory<CharacterDataVisualElement> { }

    private readonly Label m_AttackText, m_HealthText;
    private const string k_AttackTextFormat = "Attack: {0}";
    private const string k_HealthTextFormat = "Health: {0}";

    public CharacterDataVisualElement()
    {
        m_AttackText = new Label(string.Format(k_AttackTextFormat, 999));
        Add(m_AttackText);

        m_HealthText = new Label(string.Format(k_HealthTextFormat, 999));
        Add(m_HealthText);
    }

    public void UpdateDataValues(int attackValue, int healthValue)
    {
        m_AttackText.text = string.Format(k_AttackTextFormat, attackValue);
        m_HealthText.text = string.Format(k_HealthTextFormat, healthValue);
    }
}
