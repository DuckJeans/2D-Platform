using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [Header("��ȭ �ý���")]
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
        //nameText �ؽ�Ʈ�� �޾ƿͼ� �������ִ� ������Ʈ - ���� â�� �Ʒ� �׸��� ã�ƿͶ�
        nameText = GameObject.Find("Canvas/Dialogue Text Background/NPC Name").GetComponent<TextMeshProUGUI>();
        dialogueText = GameObject.Find("Canvas/Dialogue Text Background/Dialouge_Text").GetComponent<TextMeshProUGUI>();
        // Ȱ��ȭ�Ǿ� �ִ� ä��ȭ���� ����
        dialogueObject.SetActive(false);
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
        //��ȭâ�� ��Ȱ��ȭ �Ǿ��ִ� ���¿��� Ȱ��ȭ ���·� �ٲ�
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
        // npc�� ������ ȭ�鿡 ���
        nameText.text = currentNPC.npcName;
        dialogueText.text = currentNPC.dialogues[0];

        // n�� �Ŀ� 1���� ����Ͻÿ�.

        //Ű����� EŰ�� ������ ���

        //����ϱ� ��ư�� Ŭ���ؼ� ���
    }

    public void CloseText()
    {
        DialogueBackGround.SetActive(false);
    }
}
