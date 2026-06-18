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
        myText.text = "Combo:" + spwn.combo + "\r\n\r\nCrit:" + spwn.critcount + "\r\nMiss:" + spwn.misscount;
    }

    public void setEndText()
    {
        if (spwn.misscount == 0) myText.text += "\r\n\r\nFULL COMBO !";
        else myText.text += "\r\n\r\nMax Combo:" + spwn.maxcombo; 
        myText.text += "\r\nChart End, Press Enter to continue";
    }
}
