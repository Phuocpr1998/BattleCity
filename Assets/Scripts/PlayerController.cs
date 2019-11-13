using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject target;
    public GameObject bullet;
    public float speedBullet = 1f;
    public float heal = 100;

    private float lastTimeFire = 0;

    public AudioSource audioShoot;


    private bool movePermission;

    // Start is called before the first frame update
    void Start()
    {
        movePermission = true;
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0f;
        gameObject.GetComponent<Rigidbody2D>().fixedAngle = true;
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
            movePermission = true;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            gameObject.transform.Rotate(0, 0, -90);
            movePermission = true;
        }

        if (Input.GetKey(KeyCode.W) && movePermission)
        {
            Vector3 transformVec = target.transform.position - gameObject.transform.position;
            gameObject.GetComponent<Rigidbody2D>().velocity = transformVec*5;
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    void CreateBullet()
    {
        GameObject b = Instantiate(bullet);
        b.transform.position = gameObject.transform.position;
        b.GetComponent<BulletController>().targetPosition = target.transform.position;
        b.GetComponent<BulletController>().moveSpeed = speedBullet;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bulletEnemy")
        {
            heal -= collision.gameObject.GetComponent<BulletEnemyController>().heal;
            if (heal <= 0)
            {
                GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
                if (gameController != null)
                {
                    gameController.GetComponent<GameControler>().GameOver();
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        movePermission = false;
    }
}
