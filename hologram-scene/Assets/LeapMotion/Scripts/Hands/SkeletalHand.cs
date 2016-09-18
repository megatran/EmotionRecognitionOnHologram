/******************************************************************************\
* Copyright (C) Leap Motion, Inc. 2011-2014.                                   *
* Leap Motion proprietary. Licensed under Apache 2.0                           *
* Available at http://www.apache.org/licenses/LICENSE-2.0.html                 *
\******************************************************************************/

using UnityEngine;
using System.Collections;
using Leap;


/** 
 * A hand object consisting of discrete, component parts.
 * 
 * The hand can have game objects for the palm, wrist and forearm, as well as fingers.
 */
public class SkeletalHand : HandModel {
  protected const float PALM_CENTER_OFFSET = 0.015f;
    Controller controller;
    private bool model_enabled = false;
    GameObject main_model;
    private int rotate_senstivity = -80;

    void Start() {
        // Ignore collisions with self.
        
        Leap.Utils.IgnoreCollisions(gameObject, gameObject);

    for (int i = 0; i < fingers.Length; ++i) {
      if (fingers[i] != null) {
        fingers[i].fingerType = (Finger.FingerType)i;
      }
    }
  }

    void modelHandControl(GameObject model)
    {
        controller = new Controller();
        Frame frame = controller.Frame();
        Hand hand = frame.Hands.Rightmost;
        Vector position = hand.PalmPosition;
        Vector velocity = hand.PalmVelocity;
        Vector direction = hand.Direction;
        //Debug.Log("x: " + direction.x);
        //Debug.Log("y: " + direction.y);
        //Debug.Log("z: " + direction.z);

        model.transform.rotation = Quaternion.Euler(direction.x*rotate_senstivity, direction.y* rotate_senstivity, direction.z* rotate_senstivity);

    }

    void rotate_disabler()
    {
        navManager nav_script = GameObject.Find("Cube").GetComponent<navManager>();
        string[] obj_list = nav_script.object_list;
        int iter = nav_script.obj_iter;

        main_model = GameObject.Find(obj_list[iter]);
        rotate rotate_script = main_model.GetComponent<rotate>();
        rotate_script.enabled = false;
        model_enabled = true;
        modelHandControl(main_model);
    }

    void rotate_enabler()
    {
        Debug.Log("Hand not visible");
        navManager nav_script = GameObject.Find("Cube").GetComponent<navManager>();
        string[] obj_list = nav_script.object_list;
        int iter = nav_script.obj_iter;

        GameObject temp = GameObject.Find(obj_list[iter]);
        rotate rotate_script = temp.GetComponent<rotate>();
        rotate_script.enabled = true;
    }
    /** Updates the hand and its component parts by setting their positions and rotations. */
    public override void UpdateHand() {
    SetPositions();
  }

  protected Vector3 GetPalmCenter() {
    Vector3 offset = PALM_CENTER_OFFSET * Vector3.Scale(GetPalmDirection(), transform.lossyScale);
    return GetPalmPosition() - offset;
  }

  protected void SetPositions() {
        
    for (int f = 0; f < fingers.Length; ++f) {
      if (fingers[f] != null)
        fingers[f].UpdateFinger();
    }

        if (palm != null)
        {
            if (model_enabled)
            {
                modelHandControl(main_model);
            }
            else
            {
                rotate_disabler();
            }
            palm.position = GetPalmCenter();
            palm.rotation = GetPalmRotation();
        }
        

    if (wristJoint != null) {
      wristJoint.position = GetWristPosition();
      wristJoint.rotation = GetPalmRotation();
    }

    if (forearm != null) {
      forearm.position = GetArmCenter();
      forearm.rotation = GetArmRotation();
    }
  }

}


