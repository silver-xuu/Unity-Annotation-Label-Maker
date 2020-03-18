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


    [SerializeField]
    private List<GameObject> labels;
    private GameObject label;
    private GameObject dot, textWindowBackground,textWindow;

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
        dot = label.transform.Find("LabelDotBackground").gameObject;
        labelTexts = new List<string>();
        labels = new List<GameObject>();
        dotPosition = new List<Vector3>();
        textWindowPosition = new List<Vector3>();
        

    }

    public void ChangeLabelPrefab()
    {


        //set prefab dirty
        if (label == null)
            label = Resources.Load<GameObject>("Prefabs/Label") as GameObject;
        textWindowBackground = label.transform.Find("TextWindow").Find("TextBackground").gameObject;
        textWindowBackground.GetComponent<LineRenderer>().widthMultiplier = indicatorLineWidth;
        EditorUtility.SetDirty(textWindowBackground);
        
        //change line width in exsisting labels
        foreach(GameObject _label in labels)
        {
            _label.GetComponentInChildren<LineRenderer>().widthMultiplier = indicatorLineWidth;
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
            newlabel.transform.Find("TextWindow").Find("TextBackground").GetComponent<LineRenderer>().widthMultiplier = indicatorLineWidth;
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
