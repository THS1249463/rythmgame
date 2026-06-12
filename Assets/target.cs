using UnityEngine;
using TMPro;

public class target : MonoBehaviour
{
    public TMP_Text myText;
    public spear sp;

    void Start()
    {
        myText.text = "";
        myText.color = Color.red;
    }
    // Update is called once per frame
    void Update()
    {
    }

    public void sethit() {
        transform.position = new Vector3(
            sp.transform.position.x+0.54f,
            sp.transform.position.y-0.38f,
            -10f
        );
        myText.text = "+";
        myText.color = Color.red;
        Invoke("sethide", 0.5f);
    }
    void sethide()
    {
        myText.text = "";
        //Destroy(gameObject);
    }
}
