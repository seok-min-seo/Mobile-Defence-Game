using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    
    public BulletStat bulletStat { get; set; }

    public float activeTime = 3.0f;
    

    public BulletBehavior()
    {
        bulletStat = new BulletStat(0, 0);
    }

    public GameObject character;

    public void Spawn()
    {
        gameObject.SetActive(true);
        
    }

    private void OnEnable() //특정한 오브젝트가 활성화 처리 되었을때 자동으로 불러와지는 유니티의 함수
    {
        StartCoroutine(BulletInactive(activeTime));
    }

    IEnumerator BulletInactive(float activeTime)
    {
        yield return new WaitForSeconds(activeTime);
        gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
       
        transform.Translate(Vector2.right * bulletStat.speed * Time.deltaTime);
        
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
