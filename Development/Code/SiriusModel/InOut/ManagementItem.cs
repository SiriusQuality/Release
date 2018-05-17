using System;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;
using SiriusModel.InOut.Base;
using SiriusModel.Model.Phenology;
using SiriusModel.Model;
using SiriusModel.Model.CropModel;
using SiriusQualityPhenology;

namespace SiriusModel.InOut
{
    ///<summary>
    ///Management application of water and nitrogen.
    ///</summary>
    [Serializable]
    public abstract class Application<TKeyApp> : Child<TKeyApp>
        where TKeyApp : IComparable
    {
        private double nitrogen;
        private double water;

        public double Nitrogen 
        {
            get { return nitrogen; }
            set 
            { 
                this.SetStruct(ref nitrogen, ref value, "Nitrogen");
                this.Assert(nitrogen, d => d >= 0, "Ni fertilisation", ">=0", null);
            }
        }

        [XmlIgnore]
        public double Water 
        {
            get { return water; }
            set 
            {
                if (this.SetStruct(ref water, ref value, "Water"))
                {
                    NotifyPropertyChanged("WaterMM");
                    this.Assert(water, d => d >= 0, "Irrigation", ">=0", null);
                }
            }
        }

        public double WaterMM
        {
            get { return Water / Run.MMwaterToGwater; }
            set { Water = value * Run.MMwaterToGwater; }
        }

        protected Application()
        {
            Nitrogen = 0;
            Water = 0;
        }

        protected Application(double water, double nitrogen)
        {
            Nitrogen = nitrogen;
            Water = water;
        }

        public ManagementItem ManagementItemParent
        {
            get { return Parent as ManagementItem; }
        }

        public override void CheckWarnings()
        {
            Nitrogen = Nitrogen;
            Water = Water;
        }

        public override string WarningFileID
        {
            get
            {
                return (ManagementItemParent != null && ManagementItemParent.ProjectDataFileParent != null) ? ManagementItemParent.ProjectDataFileParent.ID : "?";
            }
        }

        public override bool NotifyPropertyChanged(string propertyName)
        {
            if (propertyName != "IsModified" && propertyName != "IsModifiedStr" && ManagementItemParent != null && ManagementItemParent.ProjectDataFileParent != null)
            {
                ManagementItemParent.ProjectDataFileParent.IsModified = true;
            }
            return base.NotifyPropertyChanged(propertyName);
        }
    }

    [Serializable]
    public class DateApplication : Application<DateTime>
    {
        private DateTime date;

        public DateTime Date 
        {
            get { return date; }
            set 
            {
                this.SetStruct(ref date, ref value, "Date");
                this.Assert(date, delegate(DateTime d)
                {
                    if (ManagementItemParent != null)
                    {
                        return ManagementItemParent.SowingDate.CompareTo(d) <= 0;
                    }
                    return true;
                }, "Date", ">= Sowing date", null);
            }
        }

        public DateApplication()
        {
            Date = DateTime.Today;
        }
        public DateApplication(DateTime date, double water, double nitrogen)
            : base(water, nitrogen)
        {
            Date = date;
        }

        public override void CheckWarnings()
        {
            base.CheckWarnings();
            Date = date;
        }

        public override string WarningItemName
        {
            get
            {
                return (ManagementItemParent != null) ? ManagementItemParent.Name + " " + date.ToString("u").Split()[0] : date.ToString("u").Split()[0];
            }
        }
    }

