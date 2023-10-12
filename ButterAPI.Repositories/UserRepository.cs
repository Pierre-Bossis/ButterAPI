﻿using Butter.Entities;
using Butter.Models;
using Butter.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butter.Repositories
{
    public class UserRepository : BaseRepository<UserModel, UserEntity, int>, IUserRepository
    {
        public UserRepository(string cnstr) : base(cnstr)
        {
        }

        public override UserModel Add(UserModel model)
        {
            _ctx.Add(ToEntite(model));
            _ctx.SaveChanges();

            UserEntity? user = _ctx.Users.FirstOrDefault(x => x.NickName == model.NickName);
            if (user is not null)
            {
                return ToModel(user);
            }
            return null;

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
    }
}
