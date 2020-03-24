using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Key : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(GameManager.TAGS.Player.ToString())) {

            GameManager.instance.hasKey = true;
            GameManager.instance.playerFx.InstantiateFx(2, transform.position, Quaternion.identity);
            PlayerSound.instance.PlayAShot(PlayerSound.instance.keySound);
            gameObject.SetActive(false);
        }
    }
}
