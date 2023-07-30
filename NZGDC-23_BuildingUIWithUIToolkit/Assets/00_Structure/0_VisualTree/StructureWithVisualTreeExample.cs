using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

public class StructureWithVisualTreeExample : EditorWindow
{
    [MenuItem("NZGDC23Demo/00_Structure/Visual Tree Example")]
    public static void ShowWindow()
    {
        StructureWithVisualTreeExample window = GetWindow<StructureWithVisualTreeExample>();
        window.titleContent = new GUIContent("Structure With VisualTree Example");
        window.Show();
    }

    //CreateGUI is the function where the implementation of the 
    //window's VisualElement Tree needs to occur.
    public void CreateGUI()
    {
        //Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        //Import the UXML asset and instantiate it
        VisualTreeAsset visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/00_Structure/0_VisualTree/StructureWithVisualTreeExample.uxml");
        VisualElement uxmlRoot = visualTree.Instantiate();
        //Add the root of the visualTree to the window's root VisualElement
        root.Add(uxmlRoot);

        //A stylesheet can be added to a VisualElement.
        //The style will be applied to the VisualElement and all of its children
        //according to the Selectors associated with them.
        StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/00_Structure/0_VisualTree/StructureWithVisualTreeExample.uss");
        root.styleSheets.Add(styleSheet);
    }
}