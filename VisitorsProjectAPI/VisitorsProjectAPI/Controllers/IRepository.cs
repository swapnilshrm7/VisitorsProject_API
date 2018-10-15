using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorsProjectAPI.Controllers
{
    interface IRepository
    {
        bool UserValidation(string GuardId, string Password);
    }
}
