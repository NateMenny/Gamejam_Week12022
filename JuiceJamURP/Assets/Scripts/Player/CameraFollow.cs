using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	//the player
	[SerializeField] Transform target;
	// The actual camera
	Camera realCamera;
	bool zoomedOut = false;

	//Camera zoom levels
	[SerializeField] [Range(1, 10)] float zoomedInDistance = 5;
	[SerializeField] [Range(1, 10)] float zoomedOutDistance;
	
	//the desired zoom value
	Vector3 camZoom;
	// camera current position
	Vector3 camPos;


	private void Start()
	{
		realCamera = Camera.main;
		
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
			if (!zoomedOut)
			{
				StartCoroutine(SmoothZoom(zoomedOutDistance));
				zoomedOut = true;
			}
		}
        else
        {
			if (zoomedOut)
			{
				StartCoroutine(SmoothZoom(zoomedInDistance));
				zoomedOut = false;
			}
		}


		// if player !is moving set camZoom= playerInstance.transform.z - 15, transform camPos.z = CamZoom.z
	}

	public void ZoomTo(float distance)
    {
		realCamera.orthographicSize = distance;
	}

	IEnumerator SmoothZoom(float distance)
    {
		float zoomLength = 1f; // Time it takes to complete zoom
		float timeElapsed = 0f;
		float ogCameraSize = realCamera.orthographicSize;

		while (timeElapsed < zoomLength)
        {
			realCamera.orthographicSize = Mathf.Lerp(ogCameraSize, distance, timeElapsed);
			timeElapsed += Time.deltaTime;
			yield return null;
			Debug.Log("camera move time: " + timeElapsed);
        }
		
    }
}
