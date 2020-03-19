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
        mytarget.indicatorLineWidth = EditorGUILayout.Slider("Indicator Line width", mytarget.indicatorLineWidth, 0.1f, 5f, GUILayout.Width(200));
        mytarget.dotMul =EditorGUILayout.Slider("Dot Size multiplier", mytarget.dotMul, 0.1f, 50f, GUILayout.Width(200));
        mytarget.textWindowMul = EditorGUILayout.Slider("Text Window Size multiplier", mytarget.textWindowMul, 0.1f, 50f, GUILayout.Width(200));
        if (GUI.changed)
        {
            mytarget.ChangeLabelPrefab();
        }
        EditorGUI.EndChangeCheck();
        //display popup window for adding new label
        if (showWindow)
        {
            Rect windowSize = new Rect(0, 100, 300, 100);
            Rect Window= GUI.Window(0, windowSize , LabelNameWindow,"Enter Label Name");
        }
        
       
        //if user wants to display the label text
        EditorGUI.BeginChangeCheck();
        mytarget.showAllText = EditorGUILayout.Toggle("Show All Label Text", mytarget.showAllText);
        if (GUI.changed)
        {
            mytarget.showAllLabelText(mytarget.showAllText);
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
    
    private void OnEnable()
    {
         mytarget = (LabelScript)target;
        GetTarget = new SerializedObject(mytarget);
        list= GetTarget.FindProperty("labelTexts");
    }
    
    //display some info about the label on the inspector
    public override void OnInspectorGUI()
    {

        GetTarget.Update();

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        int listsize = list.arraySize;
        if (listsize > 0) {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Label Index");
            EditorGUILayout.LabelField("Label Text");
            EditorGUILayout.EndHorizontal();
            for (int i = 0; i < listsize; i++)
            {
                EditorGUI.BeginChangeCheck();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField((i+1).ToString());
                EditorGUILayout.LabelField(list.GetArrayElementAtIndex(i).stringValue);
                EditorGUILayout.EndHorizontal();
                
                mytarget.dotPosition[i]=EditorGUILayout.Vector3Field("Dot Position", mytarget.dotPosition[i]);
                mytarget.textWindowPosition[i]=EditorGUILayout.Vector3Field("Text Position", mytarget.textWindowPosition[i]);
                if (GUI.changed) 
                {
                    mytarget.ChangePosition(i);
                }

                EditorGUI.EndChangeCheck();
            }
            EditorGUILayout.Space();
            EditorGUILayout.Space();
        }
        else
        {
            EditorGUILayout.HelpBox("Label Information will be displayed here", MessageType.Info);
        }
        
        
        
    }
    //contents of the label name window
    public void LabelNameWindow(int windowID)
    {
        EditorGUILayout.LabelField("Please enter the label name");
        mytarget.labelText = EditorGUILayout.TextField("Label Name", mytarget.labelText);
        if (mytarget.labelText.Length == 0 || mytarget.labelText=="")
        {
            EditorGUILayout.HelpBox("Label Name cannot be empty", MessageType.Warning);
        }
        if (GUILayout.Button("Ok",GUILayout.Height(30), GUILayout.Width(200)))
        {
            if (mytarget.labelText.Length>0)
            {
                showWindow = false;
                //mytarget.AddLabel();
                startTrackingMouse = true;
            }
            
            
        }

    }
   
}
