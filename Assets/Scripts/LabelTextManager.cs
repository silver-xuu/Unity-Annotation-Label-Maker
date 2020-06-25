using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

/// <summary>
/// Label class can be saved to JSON file for future functionalities
/// </summary>
[Serializable]
public class Label
{
    public int index;

    public string upperText, bottomText;

    public Vector3 dotPosition, textWindowPosition;

    public bool isLabelDisplayed;

    public float dotSize, textWindowSize, lineWidthMultiplier;

    public Vector4 textColor;
}

public class LabelTextManager : MonoBehaviour
{
    

    public Label thisLabel;

    [SerializeField]
    private TextMeshPro upperTextmesh, bottomTextmesh, indexTextmesh;
    [SerializeField]
    private LineRenderer line;
    [SerializeField]
    private GameObject dotGroup, textGroup;
    public Color textColor;

    public void NewLabel(int index, string upperText, string bottomText, Vector3 dotPosition, Vector3 textWindowPosition, bool isLabelDisplayed)
    {
        //change current label's index number
        SetIndexNumber(index);

        //change current label's text
        SetText(upperText, bottomText);

        //set dot size and position
        SetDotPosition(dotPosition);
        //SetDotSize(dotSize);

        //set text window position
        SetTextWindowPosition(textWindowPosition);
        //SetTextWindowSize(textWindowSize);


        //set connecting line width
        //SetLineWidth(lineWidth);

        //set text color
        //SetTextColor(textColor);

        //display label 
        showLabel(isLabelDisplayed);
    }
   
    
    /// <summary>
    /// two string will be passed to the function and set the label text
    /// </summary>
    /// <param name="bottomText"></param>
    /// <param name="upperText"></param>
    public void SetText(string upperText,string bottomText)
    {
        thisLabel.bottomText = bottomText;
        bottomTextmesh.text = bottomText;
        thisLabel.upperText = upperText;
        upperTextmesh.text = upperText;
        //change current label name to the bottom text
        this.name ="Label_"+ thisLabel.bottomText;
    }

    public void SetIndexNumber(int index)
    {
        thisLabel.index = index;
        indexTextmesh.text = index.ToString();
    }

    public void SetTextWindowPosition(Vector3 position)
    {
        textGroup.transform.position = position;
        thisLabel.textWindowPosition = position;
    }
    public void SetTextWindowSize(float size)
    {
        thisLabel.textWindowSize = size;
        textGroup.transform.localScale = new Vector3(size, size, size);
    }
    public void SetDotSize(float size)
    {
        thisLabel.dotSize = size;
        dotGroup.transform.localScale = new Vector3(size, size, size);
    }
    public void SetDotPosition(Vector3 position)
    {
        dotGroup.transform.position = position;
        thisLabel.dotPosition = position;
    }
    public void SetLineWidth(float widthMultiplier)
    {
        thisLabel.lineWidthMultiplier = widthMultiplier;
        line.widthMultiplier = widthMultiplier;
    }
    public void SetTextColor(Color color)
    {
        thisLabel.textColor = color;
        textColor = color;
        upperTextmesh.color = color;
        bottomTextmesh.color = color;
        indexTextmesh.color = color;

        
    }

    public void showLabel(bool isActive)
    {
        textGroup.SetActive(isActive);
        thisLabel.isLabelDisplayed = isActive;
    }

    public void toggleLabel()
    {
        showLabel(!thisLabel.isLabelDisplayed);

    }
    public void DeleteLabel()
    {
        LabelScript parent = this.GetComponentInParent<LabelScript>();
        if (parent != null && parent.labels.Contains(this))
            parent.labels.Remove(this);
        DestroyImmediate(this.gameObject);
    }
    public void ReloadLabel()
    {
        thisLabel.index = int.Parse(indexTextmesh.text);
        thisLabel.upperText = upperTextmesh.text;
        thisLabel.bottomText = bottomTextmesh.text;
        thisLabel.dotPosition = dotGroup.transform.position;
        thisLabel.textWindowPosition = textGroup.transform.position;
        thisLabel.isLabelDisplayed = textGroup.activeSelf;
    }
 
}
