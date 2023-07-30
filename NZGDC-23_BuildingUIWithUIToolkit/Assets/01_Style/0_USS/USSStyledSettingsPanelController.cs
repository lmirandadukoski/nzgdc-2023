using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class USSStyledSettingsPanelController : SettingsPanelControllerBase
{
    protected override void SetHoveredButton(PointerEnterEvent evt, Button button)
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

    protected override void SetUnhoveredButton(PointerOutEvent evt, Button button)
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

    protected override void SetActiveButton(PointerDownEvent evt, Button button)
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
}
