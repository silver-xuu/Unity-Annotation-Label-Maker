using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(LabelTextManager)), CanEditMultipleObjects]
public class LabelTextManagerEditor : Editor
{
    private LabelTextManager mytarget;
    private SerializedProperty upperTextmeshProperty, bottomTextmeshProperty, indexTextmeshProperty,lineProperty, dotGroupProperty, textGroupProperty;

    private void OnEnable()
    {
        mytarget = (LabelTextManager)target;
        upperTextmeshProperty = serializedObject.FindProperty("upperTextmesh");
        bottomTextmeshProperty = serializedObject.FindProperty("bottomTextmesh");
        indexTextmeshProperty = serializedObject.FindProperty("indexTextmesh");
        lineProperty = serializedObject.FindProperty("line");
        dotGroupProperty=serializedObject.FindProperty("dotGroup");
        textGroupProperty = serializedObject.FindProperty("textGroup");
    }
    //GUI displayed in insepector
    public override void OnInspectorGUI()
    {
        
        #region Input_Number_Field
        EditorGUI.BeginChangeCheck();
        mytarget.thisLabel.index = EditorGUILayout.IntField("Index", mytarget.thisLabel.index);
        if (GUI.changed)
        {
            mytarget.SetIndexNumber(mytarget.thisLabel.index);
        }
        EditorGUI.EndChangeCheck();
        #endregion
        #region Label_Text_Field
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Upper Label Text");
        EditorGUILayout.LabelField("Bottom Label Text");

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();

        mytarget.thisLabel.upperText = EditorGUILayout.TextField(mytarget.thisLabel.upperText);
        mytarget.thisLabel.bottomText = EditorGUILayout.TextField(mytarget.thisLabel.bottomText);
        if (GUI.changed)
            mytarget.SetText(mytarget.thisLabel.upperText, mytarget.thisLabel.bottomText);

        EditorGUILayout.EndHorizontal();
        EditorGUI.EndChangeCheck();
        #endregion
        #region Label_Position_Field
        EditorGUI.BeginChangeCheck();
        mytarget.thisLabel.dotPosition = EditorGUILayout.Vector3Field("Label Dot Position", mytarget.thisLabel.dotPosition);
        mytarget.thisLabel.textWindowPosition = EditorGUILayout.Vector3Field("Text Window Position", mytarget.thisLabel.textWindowPosition);
        if (GUI.changed)
        {
            mytarget.SetDotPosition(mytarget.thisLabel.dotPosition);
            mytarget.SetTextWindowPosition(mytarget.thisLabel.textWindowPosition);
        }
        EditorGUI.EndChangeCheck();
        #endregion
        #region Sizes_Field
        EditorGUI.BeginChangeCheck();

        mytarget.thisLabel.dotSize = EditorGUILayout.Slider("Label Dot Size",mytarget.thisLabel.dotSize, 0.001f, 50f);
        mytarget.thisLabel.textWindowSize = EditorGUILayout.Slider("Text Window Size",mytarget.thisLabel.textWindowSize, 0.001f, 50f);
        mytarget.thisLabel.lineWidthMultiplier = EditorGUILayout.Slider("Line Width",mytarget.thisLabel.lineWidthMultiplier, 0.1f, 50f);
        if (GUI.changed)
        {
            mytarget.SetDotSize(mytarget.thisLabel.dotSize);
            mytarget.SetTextWindowSize(mytarget.thisLabel.textWindowSize);
            mytarget.SetLineWidth(mytarget.thisLabel.lineWidthMultiplier);
        }
        EditorGUI.EndChangeCheck();
        #endregion
        #region Color_Field
        EditorGUI.BeginChangeCheck();
        mytarget.textColor = EditorGUILayout.ColorField("Text Color", mytarget.textColor, GUILayout.Width(200));
        if (GUI.changed)
        {
            mytarget.SetTextColor(mytarget.textColor);
        }
        EditorGUI.EndChangeCheck();
        #endregion
        #region Display_Label_Toggle
        mytarget.thisLabel.isLabelDisplayed = EditorGUILayout.Toggle("Display Label", mytarget.thisLabel.isLabelDisplayed);
        if (GUI.changed)
            mytarget.showLabel(mytarget.thisLabel.isLabelDisplayed);
        #endregion
        #region Serialized_Field
        //In case the object reference is missing, user will need assign scene objects again
        if(upperTextmeshProperty.objectReferenceValue==null)
        upperTextmeshProperty.objectReferenceValue = (TMPro.TextMeshPro)EditorGUILayout.ObjectField("Upper Text Mesh", upperTextmeshProperty.objectReferenceValue, typeof(TMPro.TextMeshPro),true);
        if (bottomTextmeshProperty.objectReferenceValue == null)
            bottomTextmeshProperty.objectReferenceValue = (TMPro.TextMeshPro)EditorGUILayout.ObjectField("Bottom Text Mesh", bottomTextmeshProperty.objectReferenceValue, typeof(TMPro.TextMeshPro), true);
        if(indexTextmeshProperty.objectReferenceValue == null)
            indexTextmeshProperty.objectReferenceValue = (TMPro.TextMeshPro)EditorGUILayout.ObjectField("Index Text Mesh", indexTextmeshProperty.objectReferenceValue, typeof(TMPro.TextMeshPro), true);
        if (lineProperty.objectReferenceValue == null)
            lineProperty.objectReferenceValue = (LineRenderer)EditorGUILayout.ObjectField("Indication Line Renderer", lineProperty.objectReferenceValue, typeof(LineRenderer), true);
        if(dotGroupProperty.objectReferenceValue == null)
            dotGroupProperty.objectReferenceValue = (GameObject)EditorGUILayout.ObjectField("Dot Group Game Object", dotGroupProperty.objectReferenceValue, typeof(GameObject), true);
        if (textGroupProperty.objectReferenceValue == null)
            textGroupProperty.objectReferenceValue = (GameObject)EditorGUILayout.ObjectField("Dot Group Game Object", textGroupProperty.objectReferenceValue, typeof(GameObject), true);
        serializedObject.ApplyModifiedProperties();
        #endregion

        #region Delete_Button
        if (GUILayout.Button("Delete This Label", GUILayout.Height(30), GUILayout.Width(200)))
            mytarget.DeleteLabel();
        #endregion
    }

}
