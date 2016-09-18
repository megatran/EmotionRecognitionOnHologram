using System;
using UnityEngine;
using System.Collections;

public class ShapeManagement : MonoBehaviour {

    SkinnedMeshRenderer thisSkinnedMeshRenderer;

    private static EmotionData emotionData = EmotionData.Get();
    float weightValue = 0f;
    public float speed = 0f;
    
    void Awake() {
        thisSkinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
    }

    void Update()
    {
        int emotionVal = 0;
        if (emotionData.Classification.Equals("angry", StringComparison.InvariantCultureIgnoreCase)) {
            emotionVal = 0;
        } else if (emotionData.Classification.Equals("happy", StringComparison.InvariantCultureIgnoreCase)) {
            emotionVal = 1;
        }

        if (thisSkinnedMeshRenderer.GetBlendShapeWeight(emotionVal) < 100f)
        {
            thisSkinnedMeshRenderer.SetBlendShapeWeight(emotionVal, weightValue);
            weightValue += speed;
        }
    }
}
