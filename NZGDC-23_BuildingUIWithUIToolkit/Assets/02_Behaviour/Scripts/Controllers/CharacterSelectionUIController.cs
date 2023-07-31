using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

//The class that controls the UI logic of character selection.
public class CharacterSelectionUIController : MonoBehaviour
{
    [SerializeField] private UIDocument m_CharacterSelectionDocument;
    [SerializeField] private CharacterSelection m_CharacterSelection;

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

        //Find the container element for the vertical scroll element.
        VisualElement verticalScrollContentContainer = m_CharacterSelectionDocument.rootVisualElement.Query<VisualElement>(className: "unity-scroll-view__content-container");
        if (verticalScrollContentContainer == null)
        {
            return;
        }

        //Initialise all of the character slots and grab a reference for the first slot.
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

        //Find the element that displays the properties of the selected character.
        m_CharacterData = m_CharacterSelectionDocument.rootVisualElement.Query<CharacterDataVisualElement>();

        //Select the first character slot
        CharacterSelected(firstCharacterSlot);        
    }

    private void CharacterSelected(CharacterSlotVisualElement selectedSlot)
    {
        //Deselect the previously-selected character slot.
        if(m_SelectedCharacterSlot != null)
        {
            m_SelectedCharacterSlot.Deselect();
        }

        //Get the newly-selected character's attack and health values.
        int attackValue = 0;
        int healthValue = 0;
        m_SelectedCharacterSlot = selectedSlot;
        if(m_SelectedCharacterSlot != null)
        {
            //Select the new character slot.
            m_SelectedCharacterSlot.Select();
            attackValue = m_SelectedCharacterSlot.Data.CharacterAttack;
            healthValue = m_SelectedCharacterSlot.Data.CharacterHealth;
        }

        if(m_CharacterData != null)
        {
            //Update the character data element with the new attack and health values.
            m_CharacterData.UpdateDataValues(attackValue, healthValue);
        }
    }
}
