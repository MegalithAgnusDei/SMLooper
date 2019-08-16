using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMLooper
{
    class Controller
    {
        public Controller()
        {

        }

        public void ParseSmFile(String path)
        {
            Model model = new Model();
            model.ParseSmFile(path);
        }
    }
}
