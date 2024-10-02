using FirebaseAdmin;
using FirebaseAdmin.Auth;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;

namespace Hubbies.Infrastructure.Auth;

public class FirebaseService : IFirebaseService
{
    public FirebaseService(IOptions<FirebaseSettings> settings)
    {
        FirebaseApp.Create(new AppOptions
        {
            Credential = GoogleCredential.FromJson(settings.Value.ServiceKey!)
        });
    }

    public async Task<string> GetEmailFromTokenAsync(string token)
    {
        var decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(token);
        return decodedToken.Claims["email"].ToString()!;
    }
}
