using UnityEngine;
using System.Collections;

public class spawn_old : MonoBehaviour
{
    [Header("音符物件陣列")]
    public GameObject[] notes;

    [Header("五個對應的座標點 (把畫面的 5 個空物件拉進來)")]
    public Transform[] spawnPoints; // Size 設定 5

    public updatecombo uc;
    public AudioSource audioSource;
    public AudioClip susSong,susSongshort,spearofjustice, UndyneTheTrueHero;
    public int misscount = 0, critcount = 0, combo = 0, maxcombo = 0;
    public bool playingstatus = false;
    public title tt;

    void Start() {
        misscount = 0; critcount = 0; combo = 0; maxcombo = 0;

        tt.appear();

        for (int i = 0; i < notes.Length; i++)
        {
            if (notes[i] != null) notes[i].SetActive(false);
        }
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Alpha1)) && !playingstatus)
        {
            playingstatus = true;
            StartCoroutine(tutorial());

        }

        if ((Input.GetKeyDown(KeyCode.Alpha4)) && !playingstatus)
        {
            playingstatus = true;
            StartCoroutine(amongus());
            Invoke("SusSongPlay", 0.8f);

        }

        if ((Input.GetKeyDown(KeyCode.Alpha3)) && !playingstatus)
        {
            playingstatus = true;
            StartCoroutine(undyne());
            Invoke("playundyne", 0.9f);

        }

        if ((Input.GetKeyDown(KeyCode.Alpha2)) && !playingstatus)
        {
            playingstatus = true;
            StartCoroutine(amongusshort());
            Invoke("SusSongPlayShort", 0.9f);

        }

        if ((Input.GetKeyDown(KeyCode.Alpha5)) && !playingstatus)
        {
            playingstatus = true;
            StartCoroutine(undyneTheTrueClip());
            Invoke("UndyneTheTrueHero", 0.9f);

        }
    }

    void ShootNote(int noteIndex, int pointIndex)
    {
        if (noteIndex >= 0 && noteIndex < notes.Length && notes[noteIndex] != null &&
            pointIndex >= 0 && pointIndex < spawnPoints.Length && spawnPoints[pointIndex] != null)
        {
            // 1. 取得畫面上該軌道 Hint 點的 Transform 物件
            Transform hintPoint = spawnPoints[pointIndex];
            Vector3 basePosition = hintPoint.position;

            // 💡 核心修改：利用 Hint 點的「正前方(forward)」來動態計算出生點
            // 不論 Hint 點轉向哪裡，長矛永遠在它前方的 100 單位距離出生，然後往回射向基準點
            // (原本寫 +200f 的世界座標，這裡配合新的 spear.cs 距離邏輯改為 forward * 100f 體感較佳，你也可以自由調整)
            Vector3 spawnPos = basePosition + hintPoint.forward * 200f;

            GameObject currentNote = notes[noteIndex];
            currentNote.SetActive(true);

            spear spearScript = currentNote.GetComponent<spear>();
            if (spearScript != null)
            {
                // 💡 關鍵修改：呼叫新版 Fire()，傳入：出生座標、基準點座標、Hint點的旋轉角度
                spearScript.Fire(spawnPos, basePosition, hintPoint.rotation);
            }
        }
    }

    // 根據你原本程式碼的 (x, y) 座標分配對應的點 (0 ~ 4)
    // 假設對應關係如下（你可以依據自己在 Unity 擺放的順序調整 pointIndex）：
    // Point 0 =左 (0, 0)
    // Point 1 =上 (-10, 0)
    // Point 2 =中 (10, 0)
    // Point 3 =下 (10, 2) / (2, 2) / (0, 10) 類型的變化點
    // Point 4 =右 (10, -2) / (0, -10) 類型的變化點

    IEnumerator amongusshort()
    {
        tt.hide();
        ResetStats();

        ShootNote(0, 2);
        yield return new WaitForSeconds(0.6f);

        ShootNote(1, 0);
        yield return new WaitForSeconds(0.3f);
        ShootNote(2, 1);
        yield return new WaitForSeconds(0.3f);
        ShootNote(3, 4);
        yield return new WaitForSeconds(0.3f);
        ShootNote(4, 3);
        yield return new WaitForSeconds(0.3f);
        ShootNote(5, 0);
        yield return new WaitForSeconds(0.3f);
        ShootNote(6, 2);
        yield return new WaitForSeconds(0.3f);
        ShootNote(7, 4);
        yield return new WaitForSeconds(0.1f);

        yield return new WaitForSeconds(0.8f);
        ShootNote(10, 1);
        yield return new WaitForSeconds(0.15f);
        ShootNote(0, 2);
        yield return new WaitForSeconds(0.15f);
        ShootNote(1, 3);
        yield return new WaitForSeconds(1f);
        //

        yield return new WaitForSeconds(1f);
        Debug.Log("CRIT:" + critcount + "/MISS:" + misscount + "/Combo:" + maxcombo);
        uc.setEndText();
        yield return new WaitUntil(
            () => Input.GetKeyDown(KeyCode.Return)
        );
        audioSource.Stop();
        tt.appear();
        playingstatus = false;
    }

    IEnumerator amongus()
    {
        tt.hide();
        ResetStats();

        ShootNote(0, 2);
        yield return new WaitForSeconds(0.6f);

        ShootNote(1, 0);
        yield return new WaitForSeconds(0.3f);
        ShootNote(2, 1);
        yield return new WaitForSeconds(0.3f);
        ShootNote(3, 4);
        yield return new WaitForSeconds(0.3f);
        ShootNote(4, 3);
        yield return new WaitForSeconds(0.3f);
        ShootNote(5, 0); 
        yield return new WaitForSeconds(0.3f);
        ShootNote(6, 2);
        yield return new WaitForSeconds(0.3f);
        ShootNote(7, 4);
        yield return new WaitForSeconds(0.1f);

        yield return new WaitForSeconds(0.8f);
        ShootNote(10, 1); 
        yield return new WaitForSeconds(0.15f);
        ShootNote(0, 2);
        yield return new WaitForSeconds(0.15f);
        ShootNote(1, 3);
        yield return new WaitForSeconds(1f);
        //
        ShootNote(2, 0);
        yield return new WaitForSeconds(0.4f);
        ShootNote(3, 4);
        yield return new WaitForSeconds(0.7f);

        ShootNote(4, 0);
        yield return new WaitForSeconds(0.3f);
        ShootNote(5, 1);
        yield return new WaitForSeconds(0.3f);
        ShootNote(6, 4);
        yield return new WaitForSeconds(0.3f);
        ShootNote(7, 3);
        yield return new WaitForSeconds(0.3f);
        ShootNote(8, 0);
        yield return new WaitForSeconds(0.3f);
        ShootNote(9, 1);
        yield return new WaitForSeconds(0.3f);
        ShootNote(10, 2);
        yield return new WaitForSeconds(1.2f);

        ShootNote(0, 0);
        yield return new WaitForSeconds(0.2f);
        ShootNote(1, 2);
        yield return new WaitForSeconds(0.2f);
        ShootNote(2, 2);
        yield return new WaitForSeconds(0.3f);
        ShootNote(3, 4);
        yield return new WaitForSeconds(0.2f);
        ShootNote(4, 2);
        yield return new WaitForSeconds(0.2f);
        ShootNote(5, 2);
        yield return new WaitForSeconds(0.3f);
        //
        ShootNote(0, 1);
        yield return new WaitForSeconds(0.7f);

        ShootNote(1, 0);
        yield return new WaitForSeconds(0.3f);
        ShootNote(2, 4);
        yield return new WaitForSeconds(0.3f);
        ShootNote(3, 0);
        yield return new WaitForSeconds(0.3f);
        ShootNote(4, 4);
        yield return new WaitForSeconds(0.3f);
        ShootNote(5, 1);
        yield return new WaitForSeconds(0.3f);
        ShootNote(6, 2);
        yield return new WaitForSeconds(0.3f);
        ShootNote(7, 3);
        yield return new WaitForSeconds(0.1f);

        yield return new WaitForSeconds(0.8f);
        ShootNote(10, 0);
        yield return new WaitForSeconds(0.15f);
        ShootNote(0, 2);
        yield return new WaitForSeconds(0.15f);
        ShootNote(1, 4);
        yield return new WaitForSeconds(1f);
        //
        ShootNote(2, 0);
        yield return new WaitForSeconds(0.4f);
        ShootNote(3, 4);
        yield return new WaitForSeconds(0.7f);

        ShootNote(4, 1);
        yield return new WaitForSeconds(0.3f);
        ShootNote(5, 3);
        yield return new WaitForSeconds(0.3f);
        ShootNote(6, 1);
        yield return new WaitForSeconds(0.3f);
        ShootNote(7, 3);
        yield return new WaitForSeconds(0.3f);
        ShootNote(8, 0);
        yield return new WaitForSeconds(0.3f);
        ShootNote(9, 2);
        yield return new WaitForSeconds(0.3f);
        ShootNote(10, 4);
        yield return new WaitForSeconds(1.2f);

        ShootNote(0, 1);
        yield return new WaitForSeconds(0.2f);
        ShootNote(1, 2);
        yield return new WaitForSeconds(0.2f);
        ShootNote(2, 2);
        yield return new WaitForSeconds(0.3f);
        ShootNote(3, 3);
        yield return new WaitForSeconds(0.2f);
        ShootNote(4, 2);
        yield return new WaitForSeconds(0.2f);
        ShootNote(5, 2);
        yield return new WaitForSeconds(0.3f);
        //
        ShootNote(0, 0);
        yield return new WaitForSeconds(0.7f);

        yield return new WaitForSeconds(1f);
        Debug.Log("CRIT:" + critcount + "/MISS:" + misscount + "/Combo:" + maxcombo);
        uc.setEndText();
        yield return new WaitUntil(
            () => Input.GetKeyDown(KeyCode.Return)
        );
        audioSource.Stop();
        tt.appear();
        playingstatus = false;
    }

    IEnumerator tutorial()
    {
        tt.hide();
        ResetStats();

        ShootNote(0, 2);
        yield return new WaitForSeconds(1f);
        ShootNote(1, 0);
        yield return new WaitForSeconds(1f);
        ShootNote(2, 1);
        yield return new WaitForSeconds(1f);
        ShootNote(3, 4);
        yield return new WaitForSeconds(1f);
        ShootNote(4, 3);
        yield return new WaitForSeconds(1f);

        yield return new WaitForSeconds(1f);
        Debug.Log("CRIT:" + critcount + "/MISS:" + misscount + "/Combo:" + maxcombo);
        uc.setEndText();
        yield return new WaitUntil(
            () => Input.GetKeyDown(KeyCode.Return)
        );
        audioSource.Stop();
        tt.appear();
        playingstatus = false;
    }

    IEnumerator undyne()
    {
        tt.hide();
        ResetStats();

        ShootNote(0, 2);
        yield return new WaitForSeconds(1.35f);
        ShootNote(1, 0);
        yield return new WaitForSeconds(1.35f);
        ShootNote(2, 4);
        yield return new WaitForSeconds(1.35f);
        ShootNote(3, 1);
        yield return new WaitForSeconds(0.7f);
        ShootNote(4, 3);
        yield return new WaitForSeconds(0.7f);
        //
        ShootNote(5, 2);
        yield return new WaitForSeconds(1.35f);
        ShootNote(6, 4);
        yield return new WaitForSeconds(1.35f);
        ShootNote(7, 3);
        yield return new WaitForSeconds(1.35f);
        ShootNote(8, 0);
        yield return new WaitForSeconds(0.7f);
        ShootNote(9, 1);
        yield return new WaitForSeconds(0.7f);
        //
        ShootNote(6, 2);
        yield return new WaitForSeconds(1.35f-0.5f);
        ShootNote(1, 0);
        yield return new WaitForSeconds(0.5f);
        ShootNote(7, 2);
        yield return new WaitForSeconds(1.35f - 0.5f);
        ShootNote(2, 4);
        yield return new WaitForSeconds(0.5f);
        ShootNote(8, 2);
        yield return new WaitForSeconds(1.35f - 0.5f);
        ShootNote(3, 1);
        yield return new WaitForSeconds(0.5f);
        ShootNote(9, 2);
        yield return new WaitForSeconds(0.7f);
        ShootNote(10, 3);
        yield return new WaitForSeconds(0.7f);
        //
        ShootNote(6, 2);
        yield return new WaitForSeconds(1.35f - 0.5f);
        ShootNote(1, 3);
        yield return new WaitForSeconds(0.5f);
        ShootNote(7, 2);
        yield return new WaitForSeconds(1.35f - 0.5f);
        ShootNote(2, 1);
        yield return new WaitForSeconds(0.5f);
        ShootNote(8, 2);
        yield return new WaitForSeconds(1.35f - 0.5f);
        ShootNote(3, 4);
        yield return new WaitForSeconds(0.5f);
        ShootNote(9, 2);
        yield return new WaitForSeconds(0.7f);
        ShootNote(10, 0);
        yield return new WaitForSeconds(0.7f);
        //
        ShootNote(6, 2);
        yield return new WaitForSeconds(1.35f - 0.5f);
        ShootNote(1, 1);
        yield return new WaitForSeconds(0.25f);
        ShootNote(2, 2);
        yield return new WaitForSeconds(0.25f);
        ShootNote(7, 1);
        yield return new WaitForSeconds(1.35f - 0.5f);
        ShootNote(3, 3);
        yield return new WaitForSeconds(0.25f);
        ShootNote(4, 2);
        yield return new WaitForSeconds(0.25f);
        ShootNote(8, 3);
        yield return new WaitForSeconds(1.35f - 0.5f);
        ShootNote(5, 1);
        yield return new WaitForSeconds(0.25f);
        ShootNote(6, 2);
        yield return new WaitForSeconds(0.25f);
        ShootNote(9, 1);
        yield return new WaitForSeconds(0.7f);
        ShootNote(10, 4);
        yield return new WaitForSeconds(0.7f);
        //
        ShootNote(6, 2);
        yield return new WaitForSeconds(1.35f - 0.5f);
        ShootNote(1, 0);
        yield return new WaitForSeconds(0.25f);
        ShootNote(2, 2);
        yield return new WaitForSeconds(0.25f);
        ShootNote(7, 0);
        yield return new WaitForSeconds(1.35f - 0.5f);
        ShootNote(3, 4);
        yield return new WaitForSeconds(0.25f);
        ShootNote(4, 2);
        yield return new WaitForSeconds(0.25f);
        ShootNote(8, 4);
        yield return new WaitForSeconds(1.35f - 0.5f);
        ShootNote(5, 0);
        yield return new WaitForSeconds(0.25f);
        ShootNote(6, 2);
        yield return new WaitForSeconds(0.25f);
        ShootNote(9, 0);
        yield return new WaitForSeconds(0.7f);
        ShootNote(10, 4);
        yield return new WaitForSeconds(0.7f);


        ShootNote(1, 1);
        yield return new WaitForSeconds(0.7f);
        ShootNote(2, 3);
        yield return new WaitForSeconds(0.7f);
        ShootNote(3, 4);
        yield return new WaitForSeconds(0.7f);
        ShootNote(4, 2);
        yield return new WaitForSeconds(0.7f);
        //1 2 3 2
        ShootNote(5, 1);
        yield return new WaitForSeconds(0.25f);
        ShootNote(6, 2);
        yield return new WaitForSeconds(0.25f);
        ShootNote(7, 3);
        yield return new WaitForSeconds(0.25f);
        ShootNote(8, 2);
        yield return new WaitForSeconds(0.65f);
        ShootNote(9, 0);
        yield return new WaitForSeconds(1f);

        yield return new WaitForSeconds(1f);
        Debug.Log("CRIT:" + critcount + "/MISS:" + misscount + "/Combo:" + maxcombo);
        uc.setEndText();
        yield return new WaitUntil(
            () => Input.GetKeyDown(KeyCode.Return)
        );
        audioSource.Stop();
        tt.appear();
        playingstatus = false;
    }

    IEnumerator undyneTheTrueClip()
    {
        tt.hide();
        ResetStats();

        
        Debug.Log("CRIT:" + critcount + "/MISS:" + misscount + "/Combo:" + maxcombo);
        uc.setEndText();
        yield return new WaitUntil(
            () => Input.GetKeyDown(KeyCode.Return)
        );
        audioSource.Stop();
        tt.appear();
        playingstatus = false;
    }



    void ResetStats()
    {
        
        misscount = 0; critcount = 0; combo = 0; maxcombo = 0;
        uc.resetText();

    }
    public void SusSongPlay()
    {
        audioSource.PlayOneShot(susSong);
    }

    public void playundyne()
    {
        audioSource.PlayOneShot(spearofjustice);
    }

    public void SusSongPlayShort()
    {
        audioSource.PlayOneShot(susSongshort);
    }
}