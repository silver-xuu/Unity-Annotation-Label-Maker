using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(LabelScript))]

public class LabelScriptEditor : Editor
{
    private LabelScript mytarget;
    SerializedProperty list;
    SerializedObject GetTarget;
    bool showWindow = false;
    bool startTrackingMouse = false;


    private void OnEnable()
    {
        mytarget = (LabelScript)target;
        GetTarget = new SerializedObject(mytarget);
        list = GetTarget.FindProperty("labels");
    }

    //GUI displayed on scene
    public void OnSceneGUI()
    {
        Handles.BeginGUI();

        //add new label button, click it will pops up another window
        if (GUILayout.Button("Add new Label", GUILayout.Height(30), GUILayout.Width(200)))
        {
            showWindow = true;
        }
        //delete last added label
        if (GUILayout.Button("Delete Last Label", GUILayout.Height(30), GUILayout.Width(200)))
        {
            mytarget.DeleteLastLabel();
        }
        //change the width of indicator line
        EditorGUI.BeginChangeCheck();
        mytarget.lineWidthMultiplier = EditorGUILayout.Slider("Indicator Line width", mytarget.lineWidthMultiplier, 0.1f, 5f, GUILayout.Width(200));
        mytarget.dotSize =EditorGUILayout.Slider("Dot Size multiplier", mytarget.dotSize, 0.001f, 50f, GUILayout.Width(200));
        mytarget.textWindowSize= EditorGUILayout.Slider("Text Window Size multiplier", mytarget.textWindowSize, 0.001f, 50f, GUILayout.Width(200));
        mytarget.textColor = EditorGUILayout.ColorField("Text Color", mytarget.textColor, GUILayout.Width(200));
        if (GUI.changed)
        {
            mytarget.ChangeLabelPrefab();
        }
        EditorGUI.EndChangeCheck();
        //display popup window for adding new label
        if (showWindow)
        {
            Rect windowSize = new Rect(0, 100, 300, 200);
            Rect Window= GUI.Window(0, windowSize , LabelNameWindow,"Enter Label Name");
        }
        
       
        //if user wants to display the label text
        EditorGUI.BeginChangeCheck();
        mytarget.showAllLabel = EditorGUILayout.Toggle("Show All Label Text", mytarget.showAllLabel);
        if (GUI.changed)
        {
            mytarget.showAllLabelText(mytarget.showAllLabel);
        }
        EditorGUI.EndChangeCheck();

        //after entering the name of new label, system will track mouse click to place the label
        if (startTrackingMouse)
        {
            EditorGUILayout.HelpBox("Now, click on where you want to place the label on the object", MessageType.Info);
        }
        if (startTrackingMouse && Event.current.type == EventType.MouseDown)
        {
            Vector2 guiPosition = Event.current.mousePosition;
            Ray ray = HandleUtility.GUIPointToWorldRay(guiPosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                mytarget.AddLabel(hit.point, hit.normal);
            }

            startTrackingMouse = false;
        }

        Handles.EndGUI();
    }
    

    
    //display some info about the label on the inspector
    public override void OnInspectorGUI()
    {
            EditorGUILayout.HelpBox("Please look at the scene window to create labels.", MessageType.Info);
        if (GUILayout.Button("Reload All Labels in Children", GUILayout.Height(30), GUILayout.Width(200)))
        {
            mytarget.ReloadAllLabelInChildren();
        }
    }
    
    //contents of the label name window
    public void LabelNameWindow(int windowID)
    {
        EditorGUILayout.LabelField("Please enter the label text");
        mytarget.index = EditorGUILayout.IntField("Index number", mytarget.index);
        EditorGUILayout.HelpBox("If input index number is 0,\nit will be set to the number in the list", MessageType.Info);
        mytarget.upperText = EditorGUILayout.TextField("Upper Label Text", mytarget.upperText);
        mytarget.bottomText = EditorGUILayout.TextField("Bottom Label Text", mytarget.bottomText);
        if (mytarget.bottomText.Length == 0 || mytarget.bottomText=="")
        {
            EditorGUILayout.HelpBox("Bottom label text cannot be empty", MessageType.Warning);
        }
        if (GUILayout.Button("Ok",GUILayout.Height(30), GUILayout.Width(200)))
        {
            if (mytarget.bottomText.Length>0)
            {
                showWindow = false;
                
                startTrackingMouse = true;
            }
            
            
        }

    }
   
}
