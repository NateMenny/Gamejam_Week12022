using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	//the player
	[SerializeField] Transform target;
	// The actual camera
	Camera realCamera;
	bool zoomedOut = false;
	[SerializeField] bool verbose;

	float timeElapsedSinceZoom;

	//Camera zoom levels
	[SerializeField] [Range(1, 10)] float zoomedInDistance = 7;
	[SerializeField] [Range(1, 10)] float zoomedOutDistance = 9;
	
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


		if (timeElapsedSinceZoom >= 1f)
		{
			if (isPlayerMoving)
			{
				if (!zoomedOut)
				{
					StartCoroutine(SmoothZoom(zoomedOutDistance));
					zoomedOut = true;
					timeElapsedSinceZoom = 0f;
				}
			}
			else
			{
				if (zoomedOut)
				{
					StartCoroutine(SmoothZoom(zoomedInDistance));
					zoomedOut = false;
					timeElapsedSinceZoom = 0f;
				}
			}
		}

		timeElapsedSinceZoom += Time.unscaledDeltaTime;
	}

	public void ZoomTo(float distance)
    {
		realCamera.orthographicSize = distance;
	}

	IEnumerator SmoothZoom(float distance)
    {
		float zoomDuration = 1f; // Time it takes to complete zoom
		float timeElapsed = 0f;
		float ogCameraSize = realCamera.orthographicSize;

		while (timeElapsed < zoomDuration)
        {
			realCamera.orthographicSize = Mathf.Lerp(ogCameraSize, distance, timeElapsed);
			timeElapsed += Time.unscaledDeltaTime;
			yield return null;
			if (verbose)
			{
				Debug.Log("camera move time: " + timeElapsed);
			}
        }
		
    }
}
