using UnityEngine;
using System.Collections;

public class ShapeManagement : MonoBehaviour {

    SkinnedMeshRenderer thisSkinnedMeshRenderer;
    float weightValue = 0f;
    public float speed = 0f;
    void Awake()
    {
        thisSkinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
    }

    void Update()
    {
        if (thisSkinnedMeshRenderer.GetBlendShapeWeight(0) < 100f)
        {
            thisSkinnedMeshRenderer.SetBlendShapeWeight(0, weightValue);
            weightValue += speed;
        }
    }
}
