using UnityEngine;
using TMPro;

public class updatecombo : MonoBehaviour
{
    public TMP_Text myText;
    public spawn_new spwn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        resetText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resetText()
    {
        myText.text = "Combo:" + "0" + "\r\n\r\nCrit:" + "0" + "\r\nMiss:" + "0";
    }

    public void updateText()
    {
        myText.text = "Combo:" + spwn.maxcombo + "\r\n\r\nCrit:" + spwn.critcount + "\r\nMiss:" + spwn.misscount;
    }
}
