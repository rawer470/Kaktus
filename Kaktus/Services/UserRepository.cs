using System;
using Kaktus.Data;
using Kaktus.Models;
using Kaktus.Services.Interfaces;

namespace Kaktus.Services;

public class UserRepository : Repository<User>, IUserRepository
{
     private readonly Context context;

    public UserRepository(Context context) : base(context)
    {
        this.context = context;
    }

    public void Update(User obj)
    {

    }
}
