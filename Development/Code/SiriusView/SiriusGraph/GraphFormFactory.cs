using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SiriusModel.Model;
using ZedGraph;

namespace SiriusView.Graph
{
    public class GraphFormFactory
    {
        public static readonly string t_Date_ID = "t vs Date";
        public static readonly string DayLength_Date_ID = "Day length";
        public static readonly string GrowthPhase_Date_ID = "Growth phase";
        public static readonly string N_SoilLayer_sl_Date_ID = "N soil";
        public static readonly string Temperatures_Date_ID = "Temperatures";
        public static readonly string TT_t_ID = "Thermal times";

        public static readonly string[] AllID =
        {
            DayLength_Date_ID,
            GrowthPhase_Date_ID,
            N_SoilLayer_sl_Date_ID,
            Temperatures_Date_ID,
            TT_t_ID,
            t_Date_ID,
        };

        public static readonly SiriusAxisValueType[][] AllCurveDef =
        {
            new SiriusAxisValueType[] { SiriusAxisValueType.Date_t, SiriusAxisValueType.DayLength, SiriusAxisValueType.Date_t, SiriusAxisValueType.DayLength_Delta},
            new SiriusAxisValueType[] { SiriusAxisValueType.Date_t, SiriusAxisValueType.GrowthPhase },
            new SiriusAxisValueType[] { SiriusAxisValueType.Date_t, SiriusAxisValueType.N_t_SoilLayer_sl},
            new SiriusAxisValueType[] { SiriusAxisValueType.Date_t, SiriusAxisValueType.T_t_Air, SiriusAxisValueType.Date_t, SiriusAxisValueType.T_t_LeafLayer, SiriusAxisValueType.Date_t, SiriusAxisValueType.T_t_SoilLayer},
            new SiriusAxisValueType[] { SiriusAxisValueType.Date_t, SiriusAxisValueType.TT_t_Air, SiriusAxisValueType.Date_t, SiriusAxisValueType.TT_t_LeafLayer, SiriusAxisValueType.Date_t, SiriusAxisValueType.TT_t_SoilLayer},
            new SiriusAxisValueType[] { SiriusAxisValueType.Date_t, SiriusAxisValueType.t },

        };

        public static readonly string[] AllXAxisTitle =
        {
            SiriusAxisValueType.Date_t.AxisTitle(),
            SiriusAxisValueType.Date_t.AxisTitle(),
            SiriusAxisValueType.Date_t.AxisTitle(),
            SiriusAxisValueType.Date_t.AxisTitle(),
            SiriusAxisValueType.Date_t.AxisTitle(),
            SiriusAxisValueType.Date_t.AxisTitle()
        };

        public static readonly string[] AllYAxisTitle =
        {
            "Day length",
            SiriusAxisValueType.GrowthPhase.AxisTitle(),
            "N soil layer",
            "Temperature", 
            "Thermal time",
            SiriusAxisValueType.t.AxisTitle(),
        };

        public static readonly AxisType[] AllXAxisType =
        {
            SiriusAxisValueType.Date_t.AxisType(),
            SiriusAxisValueType.Date_t.AxisType(),
            SiriusAxisValueType.Date_t.AxisType(),
            SiriusAxisValueType.Date_t.AxisType(),
            SiriusAxisValueType.Date_t.AxisType(), 
            SiriusAxisValueType.Date_t.AxisType()
        };

        public static readonly AxisType[] AllYAxisType =
        {
            SiriusAxisValueType.DayLength.AxisType(),
            SiriusAxisValueType.GrowthPhase.AxisType(),
            SiriusAxisValueType.N_t_SoilLayer_sl.AxisType(),
            SiriusAxisValueType.T_t_Air.AxisType(),
            SiriusAxisValueType.TT_t_Air.AxisType(),
            SiriusAxisValueType.t.AxisType(),
        };

        public static SiriusAxisValueType[] GetCurveDef(string graphID)
        {
            return AllCurveDef[Array.IndexOf(AllID, graphID)];
        }

        public static string GetXAxisTitle(string graphID)
        {
            return AllXAxisTitle[Array.IndexOf(AllID, graphID)];
        }

        public static string GetYAxisTitle(string graphID)
        {
            return AllYAxisTitle[Array.IndexOf(AllID, graphID)];
        }

        public static AxisType GetXAxisType(string graphID)
        {
            return AllXAxisType[Array.IndexOf(AllID, graphID)];
        }

        public static AxisType GetYAxisType(string graphID)
        {
            return AllYAxisType[Array.IndexOf(AllID, graphID)];
        }

        private readonly Dictionary<string, GraphForm> forms;

        #region singleton design

        private static readonly GraphFormFactory instance = new GraphFormFactory();

        public static GraphFormFactory This
        {
            get { return instance; }
        }

        private GraphFormFactory()
        {
            forms = new Dictionary<string, GraphForm>();
        }

        #endregion

        public bool Contains(string id)
        {
            return forms.ContainsKey(id);
        }

        public GraphForm[] Forms
        {
            get { return forms.Values.ToArray(); }
        }

        public GraphForm this[string id]
        {
            get
            {
                if (forms.ContainsKey(id))
                {
                    return forms[id];
                }
                else
                {
                    GraphForm newGraphForm = new GraphForm();
                    newGraphForm.GraphID = id;
                    forms.Add(id, newGraphForm);
                    return newGraphForm;
                }

                throw new Exception("Unknown graph ID : " + id);
            }
        }

        internal void Close(GraphForm graphForm)
        {
            forms.Remove(graphForm.GraphID);
        }

        internal void CloseAll()
        {
            GraphForm[] formsArray = forms.Values.ToArray();
            foreach (GraphForm graphForm in formsArray)
            {
                graphForm.Close();
            }
            forms.Clear();
        }
    }
}
