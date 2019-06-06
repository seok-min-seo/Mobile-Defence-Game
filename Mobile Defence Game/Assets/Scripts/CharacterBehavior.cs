using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterBehavior : MonoBehaviour
{
    private CharacterStat characterStat;
    private GameManager gameManager;

    public GameObject bullet;
    private Animator animator;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        characterStat = gameObject.GetComponent<CharacterStat>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        animator = gameObject.GetComponent<Animator>();
        audioSource = gameObject.GetComponent<AudioSource>();

    }

    public void attack(int damage)
    {
        animator.SetTrigger("Attack");
        audioSource.PlayOneShot(audioSource.clip);
        GameObject currentBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        currentBullet.GetComponent<BulletBehavior>().bulletStat
            = new BulletStat(10 + characterStat.level * 3, characterStat.damage);
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
        if (characterStat.canlevelUP(gameManager.seed))
        {
            characterStat.increaseLevel();
            gameManager.seed -= characterStat.upgradeCost;
            gameManager.updateText();
        }
    }
}
