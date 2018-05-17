using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SiriusModel.InOut.Base;

namespace SiriusModel.InOut
{

    ///<summary>
    ///Contain parameter of a variety or a non-variety
    ///</summary>
    [Serializable]
    public class CropParameterItem : ProjectDataFileItemNoChild
    {
        [XmlIgnore]
        public SerializableDictionary<string, double> paramValue;

        # region properties

        public SerializableDictionary<string, double> ParamValue
        {
            get
            {
                paramValue = new SerializableDictionary<string, double>(paramValue.OrderBy(k => k.Key).ToDictionary(pair => pair.Key, pair => pair.Value));
                return paramValue;
            }
            set { this.SetObject(ref paramValue, ref value, "ParamValue"); }
        }

        # endregion

        public CropParameterItem(string name)
            :base(name)
        {
            paramValue = new SerializableDictionary<string, double>();
            paramNameList = new List<string>(paramDef.Keys);
        }

        public CropParameterItem()
            :this("")
        {

        }

        public override void UpdatePath(string oldAbsolute, string newAbsolute)
        {

        }

        public override void CheckWarnings()
        {

        }

        #region parameter definition, unit, submodel

        private static Dictionary<string, string> paramDef = new Dictionary<string, string>()
            {
                // Dry mass allocation
                {"FracBEAR", "Fraction of biomass allocated to the ear during the ear growth period"},
                {"FracLaminaeBGR", "Fraction of anthesis laminae dry mass allocated to the grain"},
                {"FracSheathBGR", "Fraction of anthesis sheath dry mass allocated to the grain"},
                {"FracStemWSC", "Fraction anthesis stem dry mass in the water soluble carbohydrate pool"},
                {"SLWp", "Potential specific lamina dry mass"},
                {"SSWp", "Potential specific sheath dry mass"},
                {"PhyllDurationDMlost", "Phyllochronic duration od dead leaf degradation"},
                // Evapotranspiration
                {"Alpha", "Priestley-Taylor evapotranspiration proportionality constant"},
                {"tauAlpha", "Fraction of the total net radiation exchanged at the soil surface when AlpaE = 1"},
                // Grain
                {"AlphaAlbGlo", "Scaling coefficient of the allometric allocation of structural grain proteins"},
                {"AlphaGlu", "Scaling coefficient of the allometric allocation of storage grain proteins"},
                {"AlphaNC", "Grain structural N to C ratio"},
                {"maxGrainNC", "maximum N/C ratio in grain after the endosperm cell division phase"},
                {"minGrainNC", "minimum N/C ratio in grain after the endosperm cell division phase"},
                {"BetaAlbGlo", "Scaling exponent of the allometric allocation of structural grain proteins"},
                {"BetaGlu", "Scaling exponent of the allometric allocation of storage grain proteins"},
                {"Dcd", "Duration of the endosperm cell division phase"},
                {"Degfm", "Grain maturation duration (from physiological maturity to harvest ripeness)"},
                {"Der", "Duration of the endosperm endoreduplication phase"},
                {"Dgf", "Grain filling duration (from anthesis to physiological maturity)"},
                {"EarGR", "Fruiting efficiency"},
                {"Kcd", "Relative rate of accumulation of grain structural dry mass"},
                // Leaf layer expansion
                {"AreaPL", "Maximum potential surface area of the penultimate leaf lamina"},
                {"AreaSL", "Potential surface area of the leaves produced before floral initiation"},
                {"AreaSS", "Potential surface area of the sheath of the leaves produced before floral initiation"},
                {"L_EP", "Potential maximum length of ear peduncle"},
                {"L_IN1", "Potential maximum length of the uppermost internode"},
                {"NLL", "Number of leaves produced after floral initiation"},
                {"PexpIN", "Phyllochronic duration of internode expansion"},
                {"PexpL", "Phyllochronic duration of leaf lamina expansion"},
                {"PlagLL", "Potential phyllochronic duration between end of expansion and beginning of senescence for the leaves produced after floral initiation"},
                {"PlagSL", "Potential phyllochronic duration between end of expansion and beginning of senescence for the leaves produced before floral initiation"},
                {"PsenLL", "Potential phyllochronic duration of the senescence period for the leaves produced after floral initiation"},
                {"PsenSL", "Potential phyllochronic duration of the senescence period for the leaves produced before floral initiation"},
                {"RatioFLPL", "Ratio of flag leaf to penultimate leaf lamina surface area"},
                {"SenAccT", "Temperature threshold for starting senescence acceleration"},// #Andrea - 23/11/2015
                {"SenAccSlope", "Slope of the senescence acceleration"},// #Andrea - 23/11/2015
                // Light use efficiency
                {"FacCO2", "Sensitivity of LUE to air CO2 concentration"},
                {"Kl", "Light extinction coefficient"},
                {"LUE", "Potential radiation use efficiency under overcast conditions"},
                {"SlopeFR", "Slope of the relationship between LUE and the ratio of diffuse to total solar radiation"},
                {"StdCO2", "Standard atmospheric CO2 concentration"},
                {"TauSLN", "Relative rate of increase of LUE with specific leaf nitrogen"},
                {"LUETmax", "Maximum temperature for LUE"},
                {"LUETopt", "Optimal temperature for LUE"},
                {"LUETmin", "Minimum temperature for LUE"},
                {"LUETshape", "Shape parameter of the temperature response for LUE"},
                // N allocation
                {"AlphaKn", "Scaling coefficient of the relationship between the ratio of nitrogen to light extinction coefficients and the nitrogen nutrition index"},
                {"AlphaNNI", "Scaling coefficient of the N dilution curve"},
                {"AlphaSSN", "Scaling coefficient of the allometric relation between area-based lamina and sheath N content"},
                {"BetaKn", "Scaling exponent of the relationship between the ratio of nitrogen to light extinction coefficients and the nitrogen nutrition index"},
                {"BetaNNI", "Scaling exponent of the N dilution curve"},
                {"BetaSSN", "Scaling exponent of the relationship between area-based lamina and sheath N content"},
                {"LLOSS", "Fraction of leaf N resorbtion resulting in a reduction of LAI"},
                {"MaxLeafRRND", "Maximum relative rate of leaf nitrogen depletion"},
                {"MaxStemN", "Maximum potential stem N concentration"},
                {"MaxStemRRND", "Maximum relative rate of stem nitrogen depletion"},
                {"SLNcri", "Critical area-based nitrogen content for leaf expansion"},
                {"SLNmax0", "Maximum potential specific leaf N of the top leaf layer"},
                {"SLNmin", "Specific leaf N at which LUE is null"},
                {"StrucLeafN", "Structural N concentration of the leaves"},
                {"StrucStemN", "Structural N concentration of the true stem"},
                // Phenology
                {"AMNLFNO", "Absolute minimum possible leaf number"},
                {"AMXLFNO", "Absolute maximum leaf number"},
                {"Dse", "Thermal time from sowing to emergence"},
                {"IsVernalizable", "Depend on growth habit : 0 for Spring, 1 for Winter"},
                {"IntTvern", "Intermediate temperature for vernalization to occur"},
                {"Ldecr", "Leaf number up to which the phyllochron is decreased by Pdecr"},
                {"Lincr", "Leaf number above which the phyllochron is increased by Pincr"},
                {"MaxDL", "Saturating photoperiod above which final leaf number is not influenced by daylength"},
                {"MaxLeafSoil", "Leaf number up to which the canopy temperature is equal to the soil temperature"},
                {"MaxTvern", "Maximum temperature for vernalization to occur"},
                {"MinDL", "Threshold daylength below which it does influence vernalization rate"},
                {"MinTvern", "Minimum temperature for vernalization to occur"},
                {"P", "Phyllochron"},
                {"PNslope", "Number of primordia produced per emerged leaf"},
                {"PNini", "Number of primorida in the apex at emergence"},
                {"Pdecr", "Factor decreasing the phyllochron for leaf number less than Ldecr"},
                {"PFLLAnth", "Phyllochronic duration of the period between flag leaf ligule appearance and anthesis"},
                {"PHEADANTH","Number of phyllochron between heading and anthesis"},
                {"Pincr", "Factor increasing the phyllochron for leaf number higher than Lincr"},
                {"Rp", "Rate of change of Phyllochrone with sowing date"},
                {"SDws", "Sowing date at which Phyllochrone is minimum"},
                {"SDsa_nh", "Sowing date at which Phyllochrone is maximum in northern hemispher"},
                {"SDsa_sh", "Sowing date at which Phyllochrone is maximum in southern hemispher"},
                {"SLDL", "Daylength response of leaf production"},
                {"Tbase", "Base temperature"},
                {"VAI", "Response of vernalization rate to temperature"},
                {"VBEE", "Vernalization rate at 0°C"},
                {"PreAnthesisTmin", "Pre-anthesis minimum temperature response"},// #Andrea - 27/11/2015
                {"PreAnthesisTopt", "Pre-anthesis optimum temperature response"},// #Andrea - 27/11/2015
                {"PreAnthesisTmax", "Pre-anthesis maximum temperature response"},// #Andrea - 27/11/2015
                {"PreAnthesisShape", "Pre-anthesis temperature response, shape parameter"},// #Andrea - 27/11/2015
                {"PostAnthesisTmin", "Post-anthesis minimum temperature response"},// #Andrea - 27/11/2015
                {"PostAnthesisTopt", "Post-anthesis optimum temperature response"},// #Andrea - 27/11/2015
                {"PostAnthesisTmax", "Post-anthesis maximum temperature response"},// #Andrea - 27/11/2015
                {"PostAnthesisShape", "Post-anthesis temperature response, shape parameter"},// #Andrea - 27/11/2015
                {"intTSFLN", "Intercept of the relationship between Haun stage at terminal spikelet and final leaf number"},
                {"slopeTSFLN", "Intercept of the relationship between Haun stage at terminal spikelet and final leaf number"},
                // Root growth and N uptake
                {"BetaRWU", "Efficiency of the root system to extract water through the vertical soil profile"},
                {"DMmaxNuptake", "Crop dry mass at which the potential rate of root N uptake equals MaxNuptake"},
                {"MaxNuptake", "Maximum potential rate of root N uptake"},
                {"MaxRWU", "Maximum relative rate of root water uptake from the top soil layer"},
                {"RVER", "Rate of root vertical extension"},
                // Soil drought factor
                {"LowerFPAWexp", "Fraction of plant available water below which the rate of leaf expansion equals zero"},
                {"LowerFPAWgs", "Fraction of plant available water below which the stomatal conductance equals zero"},
                {"LowerFPAWlue", "Fraction of plant available water below which LUE equals zero"},
                {"LowerFPAWsen", "Fraction of plant available water value below which DSFmax is reached"},
                {"LowerVPD", "Canopy-to-air VPD threshold above which the rate of leaf expansion strats to decreaseand the rate of leaf senescence starts to increase"},
                {"MaxDSF", "Maximum rate of acceleration of leaf senescence in response to soil water deficit"},
                {"UpperFPAWexp", "Fraction of plant available water threshold below which the rate of leaf expansion starts to decrease"},
                {"UpperFPAWgs", "Fraction of plant available water threshold below which the stomatal conductance starts to decrease"},
                {"UpperFPAWlue", "Fraction of plant available water threshold below which LUE starts to decrease"},
                {"UpperFPAWsen", "Fraction of plant available water threshold below which the rate of leaf senescence starts to accelerate"},
                {"UpperVPD", "Canopy-to-air VPD below which the rate of leaf expansion equals zero and the rate of leaf senescence is maximum"},
                //Maize
                {"leafNoInitEmerg", "number of leaves initiated at plant emergence"},
                {"LIR","leaf initiation rate"},
                {"Leaf_tip_emerg",""},
                {"atip"," slope of leaf initiation"},
                {"ttll1","thermal time of first leaf ligulation since first leaf apparence"},
                {"a_ll1","slope of leaf ligulation"},
                {"Nfinal","Final Leaf Number"},
                {"Lagmax",""},
                {"Sigma","Leaf rank of the maximum potential elongation rate relative to maximum leaf number"},
                {"AlphaLER","Distance between inflexion points relative to maximum number of leaves"},
                {"LERa","potential growth of leaf 6"},
                {"LERb","effect of vapor pressure deficit on leaf 6 elongation"},
                {"LERc","effect of soil water deficit on leaf 6 elongation"},
                {"SLNparam",""},
                {"BLrankFLN",""},
                {"SLsize",""},
                {"BLsize",""},
                {"plantDensity"," number of plants"}
            };

