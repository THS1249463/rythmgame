//using UnityEngine;

//public class spear : MonoBehaviour
//{
//    public float speed = 200f;
//    public bool isFiring = false;

//    private Vector3 startPos;    // 記錄出生點 (Z + 100)
//    private float targetZ;       // 記錄基準點 (Z)
//    private float missZ;         // 記錄回收點 (Z - 35)

//    public playsound pl;
//    public bool hit = false, shoot = false;
//    public updatecombo uc;
//    public spawn_new spwn;

//    // 💡 核心修改：接收出生點座標與基準點 Z 軸
//    public void Fire(Vector3 spawnPosition, float baseZ)
//    {
//        gameObject.SetActive(true);
//        transform.position = spawnPosition;

//        startPos = spawnPosition;   // 這裡是 Z + 100
//        targetZ = baseZ;            // 這裡是 Z
//        missZ = baseZ - 35f;        // 這裡是 Z - 35 (回收點)

//        isFiring = true;
//    }

//    void Update()
//    {
//        if (isFiring)
//        {
//            // 從 Z + 100 朝向 Z 移動，Z 軸在減少，所以依然使用 Vector3.back
//            transform.position += Vector3.back * speed * Time.deltaTime;

//            // 💡 完美修正：當音符的 Z 軸小於或等於回收點 (Z - 35) 時回收
//            if (transform.position.z <= missZ)
//            {
//                spwn.combo = 0;
//                spwn.misscount += 1;
//                Debug.Log("MISS / " + spwn.combo + " Combo");
//                uc.updateText();
//                ResetSpear();
//            }
//        }
//    }

//    void ResetSpear()
//    {
//        isFiring = false;
//        transform.position = startPos;
//        gameObject.SetActive(false);
//    }

//    void OnCollisionEnter(Collision collision)
//    {
//        if (collision.gameObject.CompareTag("shield") && isFiring)
//        {
//            spwn.combo += 1;
//            if (spwn.combo > spwn.maxcombo) spwn.maxcombo = spwn.combo;
//            spwn.critcount += 1;
//            pl.critsound();
//            Debug.Log("CRITICAL / " + spwn.combo + " Combo");
//            uc.updateText();
//            ResetSpear();
//        }
//    }
//}

using UnityEngine;

public class spear : MonoBehaviour
{
    public float speed = 200f;
    public bool isFiring = false;

    private Vector3 startPos;
    private float targetDist;    // 改用「距離」來判斷是否 Miss，不再綁死 Z 軸
    private float missDist;
    private Vector3 moveDirection; // 儲存長矛應該飛行的方向

    public playsound pl;
    public bool hit = false, shoot = false;
    public updatecombo uc;
    public spawn_new spwn;

    // 💡 核心修改：接收出生點座標、基準點座標，以及 Hint 點的旋轉角度 (rotation)
    // 💡 修正模型預設朝下的 Fire 函式
    public void Fire(Vector3 spawnPosition, Vector3 basePosition, Quaternion hintRotation)
    {
        gameObject.SetActive(true);
        transform.position = spawnPosition;

        // 1. 計算 Hint 點的正前方方向
        Vector3 hintForward = hintRotation * Vector3.forward;

        // 2. 讓物件的 Z 軸先對準前進方向，頭頂朝上
        Quaternion lookRotation = Quaternion.LookRotation(hintForward, Vector3.up);

        // 3. 💡【關鍵修正】：因為模型預設朝下，我們需要沿著 X 軸往上旋轉 90 度
        // 將原本朝下的尖端，翻轉 90 度變成朝向正前方！
        transform.rotation = lookRotation * Quaternion.Euler(90f, 0f, 0f);

        startPos = spawnPosition;

        // 計算 Hint 點與出生點的距離
        targetDist = Vector3.Distance(startPos, basePosition);
        missDist = targetDist + 35f;

        // 關鍵：根據 Hint 點的朝向，計算出飛行的方向（朝向 Hint 點的後方飛過來）
        moveDirection = hintRotation * Vector3.back;

        isFiring = true;
    }

    void Update()
    {
        if (isFiring)
        {
            // 💡 完美修正：沿著計算好的區域方向移動，不再綁死 Vector3.back
            transform.position += moveDirection * speed * Time.deltaTime;

            // 💡 完美修正：計算目前長矛離出生點多遠，超過 135 單位（100 + 35）就判定為 MISS
            float currentDistance = Vector3.Distance(startPos, transform.position);
            if (currentDistance >= missDist)
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