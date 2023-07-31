using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterDataVisualElement : VisualElement
{
    //This is what makes this VisualElement accessible in UIBuilder.
    public new class UxmlFactory : UxmlFactory<CharacterDataVisualElement> { }

    private readonly Label m_AttackText, m_HealthText;
    private const string k_AttackTextFormat = "Attack: {0}";
    private const string k_HealthTextFormat = "Health: {0}";

    public CharacterDataVisualElement()
    {
        //The element that will display the attack value of the character.
        m_AttackText = new Label(string.Format(k_AttackTextFormat, 999));
        Add(m_AttackText);

        //The element that will display the health value of the character.
        m_HealthText = new Label(string.Format(k_HealthTextFormat, 999));
        Add(m_HealthText);
    }

    public void UpdateDataValues(int attackValue, int healthValue)
    {
        //Sets the text of the elements to be the new values.
        m_AttackText.text = string.Format(k_AttackTextFormat, attackValue);
        m_HealthText.text = string.Format(k_HealthTextFormat, healthValue);
    }
}
