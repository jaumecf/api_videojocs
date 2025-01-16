namespace sa24api.Models
{
    public class GameLevel1Dto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public float Seconds { get; set; }
        public string CreatedAt { get; set; } = string.Empty;
        public string UpdatedAt { get; set; } = string.Empty;
        public List<string> Caparrots { get; set; } = new List<string>();
    }
}
