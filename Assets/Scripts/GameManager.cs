using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public Player player;
    public GameObject playerSprite;
    public PlayerFx playerFx;
    public float RespawnTime = 2f;
    public Timer timer;
    public bool isAlive = true;
    public bool hasKey = false;
    [SerializeField]
    private Vector3 playerSpawnPoint;
   
    
    public enum TAGS
    {
        Player,
        Enemy,
        Platform,
    }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerSpawnPoint = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveProgress(Vector3 position) {
        playerFx.InstantiateFx(2, position, Quaternion.identity);
        playerSpawnPoint = player.transform.position;
    }

    public void Death() {

        if (!isAlive) {
            return;
        }

        isAlive = false;
        //death fx
        playerFx.InstantiateFx(1, player.transform.position, Quaternion.identity);
        //disabling player components
        playerSprite.SetActive(false);
        player.transform.GetComponent<BoxCollider2D>().isTrigger = true;
        player.enabled = false;
        player.transform.GetComponent<Controller2D>().enabled = false;

        timer.Add(() =>
        {
            Respawn();
        }, RespawnTime);
    }

    public void Respawn() {
        isAlive = true;
        //reseting position to last save pint
        player.transform.position = playerSpawnPoint;
        //enabling player components
        playerSprite.SetActive(true);
        player.enabled = true;
        player.transform.GetComponent<BoxCollider2D>().isTrigger = false;
        player.transform.GetComponent<Controller2D>().enabled = true;
    }

}
