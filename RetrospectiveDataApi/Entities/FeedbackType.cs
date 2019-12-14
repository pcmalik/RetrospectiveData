using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace RetrospectiveDataApi.Entities
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum FeedbackType
    {
        Positive,
        Negative,
        Idea
    }

}