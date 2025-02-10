using System;
using Kaktus.Models;

namespace Kaktus.Services.Interfaces;

public interface IUserRepository : IRepository<User>
{
    void Update(User obj);
}
