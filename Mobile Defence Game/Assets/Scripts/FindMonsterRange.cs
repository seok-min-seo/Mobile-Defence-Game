using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindMonsterRange : MonoBehaviour
{
    public GameObject character;
    private CharacterBehavior characterBehavior;
    private float coolTime;
    private float lastAttackTime;


    // Start is called before the first frame update
    void Start()
    {
        characterBehavior = character.GetComponent<CharacterBehavior>();
        coolTime = character.GetComponent<CharacterStat>().cooltime;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Monster")
        {
            if(Time.time - lastAttackTime > coolTime)
            {
                int damage = character.GetComponent<CharacterStat>().damage;
                characterBehavior.attack(damage);
                lastAttackTime = Time.time;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
