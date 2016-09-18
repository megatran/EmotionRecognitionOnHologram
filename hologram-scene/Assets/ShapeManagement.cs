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
        int emotionVal = emotionData.getEmotionNumber();

        if (thisSkinnedMeshRenderer.GetBlendShapeWeight(emotionVal) < 100f)
        {
            thisSkinnedMeshRenderer.SetBlendShapeWeight(emotionVal, emotionData.WeightValue);
            thisSkinnedMeshRenderer.SetBlendShapeWeight((emotionVal + 1) % 2, 0);
            emotionData.WeightValue += speed;
        }
    }
}
