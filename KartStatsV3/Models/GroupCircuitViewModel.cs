namespace KartStatsV3.Models
{
    public class GroupCircuitViewModel
    {
        public int GroupId { get; set; }
        public List<Circuit> Circuits { get; set; }

        public GroupCircuitViewModel()
        {
            Circuits = new List<Circuit>();
        }
    }

}
