using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            PlayerSound.instance.PlayAShot(PlayerSound.instance.saveSound);
            GameManager.instance.SaveProgress(transform.position);
        }
    }

}
