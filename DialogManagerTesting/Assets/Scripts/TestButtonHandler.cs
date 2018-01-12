using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestButtonHandler : MonoBehaviour {

    public Conversation testConversation;


	// Use this for initialization
	void Start ()
    {
        Button btn = GetComponent<Button>();

        btn.onClick.AddListener(PlayTestConversation);
	}

    void PlayTestConversation()
    {
        DialogueManager.instance.PlayConversation(testConversation);
    }

	// Update is called once per frame
	void Update () {
		
	}
}
