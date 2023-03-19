using Ink.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject[] choices;
    [SerializeField] private GameObject shopUI;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject questManager;
    [SerializeField] private GameObject questAcceptUI;
    [SerializeField] public bool questPopped;

    private string dialogueString;

    private int choiceIndex = 0;

    private TextMeshProUGUI[] choicesText;

    private Story currentStory = null;

    public Coroutine writingEffectCoroutine = null;

    public bool lettersMoving { get; private set; }
    public bool dialoguePlaying { get; private set; }

    private static DialogueManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        instance = this;
        questPopped = false;

    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    public void Start()
    {
        dialoguePlaying = false;
        lettersMoving = false;
        dialoguePanel.SetActive(false);

        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0) && lettersMoving)
        {
            StopCoroutine(writingEffectCoroutine);
            lettersMoving = false;
            dialogueText.text = dialogueString;
            DisplayChoices();
        }
        else if (Input.GetMouseButtonDown(0) && !lettersMoving && dialoguePlaying && currentStory.currentChoices.Count == 0)
        {
            ContinueStory();
        }


        if (!dialoguePlaying)
        {
            return;
        }

        if (Input.GetKeyDown("space"))
        {
            //ExitDialogueMode();
        }
    }
    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        BindAllExternalFunctions();
        dialoguePlaying = true;
        dialoguePanel.SetActive(true);

        OverWorldManager.instance.dialogueVariables.StartListening(currentStory);
        Time.timeScale = 0;

        ContinueStory();
    }

    public void ExitDialogueMode()
    {
        //yield return new WaitForSeconds(0.2f);
        OverWorldManager.instance.dialogueVariables.StopListening(currentStory);
        dialoguePlaying = false;
        dialoguePanel.SetActive(false);
        Time.timeScale = 1;
        dialogueText.text = "";
    }

    public void ContinueStory()
    {
        if (!lettersMoving && !questPopped)
        {
            if (currentStory.canContinue)
            {

                //Debug.Log("continued");
                //dialogueText.text = currentStory.Continue();
                dialogueString = currentStory.Continue();
                if (dialogueString.Equals(""))
                {
                    ExitDialogueMode();
                }
                writingEffectCoroutine = StartCoroutine(WritingEffect());
                List<string> tags = currentStory.currentTags;
                if (tags.Contains("shop"))
                {
                    shopUI.SetActive(true);
                    //this.gameObject.GetComponent<DialogTest>().enabled=true;
                }
                if (tags.Contains("addRunes"))
                {
                    gameObject.GetComponent<AddRunes>().enabled = true;
                }

            }
            else
            {
                ExitDialogueMode();
            }
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices than ui can support. Given choices:" + currentChoices.Count);
        }

        int index = 0;
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }
    }


    /*public void SelectChoiceIndex(int index)
    {
        choiceIndex = index;
    }*/
    public void MakeChoice(int index)
    {
        if (!lettersMoving)
        {
            currentStory.ChooseChoiceIndex(index);
            foreach (GameObject go in choices)
            {
                go.SetActive(false);
            }
            ContinueStory();
        }
    }

    private void BindAllExternalFunctions()
    {
        currentStory.BindExternalFunction("AddRunes", (string s) =>
        {
            AddRunes(s);
        }, false);
        currentStory.BindExternalFunction("TP", (string s) =>
        {
            TpPlayer(s);
        });
        currentStory.BindExternalFunction("getQuest", (string s) =>
        {
            //QuestManager.GetInstance().getQuest(s);
            questAcceptUI.GetComponent<QuestAcceptUI>().getQuest(s);
        });
        currentStory.BindExternalFunction("completeQuest", (string s) =>
         {
             QuestManager.GetInstance().completeQuest(s);
         });
    }
    private void AddRunes(string s)
    {
        //Debug.Log("added");
        string[] runes = s.Split(',');
        foreach (var rune in runes)
        {
            GlobalRune.Type type;
            Enum.TryParse<GlobalRune.Type>(rune, out type);
            GlobalRune.addRune(type, 1);
        }
    }
    public void HideShop()
    {
        shopUI.gameObject.SetActive(false);
        writingEffectCoroutine = StartCoroutine(WritingEffect());
    }
    private void TpPlayer(string s)
    {
        Debug.Log("tpd");
        string[] dims = s.Split(",");
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = new Vector3(int.Parse(dims[0]), int.Parse(dims[1]), int.Parse(dims[2]));
        player.GetComponent<CharacterController>().enabled = true;
    }

    private IEnumerator WritingEffect()
    {
        lettersMoving = true;
        dialogueText.text = "";
        foreach (char c in dialogueString)
        {
            yield return new WaitForSecondsRealtime(0.03f);
            dialogueText.text += c;
        }
        lettersMoving = false;
        DisplayChoices();
    }
    public Ink.Runtime.Object GetVariableState(string variableName)
    {
        Ink.Runtime.Object variableValue = null;
        OverWorldManager.instance.dialogueVariables.variables.TryGetValue(variableName, out variableValue);
        if (variableValue == null)
        {
            Debug.LogWarning("Ink Variable was found to be null: " + variableName);
        }
        return variableValue;
    }

}
