using System;

namespace PPCT.DataAccessLayer.CustomModels
{
    public class TokenModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? Expiration { get; set; }
        public string Message { get; set; }
    }
}
