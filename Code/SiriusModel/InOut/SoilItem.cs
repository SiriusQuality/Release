///<Behnam>
using System;
using System.ComponentModel;
using System.Xml.Serialization;
using SiriusModel.InOut.Base;
using System.Linq.Expressions;
using System.Windows.Forms.VisualStyles;
using SiriusModel.Model.Base;
using SiriusModel.Model.SoilModel.EnergyBalance;
using SiriusModel.Structure;

namespace SiriusModel.InOut
{
    ///<summary>
    ///Soil values. 
    ///</summary>
    [Serializable, XmlInclude(typeof(SoilLayer))]
    public class SoilItem : ProjectDataFileItem1Child<SoilLayer, double, SoilLayerGenerator>, IProjectItem
    {
        private bool isKqlUsed;
        private bool isKqCalc;
        private bool isOrgNCalc;
        private double cton;
        private double carbon;
        private double bd;
        private double gravel;
        private double clay;
        private double kq;
        private double ko;
        private double no;
        private double minNir;
        private double ndp;

        ///<Behnam>
        public static bool IsKqCalcStatic;

        ///<summary>
        ///Get or set Soil isOrgNCalc.
        ///</summary>
        public bool IsKqCalc
        {
            get {
                IsKqCalcStatic = isKqCalc;
                return isKqCalc; }
            set { this.SetStruct(ref isKqCalc, ref value, "IsKqCalc"); }
        }

        ///<summary>
        ///Get or set Soil IsKqlUsed.
        ///</summary>
        public bool IsKqlUsed
        {
            get { return isKqlUsed; }
            set { this.SetStruct(ref isKqlUsed, ref value, "IsKqlUsed"); }
        }

        ///<summary>
        ///Get or set Soil isOrgNCalc.
        ///</summary>
        public bool IsOrgNCalc
        {
            get { return isOrgNCalc; }
            set { this.SetStruct(ref isOrgNCalc, ref value, "IsOrgNCalc"); }
        }

        ///<summary>
        ///Get or set Soil C:N.
        ///</summary>
        public double CtoN
        {
            get { return cton; }
            set
            {
                this.SetStruct(ref cton, ref value, "CtoN");
                this.Assert(cton, d => d > 0 && d <= 40, "C:N ratio", ">0 and <=40", null);
            }
        }

        ///<summary>
        ///Get or set Soil total organic matter.
        ///</summary>
        public double Carbon
        {
            get { return carbon; }
            set
            {
                this.SetStruct(ref carbon, ref value, "Carbon");
                this.Assert(carbon, d => d > 0 && d <= 100, "Organic carbon", ">0 and <=100", null);
            }
        }

        ///<summary>
        ///Get or set Soil bulk density.
        ///</summary>
        public double Bd
        {
            get { return bd; }
            set
            {
                this.SetStruct(ref bd, ref value, "Bd");
                this.Assert(bd, d => d > 0 && d <= 2.65, "Bulk density", ">0 and <=2.65", null);
            }
        }

        ///<summary>
        ///Get or set Soil gravel content.
        ///</summary>
        public double Gravel
        {
            get { return gravel; }
            set
            {
                this.SetStruct(ref gravel, ref value, "Gravel");
                this.Assert(gravel, d => d >= 0 && d <= 100, "Gravel content", "=>0 and <=100", null);
            }
        }

        ///<Behnam>

        ///<summary>
        ///Get or set Soil Clay.
        ///</summary>
        public double Clay
        {
            get { return clay; }
            set
            {
                this.SetStruct(ref clay, ref value, "Clay");
                this.Assert(clay, d => d > 0 && d <= 100, "Clay coefficient", ">0 and <=100", null);
            }
        }

        ///<summary>
        ///Get or set Soil Kq.
        ///</summary>
        public double Kq 
        {
            get
            {
                if (isKqCalc)
                {
                    if (clay <= 9.5) kq = 1;
                    if (clay <= 58.3 & clay > 9.5) kq = Math.Round(1.0271 - 0.000302 * (clay * clay), 2);
                    if (clay > 58.3) kq = 0;
                }
                else kq = Math.Round(kq, 2);
                return kq;
            }
            set 
            {
                this.SetStruct(ref kq, ref value, "Kq");
                this.Assert(kq, d => d >= 0 && d <= 1, "Percolation coefficient", "=>0 and <=1", null);
            }
        }

        ///<summary>
        ///Get or set Soil ko.
        ///</summary>
        public double Ko 
        {
            get { return ko; }
            set 
            {
                this.SetStruct(ref ko, ref value, "Ko");
                this.Assert(ko, d => d > 0, "Mineralisation constant", ">0", null);
            }
        }

