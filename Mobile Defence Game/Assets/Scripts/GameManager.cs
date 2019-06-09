using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; set;}

    private void Awake() //start나 update보다 먼저 수행
    {
      if(instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    public int bulletAddCount = 0;
    public int characterAddCount = 0;
    public int monsterAddCount = 0;


    public Text seedText;
    public Text roundText;
    public Text roundStartText;

    private AudioSource audioSource;

    public int round = 0;
    public int seed = 1000;

    public int roundReadyTime = 5;     
    public int totalRound = 9;
    public int reward = 500;
    public float spawnTime = 2.5f;
    public int spawnNumber = 5;

    public int nowSelect;
    public Image select1;
    public Image select2;

    public Text clearText;
    public Text lifetext;

    public int life = 10;
    public Text loseText;

    public GameObject responSpot;

    public int decreaseLife()
    {
        if(life >= 1)
        {
            life = life - 1;
            lifetext.text = " X " + life;
            if(life == 0)
            {
                loseText.enabled = true;
                responSpot.GetComponent<CreateMonster>().enabled = false; // 몬스터 리젠 없애기
            }
        }
        return life;
    }

    public void gameClear()
    {
        clearText.enabled = true;
        responSpot.GetComponent<CreateMonster>().enabled = false;
    }
   

    public void select(int number)
    {
        if(number == 1)
        {
            nowSelect = 1;
            select1.GetComponent<Image>().color = Color.gray;
            select2.GetComponent<Image>().color = Color.white;
        }
        else
        {
            nowSelect = 2;
            select1.GetComponent<Image>().color = Color.white;
            select2.GetComponent<Image>().color = Color.gray; 
        }
        

    }
    


    public void clearRound()
    {
        if(round < totalRound)
        {
            nextRound();        //넥스트라운드 실행
            seed += reward;     // seed 보상 주고
            updateText();       //업데이트 텍스트 실행 보상받은만큼 텍스트 표기
            spawnTime -= 0.2f;  //몬스터 스폰 시간을 줄여서 난이도 상승
            spawnNumber += 3; // 이것도 이제 더 많이 나오게 시킴
            reward += 150;      // 다음에 있을 보상 150원씩 더 올려줌

            Debug.Log("총알 생성: " + bulletAddCount);
            Debug.Log("캐릭터 생성: " + characterAddCount);
            Debug.Log("몬스터 생성: " + monsterAddCount);
        }
        else if(round >= totalRound)
        {
            gameClear();

        }
    }

    public void nextRound()
    {
        round = round + 1;
        if(round == 1)
        {
            roundText.text = "ROUND 0" + round;
            roundStartText.text = "ROUND 0" + round;
        }
        else if(round < 10)
        {
            roundText.text = "ROUND 0" + round;
            roundStartText.text = "ROUND 0" + round;
            roundStartText.GetComponent<Animator>().SetTrigger("Round Start");
        }
        else
        {
            roundText.text = "ROUND " + round;
            roundStartText.text = "ROUND " + round;
            roundStartText.GetComponent<Animator>().SetTrigger("Round Start");

        }
        audioSource.PlayOneShot(audioSource.clip);
    }



    public void updateText()
    {
        seedText.text = "씨앗 " + seed;
    }


    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1920, 1200, true);
        clearText.enabled = false;
        loseText.enabled = false;
        audioSource = roundStartText.GetComponent<AudioSource>();
        updateText();
        nextRound();
        select(1);
        lifetext.text = life.ToString(); //  현재 남아있는 텍스트값을 넣어준다
    }

    // Update is called once per frame
    void Update()
    {

    }
}
