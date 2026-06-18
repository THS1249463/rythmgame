using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class spawn_new : MonoBehaviour
{
    [Header("音符物件陣列")]
    public GameObject[] notes;

    [Header("五個對應的座標點")]
    public Transform[] spawnPoints;

    [Header("譜面資料庫 (把做好的 SongData 拖進來)")]
    public SongData tutorialSong;
    public SongData amongUsShortSong;
    public SongData amongUsLongSong;
    public SongData undyneSong;
    public SongData undyneTrueHeroSong;

    [Header("外部組件綁定")]
    public updatecombo uc;
    public AudioSource audioSource;
    public title tt;

    [HideInInspector] public int misscount = 0, critcount = 0, combo = 0, maxcombo = 0;
    [HideInInspector] public bool playingstatus = false;

    private SongData currentPlayingSong = null;

    void Start()
    {
        ResetStats();
        tt.appear();

        for (int i = 0; i < notes.Length; i++)
        {
            if (notes[i] != null) notes[i].SetActive(false);
        }
    }

    void Update()
    {
        if (!playingstatus)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) PlaySong(tutorialSong);
            if (Input.GetKeyDown(KeyCode.Alpha2)) PlaySong(amongUsShortSong);
            if (Input.GetKeyDown(KeyCode.Alpha4)) PlaySong(amongUsLongSong);
            if (Input.GetKeyDown(KeyCode.Alpha3)) PlaySong(undyneSong);
            if (Input.GetKeyDown(KeyCode.Alpha5)) PlaySong(undyneTrueHeroSong);
        }
    }

    // 💡 萬用歌曲初始化
    void PlaySong(SongData song)
    {
        if (song == null) return;
        playingstatus = true;
        currentPlayingSong = song;
        StartCoroutine(PlaySongCore());
    }

    // 💡 核心變革：整款遊戲現在只需要這一個協程就能播完所有歌曲！
    IEnumerator PlaySongCore()
    {
        tt.hide();
        ResetStats();

        // 動態計算音樂播放的延遲
        Invoke("PlayCurrentAudio", currentPlayingSong.startDelay);

        float startTime = Time.time;
        int currentNoteIndex = 0;
        List<NoteData> songNotes = currentPlayingSong.notesList;

        // 只要這首歌的音符還沒全部射完，就一直用 Update 的速度檢查
        while (currentNoteIndex < songNotes.Count)
        {
            float elapsedTime = Time.time - startTime;

            // 💡 核心判定：當遊戲進行時間大於等於目前音符規定的時間，就發射！
            if (elapsedTime >= songNotes[currentNoteIndex].time)
            {
                ShootNote(songNotes[currentNoteIndex].noteIndex, songNotes[currentNoteIndex].pointIndex);
                currentNoteIndex++; // 瞄準下一顆音符
            }

            yield return null; // 每幀檢查一次，精確度高達 90fps~120fps，比 WaitForSeconds 準非常多
        }

        // 💡 歌曲音符發射完畢後的結算畫面
        yield return new WaitForSeconds(2f); // 等最後一顆長矛飛完
        Debug.Log("CRIT:" + critcount + "/MISS:" + misscount + "/Combo:" + maxcombo);
        uc.setEndText();

        // 等待玩家按下 Enter 鍵回到主畫面
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        audioSource.Stop();
        tt.appear();
        playingstatus = false;
        currentPlayingSong = null;
    }

    void ShootNote(int noteIndex, int pointIndex)
    {
        if (noteIndex >= 0 && noteIndex < notes.Length && notes[noteIndex] != null &&
            pointIndex >= 0 && pointIndex < spawnPoints.Length && spawnPoints[pointIndex] != null)
        {
            Transform hintPoint = spawnPoints[pointIndex];
            Vector3 basePosition = hintPoint.position;
            Vector3 spawnPos = basePosition + hintPoint.forward * 200f; // 200f 遠端生成

            GameObject currentNote = notes[noteIndex];
            currentNote.SetActive(true);

            spear spearScript = currentNote.GetComponent<spear>();
            if (spearScript != null)
            {
                spearScript.Fire(spawnPos, basePosition, hintPoint.rotation);
            }
        }
    }

    void PlayCurrentAudio()
    {
        if (currentPlayingSong != null && currentPlayingSong.songAudio != null)
        {
            audioSource.PlayOneShot(currentPlayingSong.songAudio);
        }
    }

    void ResetStats()
    {
        misscount = 0; critcount = 0; combo = 0; maxcombo = 0;
        uc.resetText();
    }
}