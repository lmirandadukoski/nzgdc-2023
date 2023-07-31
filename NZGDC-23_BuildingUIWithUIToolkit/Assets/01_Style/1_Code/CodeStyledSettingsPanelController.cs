using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CodeStyledSettingsPanelController : SettingsPanelControllerBase
{
    //The colour palette used in this example
    //For reasons, colour-channel values need to be normalised (i.e., value / 255).
    private static StyleColor s_BorderColour = new StyleColor(new Color(1.0f, 1.0f, 1.0f));
    private static StyleColor s_Colour = new StyleColor(new Color(0.85f, 0.85f, 0.86f));
    private static StyleColor s_DefaultBackgroundColour = new StyleColor(new Color(0.17f, 0.15f, 0.2f));
    private static StyleColor s_ActiveBackgroundColour = new StyleColor(new Color(0.31f, 0.19f, 0.53f));

    //An element's style can be modified in C# code by setting the values of the various properties in
    //VisualElement.style.
    protected override void ApplyButtonContainerStyle(VisualElement buttonContainer)
    {
        buttonContainer.style.flexGrow = 1;
        buttonContainer.style.maxWidth = new StyleLength(new Length(30.0f, LengthUnit.Percent));
        buttonContainer.style.minWidth = new StyleLength(new Length(30.0f, LengthUnit.Percent));
        buttonContainer.style.paddingLeft = new StyleLength(32.0f);
        buttonContainer.style.paddingRight = new StyleLength(32.0f);
        buttonContainer.style.alignItems = Align.Stretch;
        buttonContainer.style.justifyContent = Justify.Center;
    }

    protected override void ApplyDefaultButtonStyle(List<Button> menuButtons)
    {
        foreach (var button in menuButtons)
        {
            ApplyDefaultButtonStyle(button);
        }
    }

    protected override void SetHoveredButton(PointerEnterEvent evt, Button button)
    {
        Debug.Log($"Hovered over button {button.name}");
        if (m_HoveredButton == button)
        {
            return;
        }

        //Apply the visuals tp the newly-hovered button.
        m_HoveredButton = button;
        if (m_HoveredButton != null)
        {
            ApplyHoveredButtonStyle(m_HoveredButton);
        }
    }

    protected override void SetUnhoveredButton(PointerOutEvent evt, Button button)
    {
        if (m_HoveredButton != button)
        {
            return;
        }

        //If hovering over another button, reset the visuals.
        if (m_HoveredButton != null)
        {            
            Debug.Log($"Unhovered over button {button.name}");
            if(m_HoveredButton != m_ActiveButton)
            {
                ApplyDefaultButtonStyle(m_HoveredButton);
            }
        }

        m_HoveredButton = null;
    }

    protected override void SetActiveButton(PointerDownEvent evt, Button button)
    {
        //If clicking on another button, reset the visuals of the previously-clicked button.
        Debug.Log($"Clicked button {button.name}");
        if (m_ActiveButton != null)
        {
            ApplyDefaultButtonStyle(m_ActiveButton);
        }

        //Apply the visuals to the the newly-selected button.
        m_ActiveButton = button;
        if (m_ActiveButton != null)
        {
            ApplyActiveButtonStyle(button);
        }

        SetActiveSubpanel();
    }

    //Default button visuals
    private void ApplyDefaultButtonStyle(Button button)
    {
        button.pickingMode = PickingMode.Position;
        button.style.scale = new StyleScale(new Scale(Vector3.one));
        button.style.rotate = new StyleRotate(new Rotate(new Angle(0.0f, AngleUnit.Degree)));

        button.style.marginBottom = new StyleLength(new Length(16.0f, LengthUnit.Pixel));
        button.style.maxHeight = new StyleLength(60.0f);
        button.style.minHeight = new StyleLength(40.0f);

        StyleFloat borderWidth = new StyleFloat(3.0f);
        button.style.borderBottomWidth = borderWidth;
        button.style.borderTopWidth = borderWidth;
        button.style.borderLeftWidth = borderWidth;
        button.style.borderRightWidth = borderWidth;

        StyleLength borderRadius = new StyleLength(new Length(10.0f, LengthUnit.Pixel));
        button.style.borderTopLeftRadius = borderRadius;
        button.style.borderTopRightRadius = borderRadius;
        button.style.borderBottomLeftRadius = borderRadius;
        button.style.borderBottomRightRadius = borderRadius;

        button.style.backgroundColor = s_DefaultBackgroundColour;
        button.style.color = s_Colour;
        button.style.borderBottomColor = s_BorderColour;
        button.style.borderTopColor = s_BorderColour;
        button.style.borderLeftColor = s_BorderColour;
        button.style.borderRightColor = s_BorderColour;
    }

    //Hovered button visuals
    private void ApplyHoveredButtonStyle(Button button)
    {
        button.style.scale = new StyleScale(new Scale(Vector3.one * 1.25f));
        button.style.rotate = new StyleRotate(new Rotate(new Angle(340.0f, AngleUnit.Degree)));

        button.style.backgroundColor = s_DefaultBackgroundColour;
        button.style.color = s_Colour;
        button.style.borderBottomColor = s_BorderColour;
        button.style.borderTopColor = s_BorderColour;
        button.style.borderLeftColor = s_BorderColour;
        button.style.borderRightColor = s_BorderColour;
    }

    //Selected button visuals
    private void ApplyActiveButtonStyle(Button button)
    {
        button.pickingMode = PickingMode.Ignore;
        button.style.scale = new StyleScale(new Scale(Vector3.one));
        button.style.rotate = new StyleRotate(new Rotate(new Angle(0.0f, AngleUnit.Degree)));

        button.style.backgroundColor = s_ActiveBackgroundColour;
        button.style.color = s_Colour;
    }
}
