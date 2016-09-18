 using UnityEngine;
 
 public class EmotionData {
    private static EmotionData _data = null;

    public string Classification { get; set; }
    public int Level { get; set; }

    private EmotionData() {
         this.Classification = "happy";
         this.Level = 0;
    }

     public static EmotionData Get() {
         return _data ?? (_data = new EmotionData());
     }
}