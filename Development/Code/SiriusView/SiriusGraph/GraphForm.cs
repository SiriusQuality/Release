using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SiriusModel.InOut;
using SiriusModel.Model;
using ZedGraph;

namespace SiriusView.Graph
{
    public partial class GraphForm : Base.BaseCentralDockedForm
    {
        public GraphForm()
        {
            InitializeComponent();
        }

        public override string BaseFormID()
        {
            return graphID; 
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //RunCore.RunCoreEndEvent += RunCore_RunCoreEndEvent;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
           // RunCore.RunCoreEndEvent -= RunCore_RunCoreEndEvent;
            GraphFormFactory.This.Close(this);
        }

        void RunCore_RunCoreEndEvent(bool isNormalRun)
        {
            /*if (isNormalRun)
            {
                if (hasSoilLayer)
                {
                    GraphPane graphPane = zedGraphControl1.GraphPane;
                    CurveItem[] curveArray = graphPane.CurveList.ToArray();
                    int nbCurve = curveArray.Length;
                    int nbSoilLayer = RunCore.run2[KeywordUse.Count, KeywordUse.Soil, KeywordUse.Layer];
                    for (int i = 0; i < nbCurve; ++i)
                    {
                        CurveItem curve = curveArray[i];
                        SiriusPointList pointList = curve.Points as SiriusPointList;
                        if (pointList != null)
                        {
                            if (pointList.IsSoilLayerIndex && pointList.SoilLayerIndex >= nbSoilLayer)
                            {
                                zedGraphControl1.GraphPane.CurveList.Remove(curve);
                            }
                        }
                    }
                    int nbAxeValue = axeValues.Length;
                    for (int i = 0; i < nbAxeValue; i += 2)
                    {
                        switch (axeValues[i + 1])
                        {
                            case SiriusAxisValueType.N_t_SoilLayer_sl:
                                {
                                    for (int sl = 0; sl < nbSoilLayer; ++sl)
                                    {
                                        string labelCurve = axeValues[i + 1].AxisTitle() + " " + sl;

                                        LineItem curve = graphPane.CurveList[labelCurve] as LineItem;
                                        if (curve == null)
                                        {
                                            SiriusPointList siriusPointList = new SiriusPointList(axeValues[i], axeValues[i + 1], -1, sl);

                                            curve = graphPane.AddCurve(labelCurve, siriusPointList, Color.Black, SymbolType.None);
                                            curve.MakeUnique();
                                            curve.Symbol.Type = SymbolType.None;
                                            curve.Line.Width = 2.0f;
                                        }
                                    }
                                }
                                break;
                        }
                    }
                }

                zedGraphControl1.AxisChange();
                zedGraphControl1.Invalidate();
            }*/
        }

        private string graphID;
        private SiriusAxisValueType[] axeValues;
        private bool hasLeafLayer;
        private bool hasSoilLayer;        
        public string GraphID 
        {
            get { return graphID; }
            set 
            { 
                /*graphID = value;
                this.UpdateText(graphID);

                axeValues = GraphFormFactory.GetCurveDef(graphID);
                hasLeafLayer = false;
                hasSoilLayer = false;

                GraphPane graphPane = zedGraphControl1.GraphPane;

                graphPane.Title.IsVisible = false;
                graphPane.XAxis.Title.Text = GraphFormFactory.GetXAxisTitle(graphID);
                graphPane.YAxis.Title.Text = GraphFormFactory.GetYAxisTitle(graphID);
                graphPane.XAxis.Type = GraphFormFactory.GetXAxisType(graphID);
                graphPane.YAxis.Type = GraphFormFactory.GetYAxisType(graphID);

                int nbAxeValue = axeValues.Length;
                for (int i = 0; i < nbAxeValue; i += 2)
                {
                    switch (axeValues[i + 1])
                    {
                        default:
                            {
                                SiriusPointList siriusPointList = new SiriusPointList(axeValues[i], axeValues[i + 1], -1, -1);

                                LineItem curve = graphPane.AddCurve(axeValues[i + 1].AxisTitle(), siriusPointList, Color.Black, SymbolType.None);
                                curve.MakeUnique();
                                curve.Symbol.Type = SymbolType.None;
                                curve.Line.Width = 2.0f;
                            }
                            break;
                        case SiriusAxisValueType.N_t_SoilLayer_sl:
                            {
                                hasSoilLayer = true;
                                int nbSoilLayer = RunCore.run2[KeywordUse.Count, KeywordUse.Soil, KeywordUse.Layer];
                                for (int sl = 0; sl < nbSoilLayer; ++sl)
                                {
                                    string labelCurve = axeValues[i + 1].AxisTitle() + " " + sl;
                                    
                                    SiriusPointList siriusPointList = new SiriusPointList(axeValues[i], axeValues[i + 1], -1, sl);

                                    LineItem curve = graphPane.AddCurve(labelCurve, siriusPointList, Color.Black, SymbolType.None);
                                    curve.MakeUnique();
                                    curve.Symbol.Type = SymbolType.None;
                                    curve.Line.Width = 2.0f;
                                }
                            }
                            break;
                    }
                    
                }
                zedGraphControl1.AxisChange();
                zedGraphControl1.Invalidate();*/
            } 
        }
    }
}
