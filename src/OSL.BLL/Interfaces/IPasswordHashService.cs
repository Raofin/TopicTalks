namespace OSL.BLL.Interfaces;

internal interface IPasswordHashService
{
    (string hashedPassword, string salt) HashPasswordWithSalt(string password);
    bool VerifyPassword(string hashedPasswordFromDatabase, string saltFromDatabase, string providedPassword);
}