using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Core.Data
{
    public interface ICurrentUserService
    {
        string GetCurrentUser();
    }
}
