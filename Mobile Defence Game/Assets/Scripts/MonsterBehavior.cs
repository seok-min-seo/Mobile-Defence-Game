using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehavior : MonoBehaviour
{
    
    private MonsterStats monsterStat;
    private Animator animator;
    private bool attacking = false;

    private float lastAttackTime;
    public CharacterStat targetStat;

    public bool died = false;
    // Start is called before the first frame update
    void Start()
    {
        //겜매니저는 GameObject.FInd문으로 찾고 겟오브젝트를 써야한다
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.PlayOneShot(audioSource.clip);
        animator = gameObject.GetComponent<Animator>();
        monsterStat = gameObject.GetComponent<MonsterStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if(died)
        {
            attacking = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        transform.Translate(Vector2.left * monsterStat.speed * Time.deltaTime);
        if(attacking)
        {
            transform.Translate(Vector2.right * monsterStat.speed * Time.deltaTime); 
        }
        if(targetStat != null && targetStat.hp <= 0)
        {
            targetStat = null;
            attacking = false;
            animator.SetTrigger("Walk");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Fence")
        {
            Destroy(gameObject);
            
            GameManager.instance.decreaseLife();
        }
        else if(other.gameObject.tag == "Character")
        {
            attacking = true;
            lastAttackTime = Time.time;
            animator.SetTrigger("Attack");
            targetStat = other.gameObject.GetComponent<CharacterStat>();
        }
    }

    private void OnTriggerStay2D(Collider2D other) //계속해서 반복해서 충돌하고잇는 경우 발생하는 함수
    {
       if(other.gameObject.tag == "Character")
        {
            if (Time.time - lastAttackTime > monsterStat.cooltime)
            {
                
                int hp = other.gameObject.GetComponent<CharacterStat>().attacked(monsterStat.damage);
                animator.SetTrigger("Attack");
                if(hp <=0)
                {
                    attacking = false;
                    animator.SetTrigger("Walk");
                }
                lastAttackTime = Time.time;
            }
        }
    }
}
