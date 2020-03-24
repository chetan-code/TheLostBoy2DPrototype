using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gate : MonoBehaviour
{
    [SerializeField]
    GameManager gameManager;
    [SerializeField]
    Animator animator;
    [SerializeField]
    TextMeshProUGUI text;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(GameManager.TAGS.Player.ToString()) && gameManager.hasKey) {
            text.text = "Unlocked";
            animator.SetBool("OpenGate", true);
            gameManager.hasKey = false;
        }
    }



}
