using System.Collections;
using TMPro;
using UnityEngine;

namespace FarmerDemo
{
    public class DialogueManagerScript : MonoBehaviourSingletonBase<DialogueManagerScript>
    {
        public GameObject DialoguePanel;
        public GameObject DialogueSpeaker;
        public TMP_Text DialogueText;
        void Start()
        {
            DialoguePanel.SetActive(false);
        }

        public void ShowDialogue(string dialogue)
        {
            StartCoroutine(ShowTemporaryDialogue(dialogue));
        }

        private IEnumerator ShowTemporaryDialogue(string dialogue)
        {
            DialogueText.text = dialogue;
            DialoguePanel.SetActive(true);
            yield return new WaitForSeconds(3);
            DialoguePanel.SetActive(false);
        }
    }
}