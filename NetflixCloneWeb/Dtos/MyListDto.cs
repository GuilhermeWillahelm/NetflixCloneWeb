
namespace NetflixCloneWeb.Dtos
{
    public class MyListDto
    {
        public int Id { get; set; }
        //public int UserId { get; set; }
        //public virtual User? User { get; set; }
        public virtual ICollection<MovieDto>? Movies { get; set; }
        public virtual ICollection<SeriesDto>? Series { get; set; }

        public MyListDto()
        {
            Movies = new HashSet<MovieDto>();
            Series = new HashSet<SeriesDto>();
        }
    }
}
