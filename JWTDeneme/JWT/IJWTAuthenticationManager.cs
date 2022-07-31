namespace JWTDeneme.JWT
{
    public interface IJWTAuthenticationManager
    {
        string Authenticate(string username, string password);
     }
}
