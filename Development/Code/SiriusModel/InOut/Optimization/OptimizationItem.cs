using SiriusModel.InOut.Base;
using SiriusModel.InOut.OutputWriter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Xml.Serialization;

namespace SiriusModel.InOut
{
    ///<summary>
    ///Optimization Item
    ///</summary>
    [Serializable]
    public class OptimizationItem : ProjectDataFileItemNoChild
    {

        #region Common attributes

        private bool optiOrMaxi; // 1 for Optimization Mode, 0 for Maximisation
        public bool OptiOrMaxi
        {
            get { return optiOrMaxi; }
            set { this.SetStruct(ref optiOrMaxi, ref value, "OptiOrMaxi"); }
        }

        private bool singleOrBatch; // 1 for single Run, 0 for Batch Run
        public bool SingleOrBatch
        {
            get { return singleOrBatch; }
            set { this.SetStruct(ref singleOrBatch, ref value, "SingleOrBatch"); }
        }

        private bool isIterative;
        public bool IsIterative
        {
            get { return isIterative; }
            set { this.SetStruct(ref isIterative, ref value, "IsIterative"); }
        }

        private Book resultBook;
        [XmlIgnore]
        public Book ResultBook
        {
            get { return resultBook; }
            set { this.SetObject(ref resultBook, ref value, "ResultTable"); }
        }

        #region Algorithm selection

        private static List<string> AlgorithmList = new List<string>()
        {
            "CMA-ES",
            "NelderMeadSimplex"
        };

        private int selectedAlgo;
        [XmlIgnore]
        public int SelectedAlglo
        {
            get { return selectedAlgo; }
            set
            {
                if (this.SetStruct(ref selectedAlgo, ref value, "SelectedAlglo"))
                    NotifyPropertyChanged("ChoosedAlgo");
            }
        }

        public string ChoosedAlgo
        {
            get { return AlgorithmList[selectedAlgo]; }
            set { SelectedAlglo = AlgorithmList.IndexOf(value);}
        }

        #endregion

        #region parameters selection

        // List of parameter we want to optimize
        private List<OptiParameter> optimizedParameter;
        public List<OptiParameter> OptimizedParameter
        {
            get { return optimizedParameter; }
            set { this.SetObject(ref optimizedParameter, ref value, "OptimizedParameter"); }
        }

        #endregion

        #region Observation and objective function

        private List<OptiObservation> observations;
        public List<OptiObservation> Observations
        {
            get { return observations; }
            set { this.SetObject(ref observations, ref value, "Observations"); }
        }

        private static List<string> ObjectiveFctList = new List<string>()
        {
            "Weighted RMSE",
            "Weighted MRE",
            "Weighted RMSRE"
        };

        private int selectedObjFct;
        [XmlIgnore]
        public int SelectedObjFct
        {
            get { return selectedObjFct; }
            set
            {
                if (this.SetStruct(ref selectedObjFct, ref value, "SelectedObjFct"))
                    NotifyPropertyChanged("ChoosedObjFct");
            }
        }

        public string ChoosedObjFct
        {
            get { return ObjectiveFctList[selectedObjFct]; }
            set { SelectedObjFct = ObjectiveFctList.IndexOf(value); }
        }

        #endregion

        #endregion

        #region CMAES attributes

        private int _CMAESNbOfRound;
        public int CMAESNbOfRound
        {
            get { return _CMAESNbOfRound; }
            set { this.SetStruct(ref _CMAESNbOfRound, ref value, "CMAESNbOfRound"); }
        }

        private int _CMAESNbOfGeneration;
        public int CMAESNbOfGeneration
        {
            get { return _CMAESNbOfGeneration; }
            set { this.SetStruct(ref _CMAESNbOfGeneration, ref value, "CMAESNbOfGeneration"); }
        }

        private int _CMAES_u;
        public int CMAES_u
        {
            get { return _CMAES_u; }
            set { this.SetStruct(ref _CMAES_u, ref value, "CMAESmu"); }
        }

        private double _CMAESStopFitness;
        public double CMAESStopFitness
        {
            get { return _CMAESStopFitness; }
            set { this.SetStruct(ref _CMAESStopFitness, ref value, "CMAESStopFitness"); }
        }

        #endregion

        #region Simplex attributes

        private int _SimplexNbOfRound;
        public int SimplexNbOfRound
        {
            get { return _SimplexNbOfRound; }
            set { this.SetStruct(ref _SimplexNbOfRound, ref value, "SimplexNbOfRound"); }
        }

