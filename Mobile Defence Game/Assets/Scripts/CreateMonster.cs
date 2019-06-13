using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMonster : MonoBehaviour
{

    public List<GameObject> respawnSpotList;

    public GameObject monster1Prefab;
    public GameObject monster2Prefab;
    private GameObject monsterPrefab;
    
    

    
    private int spawnCount = 0;     //스폰된 숫자
    private IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
        
        monsterPrefab = monster1Prefab;
        coroutine = process();
        StartCoroutine(coroutine);
    }

    void Create()
    {
        //몬스터 생성 함수 시작
          
        int index = Random.Range(0, 4); //랜덤하게 1~4까지의 숫자를 박음
        GameObject respawnSpot = respawnSpotList[index];
        Instantiate(monsterPrefab, respawnSpot.transform.position, Quaternion.identity); //프리팹으로 몬스터를 박아주고
        GameManager.instance.monsterAddCount++;
        spawnCount += 1;
    }

    IEnumerator process()
    {
        while(true)
        {

            if (GameManager.instance.round > GameManager.instance.totalRound)
            {
                StopCoroutine(coroutine);
                
            }
            if (spawnCount < GameManager.instance.spawnNumber)
            {
                Create();

            }
            Debug.Log(GameManager.instance.spawnNumber);
            Debug.Log(spawnCount);
            if(spawnCount == GameManager.instance.spawnNumber &&
                GameObject.FindGameObjectWithTag("Monster") == null)
                
            {
                
                if (GameManager.instance.totalRound == GameManager.instance.round)
                {
                    
                    GameManager.instance.gameClear();
                    GameManager.instance.round += 1;
                }
                else
                {
                    
                    GameManager.instance.clearRound();                           //겜매니저에서 만든 클리어라운드를 실행
                    spawnCount = 0;
                  if (GameManager.instance.round == 4)
                    {
                        monsterPrefab = monster2Prefab;
                        GameManager.instance.spawnTime = 2.0f;
                        GameManager.instance.spawnNumber = 10;
                    }
                }
            }
            if (spawnCount == 0) yield return new WaitForSeconds(GameManager.instance.roundReadyTime);
            else yield return new WaitForSeconds(GameManager.instance.spawnTime);
        }
    }

    
}
