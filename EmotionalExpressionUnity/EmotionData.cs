 using UnityEngine;
 
 public class EmotionData {
    private static EmotionData _data;

    public string Classification { get; set; }
    public int Level { get; set; }
    public float Speed { get; set; }

    private EmotionData() {
         this.Classification = "happy";
         this.Level = 0;
         this.Speed = 2.0;
    }

     public static EmotionData get() {
        if (this._data == null) {
            this._data = new EmotionData();
        }

         return _data;
     }
}