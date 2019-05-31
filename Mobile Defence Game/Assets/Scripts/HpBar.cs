using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour
{
    public GameObject image;
    public GameObject character;

    public GameObject parent;
    private CharacterStat characterStat;
    private MonsterStats monsterStat;


    public float max = 100;
    public float current = 100;
    private float scale;

    private int maxHp = 100;
    private int hp = 100;

    

    // Start is called before the first frame update
    void Start()
    {
        scale = image.transform.localScale.x;
        characterStat = character.GetComponent<CharacterStat>();
        if(parent.name.Contains("backsusuek") || parent.name.Contains("Character"))
        {
            characterStat = parent.GetComponent<CharacterStat>();
        }
        else if(parent.name.Contains("Monster"))
        {
            monsterStat = parent.GetComponent<MonsterStats>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (characterStat != null)
        {
             maxHp = characterStat.maxHP;
             hp = characterStat.hp;
        }
        else if(monsterStat != null)
        {
             maxHp = monsterStat.maxHp;
             hp = monsterStat.hp;
        }
        current = (float) hp / (float)maxHp * 100;
        if(current < 0)
        {
            current = 0;
        }

        Vector2 temp = image.transform.localScale;
        temp.x = current / max * scale;
        image.transform.localScale = temp;
    }
}
