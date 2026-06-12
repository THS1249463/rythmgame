using UnityEngine;

public class spawn : MonoBehaviour
{
    public GameObject spr;
    public target tg;
    public playsound pl;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            GameObject spears = Instantiate(
                spr,
                new Vector3(0, 10, 150),
                Quaternion.Euler(90, 0, 0)
            );
            spear sp = spears.GetComponent<spear>();
            sp.isFiring = true;
        }
    }
}