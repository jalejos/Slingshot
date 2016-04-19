using UnityEngine;
using System.Collections;

public class SlingshotScript : MonoBehaviour {

	GameObject projectile;
	public GameObject prefabProjectile;
	GameObject launchPoint;
	Vector3 launchPos;
	bool aimingMode;
	public float velocity;

	void Awake () {
		Transform transformLaunchPoint = transform.Find ("LaunchPoint");
		launchPoint = transformLaunchPoint.gameObject;
		launchPoint.SetActive (false);
		launchPos = transformLaunchPoint.position;
	}

	void FixedUpdate () {
		if (!aimingMode) {
			return;
		}
		Vector3 mouseScreenPos = Input.mousePosition;
		mouseScreenPos.z = -Camera.main.transform.position.z;
		Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint (mouseScreenPos);
		Vector3 mouseDelta = mouseWorldPos - launchPos;
		float maxMagnitude = this.GetComponent<SphereCollider> ().radius;
		if (mouseDelta.magnitude > maxMagnitude) {
			mouseDelta.Normalize ();
			mouseDelta *= maxMagnitude;
		}
		Vector3 projectilePos = mouseDelta + launchPos;
		projectile.transform.position = projectilePos;

		if (Input.GetMouseButtonUp (0)) {
			aimingMode = false;
			projectile.GetComponent<Rigidbody> ().velocity = -mouseDelta * velocity;
			projectile.GetComponent<Rigidbody> ().isKinematic = false;
			FollowCam.cam.poi = projectile;
			projectile = null;
		}
	}

	void OnMouseEnter () {
		if(!aimingMode)
			launchPoint.SetActive (true);
	}

	void OnMouseExit () {
		if(!aimingMode)
			launchPoint.SetActive (false);
	}

	void OnMouseDown () {
		projectile = Instantiate (prefabProjectile) as GameObject;
		projectile.transform.position = launchPos;
		projectile.GetComponent<Rigidbody> ().isKinematic = true;
		aimingMode = true;
	}
}
