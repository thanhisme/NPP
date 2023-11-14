namespace Infrastructure.Models.RequestModels.Permission
{
    public class UpdatePermissionRequest : CreatePermissionRequest
    {
        public bool IsDeleted { get; set; } = false;
    }
}
