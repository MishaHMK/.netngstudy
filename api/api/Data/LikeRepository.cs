using api.DTOS;
using api.Extensions;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class LikeRepository : ILikesRepository
    {
        private readonly DataContext _context;

        public LikeRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<UserLike> GetUserLike(int sourceUserId, int likedUserId)
        {
            return await _context.Likes.FindAsync(sourceUserId, likedUserId);
        }

        public async Task<PagedList<LikeDTO>> GetUserLikes(LikeParams likeParams)
        {
            var users = _context.Users.OrderBy(u => u.UserName).AsQueryable();
            var likes = _context.Likes.AsQueryable();

            if (likeParams.Predicate == "liked") //who by loged in user liked
            {
                likes = likes.Where(like => like.SourceUserId == likeParams.UserId);
                users = likes.Select(like => like.LikedUser);
            }

            if (likeParams.Predicate == "likedBy") //who liked loged in user
            {
                likes = likes.Where(like => like.LikedUserId == likeParams.UserId);
                users = likes.Select(like => like.SourceUser);
            }


           var likedUsers = users.Select(user => new LikeDTO
            {
                UserName = user.UserName,
                KnownAs = user.KnownAs,
                Age = user.DateOfBirth.CalculateAge(),
                PhotoUrl = user.Photos.FirstOrDefault(p => p.IsMain).Url,
                City = user.City,
                UserId = user.Id
            });

            return await PagedList<LikeDTO>.CreateAsync(likedUsers, likeParams.PageNumber, likeParams.PageSize);
        }

        public async Task<AppUser> GetUserWithLikes(int userId)
        {
            return await _context.Users
                .Include(x => x.LikedUsers)
                .FirstOrDefaultAsync(x => x.Id == userId);
        }
    }
}
