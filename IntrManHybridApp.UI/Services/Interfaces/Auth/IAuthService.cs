using IntrManApp.Shared.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntrManHybridApp.UI.Services
{
    public interface IAuthService
    {
        Task Login(string username, string password);

    }
}