        [XmlIgnore]
        public Dictionary<string, string> ParamDef
        {
            get
            {
                return paramDef;
            }
        }

        private static Dictionary<string, string> paramUnit = new Dictionary<string, string>()
            {
                // Dry mass allocation
                {"FracBEAR", "dimensionless"},
                {"FracLaminaeBGR", "dimensionless"},
                {"FracSheathBGR", "dimensionless"},
                {"FracStemWSC", "dimensionless"},
                {"SLWp", "g(DM)/m²"},
                {"SSWp", "g(DM)/m²"},
                {"PhyllDurationDMlost", "°Cd"},
                // Evapotranspiration
                {"Alpha", "dimensionless"},
                {"tauAlpha", "dimensionless"},
                // Grain
                {"AlphaAlbGlo", "gN/grain"},
                {"AlphaGlu", "gN/grain"},
                {"AlphaNC", "dimensionless"},
                {"maxGrainNC", "dimensionless"},
                {"minGrainNC", "dimensionless"},
                {"BetaAlbGlo", "dimensionless"},
                {"BetaGlu", "dimensionless"},
                {"Dcd", "°Cday"},
                {"Degfm", "°Cday"},
                {"Der", "°Cday"},
                {"Dgf", "°Cday"},
                {"EarGR", "grain/g(DM)"},
                {"Kcd", "(°Cday)^-1"},
                // Leaf layer expansion
                {"AreaPL", "cm²/lamina"},
                {"AreaSL", "cm²/lamina"},
                {"AreaSS", "cm²/lamina"},
                {"L_EP", "m"},
                {"L_IN1", "m"},
                {"NLL", "leaf"},
                {"PexpIN", "phyllochron"},
                {"PexpL", "phyllochron"},
                {"PlagLL", "phyllochron"},
                {"PlagSL", "phyllochron"},
                {"PsenLL", "phyllochron"},
                {"PsenSL", "phyllochron"},
                {"RatioFLPL", "dimensionless"},
                {"SenAccT", "°C"},// #Andrea 23/11/2015
                {"SenAccSlope", "1/°C"},// #Andrea 23/11/2015
                // Light use efficiency
                {"FacCO2", "dimensionless"},
                {"Kl", "m²(ground)/m²(leaf)"},
                {"LUE", "g(DM)/MJ(PAR)"},
                {"SlopeFR", "dimensionless"},
                {"StdCO2", "ppm"},
                {"TauSLN", "m²/g(N)"},
                {"LUETmax", "°C"},
                {"LUETopt", "°C"},
                {"LUETmin", "°C"},
                {"LUETshape", "dimensionless"},
                // N allocation
                {"AlphaKn", "m²(ground)/m²(leaf)"},
                {"AlphaNNI", "g(N)/g(DM)"},
                {"AlphaSSN", "g(N)/m²"},
                {"BetaKn", "dimensionless"},
                {"BetaNNI", "dimensionless"},
                {"BetaSSN", "dimensionless"},
                {"LLOSS", "m²(leaf)/m²(ground)"},
                {"MaxLeafRRND", "1/(°Cday)"},
                {"MaxStemN", "m(N)/g(DM)"},
                {"MaxStemRRND", "1/(°Cday)"},
                {"SLNcri", "g(N)/m²(leaf)"},
                {"SLNmax0", "g(N)/m²(leaf)"},
                {"SLNmin", "g(N)/m²(leaf)"},
                {"StrucLeafN", "g(N)/g(DM)"},
                {"StrucStemN", "g(N)/g(DM)"},
                // Phenology
                {"AMNLFNO", "leaf"},
                {"AMXLFNO", "leaf"},
                {"Dse", "°Cd"},
                {"IsVernalizable", "boolean"},
                {"IntTvern", "°C"},
                {"Ldecr", "leaf"},
                {"Lincr", "leaf"},
                {"MaxDL", "hour"},
                {"MaxLeafSoil", "leaf"},
                {"MaxTvern", "°C"},
                {"MinDL", "hour"},
                {"MinTvern", "°C"},
                {"P", "°Cday"},
                {"PNslope", "°Cday"},
                {"PNini", "primordia"},
                {"Pdecr", "dimensionless"},
                {"PFLLAnth", "dimensionless"},
                {"PHEADANTH", "phyllochron"},
                {"Pincr", "dimensionless"},
                {"Rp", "1/day"},
                {"SDws", "day"},
                {"SDsa_nh", "day"},
                {"SDsa_sh", "day"},
                {"SLDL", "leaf/h (daylength)"},
                {"Tbase", "°C"},
                {"VAI", "1/(d.°C)"},
                {"VBEE", "1/d"},
                {"PreAnthesisTmin", "°C"},// #Andrea 27/11/2015
                {"PreAnthesisTopt", "°C"},// #Andrea 27/11/2015
                {"PreAnthesisTmax", "°C"},// #Andrea 27/11/2015
                {"PreAnthesisShape", "°C"},// #Andrea 27/11/2015
                {"PostAnthesisTmin", "°C"},// #Andrea 27/11/2015
                {"PostAnthesisTopt", "°C"},// #Andrea 27/11/2015
                {"PostAnthesisTmax", "°C"},// #Andrea 27/11/2015
                {"PostAnthesisShape", "°C"},// #Andrea 27/11/2015
                {"intTSFLN", "leaf"},
                {"slopeTSFLN", "dimensionless"},
                // Root growth and N uptake
                {"BetaRWU", "dimensionless"},
                {"DMmaxNuptake", "g(DM)/m²"},
                {"MaxNuptake", "g(N)/(m².d)"},
                {"MaxRWU", "1/d"},
                {"RVER", "m/°Cday"},
                // Soil drought factor
                {"LowerFPAWexp", "dimensionless"},
                {"LowerFPAWgs", "dimensionless"},
                {"LowerFPAWlue", "dimensionless"},
                {"LowerFPAWsen", "dimensionless"},
                {"LowerVPD", "hPa"},
                {"MaxDSF", "dimensionless"},
                {"UpperFPAWexp", "dimensionless"},
                {"UpperFPAWgs", "dimensionless"},
                {"UpperFPAWlue", "dimensionless"},
                {"UpperFPAWsen", "dimensionless"},
                {"UpperVPD", "hPa"},
                //Maize
                {"leafNoInitEmerg", "leaf"},
                {"LIR",""},
                {"Leaf_tip_emerg",""},
                {"atip","leaf/°Cday²"},
                {"ttll1","°Cday"},
                {"a_ll1","leaf/°Cday"},
                {"Nfinal","leaf"},
                {"Lagmax",""},
                {"Sigma","dimentionless"},
                {"AlphaLER","dimentionless"},
                {"LERa","mm/dd"},
                {"LERb","mm/[dd.kPa]"},
                {"LERc","mm/[dd.Bars]"},
                {"SLNparam",""},
                {"BLrankFLN",""},
                {"SLsize",""},
                {"BLsize",""},
                {"plantDensity","plant"}
            };

