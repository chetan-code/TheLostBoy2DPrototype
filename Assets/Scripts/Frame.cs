using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frame : MonoBehaviour
{
    [SerializeField]
    List<GameObject> gameobjects;
    [SerializeField]
    private int currentFrameIndex = 1;

    // Start is called before the first frame update
    void Start()
    {
        Transform[] transforms = GetComponentsInChildren<Transform>();

        foreach (Transform t in transforms)
        {
            if (t != transform)
            {
                gameobjects.Add(t.gameObject);
            }

        }

        //if this is not 1st frame - deactivate object
        if (currentFrameIndex != 1)
        {
            foreach (GameObject go in gameobjects)
            {
                go.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (GameObject go in gameobjects)
            {
                go.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (GameObject go in gameobjects)
            {
                go.SetActive(false);
            }
        }
    }


}
