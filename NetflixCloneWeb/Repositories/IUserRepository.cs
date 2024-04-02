using NetflixCloneWeb.Dtos;

namespace NetflixCloneWeb.Repositories
{
    public interface IUserRepository
    {
        UserDto GetUserById(int? id);
        UserDto CreateUser(UserDto user);
        bool LoginUser(UserLoginDto userLogin);
        void Logoff();
        UserDto EditUser(int id, UserDto user);
        bool DeleteUser(int id);
        int GetUserId();
        string GetUserName();

    }
}
