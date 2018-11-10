using Leap.Unity.Interaction;
using Leap.Unity.Query;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWand : MonoBehaviour
{
    [Tooltip("The scene's InteractionManager, used to get InteractionControllers and "
            + "manage handle state.")]
    public InteractionManager interactionManager;

    public GameObject WandPrefab;

    private InteractionBehaviour SelectedWand;

    // private Vector3 _moveBuffer = Vector3.zero;
    // private Quaternion _rotateBuffer = Quaternion.identity;



    // Use this for initialization
    void Start()
    {
        if (interactionManager == null)
        {
            interactionManager = InteractionManager.instance;
        }

        // PhysicsCallbacks.OnPostPhysics += onPostPhysics;

    }

	// #region Handle Movement / Rotation

    // /// <summary>
    // /// Transform handles call this method to notify the tool that they were used
    // /// to move the target object.
    // /// </summary>
    // public void NotifyHandleMovement(Vector3 deltaPosition) {
    //   _moveBuffer += deltaPosition;
    // }

    // /// <summary>
    // /// Transform handles call this method to notify the tool that they were used
    // /// to rotate the target object.
    // /// </summary>
    // public void NotifyHandleRotation(Quaternion deltaRotation) {
    //   _rotateBuffer = deltaRotation * _rotateBuffer;
    // }

    // private void onPostPhysics()
    // {
    //     // Hooked up via PhysicsCallbacks in Start(), this method will run after
    //     // FixedUpdate and after PhysX has run. We take the opportunity to immediately
    //     // manipulate the SelectedWand object's and this object's transforms using the
    //     // accumulated information about movement and rotation from the Transform Handles.

    //     // Apply accumulated movement and rotation to SelectedWand object.
    //     SelectedWand.transform.rotation = _rotateBuffer * SelectedWand.transform.rotation;
    //     this.transform.rotation = SelectedWand.transform.rotation;

    //     // Match this transform with the SelectedWand object's (this will move child
    //     // TransformHandles' transforms).
    //     SelectedWand.transform.position += _moveBuffer;
    //     this.transform.position = SelectedWand.transform.position;

    //     // Explicitly sync TransformHandles' rigidbodies with their transforms,
    //     // which moved along with this object's transform because they are children of it.
    //     SelectedWand.rigidbody.position = this.transform.position;
    //     SelectedWand.rigidbody.rotation = this.transform.rotation;

    //     // Reset movement and rotation buffers.
    //     _moveBuffer = Vector3.zero;
    //     _rotateBuffer = Quaternion.identity;
    // }

    // #endregion


    // Update is called once per frame
    void Update()
    {

    }
}
