using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickController : MonoBehaviour
{
    public float heal = 30;
    // Start is called before the first frame update
    void Start()
    {}

    // Update is called once per frame
    void Update()
    {}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            heal -= collision.gameObject.GetComponent<BulletController>().heal;
        }
        else if (collision.gameObject.tag == "bulletEnemy")
        {
            heal -= collision.gameObject.GetComponent<BulletEnemyController>().heal;
        }

        if (heal <= 0)
        {
            Destroy(gameObject);
        }
    }
}
