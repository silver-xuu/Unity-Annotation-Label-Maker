using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteInEditMode]
public class Line : MonoBehaviour
{
    public Transform LabelDot;
    public Transform text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LineRenderer line = this.GetComponent<LineRenderer>();
        line.SetPosition(0, LabelDot.position);
        line.SetPosition(1, this.transform.position);

        text.position = this.transform.position;
        //text.GetComponent<RectTransform>().sizeDelta = this.GetComponent<SpriteRenderer>().bounds.size*100;
    }
}
