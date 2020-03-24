using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(Player))]
public class PlayerInput : MonoBehaviour {


    Player player;

    [SerializeField]
    bool useMobileControl = false;

    public Joystick joystick;
    public FixedButton jumpButton;
	// Use this for initialization
	void Start () {
        player = GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
        //$debug$
            Vector2 directionalInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            player.SetDirectionalInput(directionalInput);
            if (Input.GetButtonDown("Jump"))
            {
                player.OnJumpInputDown();
            }

        if (useMobileControl) {
            directionalInput = new Vector2(joystick.Horizontal, joystick.Vertical);
            player.SetDirectionalInput(directionalInput);

            if (jumpButton.Pressed) {
                player.OnJumpInputDown();
            }
        }
               

    }


}
