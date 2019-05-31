using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStats : MonoBehaviour
{
    public float speed = 1.0f;
    public int damage;
    public float cooltime = 2.0f;
    public int hp = 20;
    public int maxHp = 20;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    public int attacked(int damage)
    {
        hp = hp - damage;
        if(hp <= 0)
        {
            animator.SetTrigger("Die");
            Destroy(gameObject, 1.0f);
            gameObject.GetComponent<MonsterBehavior>().died = true;            
        }
        return hp;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
