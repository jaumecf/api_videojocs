namespace sa24api.Models
{
    public class Event
    {
        public int idPartida { get; set; }
        public float pos_PlayerX { get; set; } 
        public float pos_PlayerY { get; set; } 
        public float pos_PlayerZ { get; set; } 
        public float rot_PlayerX { get; set; } 
        public float rot_PlayerY { get; set; } 
        public float rot_PlayerZ { get; set; } 
        public float enemicX { get; set;}
        public float enemicY { get; set;}
        public float enemicZ { get; set;}
        public int vida_Cupula { get; set;} 
        public int num_Naus { get; set;}
    }
}
