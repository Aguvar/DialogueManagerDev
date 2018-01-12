using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public static DialogueManager instance;

    public UnityEvent DialogueFinished;

    public GameObject display;
    public Text dialogueText;
    public Text speakerText;

    private Conversation conversation;
    private int conversationIndex;
    private bool conversationTakingPlace = false;
    private bool writingText = false;

    private Coroutine writingRoutine;

    public void PlayConversation(Conversation c)
    {
        EventSystem.current.SetSelectedGameObject(display);
        conversationTakingPlace = true;
        conversation = c;//Mejorar esto
        conversationIndex = -1;
        display.gameObject.SetActive(true);
        NextDialog();
    }

    public void NextDialog()
    {
        conversationIndex++;
        if (conversationIndex < conversation.texts.Length)
        {
            if (writingRoutine != null)
            {
                StopCoroutine(writingRoutine);
            }
            speakerText.text = conversation.texts[conversationIndex].speaker;
            dialogueText.text = "";
            writingRoutine = StartCoroutine(WriteText(conversation.texts[conversationIndex].charDelay));
        }
        else
        {
            display.gameObject.SetActive(false);
            conversationTakingPlace = false;
            if (DialogueFinished != null)
            {
                DialogueFinished.Invoke();
            }
        }
    }

    private IEnumerator WriteText(float delay)
    {
        writingText = true;
        foreach (var character in conversation.texts[conversationIndex].text)
        {
            dialogueText.text += character;
            yield return new WaitForSeconds(delay);
        }
        writingText = false;
    }

    public bool ConversationTakingPlace()
    {
        return conversationTakingPlace;
    }

    // Use this for initialization
    void Start()
    {
        instance = this;

        dialogueText = display.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (conversationTakingPlace && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)))
        {
            if (writingText)
            {
                SkipWrite();
            }
            else
            {
                NextDialog();
            }
        }
    }

    private void SkipWrite()
    {
        StopCoroutine(writingRoutine);
        writingText = false;
        dialogueText.text = conversation.texts[conversationIndex].text;
    }
}
