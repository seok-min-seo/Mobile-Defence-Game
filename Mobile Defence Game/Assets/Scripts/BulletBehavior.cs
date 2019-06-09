using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public GameObject character;
    public BulletStat bulletStat { get; set; }

    public float activeTime = 3.0f;
    public float spawnTime;

    public BulletBehavior()
    {
        bulletStat = new BulletStat(0, 0);
    }

    public void Spawn()
    {
        gameObject.SetActive(true);
        spawnTime = Time.time;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - spawnTime >= activeTime)
        {
            gameObject.SetActive(false);
        }
        else
        {
            transform.Translate(Vector2.right * bulletStat.speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Monster")
        {
            gameObject.SetActive(false);
            other.GetComponent<MonsterStats>().attacked(bulletStat.damage);
        }
    }
}
