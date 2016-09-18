using UnityEngine;
using System.Collections;

public class manager : MonoBehaviour {
    public Vector3 position;
    public Vector3 original_position;
	// Use this for initialization
	void Start () {
	    original_position = transform.position;
        //GetComponent<Renderer>().enabled = false;
        
    }

    public void teleportBack(GameObject model)
    {
        Debug.Log(model);
        model.transform.rotation = Quaternion.Euler(0, 0, 0);
        model.transform.position = new Vector3(1, 0, -20);


        rotate rotate_script = model.GetComponent<rotate>();
        rotate_script.enabled = true;

    }

	// Update is called once per frame
	void Update () {
	
	}
}
