using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class SettingsPanelControllerBase : MonoBehaviour
{
    [SerializeField] protected UIDocument m_SettingsPanelDocument;
    [Tooltip("The order of the documents in the collection must match the buttons that they correspond to.")]
    [SerializeField] protected VisualTreeAsset[] m_SettingsSubpanelDocuments;

    protected List<Button> m_MenuButtons;
    protected Button m_HoveredButton, m_ActiveButton;
    protected VisualElement m_SubPanelContainer;

    private void Start()
    {
        if (m_SettingsPanelDocument == null)
        {
            return;
        }

        //Grab the menu buttons container from the root Settings Panel VisualElement by using the menu-buttons-container Selector.
        VisualElement buttonContainer = m_SettingsPanelDocument.rootVisualElement.Query<VisualElement>(className: "menu-buttons-container");
        if (buttonContainer == null)
        {
            return;
        }
        ApplyButtonContainerStyle(buttonContainer);

        //Grab the subpanel container from the root Settings Panel VisualElement by using the content-container Selector.
        m_SubPanelContainer = m_SettingsPanelDocument.rootVisualElement.Query<VisualElement>(className: "content-container");
        Debug.Assert(m_SubPanelContainer != null, $"Was not able to find a {nameof(VisualElement)} with class name .content-container");

        //Grab all the Button elements from the menu button container.
        UQueryBuilder<Button> buttons = buttonContainer.Query<Button>();
        if (buttons == null)
        {
            return;
        }

        //Cast the UQueryBuilder to a List so that we can index match buttons to subpanels.
        m_MenuButtons = buttons.ToList();
        ApplyDefaultButtonStyle(m_MenuButtons);
        SubscribeToButtonCallbacks(m_MenuButtons);
        //Activate the first button and subpanel.
        SetActiveButton(null, m_MenuButtons[0]);
    }

    protected void SubscribeToButtonCallbacks(List<Button> menuButtons)
    {
        if (menuButtons == null)
        {
            return;
        }

        //Subscribe to the PointerEnter and PointerOut events to do hover visualisation, and subscribe to PointerDownEvent
        //to do active-button visualisation and swap subpanels.
        foreach (var button in menuButtons)
        {
            button.RegisterCallback<PointerEnterEvent>(evt => SetHoveredButton(evt, button), TrickleDown.TrickleDown);
            button.RegisterCallback<PointerOutEvent>(evt => SetUnhoveredButton(evt, button), TrickleDown.TrickleDown);
            button.RegisterCallback<PointerDownEvent>(evt => SetActiveButton(evt, button), TrickleDown.TrickleDown);
        }
    }

    protected virtual void ApplyButtonContainerStyle(VisualElement buttonContainer)
    {

    }

    protected virtual void ApplyDefaultButtonStyle(List<Button> menuButtons)
    {

    }

    protected abstract void SetHoveredButton(PointerEnterEvent evt, Button button);
    protected abstract void SetUnhoveredButton(PointerOutEvent evt, Button button);
    protected abstract void SetActiveButton(PointerDownEvent evt, Button button);

    protected void SetActiveSubpanel()
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

    protected int GetButtonIndex(Button button)
    {
        return m_MenuButtons.IndexOf(button);
    }
}
