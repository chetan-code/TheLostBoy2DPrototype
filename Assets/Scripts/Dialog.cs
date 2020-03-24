using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{

    public TextMeshProUGUI textDisplay;
    public Button continueButton;
    public GameObject[] gameobjectsHiddenDuringDialog;
    public bool dialogOnStart;
    public bool dialogOnTriggerEnter;
    public bool dialogCompleted = false;
    public float typingSpeed;
    public GameManager gameManager;

    public string[] sentences;

    private int index;




    // Start is called before the first frame update
    void Start()
    {

        continueButton.gameObject.SetActive(false);
        textDisplay.text = "";
        if (dialogOnStart) {
            StartConversation();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(GameManager.TAGS.Player.ToString()) && dialogOnTriggerEnter) {
            StartConversation();
        }
    }

    private void StartConversation() {
        if (!dialogCompleted)
        {
            gameManager.player.enabled = false; 
            gameManager.player.GetComponent<PlayerInput>().enabled = false;
            continueButton.gameObject.SetActive(false);
            continueButton.onClick.AddListener(NextSentence);
            foreach (GameObject go in gameobjectsHiddenDuringDialog)
            {
                go.SetActive(false);
            }
            StartCoroutine(Type());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (textDisplay.text == sentences[index]) {
            continueButton.gameObject.SetActive(true);
        }
    }

    IEnumerator Type() {

        foreach (char letter in sentences[index].ToCharArray()) {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence() {

        continueButton.gameObject.SetActive(false);

        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else {
            textDisplay.text = "";
            dialogCompleted = true;
            gameManager.player.GetComponent<PlayerInput>().enabled = true;
            gameManager.player.enabled = true;
            continueButton.gameObject.SetActive(false);
            foreach (GameObject go in gameobjectsHiddenDuringDialog)
            {
                go.SetActive(true);
            }
        }
    }
}
