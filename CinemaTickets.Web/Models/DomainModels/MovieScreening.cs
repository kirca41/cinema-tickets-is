namespace CinemaTickets.Web.Models.DomainModels
{
    public class MovieScreening : BaseEntity
    {
        public Movie Movie { get; set; }
        public Guid MovieId { get; set; }
        public DateTime DateAndTime { get; set; }
        public int TicketPrice { get; set; }
    }
}
