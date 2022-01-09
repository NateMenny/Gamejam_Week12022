using UnityEngine;

public class CameraFollow : MonoBehaviour {
	//the player
	 [SerializeField] Transform target;
	//the desired zoom value
	Vector3 camZoom;
	// camera current position
	Vector3 camPos;


	private void Start()
	{
	}

	private void LateUpdate() {
		if(!target)
        {
			target = GameManager.instance.playerInstance.transform;
		}
		transform.position = new Vector3(target.position.x, target.position.y, 0f);
	}

	private void Update()
	{
		// if player is moving set camZoom= playerInstance.transform.z - 50, transform camPos.z = camZoom.z

		// if player !is moving set camZoom= playerInstance.transform.z - 15, transform camPos.z = CamZoom.z
	}
}
