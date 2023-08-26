using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ColourChangingContainer : VisualElement
{
    //This is what makes this VisualElement accessible in UIBuilder.
    public new class UxmlFactory : UxmlFactory<ColourChangingContainer> { }

    private const string kColourDescriptionText = "The colour is {0}";
    private readonly Label m_ColourDescription;

    public ColourChangingContainer()
    {
        m_ColourDescription = new Label();
        this.Add(m_ColourDescription);
    }

    public void ApplyColour(Color colour)
    {
        string newColourDescriptionText = string.Format(kColourDescriptionText, ColorUtility.ToHtmlStringRGB(colour));
        m_ColourDescription.text = newColourDescriptionText;
        style.backgroundColor = new StyleColor(colour);
    }
}
