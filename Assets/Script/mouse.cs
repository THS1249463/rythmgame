using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public float distance = 100f;
    public bool mousefollow = true;

    [Header("引用 Spawn 腳本以獲取基準 Z 軸")]
    [SerializeField] private Transform targetObject;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            mousefollow = !mousefollow;
            Debug.Log(mousefollow ? "follow" : "deactivated");
        }

        // 💡 自動獲取基準點 Z 軸
        float targetZ = 0f;
        if (targetObject != null)
        {
            targetZ = targetObject.position.z;
        }
        else
        {
            Debug.LogWarning("FollowMouse 腳本中的 Target Object 尚未指派！");
        }   

        if (mousefollow)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = distance;

            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

            // 盾牌固定在基準點的 Z 軸平面上
            transform.position = new Vector3(
                worldPos.x,
                worldPos.y,
                targetZ
            );
        }
        else
        {
            transform.position = new Vector3(0, -100, targetZ);
        }
    }
}