using System.Threading.Tasks;


namespace ChatApp_LumbanKuizon.DependencyServices
{
    public interface iFirebaseAuth
    {
        Task<Models.FirebaseAuthResponseModel> LoginWithEmailPassword(string email, string password);
        Task<Models.FirebaseAuthResponseModel> SignUpWithEmailPassword(string name, string email, string password);
        Task<Models.FirebaseAuthResponseModel> SignOut();
        Task<Models.FirebaseAuthResponseModel> IsLoggedIn();
        Task<Models.FirebaseAuthResponseModel> ResetPassword(string email);
    }
}
