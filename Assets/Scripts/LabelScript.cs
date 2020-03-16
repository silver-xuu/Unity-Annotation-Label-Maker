using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using TMPro;



public class LabelScript : MonoBehaviour
{
    /*public Sprite cutomLabelDotBackground;
    public Sprite customTextBackground;*/

    public Color TextBackgroundColor;

    public Color LineColor;

    public float indicatorLineWidth;



    //public Vector3 labelDotSize;
    //public Vector3 labelWindowSize;

    //private Vector3 defaultDotSize,defaultWidowsize;
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
        
        //customTextBackground = Resources.Load<Sprite>("Sprite/square") as Sprite;
        //cutomLabelDotBackground = Resources.Load<Sprite>("Sprite/LabelDot") as Sprite;
        label = Resources.Load<GameObject>("Prefabs/Label") as GameObject;
        dot = label.transform.Find("LabelDotBackground").gameObject;
        labelTexts = new List<string>();
        labels = new List<GameObject>();
        dotPosition = new List<Vector3>();
        textWindowPosition = new List<Vector3>();
        /*defaultDotSize = new Vector3(0.1148119f, 0.1148119f, 0.004432891f);
        defaultWidowsize = new Vector3(1.757971f, 0.2262859f, 0.06415f);
        labelDotSize = defaultDotSize;*/
        //labelDotSize =dot.GetComponent<SpriteRenderer>().bounds.size;
        //labelWindowSize = textWindowBackground.GetComponent<SpriteRenderer>().bounds.size;

    }

    public void ChangeLabelPrefab()
    {
        Undo.RecordObject(label, "Make Changes in Editor");

        textWindowBackground = label.transform.Find("TextWindow").Find("TextBackground").gameObject;
        textWindowBackground.GetComponent<LineRenderer>().widthMultiplier = indicatorLineWidth;

        // Notice that if the call to RecordPrefabInstancePropertyModifications is not present,
        // all changes to scale will be lost when saving the Scene, and reopening the Scene
        // would revert the scale back to its previous value.
        PrefabUtility.RecordPrefabInstancePropertyModifications(label.transform);

        // Optional step in order to save the Scene changes permanently.
        EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
    }
    public void AddLabel()
    {
        if (!string.IsNullOrEmpty(labelText))
        {
            if(label==null)
                label = Resources.Load<GameObject>("Prefabs/Label") as GameObject;
            GameObject newlabel = Instantiate(label, this.transform);

            newlabel.name = labelText;
            newlabel.transform.Find("TextWindow").Find("LabelText").GetComponent<TextMeshPro>().text = labelText;
            labelTexts.Add(labelText);
            labels.Add(newlabel);
            newlabel.transform.Find("LabelDotBackground").Find("LabelIndex").GetComponent<TextMeshPro>().text = labelTexts.Count.ToString();
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

    public void ShowTextWindow()
    {

    }

}
