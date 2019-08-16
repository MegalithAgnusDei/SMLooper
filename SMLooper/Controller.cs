using SMLooper.Chart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMLooper
{
    public enum Measure { Measures }

    class Controller
    {
        SimFileInfo simFileInfo = new SimFileInfo();

        public Controller()
        {

        }

        public void ParseSmFile(String path)
        {
            Model model = new Model();
            simFileInfo = model.ParseSmFile(path);
            // возвращать инфо чтобы красивенько было, баннер там и название например
        }

        public void Start(string left, string right, string _measure)
        {
            Measure measure = Measure.Measures;
            if(_measure == "Measures")
            {
                measure = Measure.Measures;
            }
            double left_d = Double.Parse(left);
            double right_d = Double.Parse(right);

            Model model = new Model();
            model.Start(left_d, right_d, measure, simFileInfo);
        }
    }
}
