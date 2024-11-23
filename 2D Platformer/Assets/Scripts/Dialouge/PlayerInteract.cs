using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [Header("대화 시스템")]
    public GameObject DialogueBackGround;
    public TextMeshProUGUI nameText; //NPC 이름
    public TextMeshProUGUI dialogueText; //NPC 다이얼로그 출력
    public GameObject dialogueObject;
    [Space]
    public bool hasExistNPC = false;
    public NPCText currentNPC; //NPC이름을 찾아 적용
    public float typespeed; //글자가 출력되는 속도

    private Queue<string> lines = new Queue<string>();
    private string currentText;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        //nameText 텍스트를 받아와서 실행해주는 컴포넌트 - 게임 창에 아래 항목을 찾아와라
        nameText = GameObject.Find("Canvas/Dialogue Text Background/NPC Name").GetComponent<TextMeshProUGUI>();
        dialogueText = GameObject.Find("Canvas/Dialogue Text Background/Dialouge_Text").GetComponent<TextMeshProUGUI>();
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
        animator.Play("Show");
        TypeText();
    }

    public void GetDialougeByNPC(NPCText npc)
    {
        foreach (var line in npc.dialogues)
        {
            lines.Enqueue(line);
        }
    }

    private void TypeText()
    {
        if(lines.Count == 0)
        {
            EndDialouge();
            return;
        }

        DialogueBackGround.SetActive(true);
        // npc의 정보를 화면에 출력
        nameText.text = currentNPC.npcName;
        currentText = lines.Dequeue();
        
        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentText));

        // n초 후에 1번을 출력하시오.

        //키보드로 E키를 누르면 출력

        //계속하기 버튼을 클릭해서 출력
    }

    IEnumerator TypeSentence(string currentLine)
    {
        //글자마다 출력되는 시간
        dialogueText.text = ("");
        foreach(char letter in currentLine)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typespeed);
        }
    }

    private void EndDialouge()
    {
        lines.Clear();
        animator.Play("Hide");
    }

    public void CloseText()
    {
        lines.Clear();
        animator.Play("Hide");
    }
}
