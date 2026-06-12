using UnityEngine;

public class spear : MonoBehaviour
{
    public float speed = 200f;
    public bool isFiring = false;
    public Vector3 pos = new Vector3(0, 10, 300);
    public playsound pl;
    public bool hit =false,shoot = false;
    public target tg;
    public int spx, spy;

    void Start()
    {
        //pos = transform.position = new Vector3(Random.Range(-20f, 20f), Random.Range(-20f, 20f), 300);
        pos = transform.position = new Vector3(0,10, 300);
        isFiring = true;
        //(spx, spy) = (-5, 25);
        //transform.position = new Vector3(spx, spy, 150);
    }

    void Update()
    {
        //if ((Input.GetKeyDown(KeyCode.F)))
        //{
            
        //}

        if (isFiring)
        {

            transform.position += Vector3.back * speed * Time.deltaTime;
            /*if (transform.position.z <= 100f)
            {
                //tg.sethit();
                
            }*/
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
        //Destroy(gameObject);
        transform.position = pos;
        gameObject.SetActive(false);
        isFiring = true;
        //(spx, spy) = (-5, 25);
        //transform.position = new Vector3(spx, spy, 150);
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