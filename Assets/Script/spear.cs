using UnityEngine;

public class spear : MonoBehaviour
{
    public float speed = 200f;
    public bool isFiring = false;

    private Vector3 startPos;    // 記錄出生點 (Z + 100)
    private float targetZ;       // 記錄基準點 (Z)
    private float missZ;         // 記錄回收點 (Z - 35)

    public playsound pl;
    public bool hit = false, shoot = false;
    public updatecombo uc;
    public spawn spwn;

    // 💡 核心修改：接收出生點座標與基準點 Z 軸
    public void Fire(Vector3 spawnPosition, float baseZ)
    {
        gameObject.SetActive(true);
        transform.position = spawnPosition;

        startPos = spawnPosition;   // 這裡是 Z + 100
        targetZ = baseZ;            // 這裡是 Z
        missZ = baseZ - 35f;        // 這裡是 Z - 35 (回收點)

        isFiring = true;
    }

    void Update()
    {
        if (isFiring)
        {
            // 從 Z + 100 朝向 Z 移動，Z 軸在減少，所以依然使用 Vector3.back
            transform.position += Vector3.back * speed * Time.deltaTime;

            // 💡 完美修正：當音符的 Z 軸小於或等於回收點 (Z - 35) 時回收
            if (transform.position.z <= missZ)
            {
                spwn.combo = 0;
                spwn.misscount += 1;
                Debug.Log("MISS / " + spwn.combo + " Combo");
                uc.updateText();
                ResetSpear();
            }
        }
    }

    void ResetSpear()
    {
        isFiring = false;
        transform.position = startPos;
        gameObject.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("shield") && isFiring)
        {
            spwn.combo += 1;
            if (spwn.combo > spwn.maxcombo) spwn.maxcombo = spwn.combo;
            spwn.critcount += 1;
            pl.critsound();
            Debug.Log("CRITICAL / " + spwn.combo + " Combo");
            uc.updateText();
            ResetSpear();
        }
    }
}