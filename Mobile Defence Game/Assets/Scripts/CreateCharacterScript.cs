using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CreateCharacterScript : MonoBehaviour
{
    public GameObject characterPrefab1;
    public GameObject characterPrefab2;

    private GameObject CharacterPrefab;
    private GameObject character;
    private AudioSource audioSource;
    private GameManager gameManager;

    private CharacterStat characterStat;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
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

        if (gameManager.nowSelect == 1)
        {
            CharacterPrefab = characterPrefab1;
            characterStat = characterPrefab1.GetComponent<CharacterStat>();
        }
        else
        {
            CharacterPrefab = characterPrefab2;
            characterStat = characterPrefab2.GetComponent<CharacterStat>();
        }

        if (character == null)
        {
            CharacterStat characterStat = CharacterPrefab.GetComponent<CharacterStat>();
            if (characterStat.canCreate(gameManager.seed))

            {
                character = (GameObject)Instantiate(CharacterPrefab, transform.position, Quaternion.identity);
                audioSource.PlayOneShot(audioSource.clip);
                gameManager.seed -= character.GetComponent<CharacterStat>().cost;
                gameManager.updateText();
            }
        }
    }

}
