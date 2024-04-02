using Newtonsoft.Json;
using System.Text;
using NetflixCloneWeb.Dtos;
using System.Security.Claims;
using NetflixCloneWeb.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using NetflixCloneWeb.Models;
using System.Net.Http.Json;
using Microsoft.EntityFrameworkCore;

namespace NetflixCloneWeb.Repositories
{
    public class UserRepository : IUserRepository
    {
        HttpClient _httpClient = new HttpClient() { BaseAddress = new Uri("https://localhost:7015/api/") };
        private readonly IHttpContextAccessor _context;

        public UserDto CreateUser(UserDto user)
        {
            string data = JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            //HttpResponseMessage response = _httpClient.PostAsync(_httpClient.BaseAddress + "Users/Register", content).Result;
            var response = _httpClient.PostAsJsonAsync<UserDto>(_httpClient.BaseAddress + "Users/Register", user);
            response.Wait();

            if (response.Status == TaskStatus.RanToCompletion)
            {
                return null;
            }

            return user;
        }

        public bool DeleteUser(int id)
        {
            //HttpResponseMessage response = _httpClient.DeleteAsync(_httpClient.BaseAddress + "Users/" + id).Result;
            var response = _httpClient.DeleteAsync(_httpClient.BaseAddress + "Users/" + id);
            response.Wait();

            if (response.Status != TaskStatus.RanToCompletion)
            {
                return false;
            }

            return true;
        }

        public UserDto EditUser(int id, UserDto user)
        {
            string data = JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            //HttpResponseMessage response = _httpClient.PutAsync(_httpClient.BaseAddress + "Users/" + id, content).Result;
            var response = _httpClient.PutAsJsonAsync<UserDto>(_httpClient.BaseAddress + "Users/" + id, user);
            response.Wait();

            if (response.Status == TaskStatus.RanToCompletion)
            {
                return null;
            }

            return user;
        }

        public UserDto GetUserById(int? id)
        {
            if (id == null)
            {
                return null;
            }

            UserDto user = new UserDto();
            //HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "Users/" + id).Result;
            var response = _httpClient.GetFromJsonAsync<UserDto>(_httpClient.BaseAddress + "Users/" + id);
            response.Wait();

            if (response.Status == TaskStatus.RanToCompletion)
            {
                user = response.Result;
            }

            if (user == null)
            {
                return null;
            }

            return user;
        }

        public int GetUserId()
        {
            var user = _context.HttpContext.User.Claims.Where(u => u.Type == ClaimTypes.NameIdentifier).Select(x => x.Value);
            int idUser;

            try
            {
                if (user == null)
                {
                    idUser = 0;
                }
                else
                {
                    idUser = int.Parse(user.Last());
                }
            }
            catch (Exception ex)
            {
                idUser = 0;
            }

            return idUser;
        }

        public string GetUserName()
        {
            var user = _context.HttpContext.User.Claims.Where(u => u.Type == ClaimTypes.Name).Select(x => x.Value);
            string userName = "";
            try
            {
                userName = user.Last();

                return userName;
            }
            catch (Exception ex)
            {
                userName = "";
            }

            return userName;
        }

        public bool LoginUser(UserLoginDto userLogin)
        {
            string data = JsonConvert.SerializeObject(userLogin);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            //HttpResponseMessage response = _httpClient.PostAsync(_httpClient.BaseAddress + "Users/LoginUser", content).Result;
            var response = _httpClient.PostAsJsonAsync<UserLoginDto>(_httpClient.BaseAddress + "Users/LoginUser", userLogin);
            response.Wait();

            if (response.Status == TaskStatus.RanToCompletion)
            {
                data = response.Result.Content.ReadAsStringAsync().Result;
                userLogin = JsonConvert.DeserializeObject<UserLoginDto>(data);

                List<Claim> diretoAcesso = new List<Claim> {
                    new Claim(ClaimTypes.NameIdentifier, userLogin.Id.ToString()),
                    new Claim(ClaimTypes.Name, userLogin.UserName)
                };

                var identity = new ClaimsIdentity(diretoAcesso, CookieAuthenticationDefaults.AuthenticationScheme);
                var userMain = new ClaimsPrincipal(identity);

                var authProperties = new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.Now.AddHours(8),
                    IssuedUtc = DateTime.Now
                };

                var userSession = _context.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userMain, authProperties);

                return true;
            }

            return false;
        }

        public void Logoff()
        {
            _context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
