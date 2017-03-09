using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Infrastructure.Tokens
{
    /// <summary>
    /// Token Generator Interface
    /// </summary>
    public interface ITokenGenerator
    {
        string GetUserToken(string accountID, string userID);



    }
}
