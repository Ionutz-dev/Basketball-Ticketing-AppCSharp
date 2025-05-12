using model;

namespace persistence
{
    public interface IUserRepository
    {
        User FindByUsernameAndPassword(string username, string password);
    }
}