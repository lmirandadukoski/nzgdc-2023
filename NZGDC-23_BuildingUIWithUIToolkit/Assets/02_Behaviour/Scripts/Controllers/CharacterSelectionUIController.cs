using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterSelectionUIController : MonoBehaviour
{
    [SerializeField] private UIDocument m_CharacterSelectionDocument;
    [SerializeField] private CharacterSelection m_CharacterSelection;

    private const string k_CharacterSelectionContentContainerClassName = "unity-scroll-view__content-container";

    protected List<Button> m_MenuButtons;
    protected Button m_HoveredButton, m_ActiveButton;
    protected VisualElement m_SubPanelContainer;

    private CharacterSlotVisualElement m_SelectedCharacterSlot;
    private CharacterDataVisualElement m_CharacterData;

    private void Start()
    {
        if (m_CharacterSelectionDocument == null || m_CharacterSelection == null)
        {
            return;
        }

        VisualElement verticalScrollContentContainer = m_CharacterSelectionDocument.rootVisualElement.Query<VisualElement>(className: k_CharacterSelectionContentContainerClassName);
        if (verticalScrollContentContainer == null)
        {
            return;
        }

        CharacterSlotVisualElement firstCharacterSlot = null;
        for (int i = 0; i < m_CharacterSelection.SelectableCharacters.Length; i++)
        {
            CharacterSlotVisualElement characterSlot = new CharacterSlotVisualElement();
            characterSlot.CharacterSelected += CharacterSelected;
            characterSlot.Initialise(m_CharacterSelection, m_CharacterSelection.SelectableCharacters[i]);
            verticalScrollContentContainer.Add(characterSlot);

            if(i == 0)
            {
                firstCharacterSlot = characterSlot;
            }
        }

        m_CharacterData = m_CharacterSelectionDocument.rootVisualElement.Query<CharacterDataVisualElement>();

        CharacterSelected(firstCharacterSlot);        
    }

    private void CharacterSelected(CharacterSlotVisualElement selectedSlot)
    {
        if(m_SelectedCharacterSlot != null)
        {
            m_SelectedCharacterSlot.Deselect();
        }

        int attackValue = 0;
        int healthValue = 0;
        m_SelectedCharacterSlot = selectedSlot;
        if(m_SelectedCharacterSlot != null)
        {
            m_SelectedCharacterSlot.Select();
            attackValue = m_SelectedCharacterSlot.Data.CharacterAttack;
            healthValue = m_SelectedCharacterSlot.Data.CharacterHealth;
        }

        if(m_CharacterData != null)
        {
            m_CharacterData.UpdateDataValues(attackValue, healthValue);
        }
    }
}
