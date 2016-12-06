using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Core.Data
{
    /// <summary>
    /// Intrface for providing the name of the user who had perfomed the changes changes on the entity objects
    /// </summary>
    public interface ICurrentUserService
    {
        string GetCurrentUser();
    }
}
