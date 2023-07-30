using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterSlotVisualElement : VisualElement
{
    public new class UxmlFactory : UxmlFactory<CharacterSlotVisualElement> { }

    public CharacterData Data { get => m_Data; }

    private readonly VisualElement m_ContentContainer, m_CharacterImage;
    private readonly Label m_CharacterName;

    private CharacterSelection m_CharacterSelection;
    private CharacterData m_Data;

    public delegate void CharacterSelectedHandler(CharacterSlotVisualElement selectedSlot);
    public event CharacterSelectedHandler CharacterSelected;

    public CharacterSlotVisualElement()
    {
        m_ContentContainer = new VisualElement();
        m_ContentContainer.AddToClassList("character-slot__content-container");
        m_ContentContainer.AddToClassList("character-slot__content-container__default-background");
        Add(m_ContentContainer);

        m_CharacterImage = new VisualElement();
        m_CharacterImage.AddToClassList("character-image");
        m_ContentContainer.Add(m_CharacterImage);

        m_CharacterName = new Label("[CHARACTER NAME]");
        m_ContentContainer.Add(m_CharacterName);

        RegisterCallback<PointerEnterEvent>(ApplyHoverStyle);
        RegisterCallback<PointerOutEvent>(RemoveHoverStyle);
        RegisterCallback<PointerDownEvent>(SelectCharacter);
    }

    public void Initialise(CharacterSelection characterSelection, CharacterData data)
    {
        m_CharacterSelection = characterSelection;
        m_Data = data;

        m_CharacterImage.style.backgroundImage = new StyleBackground(m_Data.CharacterIcon);
        m_CharacterName.text = m_Data.CharacterName;
    }

    public void Select()
    {
        pickingMode = PickingMode.Ignore;
        m_ContentContainer.AddToClassList("character-slot__content-container__hovered-background");        

        m_CharacterSelection.SwapSelectedCharacterPrefab(m_Data.CharacterPrefab);
    }

    public void Deselect()
    {
        pickingMode = PickingMode.Position;
        m_ContentContainer.RemoveFromClassList("character-slot__content-container__hovered-background");
        m_ContentContainer.AddToClassList("character-slot__content-container__default-background");
    }

    private void ApplyHoverStyle(PointerEnterEvent evt)
    {
        if(pickingMode == PickingMode.Ignore)
        {
            return;
        }

        m_ContentContainer.RemoveFromClassList("character-slot__content-container__default-background");
        m_ContentContainer.AddToClassList("character-slot__content-container__hovered-background");
    }

    private void RemoveHoverStyle(PointerOutEvent evt)
    {
        if (pickingMode == PickingMode.Ignore)
        {
            return;
        }

        m_ContentContainer.RemoveFromClassList("character-slot__content-container__hovered-background");
        m_ContentContainer.AddToClassList("character-slot__content-container__default-background");
    }

    private void SelectCharacter(PointerDownEvent evt)
    {
        CharacterSelected?.Invoke(this);
    }
}
