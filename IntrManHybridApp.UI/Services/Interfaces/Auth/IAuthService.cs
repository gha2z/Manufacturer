using IntrManApp.Shared.Contract;

namespace IntrManHybridApp.UI.Services
{
    public interface IAuthService
    {
        Task LoginAsync(string username, string password);
        Task<List<FeatureAccessResponse>> GetFeaturesAsync();
        Task<bool> ChangePasswordAsync(ChangePasswordRequest request);
        Task<IEnumerable<ApplicationUserRoleResponse>> GetApplicationUserRolesAsync();
        Task<IEnumerable<UserRoleResponse>> GetUserRolesAsync();
        Task<bool> SetFeatureAccessAsync(SetFeatureAccessRequest request);
        Task<Guid> NewUserAsync(NewUserRequest request);
        Task<Guid> UpdateUserAsync(UpdateUserRequest request);
        Task<bool> DeleteUserAsync(Guid id);
        Task<Guid> NewOrUpdateRoleAsync(NewOrUpdateRoleRequest request);
        Task<bool> DeleteRoleAsync(Guid id);

    }
}
