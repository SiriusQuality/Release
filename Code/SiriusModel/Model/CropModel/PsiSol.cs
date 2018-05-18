using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiriusModel.Model.CropModel
{
    class PsiSol
    {
        public static double CalcPsi(double ftsw)
        {
            if (ftsw < 0.001) return -1.5;
            return Math.Min(-0.1, -0.0578 + 0.246 * Math.Log(ftsw));
        }
    }
}
