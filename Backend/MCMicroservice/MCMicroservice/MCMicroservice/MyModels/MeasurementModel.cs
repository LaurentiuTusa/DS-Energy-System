using System.Text.Json.Serialization;

namespace MCMicroservice.MyModels
{
    public class MeasurementModel
    {
        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }


        [JsonPropertyName("device_id")]
        public string DeviceId { get; set; }


        [JsonPropertyName("measurement_value")]
        public double MeasurementValue { get; set; }
    }
}
