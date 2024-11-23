using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


[System.Serializable]
public class NPCText
{
    public string npcName;
    public string[] dialogues;
}

public class NPCDialouge : MonoBehaviour
{
    [Header("��ȭ�� ����")]
    public NPCText npc;
    

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�浹 ����� �÷��̾��϶� ���� ��Ŵ
        if (collision.CompareTag("Player"))
        {
            Debug.Log($"{gameObject.name} �� ��ȣ�ۿ��� �߽��ϴ�.");
            PlayerInteract player = collision.GetComponent<PlayerInteract>();
            player.hasExistNPC = true;
            player.currentNPC = npc;
            player.GetDialougeByNPC(npc);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //�浹 ����� �÷��̾��϶� ���� ��Ŵ
        if (collision.CompareTag("Player"))
        {
            PlayerInteract player = collision.GetComponent<PlayerInteract>();
            player.hasExistNPC = false;
            player.CloseText();
            player.currentNPC = null;
        }
    }
}
