using UnityEngine;
using System.Collections;

public class spawn_new : MonoBehaviour
{
    [Header("音符物件陣列")]
    public GameObject[] notes;

    [Header("五個對應的座標點 (把畫面的 5 個空物件拉進來)")]
    public Transform[] spawnPoints; // Size 設定 5

    public updatecombo uc;
    public AudioSource audioSource;
    public AudioClip susSong;
    public int misscount = 0, critcount = 0, combo = 0, maxcombo = 0;

    void Start() {
        misscount = 0; critcount = 0; combo = 0; maxcombo = 0;
        for (int i = 0; i < notes.Length; i++)
        {
            if (notes[i] != null) notes[i].SetActive(false);
        }
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

    void ShootNote(int noteIndex, int pointIndex)
    {
        if (noteIndex >= 0 && noteIndex < notes.Length && notes[noteIndex] != null &&
            pointIndex >= 0 && pointIndex < spawnPoints.Length && spawnPoints[pointIndex] != null)
        {
            // 1. 取得畫面上該軌道基準點的完整座標
            Vector3 basePosition = spawnPoints[pointIndex].position;

            // 2. 計算音符的實際出生位置 (X、Y不變，Z 軸放到 Z + 100 的遠處)
            Vector3 spawnPos = new Vector3(basePosition.x, basePosition.y, basePosition.z + 150f);

            GameObject currentNote = notes[noteIndex];
            currentNote.SetActive(true);

            spear spearScript = currentNote.GetComponent<spear>();
            if (spearScript != null)
            {
                // 💡 關鍵修改：將「出生位置」與「基準點的 Z 軸」一起傳給音符
                spearScript.Fire(spawnPos, basePosition.z);
            }
        }
    }
    IEnumerator testchart()
    {
        ResetStats();

        // 根據你原本程式碼的 (x, y) 座標分配對應的點 (0 ~ 4)
        // 假設對應關係如下（你可以依據自己在 Unity 擺放的順序調整 pointIndex）：
        // Point 0 = (0, 0)
        // Point 1 = (-10, 0)
        // Point 2 = (10, 0)
        // Point 3 = (10, 2) / (2, 2) / (0, 10) 類型的變化點
        // Point 4 = (10, -2) / (0, -10) 類型的變化點

        ShootNote(0, 2); // 原 shoot1(0,0)
        yield return new WaitForSeconds(0.01f);
        ShootNote(10, 2); // 原 shoot11(2,2)
        yield return new WaitForSeconds(0.59f);

        ShootNote(1, 0); // 原 shoot2(-10, 0)
        yield return new WaitForSeconds(0.3f);
        ShootNote(2, 1); // 原 shoot3(0, 0)
        yield return new WaitForSeconds(0.3f);
        ShootNote(3, 4); // 原 shoot4(10, 0)
        yield return new WaitForSeconds(0.3f);
        ShootNote(4, 3); // 原 shoot5(0, 0)
        yield return new WaitForSeconds(0.3f);
        ShootNote(5, 0); // 原 shoot6(-10, 0)
        yield return new WaitForSeconds(0.3f);
        ShootNote(6, 3); // 原 shoot7(0, 0)
        yield return new WaitForSeconds(0.3f);
        ShootNote(7, 4); // 原 shoot8(10, 2)
        yield return new WaitForSeconds(0.05f);
        ShootNote(8, 2); // 原 shoot9(10, 0)
        yield return new WaitForSeconds(0.05f);
        ShootNote(9, 2); // 原 shoot10(10, -2)

        yield return new WaitForSeconds(0.9f);
        ShootNote(10, 0); // 原 shoot11(0, 10)
        yield return new WaitForSeconds(0.15f);
        ShootNote(0, 2); // 原 shoot1(0, 0)
        yield return new WaitForSeconds(0.15f);
        ShootNote(1, 4); // 原 shoot2(0, -10)
        yield return new WaitForSeconds(1f);

        yield return new WaitForSeconds(1f);
        Debug.Log("CRIT:" + critcount + "/MISS:" + misscount + "/Combo:" + maxcombo);
    }

    void ResetStats()
    {
        misscount = 0; critcount = 0; combo = 0; maxcombo = 0;
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