using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewSongData", menuName = "音樂遊戲/建立新譜面")]
public class SongData : ScriptableObject
{
    public string songName;      // 歌曲名稱
    public AudioClip songAudio;  // 歌曲音樂檔案
    public float startDelay = 0.9f; // 音樂延遲播放時間 (原本你用 Invoke 的時間)

    [Header("整首歌的音符清單 (按時間由小到大排序)")]
    public List<NoteData> notesList = new List<NoteData>();
}