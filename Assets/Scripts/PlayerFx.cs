using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFx : MonoBehaviour
{
    [SerializeField]
    private GameObject[] particleFx;

    public void InstantiateFx(int indexOfFx,Vector2 position,Quaternion rotation) {
        Instantiate(particleFx[indexOfFx], position, rotation);
    }

}
