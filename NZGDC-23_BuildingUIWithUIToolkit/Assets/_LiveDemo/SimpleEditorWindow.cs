using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Collections.Generic;
using System;

public class SimpleEditorWindow : EditorWindow
{
    [MenuItem("NZGDC23Demo/Simple Editor Window")]
    public static void ShowExample()
    {
        SimpleEditorWindow wnd = GetWindow<SimpleEditorWindow>();
        wnd.titleContent = new GUIContent("SimpleEditorWindow");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // Import UXML
        VisualTreeAsset visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/_LiveDemo/SimpleEditorWindow.uxml");
        VisualElement elementsFromUXML = visualTree.Instantiate();
        root.Add(elementsFromUXML);

        // A stylesheet can be added to a VisualElement.
        // The style will be applied to the VisualElement and all of its children.
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/_LiveDemo/SimpleEditorWindow.uss");

        //Loads the style sheet to the editor window, but this can be done in UI Builder
        root.styleSheets.Add(styleSheet);

        //SubscribeToButtonClickEvents(root);
    }

    private void SubscribeToButtonClickEvents(VisualElement root)
    {
        //Grabs all of the ColourApplyingButtons and puts them in a list
        List<ColourApplyingButton> buttons = root.Query<ColourApplyingButton>().ToList();
        //Grabs the first VisualElement of type ColourChangingContainer
        ColourChangingContainer container = root.Q<ColourChangingContainer>();

        //Hook up the button click event to the apply colour function of the colour changing container
        //element
        foreach (var button in buttons)
        {
            button.RegisterCallback<ClickEvent>((evt) => container.ApplyColour(button.colourToApply));
        }
    }
}