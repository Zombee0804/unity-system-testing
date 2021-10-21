using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DialogueManager : MonoBehaviour
{
    public DialogueObject startingDialogue;

    [Header("Dialogue Variables")]
    public DialogueObject currentDialogue;
    private int dialogueCounter;
    private bool displayedCurrent;

    [Header("Resopnse Variables")]
    public bool displayedResponses;

    public enum DIALOGUE_STATE {
        inactive,
        displaying,
        waiting
    }
    public DIALOGUE_STATE currentState;

    void Start() {
        currentDialogue = startingDialogue;
        currentState = DIALOGUE_STATE.inactive;
    }

    void Update() {
        if (currentState == DIALOGUE_STATE.inactive) {
            if (Input.GetKeyDown(KeyCode.Space) == true) {
                currentState = DIALOGUE_STATE.displaying;
                dialogueCounter = 0;
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

    void DisplaySentence(string speaker, string sentence) {
        Debug.Log(speaker + " : " + sentence);
    }

    void DisplayResposne(string inputKey, string sentence) {
        Debug.Log(" RESPONSE: " + inputKey + " - " + sentence);
    }

    void StateDisplay() {
        string[] sentences = currentDialogue.sentences;

        // If all dialogue options have been said, load the responses or end the dialogue
        if (dialogueCounter >= sentences.Length - 1) {
            if (currentDialogue.responses.Length == 0) { 
                if (dialogueCounter > sentences.Length - 1) { // It needs the second condition so that it waits for another enter
                    currentState = DIALOGUE_STATE.inactive;
                    Debug.Log("DIALOGUE ENDED");
                    currentDialogue = null;
                    displayedCurrent = true; // Makes sure it doesn't attempt to display another sentence
                }
            }
            else {
                currentState = DIALOGUE_STATE.waiting;
                displayedResponses = false;
            }
        }
        
        // Prints the current sentence if it has not already been displayed
        if (displayedCurrent == false) {
            DisplaySentence(currentDialogue.speaker, sentences[dialogueCounter]);
            displayedCurrent = true;
        }
        else {
            // Waits for the user to press enter
            if (Input.GetKeyDown(KeyCode.Return) == true) {
                dialogueCounter += 1;
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
                dialogueCounter = 0;
                displayedCurrent = false;
            }
        }
    }
}
