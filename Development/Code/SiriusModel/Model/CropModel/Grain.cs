using System;
using SiriusModel.Model.Base;
using SiriusModel.Model.Observation;
using SiriusModel.Structure;

namespace SiriusModel.Model.CropModel
{
    ///The class Grain.
    public class Grain : UniverseLink
    {
        #region Fields

        ///<summary>Ggrain structural proteins (Albumins-globulin + Amphiphilic), mgN/grain</summary>
        public double Nstructure { get;  set; }

        ///<summary>Grain structural dry , mgDM/grain</summary>
        public double Cstructure { get;  set; }

        ///<summary>Grain structural proteins at the end of cell division (Albumins-globulins + Amphiphils), mgN/grain</summary>
        public double NstructureEndCD { get;  set; }

        ///<summary>Grain structural dry matter at the end of cell division, mgDM/grain</summary>
        public double CstructureEndCD { get;  set; }

        ///<summary>Grain storage proteins (gliadin + glutenin), mgN/grain</summary>
        public double Nstorage { get;  set; }

        ///<summary>Grain starch, mg/grain</summary>
        public double Starch { get;  set; }

        ///<summary>Grain albumins-globulins, mgN/grain</summary>
        public double NalbGlo { get;  set; }

        ///<summary>Grain amphiphilic , mgN/grain</summary>
        public double Namp { get;  set; }

        ///<summary>Gliadins, mgN/grain</summary>
        public double Ngli { get;  set; }

        ///<summary>Glutenins, mgN/grain</summary>
        public double Nglu { get;  set; }

        public double CumulTT { get;  set; }

        public double GrainNumber { get { return calculateGrainNumber_.GrainNumber; } }


        private CalculateGrainNumber calculateGrainNumber_;
        #endregion 

        #region Constructors

        ///<summary>Create a new grain compartement</summary>
        ///<param name="universe">Universe of the model.</param>
        public Grain(Universe universe) : base(universe)
        {
            Nstructure = 0;
            Cstructure = 0;
            NstructureEndCD = 0;
            CstructureEndCD = 0;
            Nstorage = 0;
            Starch = 0;
            NalbGlo = 0;
            Namp = 0;
            Ngli = 0;
            Nglu = 0;
            calculateGrainNumber_ = new CalculateGrainNumber(universe);
        }

        ///<summary>Create a new grain compartment by copy</summary>
        ///<param name="universe">Universe of the model</param>
        ///<param name="toCopy">Grain to copy</param>
        public Grain(Universe universe, Grain toCopy) : base(universe)
        {
            Nstructure = toCopy.Nstructure;
            Cstructure = toCopy.Cstructure;
            NstructureEndCD = toCopy.NstructureEndCD;
            CstructureEndCD = toCopy.CstructureEndCD;
            Nstorage = toCopy.Nstorage;
            Starch = toCopy.Starch;
            NalbGlo = toCopy.NalbGlo;
            Namp = toCopy.Namp;
            Ngli = toCopy.Ngli;
            Nglu = toCopy.Nglu;
            CumulTT = toCopy.CumulTT;
            calculateGrainNumber_ = (toCopy.calculateGrainNumber_ != null) ? new CalculateGrainNumber(universe, toCopy.calculateGrainNumber_) : null;   
        }

        #endregion

        #region Init/InitAtAnthesis

        ///<summary>Init the grain at the beginning of the simulation</summary>
        public void Init()
        {
            Cstructure = 0;
            Nstructure = 0;

            CstructureEndCD = 0;
            NstructureEndCD = 0;

            Nstorage = 0;
            Starch = 0;

            NalbGlo = 0;
            Namp = 0;
            Ngli = 0;
            Nglu = 0;

        }

        ///<summary>Init the grains at anthesis</summary>
        public void InitAtAnthesis(double EarDW)
        {
            calculateGrainNumber_.Estimate(EarDW);

            CumulTT = 0;
        }

        #endregion

        #region N

