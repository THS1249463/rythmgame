using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public float distance = 100f;
    bool mousefollow = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            mousefollow = !mousefollow;
            Debug.Log(mousefollow ? "follow" : "deactivated");
        }

        if (mousefollow)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = distance;

            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

            transform.position = new Vector3(
                worldPos.x,
                worldPos.y,
                transform.position.z
            );
        }
    }
}