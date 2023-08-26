using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ColourApplyingButton : Button
{

    //This is what makes this VisualElement accessible in UIBuilder.
    public new class UxmlFactory : UxmlFactory<ColourApplyingButton, UxmlTraits> { }

    public new class UxmlTraits : VisualElement.UxmlTraits
    {
        UxmlColorAttributeDescription m_ColourToApply =
            new UxmlColorAttributeDescription { name = "colour-to-apply" };

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var ate = ve as ColourApplyingButton;

            ate.colourToApply = m_ColourToApply.GetValueFromBag(bag, cc);
            ate.PreviewColour();
        }
    }

    // Must expose your element class to a { get; set; } property that has the same name 
    // as the name you set in your UXML attribute description with the camel case format
    public Color colourToApply { get; set; }

    public ColourApplyingButton()
    {
        text = "Apply Colour";
    }

    public void PreviewColour()
    {
        style.backgroundColor = new StyleColor(colourToApply);
    }
}
