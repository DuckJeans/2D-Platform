using DS;
using DS.ScriptableObjects;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace DS
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textUi;

        private DSDialogueSO currentDialouge;
        private DSDialogueSO nextDialogue;

        [Header("Dialogue UI")]
        public GameObject DialogueBG;
        public GameObject MultipleChoice;
        public TextMeshProUGUI[] OptionalButtons;
        public bool canNextStep = false;
        public bool endDialgoue = false;

        public DSDialogueSO CurrentDialogue
        {
            get
            {
                return currentDialouge;
            }
            set
            {
                currentDialouge = value;
                OnDialogueStart?.Invoke();
            }
        }


        public Action OnDialogueStart;

        #region Event Method
        private void Awake()
        {
            DialogueBG.SetActive(false);
            OnDialogueStart += DialogueCheck;
        }

        private void Update()
        {
            ProcessDialogue();
        }
        #endregion

        #region Setup Method

        private void SetButtonText()
        {
            for (int choiceIndex = 0; choiceIndex < currentDialouge.Choices.Count; choiceIndex++)
            {
                OptionalButtons[choiceIndex].transform.parent.gameObject.SetActive(true);
                OptionalButtons[choiceIndex].text = currentDialouge.Choices[choiceIndex].NextDialogue.Text;
            }
        }
        #endregion

        void ShowText()
        {
            DialogueBG.SetActive(true);
            textUi.text = currentDialouge.Text;
        }

        #region Status Update

        private void DialogueCheck()
        {
            canNextStep = currentDialouge == null ? false : true;
        }
        #endregion

        #region Process Method

        public void ProcessDialogue()
        {
            if (Input.GetKeyDown(KeyCode.E) && canNextStep)
                OnOptionChosen();
        }

        public void OnOptionChosen()
        {
            if (endDialgoue)
            {
                EndProcess();
                return;
            }

            if (currentDialouge.IsStartingDialogue)
            {
                ShowText();
                ProcessNext();
                currentDialouge = nextDialogue;
            }
            else
            {
                currentDialouge = nextDialogue;
                ShowText();
                ProcessNext();
            }

            CheckEnd();
        }

        #endregion

        private void CheckEnd()
        {
            if (nextDialogue == null)
            {
                endDialgoue = true;
            }
        }

        private void ProcessNext()
        {
            // Multi Node?
            if (currentDialouge.Choices.Count > 1)
            {
                MultipleChoice.SetActive(true);
                SetButtonText();
                // 멀티 노드는 버튼 이벤트로 NextProcess 결정
            }
            else if (currentDialouge.Choices.Count == 1)
            {
                MultipleChoice.SetActive(false);
                nextDialogue = currentDialouge.Choices[0].NextDialogue;
            }
        }
        private void EndProcess()
        {
            endDialgoue = false;
            currentDialouge = null;
            canNextStep = false;
            DialogueBG.SetActive(false);

            foreach (var item in OptionalButtons)
            {
                item.transform.parent.gameObject.SetActive(false);
            }
        }

        #region button Method
        public void SelectNextProcess(int choiceNumber)
        {
            nextDialogue = currentDialouge.Choices[choiceNumber].NextDialogue;
            OnOptionChosen();
        }
        #endregion
    }
}
