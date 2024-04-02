namespace NetflixCloneWeb.Dtos
{
    public class VideoDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Director { get; set; }
        public string? ContentVideo { get; set; }
        public string? ThumbVideo { get; set; }
        public string? Genre { get; set; }
        public string? Type { get; set; }
        public double Rating { get; set; }
    }
}
