using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject target;
    public GameObject bullet;
    private float speedShip = 5f;
    public float speedBullet = 10f;
    public float heal = 100;

    private float lastTimeFire = 0;

    // Start is called before the first frame update
    void Start()
    {}

    // Update is called once per frame
    void Update()
    {
        changeSpirtePlayer();

        if (Time.time - lastTimeFire > 1f)
        {
            CreateBullet();
            lastTimeFire = Time.time;
        }
    }

    void changeSpirtePlayer()
    {
        float vertical = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.D))
        {
            gameObject.transform.Rotate(0, 0, 90);
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
        b.GetComponent<BulletEnemyController>().targetPosition = target.transform.position;
        b.GetComponent<BulletEnemyController>().moveSpeed = speedBullet;
    }
}
