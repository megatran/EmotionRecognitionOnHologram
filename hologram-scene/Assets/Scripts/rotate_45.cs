using UnityEngine;
using System.Collections;

public class rotate_45 : MonoBehaviour {

    private int ang;
    private int timer;

	// Use this for initialization
	void Start () {
        ang = 1;
        timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        // Sets the transforms rotation to rotate 30 degrees around the y-axis

        transform.RotateAround(transform.position, Vector3.up, ang);
        transform.RotateAround(transform.position, Vector3.left, ang);
 

        
    }
}