        [XmlIgnore]
        public Dictionary<string, string> ParamUnit
        {
            get
            {
                return paramUnit;
            }
        }

        private static Dictionary<string, string> paramModel = new Dictionary<string, string>()
            {
                // Dry mass allocation
                {"FracBEAR", "Dry mass allocation"},
                {"FracLaminaeBGR", "Dry mass allocation"},
                {"FracSheathBGR", "Dry mass allocation"},
                {"FracStemWSC", "Dry mass allocation"},
                {"SLWp", "Dry mass allocation"},
                {"SSWp", "Dry mass allocation"},
                {"PhyllDurationDMlost", "Dry mass allocation"},
                // Evapotranspiration
                {"Alpha", "Evapotranspiration"},
                {"tauAlpha", "Evapotranspiration"},
                // Grain
                {"AlphaAlbGlo", "Grain"},
                {"AlphaGlu", "Grain"},
                {"AlphaNC", "Grain"},
                {"maxGrainNC", "Grain"},
                {"minGrainNC", "Grain"},
                {"BetaAlbGlo", "Grain"},
                {"BetaGlu", "Grain"},
                {"Dcd", "Grain"},
                {"Degfm", "Grain"},
                {"Der", "Grain"},
                {"Dgf", "Grain"},
                {"EarGR", "Grain"},
                {"Kcd", "Grain"},
                // Leaf layer expansion
                {"AreaPL", "Leaf layer expansion"},
                {"AreaSL", "Leaf layer expansion"},
                {"AreaSS", "Leaf layer expansion"},
                {"L_EP", "Leaf layer expansion"},
                {"L_IN1", "Leaf layer expansion"},
                {"NLL", "Leaf layer expansion"},
                {"PexpIN", "Leaf layer expansion"},
                {"PexpL", "Leaf layer expansion"},
                {"PlagLL", "Leaf layer expansion"},
                {"PlagSL", "Leaf layer expansion"},
                {"PsenLL", "Leaf layer expansion"},
                {"PsenSL", "Leaf layer expansion"},
                {"RatioFLPL", "Leaf layer expansion"},
                {"SenAccT", "Leaf layer expansion"}, // #Andrea 27/11/2015
                {"SenAccSlope", "Leaf layer expansion"}, // #Andrea 27/11/2015
                // Light use efficiency
                {"FacCO2", "Light use efficiency"},
                {"Kl", "Light use efficiency"},
                {"LUE", "Light use efficiency"},
                {"SlopeFR", "Light use efficiency"},
                {"StdCO2", "Light use efficiency"},
                {"TauSLN", "Light use efficiency"},
                {"LUETmax", "Light use efficiency"},
                {"LUETopt", "Light use efficiency"},
                {"LUETmin", "Light use efficiency"},
                {"LUETshape", "Light use efficiency"},
                // N allocation
                {"AlphaKn", "N allocation"},
                {"AlphaNNI", "N allocation"},
                {"AlphaSSN", "N allocation"},
                {"BetaKn", "N allocation"},
                {"BetaNNI", "N allocation"},
                {"BetaSSN", "N allocation"},
                {"LLOSS", "N allocation"},
                {"MaxLeafRRND", "N allocation"},
                {"MaxStemN", "N allocation"},
                {"MaxStemRRND", "N allocation"},
                {"SLNcri", "N allocation"},
                {"SLNmax0", "N allocation"},
                {"SLNmin", "N allocation"},
                {"StrucLeafN", "N allocation"},
                {"StrucStemN", "N allocation"},
                // Phenology
                {"AMNLFNO", "Phenology"},
                {"AMXLFNO", "Phenology"},
                {"Dse", "Phenology"},
                {"IsVernalizable", "Phenology"},
                {"IntTvern", "Phenology"},
                {"Ldecr", "Phenology"},
                {"Lincr", "Phenology"},
                {"MaxDL", "Phenology"},
                {"MaxLeafSoil", "Phenology"},
                {"MaxTvern", "Phenology"},
                {"MinDL", "Phenology"},
                {"MinTvern", "Phenology"},
                {"P", "Phenology"},
                {"PNslope", "Phenology"},
                {"PNini", "Phenology"},
                {"Pdecr", "Phenology"},
                {"PFLLAnth", "Phenology"},
                {"PHEADANTH", "Phenology"},
                {"Pincr", "Phenology"},
                {"Rp", "Phenology"},
                {"SDws", "Phenology"},
                {"SDsa_nh", "Phenology"},
                {"SDsa_sh", "Phenology"},
                {"SLDL", "Phenology"},
                {"Tbase", "Phenology"},
                {"VAI", "Phenology"},
                {"VBEE", "Phenology"},
                {"PreAnthesisTmin", "Phenology"},// #Andrea 27/11/2015
                {"PreAnthesisTopt", "Phenology"},// #Andrea 27/11/2015
                {"PreAnthesisTmax", "Phenology"},// #Andrea 27/11/2015
                {"PreAnthesisShape", "Phenology"},// #Andrea 27/11/2015
                {"PostAnthesisTmin", "Phenology"},// #Andrea 27/11/2015
                {"PostAnthesisTopt", "Phenology"},// #Andrea 27/11/2015
                {"PostAnthesisTmax", "Phenology"},// #Andrea 27/11/2015
                {"PostAnthesisShape", "Phenology"},// #Andrea 27/11/2015
                {"intTSFLN", "Phenology"},
                {"slopeTSFLN", "Phenology"},
                // Root growth and N uptake
                {"BetaRWU", "Root growth and N uptake"},
                {"DMmaxNuptake", "Root growth and N uptake"},
                {"MaxNuptake", "Root growth and N uptake"},
                {"MaxRWU", "Root growth and N uptake"},
                {"RVER", "Root growth and N uptake"},
                // Drought factor
                {"LowerFPAWexp", "Drought factor"},
                {"LowerFPAWgs", "Drought factor"},
                {"LowerFPAWlue", "Drought factor"},
                {"LowerFPAWsen", "Drought factor"},
                {"LowerVPD", "Drought factor"},
                {"MaxDSF", "Drought factor"},
                {"UpperFPAWexp", "Drought factor"},
                {"UpperFPAWgs", "Drought factor"},
                {"UpperFPAWlue", "Drought factor"},
                {"UpperFPAWsen", "Drought factor"},
                {"UpperVPD", "Drought factor"},

                //Maize
                {"leafNoInitEmerg", "Maize"},
                {"LIR","Maize"},
                {"Leaf_tip_emerg","Maize"},
                {"atip","Maize"},
                {"ttll1","Maize"},
                {"a_ll1","Maize"},
                {"Nfinal","Maize"},
                {"Lagmax","Maize"},
                {"Sigma","Maize"},
                {"AlphaLER","Maize"},
                {"LERa","Maize"},
                {"LERb","Maize"},
                {"LERc","Maize"},
                {"SLNparam","Maize"},
                {"BLrankFLN","Maize"},
                {"SLsize","Maize"},
                {"BLsize","Maize"},
                {"plantDensity","Maize"}
            };

        [XmlIgnore]
        public Dictionary<string, string> ParamModel
        {
            get
            {
                return paramModel;
            }
        }


        [XmlIgnore]
        public static List<string> paramNameList;


        #endregion
    }
}