        ///<summary>Get the total N</summary>
        public double GrainTotalN
        {
            get { return GreenN + DeadN; }
        }

        ///<summary>Get the total N per grain</summary>
        public double TotalNperGrain
        {
            get { return (GrainNumber > 0) ? GrainTotalN / GrainNumber * 1000.0 : 0; }
        }

        ///<summary>Get the green N</summary>
        public double GreenN
        {
            get { return StructN + LabileN; }
        }

        ///<summary>Get the struct N</summary>
        public double StructN
        {
            get { return Nstructure * GrainNumber / 1000.0; }
        }

        ///<summary>Get the labile N</summary>
        public double LabileN
        {
            get { return Nstorage * GrainNumber / 1000.0; }
        }

        ///<summary>Get the dead N</summary>
        public double DeadN
        {
            get { return 0; }
        }

        #endregion

        #region DM

        ///<summary>Get the total DM</summary>
        public double TotalDM
        {
            get { return GreenDM + DeadDM; }
        }

        ///<summary>Get the total DM per grain</summary>
        public double TotalDMperGrain
        {
            get { return (GrainNumber > 0) ? TotalDM / GrainNumber * 1000.0 : 0; }
        }

        ///<summary>Get the green DM</summary>
        public double GreenDM
        {
            get { return StructDM + LabileDM; }
        }

        ///<summary>Get the struct DM</summary>
        public double StructDM
        {
            get { return Cstructure * GrainNumber / 1000.0; }
        }

        ///<summary>Get the labile DM</summary>
        public double LabileDM
        {
            get { return Starch * GrainNumber / 1000.0; }
        }

        ///<summary>Get the dead DM</summary>
        public double DeadDM
        {
            get { return 0; }
        }

        #endregion

        #region Ncp

        ///<summary>Get the Ncp</summary>
        public double Ncp
        {
            get { return (TotalDM > 0) ? 100 * (GrainTotalN / TotalDM) : 0; }
        }

        #endregion

        #region Convertion grain -> ha

        public double ToGrainN(double v)
        {
            return (v / GrainNumber) * 1000.0;
        }

        public double ToGrainDM(double v)
        {
            return (v / GrainNumber) * 1000.0;
        }

        public double FromGrainN(double v)
        {
            return (v * GrainNumber) / 1000.0;
        }

        public double FromGrainDM(double v)
        {
            return (v * GrainNumber) / 1000.0;
        }

        #endregion

        #region Harvest index

        ///<summary>Get the DM harvest index</summary>
        public double HarvestIndexDM(double cropTotalDM)
        {
            return 100 * TotalDM / cropTotalDM;
        }

        ///<summary>Get the N harvest index</summary>
        public double HarvestIndexN(double CropTotalN)
        {
            return 100 * GrainTotalN / CropTotalN; 
        }

        #endregion

        #region ProteinConcentration 

        public double ProteinConcentration
        {
            get { return TotalDM > 0 ? 100 * 5.7 * (GrainTotalN / TotalDM) : 0; }
        }

        #endregion

        #region % starch

        public double PercentStarch
        {
            get { return TotalDMperGrain > 0 ? 100 * Starch / TotalDMperGrain : 0; }
        }

        #endregion

        #region Percents

        ///<summary>Get the % Gliadins</summary>
        public double PercentGli
        {
            get { return 100 * Ngli / TotalNperGrain; }
        }

        ///<summary>Get the % Gluteins</summary>
        public double PercentGlu
        {
            get { return 100 * Nglu / TotalNperGrain; }
        }

        #endregion

        #region Gliadins to gluetins ratio.

        ///<summary>Get the Gliadins to gluetins ratio</summary>
        public double GliadinsToGluteins
        {
            get { return Ngli / Nglu; }
        }

        #endregion

        #region Efficiency

        public double NutilisationEfficiency(double cropTotalN)
        {
            return TotalDM / cropTotalN; 
        }

        #endregion

 
    }
}