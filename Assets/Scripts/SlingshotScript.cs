using UnityEngine;
using System.Collections;

public class SlingshotScript : MonoBehaviour {
	GameObject launchPoint;
	public GameObject projectilePrefab;
	GameObject projectile;
	Rigidbody rbProjectile;
	bool aimingMode;
	public float velocity;

	void Awake (){
		Transform launchTransform = this.transform.FindChild ("LaunchPoint");
		launchPoint = launchTransform.gameObject;
		launchPoint.SetActive (false);
	}

	void FixedUpdate (){
		if (!aimingMode) {
			return;
		}
		Vector3 mouseScreen = Input.mousePosition;
		mouseScreen.z = -Camera.main.transform.position.z;
		Vector3 mouseWorld = Camera.main.ScreenToWorldPoint (mouseScreen);
		projectile.transform.position = mouseWorld;
		float maxMagnitude = this.GetComponent<SphereCollider> ().radius;
		Vector3 projectileDelta = projectile.transform.position - launchPoint.transform.position;
		if (projectileDelta.magnitude > maxMagnitude) {
			projectileDelta.Normalize ();
			projectileDelta *= maxMagnitude;
		}
		projectile.transform.position = projectileDelta + launchPoint.transform.position;
		if (Input.GetMouseButtonUp (0)) {
			aimingMode = false;
			rbProjectile.isKinematic = false;
			rbProjectile.velocity = -projectileDelta * velocity;
			FollowCam.cam.poi = projectile;
			projectile = null;
		}
	}

	void OnMouseEnter (){
		launchPoint.SetActive (true);
	}

	void OnMouseExit (){
		if (!aimingMode)
			launchPoint.SetActive (false);
	}

	void OnMouseDown (){
		projectile = Instantiate (projectilePrefab) as GameObject;
		projectile.transform.position = launchPoint.transform.position;
		rbProjectile = projectile.GetComponent<Rigidbody> ();
		rbProjectile.isKinematic = true;
		aimingMode = true;
	}
}
