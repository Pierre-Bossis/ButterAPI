using Butter.Entities;
using Butter.Models;
using ButterAPI.Models;

namespace Butter.Repositories.Interfaces
{
    public interface IUserRepository : IRepo<UserModel, UserEntity, int,UserUpdate>
    {
        UserEntity ToEntite(UserModel Model);
        UserModel ToModel(UserEntity entite);
        UserUpdate ToModelUpdate(UserEntity entite);
        UserEntity UserUpdateToEntite(UserUpdate uu);
        bool IfNicknameExists(string nickname);
    }
}