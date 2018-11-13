using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StoryGraph;

public class SphereAppear : MonoBehaviour
{

    public Transform ThumbPoint;
    public Transform IndexPoint;
	public ParticleSystem particleSystem;
    private float _distance;
	private Vector3 _cacheLocalScale;
	public Renderer _renderer;
	public bool Popped = false;
	private enum PoseState {Idle, OkHand};
	private PoseState poseState = PoseState.Idle;


	public StoryListener popListener;

    void Start()
    {
		_cacheLocalScale = transform.localScale;
        _distance = Vector3.Distance(ThumbPoint.position, IndexPoint.position);
    }

	public void CenterMarble(){
		transform.position = Vector3.Lerp(ThumbPoint.position, IndexPoint.position, 0.5f);
        Vector3 _direction = (ThumbPoint.position - IndexPoint.position).normalized;
        transform.rotation = Quaternion.LookRotation(_direction);

		float distance = Vector3.Distance(ThumbPoint.position, IndexPoint.position); 
        
		if (distance < _distance/4 && Popped == false){
			_renderer.enabled = false;
			particleSystem.Emit(20);
			Popped = true;
			poseState = PoseState.OkHand;

			if(popListener != null)
			{
				popListener.StoryListenerAction.Invoke();	
			}
		}
		else if (distance < _distance &&  distance >= _distance/4)
        {
			_renderer.enabled = true;
            transform.localScale = new Vector3(Mathf.Lerp(transform.localScale.x, distance /_distance, 0.01f),  Mathf.Lerp(transform.localScale.y, distance /_distance, 0.01f), distance /_distance);
			Popped = false;
			poseState = PoseState.Idle; 
        } else if (distance >= _distance){
			_renderer.enabled = true;
            transform.localScale = _cacheLocalScale;
			Popped = false;
			poseState = PoseState.Idle; 
		}

        // transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
	}


    // void Update()
    // {
    //     transform.position = Vector3.Lerp(ThumbPoint.position, IndexPoint.position, 0.5f);
    //     Vector3 _direction = (ThumbPoint.position - IndexPoint.position).normalized;
    //     transform.rotation = Quaternion.LookRotation(_direction);

	// 	float distance = Vector3.Distance(ThumbPoint.position, IndexPoint.position); 
        
	// 	if (distance < _distance/4 && popped == false){
	// 		_renderer.enabled = false;
	// 		particleSystem.Emit(20);
	// 		popped = true;
	// 		poseState = PoseState.OkHand;
	// 	}
	// 	else if (distance < _distance &&  distance >= _distance/4)
    //     {
	// 		_renderer.enabled = true;
    //         transform.localScale = new Vector3(Mathf.Lerp(transform.localScale.x, distance /_distance, 0.01f),  Mathf.Lerp(transform.localScale.y, distance /_distance, 0.01f), distance /_distance);
	// 		popped = false;
	// 		poseState = PoseState.Idle; 
    //     } else if (distance >= _distance){
	// 		_renderer.enabled = true;
    //         transform.localScale = _cacheLocalScale;
	// 		popped = false;
	// 		poseState = PoseState.Idle; 
	// 	}
    //     // transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
    // }
}
