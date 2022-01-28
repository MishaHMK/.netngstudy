using api.DTOS;
using api.Models;

namespace api.Interfaces
{
    public interface IPhotoRepository
    {
        Task<bool> SaveAllAsync();
        Task<IEnumerable<PhotoForApprovalDto>> GetUnapprovedPhotos();
        Task<Photo> GetPhotoById(int id);
        void RemovePhoto(Photo photo);
    }
}
