using System;
using Newtonsoft.Json;

namespace Common
{
    [Serializable]
    public class ButtonData
    {
        [JsonProperty("id")]
        public int Id;
    
        [JsonProperty("text")]
        public string FirstName;
    
        [JsonProperty("color")]
        public float[] Color;
    
        [JsonProperty("animationType")]
        public bool AnimationType;
        
    }
}