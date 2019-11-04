using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemyController : MonoBehaviour
{
    public GameObject explode;
    public Vector3 targetPosition;
    public float moveSpeed;
    public float heal = 10;

    Rigidbody2D myBody;
    Vector3 vecFire;

    // Start is called before the first frame update
    void Start()
    {
        vecFire = targetPosition - transform.position;
        if (targetPosition.x - transform.position.x <= 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, Vector3.Angle(Vector3.up, vecFire));
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, -1 * Vector3.Angle(Vector3.up, vecFire));
        }
        myBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        myBody.velocity = vecFire * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject ex = Instantiate(explode);
        ex.transform.position = gameObject.transform.position;
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "enemy")
        {
            GameObject ex = Instantiate(explode);
            ex.transform.position = gameObject.transform.position;
            Destroy(gameObject);
        }
    }

}
