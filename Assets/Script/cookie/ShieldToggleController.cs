using UnityEngine;
using UnityEngine.InputSystem;

public class ShieldToggleController : MonoBehaviour
{
    [Header("盾牌物件")]
    public GameObject shieldObject; // 拖入階層中的 Shield Thing

    [Header("輸入設定 (Action)")]
    public InputActionReference triggerAction; // 拖入右手的 Activate Action

    private void OnEnable()
    {
        // 監聽按鈕按下與放開
        triggerAction.action.started += OnTriggerPressed;
        triggerAction.action.canceled += OnTriggerReleased;
    }

    private void OnDisable()
    {
        triggerAction.action.started -= OnTriggerPressed;
        triggerAction.action.canceled -= OnTriggerReleased;
    }

    private void OnTriggerPressed(InputAction.CallbackContext context)
    {
        if (shieldObject != null)
        {
            shieldObject.SetActive(true); // 按下時顯示盾牌
        }
    }

    private void OnTriggerReleased(InputAction.CallbackContext context)
    {
        if (shieldObject != null)
        {
            shieldObject.SetActive(false); // 放開時隱藏盾牌
        }
    }
}