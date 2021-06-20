using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Threading.Tasks;
using PixelStamp.Core.Entities;
using PixelStamp.Infrastructure.DbContexts;
using PixelStamp.Core.Interfaces.Repositories;
using PixelStamp.Core.Dtos;
using System.Linq;
using System.Security.Claims;

namespace PixelStamp.Infrastructure.Repositories
{
    public class UserRepository : RepositoryBase,IUserRepository
    {
        private readonly UserManager<AppUser> _userManager;
        public UserRepository(UserManager<AppUser> userManager, CommandDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor, IDistributedCache cache)
            : base(context, mapper, httpContextAccessor)
        {
            _userManager = userManager;
        }
        
        public async Task<UserDto> GetAsync(Guid id)
        {
            var user = await commandDb.Users
                    .Include(m => m.UserRoles)
                        .ThenInclude(ur => ur.Role)
                    .Include(c => c.UserClaims)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(u => u.Id == id);

            return mapper.Map<UserDto>(user);
        }
        public async Task AddAsync(UserDto userDto)
        {
            var entityToAdd = mapper.Map<UserDto, AppUser>(userDto);
            entityToAdd.Id = Guid.NewGuid();
            entityToAdd.CreatedOn = DateTime.Now;
            entityToAdd.CreatedBy = UserId;

            var result = await _userManager.CreateAsync(entityToAdd, userDto.Password);

            if (result.Succeeded)
            {
                // if the user created add the roles 
                await _userManager.AddToRolesAsync(entityToAdd, userDto.Roles);
            }

            mapper.Map(entityToAdd, userDto);
        }

        public async Task<AppUser> GetByUserName(string userName)
        {
            return await commandDb.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserName == userName);
        }

        public async Task<bool> CheckUserExists(string userName)
        {
            var userInDb = await _userManager.FindByNameAsync(userName);
            if (userInDb != null)
                return true;

            return false;
        }
        public async Task EditAsync(UserDto userDto)
        {
            AppUser user = await _userManager.Users
               .Include(m => m.UserRoles)
                .ThenInclude(ur => ur.Role)
               .Include(c => c.UserClaims)
               .FirstOrDefaultAsync(u => u.Id == userDto.Id.Value);

            //mapper.Map(userDto, user);
            user.Budget = userDto.Budget;
            var roles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, roles.ToArray());

            // Add the roles from the model
            //await _userManager.AddToRolesAsync(user, userDto.Roles);

            // add the claims from the model
            //if (userDto.Claims.Count() > 0)
            //    await AddUserClaims(user, userDto);

            user.UpdatedBy = UserId;
            user.UpdatedOn = DateTime.Now;

            await _userManager.UpdateAsync(user);
            mapper.Map(user, userDto);
        }

        private async Task AddUserClaims(AppUser user, UserDto userDto)
        {
            // get all user's claims, and remove them
            var claims = await _userManager.GetClaimsAsync(user);
            await _userManager.RemoveClaimsAsync(user, claims.ToArray());

            // add Permission claims from the model
            foreach (var claimValue in userDto.Claims)
            {
                // get claim details by claim value
                var claimDetails = await commandDb.RoleClaims.FirstOrDefaultAsync(c => c.ClaimValue == claimValue);

                // get role details by role id
                var roleDetails = await commandDb.Roles.FirstOrDefaultAsync(r => r.Id == claimDetails.RoleId);

                // check user has role to add claim
                var hasRole = await _userManager.IsInRoleAsync(user, roleDetails.Name);

                if (hasRole)
                {
                    var claim = new Claim(claimDetails.ClaimType, claimDetails.ClaimValue);
                    await _userManager.AddClaimAsync(user, claim);
                }
            }
        }



    }
}