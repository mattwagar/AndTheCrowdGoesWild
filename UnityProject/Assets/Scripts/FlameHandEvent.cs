using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StoryGraph;

public class FlameHandEvent : MonoBehaviour
{

    public Transform ThumbPoint;
    public Transform IndexPoint;
    public Transform PalmPoint;
	public GameObject FlameHands;
    private float _distance;
	public bool Pinched = false;
	public bool Flicked = false;



    void Start()
    {
        _distance = 0.14f;
    }

	public void Reset(){
		Pinched = false;
		Flicked = false;
	}

	public void CenterFlame(){

		Vector3 MidPoint = Vector3.Lerp(ThumbPoint.position, IndexPoint.position, 0.5f);

		float distance = Vector3.Distance(ThumbPoint.position, IndexPoint.position); 

		Debug.Log(distance);
		
        
		if (distance < _distance/4 && Pinched == false){
			Pinched = true;
		}
		else if (distance < _distance &&  distance >= _distance/4)
        {
			if(Pinched){
				Flicked = true;
			}
			Pinched = false;

			FlameHands.transform.position = Vector3.Lerp(PalmPoint.position, MidPoint, distance / _distance);
        } else if (distance >= _distance){
			if(Pinched){
				Flicked = true;
			}
			Pinched = false;

			FlameHands.transform.position = PalmPoint.position;
		}
	}
}
