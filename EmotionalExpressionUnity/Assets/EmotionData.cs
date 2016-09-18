 using System;
 using UnityEngine;
 
 public class EmotionData {
    private static EmotionData _data = null;

    public float WeightValue { get; set; }
    public int Level { get; set; }

    private string _classification;
    public string Classification
    {
        get { return _classification; }
        set {
            // reset weight when the emotion changes
            if (value != null && !value.Equals(_classification, StringComparison.InvariantCultureIgnoreCase)) {
                Debug.Log("Switching");
                WeightValue = 0f;
            }
            _classification = value;
        }
    }
    private EmotionData() {
        this.Classification = "happy";
        this.Level = 0;
        this.WeightValue = 0f;
    }

    public int getEmotionNumber() {
        if (this.Classification.Equals("angry", StringComparison.InvariantCultureIgnoreCase)) {
            return 0;
        } else if (this.Classification.Equals("happy", StringComparison.InvariantCultureIgnoreCase)) {
            return 1;
        }

        return 0;
    }

    public static EmotionData Get() {
         return _data ?? (_data = new EmotionData());
     }
}