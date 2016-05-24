using UnityEngine;
using System.Collections;

public class Pillar1 : MonoBehaviour {

	Vector3 initialRotation;

	// Use this for initialization
	void Start () {
		initialRotation = this.gameObject.transform.rotation.eulerAngles;
	}

	void Update () {
		Vector3 rot = this.gameObject.transform.rotation.eulerAngles;
		if (hasRotatedEnough (initialRotation, rot))
//			Destroy (this.gameObject);
			return;
	}

	bool hasRotatedEnough(Vector3 initial, Vector3 actual){
		if (Mathf.DeltaAngle (initial.z, actual.z) >= 90 || Mathf.DeltaAngle (initial.z, actual.z) <= -90)
			return true;
		return false;
	}

	void OnCollisionEnter(Collision col){
		if (col.relativeVelocity.magnitude > 9)
			Destroy (this.gameObject);
	}
}
