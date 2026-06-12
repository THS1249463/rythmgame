using UnityEngine;

public class spear : MonoBehaviour
{
    public float speed = 200f;
    public bool isFiring = false;
    public Vector3 pos = new Vector3(0, 10, 150);
    public playsound pl;
    public bool hit =false;
    public target tg;

    void Start()
    {
        pos = transform.position = new Vector3(Random.Range(-20f, 20f), Random.Range(-20f, 20f), 150);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isFiring = true;
        }

        if (isFiring)
        {
            transform.position += Vector3.back * speed * Time.deltaTime;
            if (transform.position.z <= 100f)
            {
                tg.sethit();
                
            }
            if (transform.position.z <= -22f)
            {
                Debug.Log("MISS");
                ResetSpear();
            }
        }
    }

    void ResetSpear()
    {
        isFiring = false;
        Destroy(gameObject);
        //transform.position = new Vector3(Random.Range(-30f, 30f), Random.Range(0f, 25f), 150);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "shield" && isFiring)
        {
            pl.critsound();
            Debug.Log("CRITICAL");
            ResetSpear();
        }
    }
}