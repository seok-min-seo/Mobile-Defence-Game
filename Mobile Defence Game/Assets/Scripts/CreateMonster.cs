using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMonster : MonoBehaviour
{

    public List<GameObject> respawnSpotList;

    public GameObject monster1Prefab;
   
    public GameObject monster2Prefab;
    
    private GameObject monsterPrefab;

    private float lastSpawnTime;    //마지막으로 스폰된 시간
    private int spawnCount = 0;     //스폰된 숫자

    // Start is called before the first frame update
    void Start()
    {
        
        monsterPrefab = monster1Prefab;
        lastSpawnTime = Time.time;  //마지막으로 스폰된 시간에 현재 시간을 박아줌
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.round <= GameManager.instance.totalRound) //지금 라운드가 전체라운드보다 작거나같다면 즉 겜이 진행중이라면
        {
            float timeGap = Time.time - lastSpawnTime;  //현재시간 - 마지막으로 스폰된 시간
            if (((spawnCount == 0 && timeGap > GameManager.instance.roundReadyTime) //새 라운드가 시작되면 몬스터를 만듦
            || timeGap > GameManager.instance.spawnTime)     //또는 스폰시간이 갭보다 크면 몬스터를 만듦
            && spawnCount < GameManager.instance.spawnNumber)    //그리고 스폰숫자가 정해진 스폰숫자보다 작으면 즉 라운드가 진행중이라면 몬스터를만듦
            {
                //몬스터 생성 함수 시작
                lastSpawnTime = Time.time;  //우선 마지막으로 만든 시간에 현재시간을 박음     
                int index = Random.Range(0, 4); //랜덤하게 1~4까지의 숫자를 박음
                GameObject respawnSpot = respawnSpotList[index];
                Instantiate(monsterPrefab, respawnSpot.transform.position, Quaternion.identity); //프리팹으로 몬스터를 박아주고
                spawnCount += 1;                    // 스폰숫자를 올려준다
            }
            if (spawnCount == GameManager.instance.spawnNumber &&                //스폰숫자가 스폰넘버랑 같거나
                GameObject.FindGameObjectWithTag("Monster") == null)    //게임오브젝트를 찾아봤을떄 전장에 반응하지않으면
            {
                GameManager.instance.clearRound();                           //겜매니저에서 만든 클리어라운드를 실행
                spawnCount = 0;
                lastSpawnTime = Time.time;

                if(GameManager.instance.round >= 4)
                {
                    monsterPrefab = monster2Prefab;
                    GameManager.instance.spawnTime = 2.0f;
                    GameManager.instance.spawnNumber = 10;
                }
            }
        }
    }
}
