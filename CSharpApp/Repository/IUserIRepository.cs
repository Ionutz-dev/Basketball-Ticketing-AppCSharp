namespace CSharpApp.Repository;

using Model;

public interface IUserIRepository
{
    User? FindByUsername(string username);
}