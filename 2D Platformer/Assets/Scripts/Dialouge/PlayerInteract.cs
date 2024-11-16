using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [Header("대화 시스템")]
    public GameObject DialogueBackGround;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public GameObject dialogueObject;
    [Space]
    public bool hasExistNPC = false;
    public NPCText currentNPC;

    // Start is called before the first frame update
    void Start()
    {
        //nameText 텍스트를 받아와서 실행해주는 컴포넌트 - 게임 창에 아래 항목을 찾아와라
        nameText = GameObject.Find("Canvas/Dialogue Text Background/NPC Name").GetComponent<TextMeshProUGUI>();
        dialogueText = GameObject.Find("Canvas/Dialogue Text Background/Dialouge_Text").GetComponent<TextMeshProUGUI>();
        // 활성화되어 있는 채팅화면을 꺼라
        dialogueObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        InteractNPC(hasExistNPC);
    }

    public void InteractNPC(bool canSpeak)
    {
        // 플레이어가 E버튼을 눌렀을때 + NPC가 주변에 존재한가
        if (Input.GetKeyDown(KeyCode.E) && canSpeak) // && = and
        {
            EnableDialogue();
        }
    }

    private void EnableDialogue()
    {
        //대화창이 비활성화 되어있는 상태에서 활성화 상태로 바꿈
        if (DialogueBackGround.activeInHierarchy)
        {
            DialogueBackGround.SetActive(false);
        }
        else
        {
            TypeText();
        }
    }

    private void TypeText()
    {
        DialogueBackGround.SetActive(true);
        // npc의 정보를 화면에 출력
        nameText.text = currentNPC.npcName;
        dialogueText.text = currentNPC.dialogues[0];

        // n초 후에 1번을 출력하시오.

        //키보드로 E키를 누르면 출력

        //계속하기 버튼을 클릭해서 출력
    }

    public void CloseText()
    {
        DialogueBackGround.SetActive(false);
    }
}
