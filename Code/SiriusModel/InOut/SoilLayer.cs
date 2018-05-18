///<Behnam>
using System;
using System.Xml.Serialization;
using SiriusModel.InOut.Base;
using System.Windows.Forms;
using SiriusModel.Model.Base;
using SiriusModel.Model.SoilModel.EnergyBalance;


namespace SiriusModel.InOut
{
    [Serializable]
    public class SoilLayer : Child<double>  
    {
        private double clay;
        private double kql;
        private double ssat;
        private double sdul;
        private double sll;
        private double depth;
        
        ///<summary>
        ///The Layer Clay.
        ///</summary>
        public double Clay
        {
            get { return clay; }
            set
            {
                this.SetStruct(ref clay, ref value, "Clay");
                AssertClay();
            }
        }

        private void AssertClay()
        {
            this.Assert(clay, d => d >= 0 && d < 100, "Clay content", ">=0 and <=100", null);
        }

        ///<summary>
        ///The Layer Kq.
        ///</summary>
        public double Kql
        {
            get
            {
                if (SoilItem.IsKqCalcStatic)
                {
                    if (clay <= 9.5) kql = 1; Kql = kql;
                    if (clay <= 58.3 & clay > 9.5) kql = Math.Round(1.0271 - 0.000302 * (clay * clay), 2); Kql = kql;
                    if (clay > 58.3) kql = 0; Kql = kql;
                }
                else kql = Math.Round(kql, 2); Kql = kql;
                return kql;
            }
            set
            {
                this.SetStruct(ref kql, ref value, "Kql");
            }
        }

        ///<summary>
        ///The Layer SSAT.
        ///</summary>
        public double SSAT
        {
            get { return ssat; }
            set
            {
                this.SetStruct(ref ssat, ref value, "SSAT");
                AssertSSAT();
                AssertSDUL();
                AssertSLL();
            }
        }

        private void AssertSSAT()
        {
            this.Assert(ssat, d => d >= 0 && d < 100 && d >= sdul, "Saturation", ">=0 and <=100 and >=Field capacity", null);
        }

        ///<summary>
        ///The Layer SDUL.
        ///</summary>
        public double SDUL
        {
            get { return sdul; }
            set
            {
                if (this.SetStruct(ref sdul, ref value, "SDUL")) NotifyPropertyChanged("AWC");
                AssertSSAT();
                AssertSDUL();
                AssertSLL();
            }
        }

        private void AssertSDUL()
        {
            this.Assert(sdul, d => d >= 0 && d <= 100 && d <= ssat && d >= sll, "Field capacity", ">=0 and <=100 and <=Saturation and >=Perment wilting point", null);
        }

        ///<summary>
        ///The Layer SLL.
        ///</summary>
        public double SLL
        {
            get { return sll; }
            set
            {
                if (this.SetStruct(ref sll, ref value, "SLL")) NotifyPropertyChanged("AWC");
                AssertSSAT();
                AssertSDUL();
                AssertSLL();
            }
        }

        private void AssertSLL()
        {
            this.Assert(sll, d => d >= 0 && d <= 100 && d <= ssat && d <= sdul, "Perment wilting point", ">=0 and <=100 and <=Saturation and <=Field capacity", null);
        }

        ///<summary>
        ///The Layer depth.
        ///</summary>
        public double Depth
        {
            get { return depth; }
            set 
            {
                if (this.SetStruct(ref depth, ref value, "Depth")) NotifyPropertyChanged("AWC");
                this.Assert(depth, d => d > 0 && d < 10, "Depth", ">0 and <10", null);
            }
        }

        [XmlIgnore]
        public double AWC
        {
            get
            {
                var soilItem = SoilItemParent;
                double res = 0;

                if (soilItem != null)
                {
                    var depthMM = Depth * 1000;
                    var thisIndex = soilItem.Layers.IndexOf(this);
                    if (thisIndex > 0)
                        res = (depthMM - (soilItem.Layers[thisIndex - 1].Depth * 1000)) * (SDUL - SLL) / 100.0;
                    else
                        res = depthMM * (SDUL - SLL) / 100.0;

                    res = Math.Round(res * 10) / 10; // Arrondi un chiffre après la virgule
                }
                return res;
            }
        }

        public SoilLayer()
        {
            Clay = 30;
            Kql = Math.Round(1.0271 - 0.000302 * (900), 2);
            SSAT = 56;
            SDUL = 36;
            SLL = 16;
            Depth = 1;
        }

        public SoilLayer(double clay, double kql, double ssat, double sdul, double sll)
        {
            Clay = clay;
            Kql = kql;
            SSAT = ssat;
            SDUL = sdul;
            SLL = sll;
        }

     public override bool NotifyPropertyChanged(string propertyName)
        {
            if (SoilItemParent != null && SoilItemParent.ProjectDataFileParent != null) SoilItemParent.ProjectDataFileParent.IsModified = true;
            if (base.NotifyPropertyChanged(propertyName))
            {
                if (propertyName == "SDUL" || propertyName == "SLL")
                {
                    NotifyPropertyChanged("AWC");
                }
                else if (propertyName == "AWC" && SoilItemParent != null)
                {
                    SoilItemParent.NotifyPropertyChanged("TotalAWC");
                }
                return true;
            }
            return false;
        }
        
        public SoilItem SoilItemParent
        {
            get { return Parent as SoilItem; }
        }

        public override void CheckWarnings()
        {
            Clay = Clay;
            Kql = Kql;
            SSAT = SSAT;
            SDUL = SDUL;
            SLL = SLL; 
            Depth = Depth;
        }

        public override string WarningFileID
        {
            get
            {
                return (SoilItemParent!=null && SoilItemParent.ProjectDataFileParent != null) ? SoilItemParent.ProjectDataFileParent.ID : "?";
            }
        }

        public override string WarningItemName
        {
            get
            {
                return (SoilItemParent != null) ? SoilItemParent.Name + " " + Depth : "?";
            }
        }
    }
}
///</Behnam>
