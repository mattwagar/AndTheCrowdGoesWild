using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StoryGraph;

public class ForcePush : MonoBehaviour {

	private Vector3[] cachedPositions;
	public StoryListener onForcePush;
	public float distanceThreshold = 1f;
	public float averageDistance;
	public bool pushed = false;

	void Start(){
		ResetAverageDistance();
	}

	public void ResetAverageDistance(){
		pushed = false;
		averageDistance = 0f;
		Vector3 startPos = transform.position;
		cachedPositions = new Vector3[]{startPos,startPos,startPos};
	}
	
	public void ForcePushGesture(){
		averageDistance =  AverageDistance(cachedPositions);
		if(onForcePush != null && onForcePush.StoryListenerAction != null && averageDistance > distanceThreshold){
			pushed = true;
			onForcePush.StoryListenerAction.Invoke();
		}
		cachedPositions = new Vector3[]{transform.position,cachedPositions[0],cachedPositions[1]};
	}

	float AverageDistance(Vector3[] vecs){
		float sumDistance = 0f;
		if(vecs.Length > 1){
			for(int i = 1; i < vecs.Length; i++)
			{
				sumDistance += Vector3.Distance(vecs[i-1], vecs[i]);
			}
			return (sumDistance / (vecs.Length-1));
		} else return 0;
	}
}
