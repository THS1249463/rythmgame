using UnityEngine;
using System.Collections;

public class spawn : MonoBehaviour
{
    public GameObject spr,spr1,spr2;
    public target tg;
    public playsound pl;

    void Start() {
        spr.SetActive(false);
        spr1.SetActive(false);
        spr2.SetActive(false);
    }
    void Update()
    {
        /*
 -5 25
  5 25
  15 10
  5 -10
-5 -10
-15 10
 */
        if ((Input.GetKeyDown(KeyCode.F)))
        {
            shoot1(0,10);

        }
        if ((Input.GetKeyDown(KeyCode.G)))
        {
            shoot2(0,10);
        }
        if ((Input.GetKeyDown(KeyCode.H)))
        {
            shoot3(0,10);
        }
        if ((Input.GetKeyDown(KeyCode.J)))
        {
            StartCoroutine(ShootSequence());
        }
    }
    void shoot1(int spx,int spy) 
    {
        spr.transform.position = new Vector3(spx, spy, 150);
        spr.SetActive(true);
    }
    void shoot2(int spx, int spy)
    {
        spr1.transform.position = new Vector3(spx, spy, 150);
        spr1.SetActive(true);
    }
    void shoot3(int spx, int spy)
    {
        spr2.transform.position = new Vector3(spx, spy, 150);
        spr2.SetActive(true);
    }

    IEnumerator ShootSequence()
    {
        shoot1(0,20);
        yield return new WaitForSeconds(1f);

        shoot2(0,10);
        yield return new WaitForSeconds(1f);

        shoot3(0,-20);
        yield return new WaitForSeconds(1f);
    }
}