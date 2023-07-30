using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SettingsPanelController : MonoBehaviour
{
    [SerializeField] private UIDocument m_SettingsPanelDocument;
    [Tooltip("The order of the documents in the collection must match the buttons that they correspond to.")]
    [SerializeField] private VisualTreeAsset[] m_SettingsSubpanelDocuments;

    private List<Button> m_MenuButtons;
    private Button m_HoveredButton, m_ActiveButton;
    private VisualElement m_SubPanelContainer;

    private void Start()
    {
        if (m_SettingsPanelDocument == null)
        {
            return;
        }

        //Grab the menu buttons container from the root Settings Panel VisualElement by using the menu-buttons-container Selector.
        VisualElement buttonContainer = m_SettingsPanelDocument.rootVisualElement.Query<VisualElement>(className: "menu-buttons-container");
        if(buttonContainer == null)
        {
            return;
        }

        //Grab the subpanel container from the root Settings Panel VisualElement by using the content-container Selector.
        m_SubPanelContainer = m_SettingsPanelDocument.rootVisualElement.Query<VisualElement>(className: "content-container");
        Debug.Assert(m_SubPanelContainer != null, $"Was not able to find a {nameof(VisualElement)} with class name .content-container");

        //Grab all the Button elements from the menu button container.
        UQueryBuilder<Button> buttons = buttonContainer.Query<Button>();
        if(buttons == null)
        {
            return;
        }

        //Cast the UQueryBuilder to a List so that we can index match buttons to subpanels.
        m_MenuButtons = buttons.ToList();
        SubscribeToButtonCallbacks();
        //Activate the first button and subpanel.
        SetActiveButton(null, m_MenuButtons[0]);
    }

    private void SubscribeToButtonCallbacks()
    {
        if(m_MenuButtons == null)
        {
            return;
        }

        //Subscribe to the PointerEnter and PointerOut events to do hover visualisation, and subscribe to PointerDownEvent
        //to do active-button visualisation and swap subpanels.
        foreach (var button in m_MenuButtons)
        {
            button.RegisterCallback<PointerEnterEvent>(evt => SetHoveredButton(evt, button), TrickleDown.TrickleDown);
            button.RegisterCallback<PointerOutEvent>(evt => SetUnhoveredButton(evt, button), TrickleDown.TrickleDown);
            button.RegisterCallback<PointerDownEvent>(evt => SetActiveButton(evt, button), TrickleDown.TrickleDown);
        }
    }

    private void SetHoveredButton(PointerEnterEvent evt, Button button)
    {
        Debug.Log($"Hovered over button {button.name}");
        if(m_HoveredButton == button)
        {
            return;
        }

        //Add the new hovered button to the hovered-button class list to set the visuals.
        m_HoveredButton = button;
        if(m_HoveredButton != null)
        {
            m_HoveredButton.AddToClassList("hovered-menu-button");
        }
    }

    private void SetUnhoveredButton(PointerOutEvent evt, Button button)
    {        
        if (m_HoveredButton != button)
        {
            return;
        }

        //If hovering over another button, remove the previously-hovered buttons from the hovered-button
        //class list to reset the visuals.
        if (m_HoveredButton != null)
        {
            Debug.Log($"Unhovered over button {button.name}");
            m_HoveredButton.RemoveFromClassList("hovered-menu-button");
        }

        m_HoveredButton = null;
    }

    private void SetActiveButton(PointerDownEvent evt, Button button)
    {
        //If clicking on another button, remove the previously-clicked buttons from the selected-button
        //class list to reset the visuals.
        Debug.Log($"Clicked button {button.name}");
        if (m_ActiveButton != null)
        {
            m_ActiveButton.RemoveFromClassList("selected-menu-button");
        }

        //Add the new selected button to the selected-button class list to set the visuals.
        m_ActiveButton = button;
        if(m_ActiveButton != null)
        {
            button.AddToClassList("selected-menu-button");
        }

        SetActiveSubpanel();
    }



    private void SetActiveSubpanel()
    {
        //Get the index of the new active button.
        int buttonIndex = GetButtonIndex(m_ActiveButton);
        if (m_SubPanelContainer == null || buttonIndex == -1)
        {
            return;
        }

        //Remove the old subpanel from the subpanel container.
        m_SubPanelContainer.Clear();
        //Instantiate the new subpanel and add it to the container.
        var subPanel = m_SettingsSubpanelDocuments[buttonIndex].Instantiate();
        m_SubPanelContainer.Add(subPanel);
        subPanel.StretchToParentSize();
    }

    private int GetButtonIndex(Button button)
    {
        return m_MenuButtons.IndexOf(button);
    }
}
