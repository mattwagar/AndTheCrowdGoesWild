using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StoryGraph;

public class DownwardPush : MonoBehaviour
{

    // Use this for initialization
    //Use this to get downwards: Vector3.Dot(object.normal, Vector3.forward);

    private Vector3[] cachedPositions;
    public StoryListener onForcePush;
    public float distanceThreshold = 1f;
    public float averageDistance;
    public bool pushed = false;
    public Transform handTransform;
    public float degreesThreshold;

    public bool facingDownwards = false;
    void Start()
    {
        ResetAverageDistance();

    }

    public void ResetAverageDistance()
    {
        pushed = false;
        averageDistance = 0f;
        Vector3 startPos = transform.position;
        cachedPositions = new Vector3[] { startPos, startPos, startPos };
    }

    public void ForcePushGesture()
    {
        averageDistance = AverageDistance(cachedPositions);
        if (averageDistance > distanceThreshold)
        {
            pushed = true;
            // onForcePush.StoryListenerAction.Invoke();
        }
        cachedPositions = new Vector3[] { transform.position, cachedPositions[0], cachedPositions[1] };
    }

    float AverageDistance(Vector3[] vecs)
    {
        float sumDistance = 0f;
        if (checkFacingDownwards(degreesThreshold))
		Debug.Log("It's facing downwards");
        {
            if (vecs.Length > 1)
            {
                for (int i = 1; i < vecs.Length; i++)
                {
                    sumDistance += Vector3.Distance(vecs[i - 1], vecs[i]);
                }
                return (sumDistance / (vecs.Length - 1));
            }
            else return 0;
        }
        return 0;

    }

    /*public bool checkFacingDownwards(){ 
		Vector3 handNormal = new Vector3(0f, handTransform.rotation.eulerAngles.y, 0f*0);
		if(Vector3.Dot(handNormal, Vector3.down) > .8f){
			facingDownwards = true;
			Debug.Log("Facing Downwards");
		}
		else{
			facingDownwards = false;
			Debug.Log("Not Facing Downwards");
		}
		return facingDownwards;
	} */
    public bool checkFacingDownwards(float degreesThreshold)
    {
        return Vector3.Dot(-Vector3.up, handTransform.forward) > degreesThreshold; 
    }
}



