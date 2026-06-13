using UnityEngine;
using System.Collections;

public class spawn : MonoBehaviour
{
    public GameObject spr,spr1,spr2, spr3, spr4, spr5, spr6, spr7, spr8,spr9, spr10;
    public updatecombo uc;
    public AudioSource audioSource;
    public AudioClip susSong;
    public int misscount = 0, critcount = 0, combo = 0, maxcombo = 0;

    void Start() {
        misscount = 0; critcount = 0; combo = 0; maxcombo = 0;
        spr.SetActive(false);
        spr1.SetActive(false);
        spr2.SetActive(false);
        spr3.SetActive(false);
        spr4.SetActive(false);
        spr5.SetActive(false);
        spr6.SetActive(false);
        spr7.SetActive(false);
        spr8.SetActive(false);
        spr9.SetActive(false);
        spr10.SetActive(false);
    }

    /*
0,10
0,-10
0,0
20,0
10,0
-10,0
-20,0
*/

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.F)))
        {
            StartCoroutine(testchart());
            Invoke("SusSongPlay", 0.69f);

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

    void shoot4(int spx, int spy)
    {
        spr3.transform.position = new Vector3(spx, spy, 150);
        spr3.SetActive(true);
    }
    void shoot5(int spx, int spy)
    {
        spr4.transform.position = new Vector3(spx, spy, 150);
        spr4.SetActive(true);
    }
    void shoot6(int spx, int spy)
    {
        spr5.transform.position = new Vector3(spx, spy, 150);
        spr5.SetActive(true);
    }

    void shoot7(int spx, int spy)
    {
        spr6.transform.position = new Vector3(spx, spy, 150);
        spr6.SetActive(true);
    }

    void shoot8(int spx, int spy)
    {
        spr7.transform.position = new Vector3(spx, spy, 150);
        spr7.SetActive(true);
    }
    void shoot9(int spx, int spy)
    {
        spr8.transform.position = new Vector3(spx, spy, 150);
        spr8.SetActive(true);
    }
    void shoot10(int spx, int spy)
    {
        spr9.transform.position = new Vector3(spx, spy, 150);
        spr9.SetActive(true);
    }
    void shoot11(int spx, int spy)
    {
        spr10.transform.position = new Vector3(spx, spy, 150);
        spr10.SetActive(true);
    }

    IEnumerator testchart()
    {
        uc.resetText();
        misscount = 0; critcount = 0; combo = 0; maxcombo = 0;

        shoot1(0,0);
        yield return new WaitForSeconds(0.01f);
        shoot11(2, 2);
        yield return new WaitForSeconds(0.59f);

        shoot2(-10, 0);
        yield return new WaitForSeconds(0.3f);
        shoot3(0, 0);
        yield return new WaitForSeconds(0.3f);
        shoot4(10, 0);
        yield return new WaitForSeconds(0.3f);
        shoot5(0, 0);
        yield return new WaitForSeconds(0.3f);
        shoot6(-10, 0);
        yield return new WaitForSeconds(0.3f);
        shoot7(0, 0);
        yield return new WaitForSeconds(0.3f);
        shoot8(10, 2);
        yield return new WaitForSeconds(0.05f);
        shoot9(10, 0);
        yield return new WaitForSeconds(0.05f);
        shoot10(10, -2);
        yield return new WaitForSeconds(0.9f);

        shoot11(0, 10);
        yield return new WaitForSeconds(0.15f);
        shoot1(0, 0);
        yield return new WaitForSeconds(0.15f);
        shoot2(0, -10);
        yield return new WaitForSeconds(1f);

        yield return new WaitForSeconds(1f);
        Debug.Log("CRIT:" + critcount + "/MISS:" + misscount + "/Combo:" + maxcombo);
    }

    public void SusSongPlay()
    {
        audioSource.PlayOneShot(susSong);
    }

    public void SusSongPlay_0()
    {
        audioSource.PlayOneShot(susSong);
    }

    public void SusSongPlay_1()
    {
        audioSource.PlayOneShot(susSong);
    }
}