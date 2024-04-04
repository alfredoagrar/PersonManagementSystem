
using System.Text.Json.Serialization;

public class Persona
{

    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("nombre")]
    public string Nombre { get; set; }
    [JsonPropertyName("apellidoPaterno")]
    public string ApellidoPaterno { get; set; }
    [JsonPropertyName("apellidoMaterno")]
    public string? ApellidoMaterno { get; set; }
    [JsonPropertyName("identificacion")]
    public string Identificacion { get; set; }
}
