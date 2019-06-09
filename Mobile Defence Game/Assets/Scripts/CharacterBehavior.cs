using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterBehavior : MonoBehaviour
{
    private CharacterStat characterStat;

    public GameObject bullet;
    private Animator animator;
    private AudioSource audioSource;

    private GameObject bulletObjectPool;
    private objectPooler bulletObjectPooler;

    // Start is called before the first frame update
    void Start()
    {
        characterStat = gameObject.GetComponent<CharacterStat>();
        animator = gameObject.GetComponent<Animator>();
        audioSource = gameObject.GetComponent<AudioSource>();
        if(gameObject.name.Contains("backsusuek"))
        {
            bulletObjectPool = GameObject.Find("Bullet1 Object Pool");
        }
        else if(gameObject.name.Contains("Character 2"))
        {
            bulletObjectPool = GameObject.Find("Bullet2 Object Pool");
        }
        bulletObjectPooler = bulletObjectPool.GetComponent<objectPooler>();
    }

    public void attack(int damage)
    {

        GameObject bullet = bulletObjectPooler.getObject();
        if (bullet == null) return;
        bullet.transform.position = gameObject.transform.position;

        bullet.GetComponent<BulletBehavior>().bulletStat
            = new BulletStat(10 + characterStat.level * 3, characterStat.damage);
      
        animator.SetTrigger("Attack");
        audioSource.PlayOneShot(audioSource.clip);
        bullet.GetComponent<BulletBehavior>().Spawn();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        //마우스 : -1, 모바일 : 0이상
        if (EventSystem.current.IsPointerOverGameObject(-1) == true) return;
        if (EventSystem.current.IsPointerOverGameObject(0) == true) return;

        //위의 소스코드는 만약 UI가 존재한다면 마우스 감지를 하지말아라 라는 소스코드
        if (characterStat.canlevelUP(GameManager.instance.seed))
        {
            characterStat.increaseLevel();
            GameManager.instance.seed -= characterStat.upgradeCost;
            GameManager.instance.updateText();
        }
    }
}
