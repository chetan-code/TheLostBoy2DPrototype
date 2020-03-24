using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{

    public static PlayerSound instance;

    public AudioClip jumpSound;
    public AudioClip saveSound;
    public AudioClip keySound;

    [SerializeField]
    private AudioClip[] playerSounds;

    [SerializeField]
    private bool isSoundPlaying = false;

    [SerializeField]
    private AudioClip[] footstesSfx;
    [SerializeField]
    private float timeBetweenSteps = 1f;

    private AudioSource audioSource;


    private float time;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else {
            Debug.LogError("More than one instaance of PlayerSound. Deleting Object");
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        time = 0;
    }


    private void Update()
    {
        time += Time.deltaTime;
        if (time >= timeBetweenSteps && Mathf.Abs(Player.instance.velocity.x) > 0.01f && Player.instance.controller.collisions.below) {
            PlayAShot(footstesSfx[Random.Range(0, footstesSfx.Length)]);
            time = 0f;
        }
    }

    public void PlayAShot(AudioClip clip) {
        if (isSoundPlaying) {
            audioSource.PlayOneShot(clip);
        }

        return;
    }
}
