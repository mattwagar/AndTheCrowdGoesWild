using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StoryGraph;

public class PopMarbleEvent : MonoBehaviour
{

    public Transform ThumbPoint;
    public Transform IndexPoint;
	public ParticleSystem particleSystem;
    protected float _distance;
	protected Vector3 _cacheLocalScale;
	public bool Popped = false;
	protected enum PoseState {Idle, OkHand};
	protected PoseState poseState = PoseState.Idle;


	public StoryListener popListener;

    void Start()
    {
		_cacheLocalScale = Vector3.one;
        _distance = Vector3.Distance(ThumbPoint.position, IndexPoint.position);
    }

	public void ResetPop(){
		Popped = false;
	}

	public virtual void CenterMarble(){
		transform.position = Vector3.Lerp(ThumbPoint.position, IndexPoint.position, 0.5f);
        Vector3 _direction = (ThumbPoint.position - IndexPoint.position).normalized;
        transform.rotation = Quaternion.LookRotation(_direction);

		float distance = Vector3.Distance(ThumbPoint.position, IndexPoint.position); 
        
		if (distance < _distance/4 && Popped == false){
			particleSystem.Emit(20);
			Popped = true;
		}
		else if (distance < _distance &&  distance >= _distance/4)
        {
            transform.localScale = new Vector3(Mathf.Lerp(transform.localScale.x, distance /_distance, 0.1f),  Mathf.Lerp(transform.localScale.y, distance /_distance, 0.1f), Mathf.Lerp(transform.localScale.z, distance /_distance, 0.1f));
			Popped = false;
        } else if (distance >= _distance){
            transform.localScale = _cacheLocalScale;
			Popped = false;
		}
	}
}
