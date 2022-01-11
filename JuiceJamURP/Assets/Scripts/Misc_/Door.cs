using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public float openDistance;
    public float timeToOpen = 0.5f;
    public LightBulbObjective[] doorObjectives;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool allObjectivesComplete = true;
        for (int i = 0; i < doorObjectives.Length; i++)
        {
            if (doorObjectives[i])
            {
                if (doorObjectives[i].IsOn == false)
                {
                    allObjectivesComplete = false;
                    break;
                }
            }
        }

        if(allObjectivesComplete)
        {
            StartCoroutine(OpenDoor());
        }
    }

    IEnumerator OpenDoor()
    {
        Vector3 oldPos = transform.position;
        Vector3 newPos = transform.position + Vector3.right * openDistance;
        float timeElapsed = 0f;
        while (timeElapsed < timeToOpen)
        {
            Vector3.Lerp(oldPos, newPos, timeElapsed);
            yield return null;
            timeElapsed += Time.unscaledDeltaTime;
        }
    }
}
