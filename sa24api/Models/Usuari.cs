using System.Text.Json.Serialization;

namespace sa24api.Models
{
    public class Usuari
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        [JsonIgnore]
        public string PasswordHash { get; set; } = string.Empty;
        public string? Foto { get; set; }
        public string? fotoMimeType { get; set; }
        public string? Token { get; set; }
        public bool Instructor { get; set; }
    }
}
