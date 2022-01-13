using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicIS
{
    class DBClass
    {
        private static ClinicEntities _context;
        public static ClinicEntities GetContext()
        {
            if (_context == null)
                _context = new ClinicEntities();
            return _context;
        }
    }
}
