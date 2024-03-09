using Utils.Shared.DTO;

namespace ToDoAPI.Repository.IRepository
{
    public interface IAministrationUserRepository
    {
        bool IsUniqueUser(string username);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginrequestdto);
        Task<AdministrationUserDTO> Register(RegistrationRequestDTO registrationrequestdto);
    }
}
