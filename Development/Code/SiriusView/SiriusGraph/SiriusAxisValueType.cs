using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZedGraph;

namespace SiriusView.Graph
{
    public enum SiriusAxisValueType
    {
        Date_t,
        DayLength,
        DayLength_Delta,
        GrowthPhase,
        N_t_SoilLayer_sl,
        t,
        T_t_Air,
        T_t_LeafLayer,
        T_t_SoilLayer,
        TT_t_Air,
        TT_t_LeafLayer,
        TT_t_SoilLayer,
    }

    public static class SiriusAxeValueTypeHelper
    {
        private static readonly string[] AllAxisTitle = 
        {
            "Date",
            "Day length",
            "Day length delta",
            "Growth phase",
            "N soil layer", 
            "t",
            "T air",
            "T canopy",
            "T soil",
            "TT air",
            "TT canopy", 
            "TT soil"
        };

        public static readonly AxisType[] AllAxisType =
        {
            ZedGraph.AxisType.Date,
            ZedGraph.AxisType.Linear,
            ZedGraph.AxisType.Linear,
            ZedGraph.AxisType.Linear,
            ZedGraph.AxisType.Linear,
            ZedGraph.AxisType.Linear,
            ZedGraph.AxisType.Linear,
            ZedGraph.AxisType.Linear,
            ZedGraph.AxisType.Linear,
            ZedGraph.AxisType.Linear,
            ZedGraph.AxisType.Linear
        };

        public static string AxisTitle(this SiriusAxisValueType axeValueType)
        {
            return AllAxisTitle[(int)axeValueType];
        }

        public static AxisType AxisType(this SiriusAxisValueType axeValueType)
        {
            return AllAxisType[(int)axeValueType];
        }
    }
}
