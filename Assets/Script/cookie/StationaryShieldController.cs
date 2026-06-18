using UnityEngine;
using UnityEngine.InputSystem;

public class StationaryShieldController : MonoBehaviour
{
    [Header("手部射線設定")]
    public Transform handTransform;
    public float maxRayDistance = 200f;

    [Header("輸入設定 (Action)")]
    public InputActionReference triggerAction;

    private ShieldZoneCounter currentTargetCounter; // 💡 改為記錄目前的計數器組件
    private bool isTriggerPressed = false;
    private LineRenderer lineRenderer;
    private int layerMask;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer != null) lineRenderer.enabled = false;
        layerMask = LayerMask.GetMask("ShieldZone");
    }

    private void OnEnable()
    {
        triggerAction.action.started += context => {
            isTriggerPressed = true;
            if (lineRenderer != null) lineRenderer.enabled = true;
        };
        triggerAction.action.canceled += context => {
            isTriggerPressed = false;
            if (lineRenderer != null) lineRenderer.enabled = false;
            LeaveCurrentZone(); // 放開按鈕等於手移開
        };
    }

    private void OnDisable()
    {
        triggerAction.action.started -= context => isTriggerPressed = true;
        triggerAction.action.canceled -= context => isTriggerPressed = false;
    }

    void Update()
    {
        if (isTriggerPressed)
        {
            CheckShieldRaycast();
        }
    }

    private void CheckShieldRaycast()
    {
        Ray ray = new Ray(handTransform.position, handTransform.forward);
        RaycastHit hit;

        Vector3 lineEndPoint = handTransform.position + (handTransform.forward * maxRayDistance);

        if (Physics.Raycast(ray, out hit, maxRayDistance, layerMask))
        {
            lineEndPoint = hit.point;

            if (hit.collider.CompareTag("ShieldTriggerZone"))
            {
                Transform pointParent = hit.collider.transform.parent;

                if (pointParent != null)
                {
                    // 💡 關鍵修改：取得該點父物件上的計數器組件
                    ShieldZoneCounter newCounter = pointParent.GetComponent<ShieldZoneCounter>();

                    if (newCounter != null)
                    {
                        // 如果射線換到了新的點
                        if (currentTargetCounter != newCounter)
                        {
                            LeaveCurrentZone(); // 先離開舊的點（舊點人數 -1）
                            currentTargetCounter = newCounter;
                            currentTargetCounter.AddHand(); // 進入新的點（新點人數 +1）
                        }

                        DrawLaser(handTransform.position, lineEndPoint);
                        return;
                    }
                }
            }
        }

        // 沒射中任何大碰撞箱，執行離開邏輯
        LeaveCurrentZone();
        DrawLaser(handTransform.position, lineEndPoint);
    }

    private void DrawLaser(Vector3 start, Vector3 end)
    {
        if (lineRenderer != null && lineRenderer.enabled)
        {
            lineRenderer.SetPosition(0, start);
            lineRenderer.SetPosition(1, end);
        }
    }

    private void LeaveCurrentZone()
    {
        if (currentTargetCounter != null)
        {
            currentTargetCounter.RemoveHand(); // 告訴該點：「我走了一隻手」
            currentTargetCounter = null;
        }
    }
}