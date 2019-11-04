using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject target;
    public GameObject bullet;
    public int numberBullet = 1;
    public float speedBullet = 1f;
    public float heal = 100;

    private float lastTimeFire = 0;

    public AudioSource audioShoot;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        changeSpirtePlayer();
        if (Input.GetKey(KeyCode.J) || Input.GetMouseButton(0))
        {
            if (Time.time - lastTimeFire > 0.2f)
            {
                CreateBullet();
                audioShoot.Play();
                lastTimeFire = Time.time;
            }
        }
    }

    void changeSpirtePlayer()
    {
        float vertical = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.D))
        {
            gameObject.transform.Rotate(0, 0, 90);
            target.transform.Rotate(0, 0, 90);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            gameObject.transform.Rotate(0, 0, -90);
        }

        if (Input.GetKey(KeyCode.W))
        {
            Vector3 transformVec = (target.transform.position - gameObject.transform.position) * Time.deltaTime;
            gameObject.transform.position += transformVec;
        }
    }

    void CreateBullet()
    {
        GameObject b = Instantiate(bullet);
        b.transform.position = gameObject.transform.position;
        b.GetComponent<BulletController>().targetPosition = target.transform.position;
        b.GetComponent<BulletController>().moveSpeed = speedBullet;
    }
    
}