        ///<Behnam>
        ///<summary>
        ///Get or set Soil No.
        ///</summary>
        public double No 
        {
            get
            {
                if (isOrgNCalc)
                {
                    no = Math.Round((carbon/100/cton) * (0.4*1e6*bd*(1-(gravel/100))), 1); // gN/m2
                }
                else
                {
                    no = Math.Round(no, 1);
                }
                return no;
            }
            set 
            {
               this.SetStruct(ref no, ref value, "No");
               this.Assert(no, d => d > 0, "Organic N in top 40cm", ">0", null);
            }
        }
        ///</Behnam>

        ///<summary>
        ///Get or Set Soil minNir.
        ///</summary>
        public double MinNir 
        {
            get { return minNir; }
            set 
            {
                this.SetStruct(ref minNir, ref value, "MinNir");
                this.Assert(minNir, d => d > 0, "Minimum inorganic N", ">0", null);
            }
        }

        ///<summary>
        ///Get or set SoilAlpha.
        ///</summary>
        public double Ndp 
        {
            get { return ndp; }
            set 
            {
                this.SetStruct(ref ndp, ref value, "Ndp");
                this.Assert(ndp, d => d > 0, "Nitrogen denitrification pulse", ">0", null);
            }
        }

        public double TotalAWC
        {
            get
            {
                var nbLayer = Layers.Count;
                double totalAWC = 0;
                for (var i = 0; i < nbLayer; ++i)
                {
                    totalAWC += Layers[i].AWC;
                }
                return Math.Round(totalAWC,1);
            }
        }

        ///<summary>
        ///Contains all Layer instances.
        ///</summary>
        [XmlIgnore]
        public BaseBindingList<SoilLayer> Layers { get { return BindingItems1; }}

        public SoilLayer[] LayersArray
        {
            get { return BindingItemsArray1; }
            set { BindingItemsArray1 = value; }

        }

        ///<summary>
        ///Create a new Soil.
        ///</summary>
        public SoilItem(string name)
            : base(name)
        {
            IsKqlUsed = false;
            IsKqCalc = false;
            IsOrgNCalc = false;
            CtoN = 10;
            Carbon = 4;
            Bd = 1.65;
            Gravel = 4;
            Clay = 30;
            Kq = Math.Round(1.0271 - 0.000302 * (900), 2);
            Ko = 3.0 / 100000.0;
            No = 800.0;
            MinNir = 0.15;
            Ndp = 0.05;
            BindingItems1.ListChanged += BindingItems1ListChanged;
        }

        void BindingItems1ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded
                || (e.ListChangedType == ListChangedType.ItemChanged && e.PropertyDescriptor != null && e.PropertyDescriptor.Name == "Depth")
                || e.ListChangedType == ListChangedType.Reset)
            {
                NotifyPropertyChanged("TotalAWC");
            }
            if (e.ListChangedType == ListChangedType.ItemDeleted
                || e.ListChangedType == ListChangedType.ItemMoved)
            {
                NotifyPropertyChanged("TotalAWC");
            }
        }

        public SoilItem()
            : this("")
        {
        }

        public override void UpdatePath(string oldAbsolute, string newAbsolute)
        {
        }

        public override void CheckWarnings()
        {
            base.CheckWarnings();
            IsKqlUsed = IsKqlUsed;
            IsKqCalc = IsKqCalc;
            IsOrgNCalc = IsOrgNCalc;
            CtoN = CtoN;
            Carbon = Carbon;
            Bd = Bd;
            Gravel = Gravel;
            Clay = Clay;
            Kq = Kq;
            Ko = Ko;
            No = No;
            MinNir = MinNir;
            Ndp = Ndp;
        }
    }

    [Serializable]
    public class SoilLayerGenerator : ChildKeyGeneratorSorted<SoilLayer, double>
    {
        public override bool Selectable
        {
            get { return false; }
        }
        public override bool Sorted
        {
            get { return true; }
        }

        public override bool NullSelectable
        {
            get { return false; }
        }

        public override string KeyPropertyName
        {
            get { return "Depth"; }
        }

        public override Func<SoilLayer, double> KeySelector
        {
            get { return soilLayer => soilLayer.Depth; }
        }

        public override Func<SoilLayer, double, double> KeySetter
        {
            get { return delegate(SoilLayer soilLayer, double depth) { soilLayer.Depth = depth; return depth; }; }
        }

        public override void CreateNullSelectable(BaseBindingList<SoilLayer> selectable)
        {
            throw new NotImplementedException();
        }
    }
}

///</Behnam>
