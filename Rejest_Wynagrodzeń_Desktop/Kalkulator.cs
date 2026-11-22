using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rejest_Wynagrodzeń_Desktop
{
    public class Kalkulator
    {
        public double Brutto { get; set; }
        public bool Ulga { get; set; }

        public Kalkulator(double brutto, bool ulga)
        {
            Brutto = brutto;
            Ulga = ulga;
        }

        public bool CzyPoprawna()
        {
            return Brutto > 0;
        }

        public double ZUS()
        {
            return Brutto * 0.1371;
        }

        public double Zdrowotna()
        {
            return (Brutto - ZUS()) * 0.09;
        }

        public double PIT()
        {
            if (Ulga)
                return 0;

            return (Brutto - ZUS()) * 0.12;
        }

        public double Netto()
        {
            return Brutto - ZUS() - Zdrowotna() - PIT();
        }
    }
}
