using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiriusModel.Model.SoilModel.EnergyBalance
{
    public class EnergyBalanceBiomaWrapper:UniverseLink
    {

        #region output

        public double NetRadiation_ { get { return energyBalanceState.netRadiation; } }
        public double DiffusionLimitedEvaporation_ { get { return energyBalanceState.diffusionLimitedEvaporation; } }
        public double EnergyLimitedEvaporation_ { get { return energyBalanceState.energyLimitedEvaporation; } }
        public double EvapoTranspirationPriestlyTaylor_ { get { return energyBalanceState.evapoTranspirationPriestlyTaylor; } }
        public double EvapoTranspirationPenman_ { get { return energyBalanceState.evapoTranspirationPenman; } }
        public double PotentialTranspiration_ { get { return energyBalanceState.potentialTranspiration; } }
        public double PotentialEvapoTranspiration_ { get { return energyBalanceState.potentialEvapoTranspiration; } }
        public double Conductance_ { get { return energyBalanceState.conductance; } }
        public double SoilEvaporation_ { get { return energyBalanceState.soilEvaporation; } }
        public double SoilHeatFlux_ { get { return energyBalanceState.soilHeatFlux; } }
        public double CropHeatFlux_ { get { return energyBalanceState.cropHeatFlux; } }
        public double MinCanopyTemperature_ { get { return energyBalanceState.minCanopyTemperature; } }
        public double MaxCanopyTemperature_ { get { return energyBalanceState.maxCanopyTemperature; } }
        public double[] HourlyCanopyTemperature_ { get { return energyBalanceState.hourlyCanopyTemperature; } }
        public double EvapoTranspiration_ { get { return energyBalanceState.evapoTranspiration; } }
        public double[] HourlyVPDAirLeaf { get { return energyBalanceState.hourlyVPDairLeaf; } }
        public double[] VPDeq { get { return energyBalanceState.VPDeq; } }

        #endregion

        public EnergyBalanceBiomaWrapper(Universe universe) : base(universe)
        {
            energyBalanceComponent = new SiriusQualityEnergyBalance.EnergyBalance();
            energyBalanceState = new SiriusQualityEnergyBalance.EnergyBalanceState();
            loadParameters();

        }

        public EnergyBalanceBiomaWrapper(Universe universe, EnergyBalanceBiomaWrapper toCopy, bool copyAll)
            : base(universe)
        {
            energyBalanceState = (toCopy.energyBalanceState != null) ? new SiriusQualityEnergyBalance.EnergyBalanceState(toCopy.energyBalanceState, copyAll) : null;
            if (copyAll)
            {
                energyBalanceComponent = (toCopy.energyBalanceComponent != null) ? new SiriusQualityEnergyBalance.EnergyBalance(toCopy.energyBalanceComponent) : null;
            }
        }

        public void Init(/*bool isFirstInit=true*/)//not sure if it is really needed
        {
            energyBalanceState.netRadiation = 0;
            /*if(isFirstInit)*/ energyBalanceState.diffusionLimitedEvaporation = 0;
            /*if(isFirstInit)*/ energyBalanceState.energyLimitedEvaporation = 0;
            /*if(isFirstInit)*/ energyBalanceState.evapoTranspirationPriestlyTaylor = 0;
            /*if(isFirstInit)*/ energyBalanceState.evapoTranspirationPenman = 0;
            energyBalanceState.potentialTranspiration = 0;
            energyBalanceState.potentialEvapoTranspiration = 0;
            energyBalanceState.conductance = 0;
           /*if(isFirstInit)*/ energyBalanceState.soilEvaporation = 0;
            energyBalanceState.soilHeatFlux = 0;
            energyBalanceState.cropHeatFlux = 0;
            energyBalanceState.minCanopyTemperature = 0;
            energyBalanceState.maxCanopyTemperature = 0;           
            energyBalanceState.evapoTranspiration = 0;


            for (int i=0;i<24 ;i++)
            {
                energyBalanceState.hourlyCanopyTemperature[i]=0;
            }
        }

        public void EstimateEnergyBalance(double deficitOnTopLayers, double minTair, double maxTair, double[] hourlyTemp, double solarRadiation,
            double extraSolarRadiation, double vaporPressure, double tau, double hslope,
            double plantHeight, double wind, double VPDair, bool isWindVpDefined, double[] hourlyVPDair, double[] hourlysolarRadiation, double Wc_3cm, double Ntip, double[] RH)
        {

            energyBalanceState.deficitOnTopLayers = deficitOnTopLayers;
            energyBalanceState.minTair = minTair;
            energyBalanceState.maxTair = maxTair;
            energyBalanceState.hourlyTemp = hourlyTemp;
            energyBalanceState.solarRadiation = solarRadiation;
            energyBalanceState.extraSolarRadiation = extraSolarRadiation;
            energyBalanceState.vaporPressure = vaporPressure;
            energyBalanceState.tau = tau;
            energyBalanceState.hslope = hslope;
            energyBalanceState.plantHeight = plantHeight;
            energyBalanceState.wind = wind;
            energyBalanceState.VPDair = VPDair;
            energyBalanceState.isWindVpDefined = isWindVpDefined ? 1 :0;
            //for maize
            for (int i = 0; i < 24; i++) hourlyVPDair[i] = hourlyVPDair[i] / 10.0;//Conversion hPa to kPa
            energyBalanceState.hourlyVPDair = hourlyVPDair;
            energyBalanceState.hourlySolarRadiation = hourlysolarRadiation;
            energyBalanceState.wc3cm = Wc_3cm;
            energyBalanceState.Ntip = Ntip;
            energyBalanceState.RH = RH;

            energyBalanceComponent.Estimate(energyBalanceState);
        }

        private void loadParameters()
        {
            energyBalanceComponent.SwitchMaize = SwitchMaize ? 1 :0 ;
            energyBalanceComponent.albedoCoefficient = albedoCoefficient;
            energyBalanceComponent.stefanBoltzman = stefanBoltzman;
            energyBalanceComponent.elevation = Elevation;
            energyBalanceComponent.soilDiffusionConstant = soilDiffusionConstant;
            energyBalanceComponent.lambda = lambda;
            energyBalanceComponent.psychrometricConstant = psychrometricConstant;
            energyBalanceComponent.Alpha = Alpha;
            energyBalanceComponent.tauAlpha = tauAlpha;
            energyBalanceComponent.vonKarman = vonKarman;
            energyBalanceComponent.heightWeatherMeasurements = heightWeatherMeasurements;
            energyBalanceComponent.zm = zm;
            energyBalanceComponent.zh = zh;
            energyBalanceComponent.d = d;
            energyBalanceComponent.rhoDensityAir = rhoDensityAir;
            energyBalanceComponent.specificHeatCapacityAir = specificHeatCapacityAir;
        }

        private SiriusQualityEnergyBalance.EnergyBalance energyBalanceComponent;
        private SiriusQualityEnergyBalance.EnergyBalanceState energyBalanceState;
     
    }
}
