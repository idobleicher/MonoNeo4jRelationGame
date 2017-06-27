using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GameForOurProject.DBConnection.DAL;

namespace GameForOurProject.DBConnection
{
    public interface IQueries
    {
        void QuerySearch(string query);
    }
}
