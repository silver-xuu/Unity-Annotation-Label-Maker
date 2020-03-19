using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using TMPro;


[RequireComponent(typeof(MeshCollider))]

public class LabelScript : MonoBehaviour
{

    public Color TextBackgroundColor;

    public Color LineColor;

    public float indicatorLineWidth;
    public bool showAllText=true;

    public float dotMul, textWindowMul;


    [SerializeField]
    private List<GameObject> labels;
    private GameObject label;
    private Transform dot, textWindowBackground,textWindow;

    public string labelText;
    [SerializeField]
    private List<string> labelTexts;
    [SerializeField]
    public List<Vector3> dotPosition;
    public List<Vector3> textWindowPosition;
    //Set default value for label maker
    private void Reset()
    {

        
       label = Resources.Load<GameObject>("Prefabs/Label") as GameObject;
        textWindow = label.transform.Find("TextWindow");
        textWindowBackground = textWindow.Find("TextBackground");
        dot = label.transform.Find("LabelDotBackground");
        dotMul = dot.localScale.x;
        textWindowMul = textWindow.localScale.x;
        labelTexts = new List<string>();
        labelText ="";
        labels = new List<GameObject>();
        dotPosition = new List<Vector3>();
        textWindowPosition = new List<Vector3>();
        

    }

    public void ChangeLabelPrefab()
    {


        //set prefab dirty
        if (label == null)
        {
            //label = Resources.Load<GameObject>("Prefabs/Label") as GameObject;
            Debug.Log("Cannot load label, please reset the script from inspector");
            return;
        }



        Undo.RecordObject(label.transform, "make change to the line width, txetwindow and dot size");
        textWindow.localScale = new Vector3(textWindowMul, textWindowMul, textWindowMul);
        dot.transform.localScale = new Vector3(dotMul,dotMul,dotMul);
        textWindowBackground.GetComponent<LineRenderer>().widthMultiplier = indicatorLineWidth;
        PrefabUtility.RecordPrefabInstancePropertyModifications(label.transform);

        //change line width in exsisting labels
        if (labels.Count > 0)
        {
            foreach (GameObject _label in labels)
            {
                _label.GetComponentInChildren<LineRenderer>().widthMultiplier = indicatorLineWidth;
                _label.transform.Find("LabelDotBackground").localScale = new Vector3(dotMul, dotMul, dotMul);
                _label.transform.Find("TextWindow").localScale = new Vector3(textWindowMul, textWindowMul, textWindowMul);
            }
        }
    }
        
        
    public void AddLabel(Vector3 labelPosition,Vector3 surfaceNormal)
    {
        if (!string.IsNullOrEmpty(labelText))
        {
            if(label==null)
                label = Resources.Load<GameObject>("Prefabs/Label") as GameObject;
            GameObject newlabel = Instantiate(label, this.transform);

            newlabel.name = "label_" +labelText;
            newlabel.transform.Find("TextWindow").Find("LabelText").GetComponent<TextMeshPro>().text = labelText;
            labelTexts.Add(labelText);
            labels.Add(newlabel);
           
            newlabel.transform.Find("LabelDotBackground").position = labelPosition + surfaceNormal*0.05f;
            newlabel.transform.Find("LabelDotBackground").rotation = Quaternion.LookRotation(-surfaceNormal);
            newlabel.transform.Find("TextWindow").position = labelPosition + surfaceNormal;
            newlabel.transform.Find("LabelDotBackground").Find("LabelIndex").GetComponent<TextMeshPro>().text = labelTexts.Count.ToString();
            // newlabel.transform.Find("TextWindow").Find("TextBackground").GetComponent<LineRenderer>().widthMultiplier = indicatorLineWidth;
            newlabel.GetComponent<LabelTextManager>().showText(showAllText);
            dotPosition.Add(newlabel.transform.Find("LabelDotBackground").position);
            textWindowPosition.Add(newlabel.transform.Find("TextWindow").position);

        }
        
    }
    public void DeleteLastLabel()
    {
        if (labelTexts.Count > 0)
        {
            GameObject destroyObject = labels[labels.Count - 1];
            
            labels.Remove(destroyObject);
            DestroyImmediate(destroyObject);
            labelTexts.RemoveAt(labelTexts.Count - 1);
            dotPosition.RemoveAt(dotPosition.Count - 1);
            textWindowPosition.RemoveAt(textWindowPosition.Count - 1);
        }
        
        
    }
    public void ChangePosition(int i)
    {
        
        Transform dotTrans = labels[i].transform.Find("LabelDotBackground");
        Transform windowTrans = labels[i].transform.Find("TextWindow");
        dotTrans.position = dotPosition[i];
        windowTrans.position = textWindowPosition[i];

    }
    public void showAllLabelText(bool value)
    {
        if (labels == null)
            return;
        foreach (GameObject _label in labels)
        {
            _label.GetComponent<LabelTextManager>().showText(value);
        }
    }


}
