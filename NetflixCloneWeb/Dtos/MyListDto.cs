
namespace NetflixCloneWeb.Dtos
{
    public class MyListDto
    {
        public int Id { get; set; }
        //public int UserId { get; set; }
        //public virtual User? User { get; set; }
        public virtual ICollection<VideoDto>? Videos { get; set; }

        public MyListDto()
        {
            Videos = new HashSet<VideoDto>();
        }
    }
}
