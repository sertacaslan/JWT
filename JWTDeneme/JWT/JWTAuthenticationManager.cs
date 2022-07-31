using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTDeneme.JWT
{
    public class JWTAuthenticationManager : IJWTAuthenticationManager
    {
        private readonly string _Key;
        public JWTAuthenticationManager(string Key)
        {
            _Key = Key;
        }
        private readonly IDictionary<string, string> users = new Dictionary<string, string>
        {
            {"admin","pass" },
            {"user","1" }
        };

        public string Authenticate(string username, string password)
        {
            if (!users.Any(x => x.Key == username && x.Value == password))
            {
                return null;
            }
            var tokenHandler = new JwtSecurityTokenHandler();// token handle eden nesne
            var tokenKey = Encoding.ASCII.GetBytes(_Key);//token anahtarı, ctordan gelir
            var tokenDescriptor = new SecurityTokenDescriptor// token ayarları
            {
                Subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, username) }),
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials=new SigningCredentials(new SymmetricSecurityKey(tokenKey),SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
