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
    private void OnEnable()
    {
         mytarget = (LabelScript)target;
        GetTarget = new SerializedObject(mytarget);
        list= GetTarget.FindProperty("labelTexts");
    }
    public override void OnInspectorGUI()
    {

        //mytarget.customTextBackground = (Sprite)EditorGUIUtility.Load("/Sprite/square");
        GetTarget.Update();
        //mytarget.customTextBackground = (Sprite)EditorGUILayout.ObjectField("Text Background", mytarget.customTextBackground, typeof(Sprite), allowSceneObjects: true);
        //mytarget.cutomLabelDotBackground = (Sprite)EditorGUILayout.ObjectField("Dot Background", mytarget.cutomLabelDotBackground, typeof(Sprite), allowSceneObjects: true);
        //mytarget.labelDotSize=EditorGUILayout.Vector3Field("Dot Size", mytarget.labelDotSize);
        //mytarget.labelWindowSize=EditorGUILayout.Vector3Field("Text Window Size", mytarget.labelWindowSize);

        mytarget.LineColor = EditorGUILayout.ColorField("Line Color", mytarget.LineColor);
        mytarget.indicatorLineWidth = EditorGUILayout.Slider("Line width", mytarget.indicatorLineWidth, 0.1f, 1f);
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
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField((i+1).ToString());
                EditorGUILayout.LabelField(list.GetArrayElementAtIndex(i).stringValue);
                EditorGUILayout.EndHorizontal();
                EditorGUI.BeginChangeCheck();
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

        mytarget.labelText=EditorGUILayout.TextField("Label Text", mytarget.labelText);
        
        if (GUILayout.Button("Add new Label", GUILayout.Height(50)))
        {
            mytarget.AddLabel();
        }
        if(GUILayout.Button("Delete Last Label", GUILayout.Height(50))){
            mytarget.DeleteLastLabel();
        }
    }
    /*public void OnInspectorUpdate()
    {
        LabelScript mytarget = (LabelScript)target;
        mytarget.ChangeLabelPrefab();
    }*/
}
