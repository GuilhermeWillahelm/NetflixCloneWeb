using NetflixCloneWeb.Dtos;

namespace NetflixCloneWeb.Repositories
{
    public interface IMyListRepository
    {
        MyListDto GetMyList(int id);
    }
}
