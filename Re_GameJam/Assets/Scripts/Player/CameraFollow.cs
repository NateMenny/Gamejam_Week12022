using UnityEngine;

public class CameraFollow : MonoBehaviour {

	Transform target;

	private void Start()
	{
		target = GameManager.instance.playerInstance.transform;
	}

	private void LateUpdate() {
		transform.position = target.position;
	}
}
