using AutoMapper;
using PixelStamp.Core.Dtos;
using PixelStamp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PixelStamp.Core.Mapping
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<AppUser, UserDto>()
                .ForMember(res => res.Roles, opt => opt.MapFrom(u => u.UserRoles.Select(r => r.Role)));

            CreateMap<UserDto, AppUser>()
                .AfterMap((userres, user) =>
                {
                    var userRoles = new List<AppUserRole>();
                    foreach (var role in user.UserRoles)
                        if (!userres.Roles.Contains(role.RoleId.ToString()))
                            userRoles.Add(role);

                    foreach (var item in userRoles)
                        user.UserRoles.Remove(item);

                    foreach (var id in userres.Roles)
                        if (!user.UserRoles.Select(c => c.RoleId.ToString()).Contains(id))
                            user.UserRoles.Add(new AppUserRole
                            {
                                RoleId = Guid.Parse(id),
                                UserId = user.Id
                            });

                });
        }
    }
}
