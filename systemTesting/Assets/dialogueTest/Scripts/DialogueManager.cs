using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DialogueManager : MonoBehaviour
{
    public DialogueObject startingDialogue;

    [Header("Dialogue Variables")]
    public DialogueObject currentDialogue;
    private int sentenceIndex;
    private bool displayedCurrent;

    [Header("Resopnse Variables")]
    public bool displayedResponses;

    [Header("Other")]
    public int dialogueIndex;
    public DialogueObject[] additionalDialogues;
    
    public enum DIALOGUE_STATE {
        inactive,
        displaying,
        waiting
    }
    public DIALOGUE_STATE currentState;

    [Header("UI Settings")]
    public SpriteRenderer dialogueBox;
    public Text speakerText;
    public Text dialogueText;

    void Start() {
        currentDialogue = startingDialogue;
        currentState = DIALOGUE_STATE.inactive;
        dialogueIndex = 0;
        ClearDialogueBox();
    }

    void Update() {
        if (currentState == DIALOGUE_STATE.inactive && currentDialogue != null) {
            if (Input.GetKeyDown(KeyCode.Space) == true) {
                currentState = DIALOGUE_STATE.displaying;
                sentenceIndex = 0;
                displayedCurrent = false;
            }
        }

        if (currentState == DIALOGUE_STATE.displaying) {
            StateDisplay();
        }

        if (currentState == DIALOGUE_STATE.waiting) {
            StateWaiting();
        }
    }

    void StateDisplay() {
        string[] sentences = currentDialogue.sentences;

        // If all dialogue options have been said, load the responses or end the dialogue
        if (sentenceIndex >= sentences.Length - 1) {
            if (currentDialogue.responses.Length == 0) { 
                if (sentenceIndex > sentences.Length - 1) { // It needs the second condition so that it waits for another enter
                    currentState = DIALOGUE_STATE.inactive;
                    displayedCurrent = true; // Makes sure it doesn't attempt to display another sentence

                    if (dialogueIndex < additionalDialogues.Length) {
                        currentDialogue = additionalDialogues[dialogueIndex];
                        dialogueIndex += 1;
                        ClearDialogueBox();
                    }
                    else {
                        currentDialogue = null;
                        ClearDialogueBox();
                    }
                }
            }
            else {
                currentState = DIALOGUE_STATE.waiting;
                displayedResponses = false;
            }
        }
        
        // Prints the current sentence if it has not already been displayed
        if (displayedCurrent == false) {
            DisplaySentence(currentDialogue.speaker, sentences[sentenceIndex]);
            displayedCurrent = true;
        }
        else {
            // Waits for the user to press enter
            if (Input.GetKeyDown(KeyCode.Return) == true) {
                sentenceIndex += 1;
                displayedCurrent = false;
            }
        }
    }

    void StateWaiting() {
        ResponseObject[] responses = currentDialogue.responses;
        if (displayedResponses == false) {
            for (var i = 0; i < responses.Length; i += 1) {
                var displayIndex = i + 1;
                DisplayResposne(displayIndex.ToString(), responses[i].responseText);
            }
            displayedResponses = true;
        }
        else {
            int inputChoice = 0;
            if (Input.anyKey) {
                for (var i = 0; i < Input.inputString.Length; i += 1) {
                    char currentKey = Input.inputString[i];
                    if (Char.IsDigit(currentKey) == true) {
                        int keyInt = int.Parse(currentKey.ToString());
                        if (keyInt <= responses.Length) {
                            inputChoice = keyInt;
                            break;
                        }
                    }
                }
            }

            if (inputChoice != 0) {
                currentDialogue = responses[inputChoice-1].nextDialogue;
                currentState = DIALOGUE_STATE.displaying;
                sentenceIndex = 0;
                displayedCurrent = false;
            }
        }
    }

    void DisplaySentence(string speaker, string sentence) {
        dialogueBox.enabled = true;
        string displayText = "";
        for (var i = 0; i < sentence.Length; i += 1) {
            if (sentence[i] + "" == " ") {
                displayText += "     ";
            }
            else {
                displayText += sentence[i];
            }
        }
        speakerText.text = speaker;
        dialogueText.text = displayText;
    }

    void DisplayResposne(string inputKey, string sentence) {
        dialogueBox.enabled = true;
        Debug.Log(" RESPONSE: " + inputKey + " - " + sentence);
    }

    void ClearDialogueBox() {
        DisplaySentence(" ", " ");
        dialogueBox.enabled = false;
    }
}
