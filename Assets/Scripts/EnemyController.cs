using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject target;
    public GameObject bullet;
    public float speedBullet = 10f;
    public float heal = 100;

    private float lastTimeFire = 0;
    private bool isMove = true;
    private bool isLeft = false;
    private bool isRight = false;

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
        if (isRight)
        {
            gameObject.transform.Rotate(0, 0, 90);
            isRight = false;
        }
        else if (isLeft)
        {
            gameObject.transform.Rotate(0, 0, -90);
            isLeft = false;
        }

        if (isMove)
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
