using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultWPF.Resources
{
    public static class Core
    {
        public static MainWindow mainWindow;
        public static DefaultDBEntities DB = new DefaultDBEntities();
        public static Users currentUser;
    }
}
