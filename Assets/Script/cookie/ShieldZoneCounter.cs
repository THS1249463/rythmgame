using UnityEngine;

public class ShieldZoneCounter : MonoBehaviour
{
    public GameObject shieldMesh; // 拖入底下的 Shield_Mesh
    private int pointingHandCount = 0; // 目前指著這個區域的手部數量

    // 當有手指向這裡時，由手的腳本來呼叫
    public void AddHand()
    {
        pointingHandCount++;
        UpdateShieldState();
    }

    // 當有手移開這裡時，由手的腳本來呼叫
    public void RemoveHand()
    {
        pointingHandCount--;
        if (pointingHandCount < 0) pointingHandCount = 0; // 安全機制
        UpdateShieldState();
    }

    private void UpdateShieldState()
    {
        if (shieldMesh != null)
        {
            // 只要指著的手數量大於 0，盾牌就必須亮著！
            shieldMesh.SetActive(pointingHandCount > 0);
        }
    }

    // 當遊戲開始或重置時，確保歸零
    public void ResetCounter()
    {
        pointingHandCount = 0;
        if (shieldMesh != null) shieldMesh.SetActive(false);
    }
}