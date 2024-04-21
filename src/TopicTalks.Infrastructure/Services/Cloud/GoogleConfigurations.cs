using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;

namespace TopicTalks.Infrastructure.Services.Cloud;

public class GoogleConfigurations
{
    public DriveService GetDriveService()
    {
        var credential = GoogleCredential.FromFile("GoogleCredentials.json")
            .CreateScoped(DriveService.Scope.Drive);

        return new DriveService(new BaseClientService.Initializer() {
            HttpClientInitializer = credential,
            ApplicationName = "Your Application Name",
        });
    }
}