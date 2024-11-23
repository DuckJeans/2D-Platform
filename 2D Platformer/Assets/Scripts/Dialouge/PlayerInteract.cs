using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [Header("��ȭ �ý���")]
    public GameObject DialogueBackGround;
    public TextMeshProUGUI nameText; //NPC �̸�
    public TextMeshProUGUI dialogueText; //NPC ���̾�α� ���
    public GameObject dialogueObject;
    [Space]
    public bool hasExistNPC = false;
    public NPCText currentNPC; //NPC�̸��� ã�� ����
    public float typespeed; //���ڰ� ��µǴ� �ӵ�

    private Queue<string> lines = new Queue<string>();
    private string currentText;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        //nameText �ؽ�Ʈ�� �޾ƿͼ� �������ִ� ������Ʈ - ���� â�� �Ʒ� �׸��� ã�ƿͶ�
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
        // �÷��̾ E��ư�� �������� + NPC�� �ֺ��� �����Ѱ�
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
        // npc�� ������ ȭ�鿡 ���
        nameText.text = currentNPC.npcName;
        currentText = lines.Dequeue();
        
        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentText));

        // n�� �Ŀ� 1���� ����Ͻÿ�.

        //Ű����� EŰ�� ������ ���

        //����ϱ� ��ư�� Ŭ���ؼ� ���
    }

    IEnumerator TypeSentence(string currentLine)
    {
        //���ڸ��� ��µǴ� �ð�
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
