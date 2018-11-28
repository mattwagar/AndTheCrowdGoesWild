using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StoryGraph;

public class PopFireMarbleEvent : PopMarbleEvent
{

	public ParticleSystem sparkSystem;
	public ParticleSystem flashSystem;

	public override void CenterMarble(){
		transform.position = Vector3.Lerp(ThumbPoint.position, IndexPoint.position, 0.5f);
        Vector3 _direction = (ThumbPoint.position - IndexPoint.position).normalized;
        transform.rotation = Quaternion.LookRotation(_direction);

		float distance = Vector3.Distance(ThumbPoint.position, IndexPoint.position); 
        
		if (distance < _distance/4 && Popped == false){
			sparkSystem.Play();
			flashSystem.Play();
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
