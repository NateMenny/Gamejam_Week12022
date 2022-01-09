using UnityEngine;

public class CameraFollow : MonoBehaviour {
	//the player
	 [SerializeField] Transform target;
	// The actual camera
	[SerializeField] GameObject realCamera;

	//Camera zoom levels
	[SerializeField] [Range(1, 10)] int zoomedInDistance = 5;
	[SerializeField] [Range(1, 10)] int zoomedOutDistance;
	
	//the desired zoom value
	Vector3 camZoom;
	// camera current position
	Vector3 camPos;


	private void Start()
	{
		if (!realCamera) Debug.Log("You forgot to set the real camera goofball ;)");
		
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
		bool isPlayerMoving = GameManager.instance.playerInstance.GetComponent<PlayerMovement2D>().IsMoving;
		// if player is moving set camZoom= playerInstance.transform.z - 50, transform camPos.z = camZoom.z
		if (isPlayerMoving)
        {
			ZoomTo(zoomedOutDistance);
		}
        else
        {
			ZoomTo(zoomedInDistance);
		}


		// if player !is moving set camZoom= playerInstance.transform.z - 15, transform camPos.z = CamZoom.z
	}

	public void ZoomTo(float distance)
    {
		realCamera.GetComponent<Camera>().orthographicSize = distance;
	}
}
