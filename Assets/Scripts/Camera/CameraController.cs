using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public float moveSpeed_X;
	public float moveSpeed_Y;
	public float rotateSpeed_X;
	public float rotateSpeed_Y;
	public float moveSpeed_QE;

	public float clampH_L = 8.0f;
	public float clampH_M = 16.0f;
	public float clampX_L = 14.0f;
	public float clampX_R = 50.0f;
	public float clampZ_B = 14.0f;
	public float clampZ_F = 47.0f;

	float move_H;
	float move_V;
	float rotate_H;
	float rotate_V;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		move_H = Input.GetAxis ("Horizontal");
		move_V = Input.GetAxis ("Vertical");

		if (Input.GetKey(KeyCode.Q)) {
			transform.position += Vector3.up * moveSpeed_QE * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.E)) {
			transform.position -= Vector3.up * moveSpeed_QE * Time.deltaTime;
		}

		if (Input.GetMouseButton (1)) {
			rotate_H = Input.GetAxis ("Mouse X");
			rotate_V = Input.GetAxis ("Mouse Y");
		} else {
			rotate_H = 0;
			rotate_V = 0;
		}

		CameraMove ();
		CameraRotate ();
		ClampCameraPos ();
	}

	void CameraMove ()
	{
		transform.Translate (Vector3.forward * moveSpeed_Y * move_V * Time.deltaTime, Space.Self);
		transform.Translate (Vector3.right * moveSpeed_X * move_H * Time.deltaTime, Space.Self);
	}

	void CameraRotate ()
	{
		transform.Rotate (Vector3.up * rotate_H * rotateSpeed_X * Time.deltaTime, Space.World);
		transform.Rotate (Vector3.right * rotate_V * rotateSpeed_Y * Time.deltaTime, Space.Self);
	}

	void ClampCameraPos ()
	{
		float x = transform.position.x;
		float y = transform.position.y;
		float z = transform.position.z;

		x = Mathf.Clamp (x,clampX_L,clampX_R);
		y = Mathf.Clamp (y,clampH_L,clampH_M);
		z = Mathf.Clamp (z,clampZ_B, clampZ_F);

		Vector3 clampPos = new Vector3 (x,y,z);
		transform.position = clampPos;
	}
}
