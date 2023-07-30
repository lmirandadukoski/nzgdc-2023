using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

public class StructureWithCodeExample : EditorWindow
{
    [MenuItem("NZGDC23Demo/00_Structure/Code Example")]
    public static void ShowWindow()
    {
        StructureWithCodeExample window = GetWindow<StructureWithCodeExample>();
        window.titleContent = new GUIContent("Structure With Code Example");
        window.Show();
    }

    //CreateGUI is the function where the implementation of the 
    //window's VisualElement Tree needs to occur.
    public void CreateGUI()
    {
        //Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        //Creating a container with labels
        VisualElement labels = CreateLabelsContainer();
        labels.style.paddingBottom = new StyleLength(16);
        root.Add(labels);

        //Creating a container with nested VisualElements
        VisualElement nestedElements = CreateNestedElementsContainer();
        root.Add(nestedElements);

        //A stylesheet can be added to a VisualElement.
        //The style will be applied to the VisualElement and all of its children
        //according to the Selectors associated with them.
        StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/00_Structure/1_Code/StructureWithVisualCodeExample.uss");
        root.styleSheets.Add(styleSheet);
    }

    private VisualElement CreateLabelsContainer()
    {
        //Creating a VisualElement to serve as the root
        //of the labels.
        VisualElement container = new VisualElement();

        //Creating the title label and setting its name
        //so that the style sheet can match it.
        Label title = new Label("A bunch of labels")
        {
            name = "title"
        };
        container.Add(title);

        //Creating 3 labels, and setting their names to
        //the corresponding style I want applied to them.
        for (int i = 0; i < 3; i++)
        {
            Label helloWorldLabel = new Label("Hello world")
            {
                name = string.Format("label-{0}", i)
            };
            container.Add(helloWorldLabel);
        }

        return container;
    }

    private VisualElement CreateNestedElementsContainer()
    {
        //Creating a VisualElement to serve as the root
        //of the nested elements.
        VisualElement container = new VisualElement();

        //Creating the title label and setting its name
        //so that the style sheet can match it.
        Label title = new Label("Nested VisualElements")
        {
            name = "title"
        };
        title.style.paddingBottom = new StyleLength(16);
        container.Add(title);

        //Creating a VisualElement to serve as the root
        //of the horizontal button group.
        //Adding it to the horizontal-button-group class
        //so that the appropriate style is applied.
        VisualElement buttonContainer = new VisualElement();
        buttonContainer.AddToClassList("horizontal-button-group");
        buttonContainer.style.paddingBottom = new StyleLength(16);
        container.Add(buttonContainer);

        //Creating 3 buttons
        for (int i = 0; i < 3; i++)
        {
            buttonContainer.Add(CreateButton(i));
        }

        //Creating a VisualElement to serve as the root
        //of the vertical radio button group.
        //Adding it to the vertical-radiobutton-group class
        //so that the appropriate style is applied.
        VisualElement radioButtonContainer = new VisualElement();
        radioButtonContainer.AddToClassList("vertical-radiobutton-group");
        container.Add(radioButtonContainer);

        //Creating 3 radio buttons
        for (int i = 0; i < 3; i++)
        {
            radioButtonContainer.Add(CreateRadioButton(i));
        }

        return container;
    }

    private VisualElement CreateButton(int buttonIndex)
    {
        //Setting the button's display text.
        Button button = new Button()
        {
            text = string.Format("Button {0}", buttonIndex + 1)
        };

        return button;
    }

    private VisualElement CreateRadioButton(int radioButtonIndex)
    {
        //Contructing a new radio button and Setting its display text.
        return new RadioButton(string.Format("Radio Button {0}", radioButtonIndex + 1));
    }
}
