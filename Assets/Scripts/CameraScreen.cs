using UnityEngine;
using System.Collections;

// Author: Tiffany Fisher
// Modified by: Nathan Boehning
// Purpose: To allow the camera to pan over the level via acceleration

public class CameraScreen : MonoBehaviour
{

	public float perspectiveZoomSpeed = 0.5f;       // How fast to zoom a perspective camera
	public float orthographicZoomSpeed = 0.5f;      // How fast to zoom a orthographic camera
	public Camera myCamera;                         // Holds reference to the camera

	private float hMovement = 0f;
	private float vMovement = 0f;
	private float cameraMoveSpeed = 0.01f;
	private float cameraMove = 0.01f;
	private bool isOrthographic = false;

	// Use this for initialization
	void Start ()
	{
		if (myCamera.orthographic)
		{
			cameraMoveSpeed = cameraMove * myCamera.orthographicSize;
			isOrthographic = true;
		}
		else
			cameraMoveSpeed = cameraMove * myCamera.fieldOfView;


	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.Translate(hMovement, vMovement, 0);

		// If there are two touches on the device
		if (Input.touchCount == 2)
		{
			// Store both touches
			Touch touchZero = Input.GetTouch(0);
			Touch touchOne = Input.GetTouch(1);

			// Find the position in the previous frame for each touch
			Vector2 touchZeroPrev = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrev = touchOne.position - touchOne.deltaPosition;

			// Find the magnitude between touches in each frame
			float prevTouchDeltaMag = (touchZeroPrev - touchOnePrev).magnitude;
			float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

			// Find the difference in the distances between each frame
			float deltaMagnitudeDifference = prevTouchDeltaMag - touchDeltaMag;

			// If the camera is orthographic
			if (isOrthographic)
			{
				// Change the orthographic size based on the distance between the touches
				myCamera.orthographicSize += deltaMagnitudeDifference * orthographicZoomSpeed;

				// Make sure the orthographic size never drops below zero
				myCamera.orthographicSize = Mathf.Max(myCamera.orthographicSize, 0.1f);
				cameraMoveSpeed = cameraMove * myCamera.orthographicSize;

			}
			else
			{
				// Change the FOV based on change distance
				myCamera.fieldOfView += deltaMagnitudeDifference * perspectiveZoomSpeed;
				myCamera.fieldOfView = Mathf.Clamp(myCamera.fieldOfView, 0.1f, 179.9f);
				cameraMoveSpeed = cameraMove * myCamera.fieldOfView;
			}
		}

		// Move the camera via acceleration
		float tempX = (float) System.Math.Round(Input.acceleration.x, 2);
		float tempY = (float) System.Math.Round(Input.acceleration.y, 2);

		if ((tempX > 0.02f || tempX < -0.02f) || (tempY > 0.02f || tempY < -0.02))
		{
			transform.Translate(new Vector3(tempX * cameraMoveSpeed, tempY * cameraMoveSpeed));
				Debug.Log("Moving: hMove: " + (tempX*cameraMoveSpeed) + "vMove: " + (tempY*cameraMoveSpeed) + "\ncameraMoveSpeed: " +
				cameraMoveSpeed + "\naccelX: " + Input.acceleration.x + "accelY: " + Input.acceleration.y);
		}
		else
		{
			Debug.Log("Not Moving: hMove: " + (tempX * cameraMoveSpeed) + "vMove: " + (tempY * cameraMoveSpeed) + "\ncameraMoveSpeed: " +
				cameraMoveSpeed + "\naccelX: " + Input.acceleration.x + "accelY: " + Input.acceleration.y);
		}
	}
}
