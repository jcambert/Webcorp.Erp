using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.Model;

namespace Webcorp.Business
{
    public interface IAuthenticationService
    {
        Task<bool> Login(string societe,string username, string password);
        
        Task<bool> Logout();

        Utilisateur Utilisateur { get;  }

    }
}
