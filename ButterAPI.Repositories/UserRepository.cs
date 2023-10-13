using Butter.Entities;
using Butter.Models;
using Butter.Repositories.Interfaces;
using ButterAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butter.Repositories
{
    public class UserRepository : BaseRepository<UserModel, UserEntity, int, UserUpdate>, IUserRepository
    {
        public UserRepository(string cnstr) : base(cnstr)
        {
        }

        public override UserModel Add(UserModel model)
        {
            if (IfNicknameExists(model.NickName)) return null;
            _ctx.Add(ToEntite(model));
            _ctx.SaveChanges();

            UserEntity? user = _ctx.Users.FirstOrDefault(x => x.NickName == model.NickName);
            if (user is not null)
            {
                return ToModel(user);
            }
            return null;

        }

        public override UserUpdate Update(UserUpdate model)
        {
            var existingUser = _ctx.Users.FirstOrDefault(x => x.UserId == model.UserId);
            if (existingUser is not null)
            {
                if (IfNicknameExists(model.NickName)) return null;
                existingUser.NickName = model.NickName;
                existingUser.UserId = model.UserId;
                existingUser.Town = model.Town;
                existingUser.Genre = model.Genre;
                existingUser.BirthDate = model.BirthDate;
                existingUser.Email = model.Email;
                _ctx.SaveChanges();
            }
            return ToModelUpdate(existingUser);
        }

        public override UserEntity ToEntite(UserModel Model)
        {
            if (Model == null) return null;
            return new UserEntity()
            {
                BirthDate = Model.BirthDate,
                Email = Model.Email,
                NickName = Model.NickName,
                Password = Model.Password,
                Town = Model.Town,
                UserId = Model.UserId,
                Genre = Model.Genre
            };
        }

        public override UserModel ToModel(UserEntity entite)
        {
            if (entite == null) return null;
            return new UserModel()
            {
                BirthDate = entite.BirthDate,
                Email = entite.Email,
                NickName = entite.NickName,
                Password = "***************",
                Town = entite.Town,
                UserId = entite.UserId,
                Genre = entite.Genre
            };
        }

        public override UserUpdate ToModelUpdate(UserEntity entite)
        {
            if (entite == null) return null;
            return new UserUpdate()
            {
                UserId = entite.UserId,
                BirthDate = entite.BirthDate,
                Email = entite.Email,
                NickName = entite.NickName,
                Town = entite.Town,
                Genre = entite.Genre
            };
        }
        public override UserEntity UserUpdateToEntite(UserUpdate model)
        {
            if (model is null) return null;
            return new UserEntity()
            {
                UserId = model.UserId,
                BirthDate = model.BirthDate,
                Email = model.Email,
                NickName = model.NickName,
                Town = model.Town,
                Genre = model.Genre
            };
        }

        public bool IfNicknameExists(string nickname)
        {
            UserEntity? u = _ctx.Users.FirstOrDefault(x=>x.NickName == nickname);
            if (u == null) return false;
            return true;
        }
    }
}
