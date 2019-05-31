using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat : MonoBehaviour
{
    public int level = 1;       //캐릭터의 레벨
    public int hp = 30;     //현재 체력 값
    public int maxHP = 30;  // 최대 체력
    public int damage = 5;  // 캐릭터의 공격력
    public int cost = 130; // 캐릭터 구매 코스트
    public int upgradeCost = 200; // 캐릭터 업그레이드 코스트
    public float cooltime = 2.0f;


    private Animator animator;

    public int attacked(int damage)
    {
        hp = hp - damage;
        if(hp <= 0)
        {
            animator.SetTrigger("Die");
            Destroy(gameObject, 1.5f);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        return hp;
    }

    public bool canCreate(int seed)
    {
        if(cost <= seed)
        {
            return true;
        }
        return false;
    }
    

    // 레벨업이 가능한지 여부를 반환합니다.
    public bool canlevelUP(int seed)
    {
        if(level < 3)
        {
            if (upgradeCost <= seed)
            {
                return true;
            }
            return false;
        }
        else
        {
            return false;
        }
    }

    public void increaseLevel()
    {
        if(level == 1)
        {
            level = 2;
            maxHP += 25;
            hp = maxHP;
            damage += 5;
            transform.localScale += new Vector3(0.01f, 0.01f, 0);
        }
        else if(level == 2)
        {
            level = 3;
            maxHP += 50;
            hp = maxHP;
            damage += 5;
            transform.localScale += new Vector3(0.01f, 0.01f, 0);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
