using UnityEngine;

[System.Serializable]
public struct NoteData
{
    [Header("發射時間 (秒)")]
    public float time;     // 這顆音符要在歌曲開播後第幾秒射出來

    [Header("音符種類 (notes 陣列索引)")]
    public int noteIndex;  // 例如：0 ~ 10

    [Header("發射軌道 (spawnPoints 陣列索引)")]
    public int pointIndex; // 0:左, 1:上, 2:中, 3:下, 4:右
}