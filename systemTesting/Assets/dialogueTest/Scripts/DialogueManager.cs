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

    [Header("Resopnse Logic Variables")]
    public bool displayedResponses;
    public int currentlySelected;

    [Header("Response UI Variables")]
    public float responseYPos;
    public float screenWidth;
    public float[] response2Pos;
    public float[] response3Pos;

    [Header("UI Settings")]
    public Text speakerText;
    public Text dialogueText;
    public Font dialogueFont;
    public Transform dialogueCanvas;
    public SpriteRenderer dialogueBox;
    private List<Text> UIResponses;

    [Header("Other")]
    public int dialogueIndex;
    public DialogueObject[] additionalDialogues;
    
    public enum DIALOGUE_STATE {
        inactive,
        displaying,
        waiting
    }
    public DIALOGUE_STATE currentState;

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
            UIResponses = DisplayResponses(responses);
            displayedResponses = true;
        }
        else {
            int confirmedChoice = 0;
            if (Input.anyKey) {
                for (var i = 0; i < Input.inputString.Length; i += 1) {
                    char currentKey = Input.inputString[i];
                    if (Char.IsDigit(currentKey) == true) {
                        int keyInt = int.Parse(currentKey.ToString());
                        if (keyInt <= UIResponses.Count) {
                            currentlySelected = keyInt;
                            for (var x = 0; x < UIResponses.Count; x += 1) {
                                if (x == currentlySelected-1) {
                                    UIResponses[x].color = Color.yellow;
                                }
                                else {
                                    UIResponses[x].color = Color.white;
                                }
                            }
                            break;
                        }
                        else {
                            Debug.Log("Invalid Selection");
                            Debug.Log(UIResponses.Count);
                        }
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Return) == true && confirmedChoice == 0) {
                confirmedChoice = currentlySelected;
            }

            if (confirmedChoice != 0) {
                currentDialogue = responses[confirmedChoice-1].nextDialogue;
                if (currentDialogue == null) {
                    currentState = DIALOGUE_STATE.inactive;
                    sentenceIndex = 0;
                    displayedCurrent = false;
                    ClearResponses();
                    ClearDialogueBox();
                }
                else {
                    currentState = DIALOGUE_STATE.displaying;
                    sentenceIndex = 0;
                    displayedCurrent = false;
                    ClearResponses();
                }
            }
        }
    }

    void DisplaySentence(string speaker, string sentence) {
        dialogueBox.enabled = true;
        speakerText.text = speaker;
        dialogueText.text = ExpandSpaces(sentence);
    }

    List<Text> DisplayResponses(ResponseObject[] responses) {
        List<Text> UIResponses = new List<Text>();
        for (var i = 0; i < responses.Length; i += 1) {
            float xPos = 0;
            if (responses.Length == 2) {
                xPos = response2Pos[i];
            }
            else if (responses.Length == 3) {
                xPos = response3Pos[i];
            }
            Text r = CreateText("response" + (i+1).ToString(), dialogueCanvas, xPos, responseYPos, ExpandSpaces(responses[i].responseText));
            UIResponses.Add(r);
        }

        return UIResponses;
    }

    void ClearDialogueBox() {
        DisplaySentence(" ", " ");
        dialogueBox.enabled = false;
    }

    void ClearResponses() {
        foreach (Text t in UIResponses) {
            Destroy(t.gameObject);
        }
    }

    string ExpandSpaces(string text) {
        string newText = "";
        for (var i = 0; i < text.Length; i += 1) {
            if (text[i] + "" == " ") {
                newText += "     ";
            }
            else {
                newText += text[i];
            }
        }

        return newText;
    }

    Text CreateText(string objectName, Transform canvas, float x, float y, string displayText)
    {
        GameObject textObject = new GameObject(objectName);
        textObject.transform.SetParent(canvas);

        RectTransform trans = textObject.AddComponent<RectTransform>();
        trans.sizeDelta = new Vector2(250, 30);
        trans.localScale = new Vector3(1, 1, 1);
        trans.anchoredPosition3D = new Vector3(x, y, 0);

        Text text = textObject.AddComponent<Text>();
        text.text = displayText;
        text.font = dialogueFont;
        text.fontSize = 24;
        text.color = Color.white;
        text.alignment = TextAnchor.MiddleCenter;

        return text;
    }
}