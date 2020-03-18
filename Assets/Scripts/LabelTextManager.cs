using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LabelTextManager : MonoBehaviour
{
    [SerializeField]
    private GameObject labelText;
    
    // Start is called before the first frame update
    void Reset()
    {
        labelText = this.transform.Find("TextWindow").gameObject;
    }
    public void showText(bool value) {
        labelText.SetActive(value);
    }
    
    public void toggleText()
    {
        labelText.SetActive(!labelText.activeSelf);
    }

}