    [Serializable]
    public class DateApplicationGenerator : ChildKeyGeneratorSorted<DateApplication, DateTime>
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
            get { return "Date"; }
        }

        public override Func<DateApplication, DateTime> KeySelector
        {
            get { return dateApp => dateApp.Date; }
        }

        public override Func<DateApplication, DateTime, DateTime> KeySetter
        {
            get { return delegate(DateApplication dateApp, DateTime date) { dateApp.Date = date; return date; }; }
        }

        public override void CreateNullSelectable(BaseBindingList<DateApplication> selectable)
        {
            throw new NotImplementedException();
        }
    }

    [Serializable]
    public class GrowthStageApplication : Application<GrowthStage>
    {
        private GrowthStage growthStage;

        public GrowthStage GrowthStage 
        {
            get { return growthStage; }
            set { this.SetStruct(ref growthStage, ref value, "GrowthStage"); }
        }

        public GrowthStageApplication()
        {
            GrowthStage = GrowthStage.ZC_00_Sowing;
        }
        public GrowthStageApplication(GrowthStage growthStage, double water, double nitrogen)
            : base(water, nitrogen)
        {
            GrowthStage = growthStage;
        }

        public override string WarningItemName
        {
            get
            {
                return (ManagementItemParent != null) ? ManagementItemParent.Name + " " + Phase.growthStageAsString(growthStage) : Phase.growthStageAsString(growthStage);
            }
        }
    }

    [Serializable]
    public class GrowthStageApplicationGenerator : ChildKeyGeneratorSorted<GrowthStageApplication, GrowthStage>
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
            get { return "GrowthStage"; }
        }

        public override Func<GrowthStageApplication, GrowthStage> KeySelector
        {
            get { return growthStageApp => growthStageApp.GrowthStage; }
        }

        public override Func<GrowthStageApplication, GrowthStage, GrowthStage> KeySetter
        {
            get { return delegate(GrowthStageApplication growthStageApp, GrowthStage growthStage) { growthStageApp.GrowthStage = growthStage; return growthStage; }; }
        }

        public override void CreateNullSelectable(BaseBindingList<GrowthStageApplication> selectable)
        {
            throw new NotImplementedException();
        }
    }

    ///<summary>
    ///Management values.
    ///</summary>
    [Serializable, XmlInclude(typeof(GrowthStage)), 
    XmlInclude(typeof(DateApplication)), XmlInclude(typeof(GrowthStageApplication))]
    public class ManagementItem : ProjectDataFileItem2Child<
        DateApplication, DateTime, DateApplicationGenerator, 
        GrowthStageApplication, GrowthStage, GrowthStageApplicationGenerator>
    {
        private string experimentName;
        private string species;
        private bool isSowDateEstimate;
        private bool doRelax;
        private DateTime sowingDate;
        private DateTime finalSowingDate;
        private double skipDays;
        private double checkDaysTemp;
        private double checkDaysPcp;
        private double checkDepth;
        private double cumPcpThr;
        private double soilMoistThr;
        private double tAveThr;
        private double tMinThr;
        private double soilWorkabThr;
        private double sowingDensity;
        private bool isTotalNitrogen;
        private double totalNApplication;
        private double co2;
        private bool isWDinPerc;
        private double soilWaterDeficit;
        private double totalNi;
        private double topNi;
        private double midNi;
        private double observedGrainNumber;
        private double targetFertileShootNumber;


        ///<Behnam>
        ///<Comment>Adding additional required properties related to estimating sowing date or 
        ///using a fixed sowing date
        ///</Comment>

        ///<summary>
        ///Name of the species
        ///</summary>
        public string Species
        {
            get { return species; }
            set { this.SetObject(ref species, ref value, "Species"); }
        }

        ///<summary>
        ///Name of the experiment
        ///</summary>
        public string ExperimentName
        {
            get { return experimentName; }
            set { this.SetObject(ref experimentName, ref value, "ExperimentName"); }
        }

        ///<summary>
        ///Get or set the sowing date estimation flag.
        ///</summary>
        public bool IsSowDateEstimate
        {
            get { return isSowDateEstimate; }
            set { this.SetStruct(ref isSowDateEstimate, ref value, "IsSowDateEstimate"); }
        }

        ///<summary>
        ///Get or set the sowing date criteria are relaxed over the sowing window.
        ///</summary>
        public bool DoRelax
        {
            get { return doRelax; }
            set { this.SetStruct(ref doRelax, ref value, "DoRelax"); }
        }

        ///<summary>
        ///Get or set the sowing date.
        ///</summary>
        public DateTime SowingDate 
        {
            get { return sowingDate; }
            set { this.SetStruct(ref sowingDate, ref value, "SowingDate"); CheckWarnings(); }
        }

        ///<summary>

        ///<summary>
        ///Get or set the finally used (estimated or fixed) sowing date.
        ///</summary>
        [XmlIgnore]
        public DateTime FinalSowingDate
        {
            get { return finalSowingDate; }
            set { this.SetStruct(ref finalSowingDate, ref value, "FinalSowingDate"); CheckWarnings(); }
        }
        
        ///<summary>
        ///Get or set the number of days that should be skipped before sowing occures.
        ///</summary>
        public double SkipDays
        {
            get { return Math.Round(skipDays, 0); }
            set
            {
                this.SetStruct(ref skipDays, ref value, "SkipDays");
                this.Assert(skipDays, d => d >= 1, "SkipDays", ">=1", null);
            }
        }

        ///<summary>
        ///Get or set the number of days that should be checked for temperature criteria.
        ///</summary>
        public double CheckDaysTemp
        {
            get { return Math.Round(checkDaysTemp, 0); }
            set
            {
                this.SetStruct(ref checkDaysTemp, ref value, "CheckDaysTemp");
                this.Assert(checkDaysTemp, d => d >= 0, "CheckDaysTemp", ">=0", null);
            }
        }

        ///<summary>
        ///Get or set the number of days that should be checked for precipitation criteria.
        ///</summary>
        public double CheckDaysPcp
        {
            get { return Math.Round(checkDaysPcp, 0); }
            set
            {
                this.SetStruct(ref checkDaysPcp, ref value, "CheckDaysPcp");
                this.Assert(checkDaysPcp, d => d >= 0, "CheckDaysPcp", ">=0", null);
            }
        }

        ///<summary>
        ///Get or set the depth of soil to be checked in cm.
        ///</summary>
        public double CheckDepth
        {
            get { return checkDepth; }
            set
            {
                this.SetStruct(ref checkDepth, ref value, "CheckDepth");
                this.Assert(checkDepth, d => d >= 0, "CheckDepth", ">=0", null);
            }
        }

        ///<summary>
        ///Get or set the soil moisture threshold for germination.
        ///</summary>
        public double CumPcpThr
        {
            get { return cumPcpThr; }
            set
            {
                this.SetStruct(ref cumPcpThr, ref value, "CumPcpThr");
                this.Assert(cumPcpThr, d => d >= 0, "Cumulative precipitation threshold", ">=0", null);
            }
        }

        ///<summary>
        ///Get or set the soil moisture threshold for germination.
        ///</summary>
        public double SoilMoistThr
        {
            get { return soilMoistThr; }
            set
            {
                this.SetStruct(ref soilMoistThr, ref value, "SoilMoistThr");
                this.Assert(soilMoistThr, d => d >= 0 && d <= 1, "Soil moisture threshold for germination", ">=0 and >=1", null);
            }
        }

        ///<summary>
        ///Get or set the average daily air temperature threshold for frost.
        ///</summary>
        public double TAveThr
        {
            get { return tAveThr; }
            set
            {
                this.SetStruct(ref tAveThr, ref value, "TAveThr");
                this.Assert(tAveThr, d => d >= -10, "Average daily air temperature threshold for frost", ">=-10", null);
            }
        }

        ///<summary>
        ///Get or set the minimum daily air temperature threshold for frost.
        ///</summary>
        public double TMinThr
        {
            get { return tMinThr; }
            set
            {
                this.SetStruct(ref tMinThr, ref value, "TMinThr");
                this.Assert(tMinThr, d => d >= -10, "Minimum daily air temperature threshold for frost", ">=-10", null);
            }
        }

        ///<summary>
        ///Get or set the soil temperature threshold for workability.
        ///</summary>
        public double SoilWorkabThr
        {
            get { return soilWorkabThr; }
            set
            {
                this.SetStruct(ref soilWorkabThr, ref value, "SoilWorkabThr");
                this.Assert(soilWorkabThr, d => d >= 0, "Soil moisture threshold for workability", ">=0", null);
            }
        }
        ///</Behnam>


        ///<summary>
        ///Get or set the stem density.
        ///</summary>
        public double SowingDensity
        {
            get { return sowingDensity; }
            set 
            {
                this.SetStruct(ref sowingDensity, ref value, "SowingDensity");
                this.Assert(targetFertileShootNumber, d => d > 0 && d >= SowingDensity, "Target fertile shoot number", ">0 and >=Sowing density", null);
                this.Assert(sowingDensity, d => d > 0 && d <= TargetFertileShootNumber, "Sowing density", ">0 and <=Target fertile shoot number", null);
            }
        }

        ///<Behnam>
        ///<Comment>Adding additional required properties related to N application in percentage (i.e. shares)</Comment>

        ///<summary>
        ///Get or set the total nitrogen application flag.
        ///</summary>
        public bool IsTotalNitrogen
        {
            get { return isTotalNitrogen; }
            set { this.SetStruct(ref isTotalNitrogen, ref value, "IsTotalNitrogen"); }
        }

        ///<summary>
        ///Get or set the total N ferlisation.
        ///</summary>
        public double TotalNApplication
        {
            get { return totalNApplication; }
            set
            {
                this.SetStruct(ref totalNApplication, ref value, "TotalNApplication");
                this.Assert(totalNApplication, d => d >= 0, "TotalNApplication", ">=0", null);
            }
        }

        ///<summary>
        ///Get or set whether an annual trend is applied on N ferlisation data.
        ///</summary>
        private bool isNTrendApplied;
        public bool IsNTrendApplied
        {
            get { return isNTrendApplied; }
            set { this.SetStruct(ref isNTrendApplied, ref value, "IsNTrendApplied"); }
        }

        ///<summary>
        ///Get or set the base year of N ferlisation trend.
        ///</summary>
        private int nTrendBaseYear;
        public int NTrendBaseYear
        {
            get { return nTrendBaseYear; }
            set
            {
                this.SetStruct(ref nTrendBaseYear, ref value, "NTrendBaseYear");
                this.Assert(nTrendBaseYear, d => d >= 1900, "NTrendBaseYear", ">=0", null);
            }
        }

        ///<summary>
        ///Get or set the slope of N ferlisation trend.
        ///</summary>
        private int nTrendSlope;
        public int NTrendSlope
        {
            get { return nTrendSlope; }
            set { this.SetStruct(ref nTrendSlope, ref value, "NTrendSlope"); }
        }

        ///<summary>
        ///Get or set whether an annual trend is applied on N ferlisation data.
        ///</summary>
        private bool isCO2TrendApplied;
        public bool IsCO2TrendApplied
        {
            get { return isCO2TrendApplied; }
            set { this.SetStruct(ref isCO2TrendApplied, ref value, "IsCO2TrendApplied"); }
        }

        ///<summary>
        ///Get or set the base year of N ferlisation trend.
        ///</summary>
        private int cO2TrendBaseYear;
        public int CO2TrendBaseYear
        {
            get { return cO2TrendBaseYear; }
            set
            {
                this.SetStruct(ref cO2TrendBaseYear, ref value, "CO2TrendBaseYear");
                this.Assert(cO2TrendBaseYear, d => d >= 1900, "CO2TrendBaseYear", ">=0", null);
            }
        }

        ///<summary>
        ///Get or set the slope of N ferlisation trend.
        ///</summary>
        private double cO2TrendSlope;
        public double CO2TrendSlope
        {
            get { return cO2TrendSlope; }
            set { this.SetStruct(ref cO2TrendSlope, ref value, "CO2TrendSlope"); }
        }

        ///<summary>
        ///Get or set whether NNI is used for N fertilisation.
        ///</summary>
        private bool isNNIUsed;
        public bool IsNNIUsed
        {
            get { return isNNIUsed; }
            set { this.SetStruct(ref isNNIUsed, ref value, "IsNNIUsed"); }
        }

        ///<summary>
        ///Get or set the NNI threshold to trigger N fertilisation.
        ///</summary>
        private double nNIThreshold;
        public double NNIThreshold
        {
            get { return nNIThreshold; }
            set { this.SetStruct(ref nNIThreshold, ref value, "NNIThreshold"); }
        }

        ///<summary>
        ///Get or set the mutiplier of the amount of N fertilisation estimated based on NNI.
        ///</summary>
        private double nNIMultiplier;
        public double NNIMultiplier
        {
            get { return nNIMultiplier; }
            set { this.SetStruct(ref nNIMultiplier, ref value, "NNIMultiplier"); }
        }

        ///<summary>
        ///Get or set whether precipitation is checked for N application at growth stages.
        ///</summary>
        private bool isCheckPcpN;
        public bool IsCheckPcpN
        {
            get { return isCheckPcpN; }
            set { this.SetStruct(ref isCheckPcpN, ref value, "IsCheckPcpN"); }
        }

        ///<summary>
        ///Get or set the number of checked days for precipitation for N application at growth stages.
        ///</summary>
        private int checkDaysPcpN;
        public int CheckDaysPcpN
        {
            get { return checkDaysPcpN; }
            set
            {
                this.SetStruct(ref checkDaysPcpN, ref value, "CheckDaysPcpN");
                this.Assert(checkDaysPcpN, d => d > 0, "CheckDaysPcpN", ">0", null);
            }
        }

        ///<summary>
        ///Get or set the maximum number of days for the application event can be postponed.
        ///</summary>
        private int maxPostponeN;
        public int MaxPostponeN
        {
            get { return maxPostponeN; }
            set
            {
                this.SetStruct(ref maxPostponeN, ref value, "MaxPostponeN");
                this.Assert(maxPostponeN, d => d > 0, "MaxPostponeN", ">0", null);
            }
        }

        ///<summary>
        ///Get or set the minimum cumulative precipitation to apply N at or after growth stages.
        ///</summary>
        private double cumPcpThrN;
        public double CumPcpThrN
        {
            get { return cumPcpThrN; }
            set
            {
                this.SetStruct(ref cumPcpThrN, ref value, "CumPcpThrN");
                this.Assert(cumPcpThrN, d => d >= 0, "CumPcpThrN", ">=0", null);
            }
        }

        ///</Behnam>

        ///<summary>
        ///Get or set the CO2 concentration.
        ///</summary>
        public double CO2
        {
            get { return co2; }
            set 
            {
                this.SetStruct(ref co2, ref value, "CO2");
                this.Assert(co2, d => d > 0, "CO2", ">0", null);
            }
        }

        ///<summary>
        ///Get or set the initial water deficit type (is in percentage?).
        ///</summary>
        public bool IsWDinPerc
        {
            get { return isWDinPerc; }
            set { this.SetStruct(ref isWDinPerc, ref value, "IsWDinPerc"); }
        }

        ///<summary>
        ///Get or set the initial water deficit.
        ///</summary>
        public double SoilWaterDeficit
        {
            get { return soilWaterDeficit; }
            set 
            {
                this.SetStruct(ref soilWaterDeficit, ref value, "SoilWaterDeficit");
                if (IsWDinPerc)
                {
                    this.Assert(soilWaterDeficit, d => d >= 0 && d <= 100, "Soil water deficit percentage at sowing", ">=0 and <=100", null);
                }
                else
                {
                    this.Assert(soilWaterDeficit, d => d >= 0, "Soil water deficit at sowing", ">=0", null);
                }
            }
        }

        ///<summary>
        ///Get or set the total inorganic Ni at sowing.
        ///</summary>
        public double TotalNi
        {
            get { return totalNi; }
            set 
            {
                this.SetStruct(ref totalNi, ref value, "TotalNi");
                this.Assert(totalNi, d => d >= 0, "Total inorganic N", ">=0", null);
            }
        }

        ///<summary>
        ///Get or set top (0, 1/3) soilDef Layer concentration in inorganic Ni.
        ///</summary>
        public double TopNi
        {
            get { return topNi; }
            set 
            {
                this.SetStruct(ref topNi, ref value, "TopNi");
                this.Assert(topNi, d => d >= 0 && d <= 100, "% inorganic in top 33%", ">=0 and <=100", null);
                AssertTopMidNi();
            }
        }

        private void AssertTopMidNi()
        {
            this.Assert(topNi + midNi, d => d >= 0 && d <= 100, "% inorganic in top and middle 33%", ">=0 and <=100", null);
        }

        ///<summary>
        ///Get or set middle (1/3, 2/3) soilDef Layer concentration in inorganic Ni.
        ///</summary>
        public double MidNi
        {
            get { return midNi; }
            set 
            {
                this.SetStruct(ref midNi, ref value, "MidNi");
                this.Assert(midNi, d => d >= 0 && d <= 100, "% inorganic in middle 33%", ">=0 and <=100", null);
                AssertTopMidNi();
            }
        }

        ///<summary>
        ///Get or set grain number value, used if CalculateGrainNumber is false.
        ///</summary>
        public double ObservedGrainNumber
        {
            get { return observedGrainNumber; }
            set 
            {
                this.SetStruct(ref observedGrainNumber, ref value, "ObservedGrainNumber");
                this.Assert(observedGrainNumber, d => d == -1 || (d > 0 && d == (((int)d))), "Observed grain number", "=-1 or (>0 and integer)", null); 
            }
        }

        public double TargetFertileShootNumber
        {
            get { return targetFertileShootNumber; }
            set 
            {
                this.SetStruct(ref targetFertileShootNumber, ref value, "TargetFertileShootNumber");
                this.Assert(sowingDensity, d => d > 0 && d <= TargetFertileShootNumber, "Sowing density", ">0 and <=Target fertile shoot number", null);
                this.Assert(targetFertileShootNumber, d => d > 0 && d >= SowingDensity, "Target fertile shoot number", ">0 and >=Sowing density", null);
            }
        }

        public double TotalNfertilisation
        {
            get
            {
                ///<Behnam (2015.10.23)>
                ///<Comment>To make sure the sum of N application shares is less than or equal to 100%, if applicable</Comment>
                var totalfert = DateApplications.Sum(dateApp => dateApp.Nitrogen)
                        + GrowthStageApplications.Sum(growthStageApp => growthStageApp.Nitrogen);
                if (IsTotalNitrogen) 
                    this.Assert(totalfert, d => d >= 0 && d <= 100, "Sum of N application shares", ">=0 and <=100", null);
                return totalfert;
                ///</Behnam>
            }
        }

        public double TotalIrrigation
        {
            get
            {
                return
                    DateApplications.Sum(dateApp => dateApp.WaterMM)
                    + GrowthStageApplications.Sum(growthStageApp => growthStageApp.WaterMM);
            }
        }

        ///<summary>
        ///Management applications by date.
        ///</summary>
        public BindingList<DateApplication> DateApplications { get { return BindingItems1; } }

        ///<summary>
        ///Management applications by crop growth stage.
        ///</summary>
        public BindingList<GrowthStageApplication> GrowthStageApplications { get { return BindingItems2; } }

        ///<summary>
        ///Create a new Management.
        ///</summary>
        public ManagementItem(string name, string experimentName = "no experiment")
            : base(name)
        {
            ExperimentName = experimentName;
            Species = "";
            IsSowDateEstimate = false;
            DoRelax = true;
            SowingDate = new DateTime(2005, 12, 10);
            FinalSowingDate = SowingDate;
            SkipDays = 21;
            CheckDaysTemp = 10;
            CheckDaysPcp = 3;
            CheckDepth = 30;
            CumPcpThr = 10;
            SoilMoistThr = 0.75;
            TAveThr = 10;
            TMinThr = -4;
            SoilWorkabThr = 1.2;
            TargetFertileShootNumber = 250;
            SowingDensity = 200;
            IsTotalNitrogen = false;
            TotalNApplication = 0;
            IsNTrendApplied = false;
            NTrendBaseYear = 2000;
            NTrendSlope = 0;
            IsCO2TrendApplied = false;
            CO2TrendBaseYear = 2000;
            CO2TrendSlope = 0;
            IsNNIUsed = false;
            NNIThreshold = 0.8;
            NNIMultiplier = 1;
            IsCheckPcpN = false;
            CumPcpThrN = 7;
            CheckDaysPcpN = 3;
            MaxPostponeN = 10;
            CO2 = 350;
            IsWDinPerc = false;
            SoilWaterDeficit = 0;
            TotalNi = 2.0;
            TopNi = 50.0;
            MidNi = 40.0;
            ObservedGrainNumber = -1;
        }

        public ManagementItem()
            : this("")
        {
            BindingItems1.ListChanged += BindingItemsListChanged;
            BindingItems2.ListChanged += BindingItemsListChanged;
        }

        void BindingItemsListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemChanged && e.PropertyDescriptor != null)
            {
                if (e.PropertyDescriptor.Name == "Nitrogen")
                {
                    NotifyPropertyChanged("TotalNfertilisation");
                }
                else if (e.PropertyDescriptor.Name == "Water")
                {
                    NotifyPropertyChanged("TotalIrrigation");
                }
            }
            else if (e.ListChangedType == ListChangedType.ItemDeleted)
            {
                NotifyPropertyChanged("TotalNfertilisation");
                NotifyPropertyChanged("TotalIrrigation");
            }
        }

        public override void UpdatePath(string oldAbsolute, string newAbsolute)
        {
        }

        public override void CheckWarnings()
        {
            base.CheckWarnings();
            SowingDensity = SowingDensity;
        }
    }
}
