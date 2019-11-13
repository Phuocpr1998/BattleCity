using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public GameObject target;
    public GameObject bullet;
    public float speedBullet = 10f;
    public float heal = 100;

    private float lastTimeFire = 0;
    private bool isMove = true;
    private bool isLeft = false;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0f;
        gameObject.GetComponent<Rigidbody2D>().fixedAngle = true;
    }

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
        if (isLeft)
        {
            gameObject.transform.Rotate(0, 0, -90);
            isLeft = false;
        }

        if (isMove)
        {
            Vector3 transformVec = target.transform.position - gameObject.transform.position;
            gameObject.GetComponent<Rigidbody2D>().velocity = transformVec * 2;
        }
    }

    void CreateBullet()
    {
        GameObject b = Instantiate(bullet);
        b.transform.position = gameObject.transform.position;
        b.GetComponent<BulletEnemyController>().targetPosition = target.transform.position;
        b.GetComponent<BulletEnemyController>().moveSpeed = speedBullet;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isLeft = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            heal -= collision.gameObject.GetComponent<BulletController>().heal;
            if (heal <= 0)
            {
                Destroy(gameObject);
                GameObject numberEnemy = GameObject.FindGameObjectWithTag("numberEnemy");
                numberEnemy.GetComponent<Text>().text = (GameObject.FindGameObjectsWithTag("enemy").Length - 1).ToString();
                if (GameObject.FindGameObjectsWithTag("enemy").Length == 1)
                {
                    GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
                    if (gameController != null)
                    {
                        gameController.GetComponent<GameControler>().NextMap();
                    }
                }
            }
        }
    }
}
