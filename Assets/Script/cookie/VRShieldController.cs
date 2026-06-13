using UnityEngine;
using UnityEngine.InputSystem; // 必須使用新版 Input System

public class VRShieldController : MonoBehaviour
{
    [Header("輸入設定 (Action Settings)")]
    // 在 Inspector 中指定你要的手把側鍵（例如：Left Hand / Grip 或 Right Hand / Grip）
    [SerializeField] private InputActionProperty gripAction;

    [Header("基準座標點設定")]
    // 將你希望作為基準的目標物件（例如 Point0）拉進來
    [SerializeField] private Transform targetObject;

    [Header("盾牌視覺物件 (把盾牌的 Mesh/外觀物件拉進來)")]
    [SerializeField] private GameObject shieldVisual;

    void OnEnable()
    {
        // 訂閱事件：當側鍵被「確實按下 (Performed)」與「放開 (Canceled)」
        gripAction.action.performed += OnGripPressed;
        gripAction.action.canceled += OnGripReleased;
    }

    void OnDisable()
    {
        // 取消訂閱，維持良好的記憶體管理習慣
        gripAction.action.performed -= OnGripPressed;
        gripAction.action.canceled -= OnGripReleased;
    }

    void Start()
    {
        // 遊戲一開始，預設讓盾牌是隱藏的
        if (shieldVisual != null)
        {
            shieldVisual.SetActive(false);
        }
    }

    void Update()
    {
        // 💡 保持 Z 軸死死對齊目標座標點
        if (targetObject != null)
        {
            // 抓取目標物件的 X、Y、Z，讓盾牌在空間中位置與防線完美並行
            transform.position = new Vector3(
                targetObject.position.x,
                targetObject.position.y,
                targetObject.position.z
            );
        }
    }

    // 當按下側鍵時觸發
    private void OnGripPressed(InputAction.CallbackContext context)
    {
        if (shieldVisual != null)
        {
            shieldVisual.SetActive(true); // 產生/顯示盾牌
            Debug.Log("VR 側鍵按下：產生盾牌！");
        }
    }

    // 當放開側鍵時觸發
    private void OnGripReleased(InputAction.CallbackContext context)
    {
        if (shieldVisual != null)
        {
            shieldVisual.SetActive(false); // 消失/隱藏盾牌
            Debug.Log("VR 側鍵放開：盾牌消失！");
        }
    }
}