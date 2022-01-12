using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchJoystickRotation : MonoBehaviour
{
	public Joystick joystick;
	public GameObject Object;
	Vector2 GameobjectRotation;
	private float GameobjectRotation2;

	public bool FacingRight = true;

	void Update()
	{
		//Gets the input from the jostick
		GameobjectRotation = new Vector2(joystick.Horizontal, joystick.Vertical);

		if (GameobjectRotation.y > 0)
		{
			//Rotates the object if the player is facing right
			GameobjectRotation2 = GameobjectRotation.x * 90 + GameobjectRotation.y;
			Object.transform.rotation = Quaternion.Euler(0f, GameobjectRotation2, 0f);
		}
		else if(GameobjectRotation.y < 0)
		{
			//Rotates the object if the player is facing left
			GameobjectRotation2 = GameobjectRotation.x * 90 + GameobjectRotation.y ;
			Object.transform.rotation = Quaternion.Euler(0f, -GameobjectRotation2+180, 0f);
			print(22);
		}
	}
}