        private int _SimplexNbOfGeneration;
        public int SimplexNbOfGeneration
        {
            get { return _SimplexNbOfGeneration; }
            set { this.SetStruct(ref _SimplexNbOfGeneration, ref value, "SimplexNbOfGeneration"); }
        }

        private double _SimplexStopFitness;
        public double SimplexStopFitness
        {
            get { return _SimplexStopFitness; }
            set { this.SetStruct(ref _SimplexStopFitness, ref value, "SimplexStopFitness"); }
        }

        #endregion

        private RunItem runItem;
        public RunItem RunItem
        {
            get { return runItem; }
            set { this.SetObject(ref runItem, ref value, "RunItem"); }
        }

        private ObservationItem observationItem;
        public ObservationItem ObservationItem
        {
            get { return observationItem; }
            set { this.SetObject(ref observationItem, ref value, "ObservationItem"); }
        }

        #region Constructor and interface methods

        public OptimizationItem(string name)
            : base(name)
        {
            // Default parameters value
            // Common attributes
            OptiOrMaxi = true;
            SingleOrBatch = true;
            IsIterative = false;
            ChoosedAlgo = "CMA-ES";
            ChoosedObjFct = "Weighted MRE";
            ResultBook = new Book();

            Observations = new List<OptiObservation>();
            OptimizedParameter = new List<OptiParameter>();

            SimplexNbOfRound = 3;
            SimplexNbOfGeneration = 20;
            SimplexStopFitness = 0.9;

            // CMA-ES algorithm
            CMAESNbOfRound = 3;
            CMAESNbOfGeneration = 20;
            CMAESStopFitness = 0.9;
            CMAES_u = 4;
        }

        public OptimizationItem()
            : this("")
        {

        }

        public OptimizationItem(OptimizationItem optimizationItem)
            : base(optimizationItem.Name)
        {
            // Default parameters value
            // Common attributes
            OptiOrMaxi = optimizationItem.OptiOrMaxi;
            SingleOrBatch = optimizationItem.SingleOrBatch;
            IsIterative = optimizationItem.IsIterative;
            ChoosedAlgo = optimizationItem.ChoosedAlgo;
            ChoosedObjFct = optimizationItem.ChoosedObjFct;
            ResultBook = optimizationItem.ResultBook;
            Observations = optimizationItem.Observations;
            OptimizedParameter = optimizationItem.OptimizedParameter;
            RunItem = optimizationItem.RunItem;


            SimplexNbOfRound = optimizationItem.SimplexNbOfRound;
            SimplexNbOfGeneration = optimizationItem.SimplexNbOfGeneration;
            SimplexStopFitness = optimizationItem.SimplexStopFitness;

            // CMA-ES algorithm
            CMAESNbOfRound = optimizationItem.CMAESNbOfRound;
            CMAESNbOfGeneration =optimizationItem.CMAESNbOfGeneration;
            CMAESStopFitness = optimizationItem.CMAESStopFitness;
            CMAES_u = optimizationItem.CMAES_u;
        }

        public override void UpdatePath(string oldAbsolute, string newAbsolute)
        {

        }

        public override void CheckWarnings()
        {

        }

        #endregion
    }

    ///<summary>
    ///Used to record distribution parameters of a selected Sirius parameter
    ///</summary>
    [XmlRoot("Parameter")]
    public class OptiParameter : IEquatable<OptiParameter>, IComparable<OptiParameter>
    {
        public string Name { get; set; } // Name of the parameter

        public double Min { get; set; }
        public double Max { get; set; }
        
        public OptiParameter(string name)
        {
            Name = name;
            Min = 0;
            Max = 10;
        }

        public OptiParameter()
            :this(" ")
        {
        }

        public bool Equals(OptiParameter other)
        {
            return (other.Name == this.Name);
        }

        public int CompareTo(OptiParameter other)
        {
            return this.Name.CompareTo(other.Name);
        }
    }

    [XmlRoot("Observation")]
    public class OptiObservation : IEquatable<OptiObservation>, IComparable<OptiObservation>
    {
        public string Name { get; set; }
        public double Weight { get; set; }

        public OptiObservation(string name)
        { 
            Name = name;
            Weight = 1;
        }

        public OptiObservation()
            : this(" ")
        {
        }

        public int CompareTo(OptiObservation other)
        {
            return this.Name.CompareTo(other.Name);
        }

        public bool Equals(OptiObservation other)
        {
            return (this.Name == other.Name);
        }

        public bool Equals(string name)
        {
            return (this.Name == name);
        }
    }
}
