using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZedGraph;
using SiriusModel.InOut;
using SiriusModel.Model;

namespace SiriusView.Graph
{
    public class SiriusPointList : IPointList
    {
        public readonly SiriusAxisValueType XType;
        public readonly SiriusAxisValueType YType;

        private readonly int leafLayerIndex;
        private readonly int soilLayerIndex;

        public bool IsLeafLayerIndex { get { return leafLayerIndex != -1; } }
        public bool IsSoilLayerIndex { get { return soilLayerIndex != -1; } }

        public int LeafLayerIndex { get { return leafLayerIndex; } }
        public int SoilLayerIndex { get { return soilLayerIndex; } }
        
        private PointPair[] generatedPointPairs;

        public SiriusPointList(SiriusAxisValueType xType, SiriusAxisValueType yType, int leafLayerIndex, int soilLayerIndex)
        {
            this.XType = xType;
            this.YType = yType;
            this.leafLayerIndex = leafLayerIndex;
            this.soilLayerIndex = soilLayerIndex;

            generatedPointPairs = null;
        }

        private int IPointListCount
        {
            get { return 0;} // RunCore.run2[KeywordUse.Count, Day]; }
        }

        #region IPointList Members

        int IPointList.Count
        {
            get { return IPointListCount; }
        }

        public PointPair this[int index]
        {
            get 
            {
                int count = IPointListCount;
                if (generatedPointPairs == null)
                {
                    generatedPointPairs = new PointPair[count];
                }
                if (generatedPointPairs.Length != count)
                {
                    Array.Resize(ref generatedPointPairs, count);
                }
                if (generatedPointPairs[index] == null)
                {
                    generatedPointPairs[index] = new PointPair();
                }
                PointPair generatedPointPair = generatedPointPairs[index];
                /*switch (XType)
                {
                    case SiriusAxisValueType.Date_t:
                        generatedPointPair.X = RunCore.run2[Date, index].ToOADate();
                        break;
                    case SiriusAxisValueType.DayLength:
                        generatedPointPair.X = (double)RunCore.run2[DayLength, index];
                        break;
                    case SiriusAxisValueType.DayLength_Delta:
                        generatedPointPair.X = (double)RunCore.run2[DayLength, index, Delta];
                        break;
                    case SiriusAxisValueType.GrowthPhase:
                        generatedPointPair.X = (double)((int)RunCore.run2[GrowthPhase, index, Crop]);
                        break;
                    case SiriusAxisValueType.t:
                        generatedPointPair.X = index;
                        break;
                    case SiriusAxisValueType.T_t_Air:
                        generatedPointPair.X = (double)RunCore.run2[T, index, Air, Mean];
                        break;
                    case SiriusAxisValueType.T_t_LeafLayer:
                        generatedPointPair.X = (double)RunCore.run2[T, index, Leaf, Mean];
                        break;
                    case SiriusAxisValueType.T_t_SoilLayer:
                        generatedPointPair.X = (double)RunCore.run2[T, index, Soil, Mean];
                        break;
                    case SiriusAxisValueType.TT_t_Air:
                        generatedPointPair.X = (double)RunCore.run2[TT, index, Air];
                        break;
                    case SiriusAxisValueType.TT_t_LeafLayer:
                        generatedPointPair.X = (double)RunCore.run2[TT, index, Leaf];
                        break;
                    case SiriusAxisValueType.TT_t_SoilLayer:
                        generatedPointPair.X = (double)RunCore.run2[TT, index, Soil];
                        break;
                }
                switch (YType)
                {
                    case SiriusAxisValueType.Date_t:
                        generatedPointPair.Y = RunCore.run2[Date, index].ToOADate();
                        break;
                    case SiriusAxisValueType.DayLength:
                        generatedPointPair.Y = (double)RunCore.run2[DayLength, index];
                        break;
                    case SiriusAxisValueType.DayLength_Delta:
                        generatedPointPair.Y = (double)RunCore.run2[DayLength, index, Delta];
                        break;
                    case SiriusAxisValueType.GrowthPhase:
                        generatedPointPair.Y = (double)((int)RunCore.run2[GrowthPhase, index, Crop]);
                        break;
                    case SiriusAxisValueType.t:
                        generatedPointPair.Y = index;
                        break;
                    case SiriusAxisValueType.T_t_Air:
                        generatedPointPair.Y = (double)RunCore.run2[T, index, Air, Mean];
                        break;
                    case SiriusAxisValueType.T_t_LeafLayer:
                        generatedPointPair.Y = (double)RunCore.run2[T, index, Leaf, Mean];
                        break;
                    case SiriusAxisValueType.T_t_SoilLayer:
                        generatedPointPair.Y = (double)RunCore.run2[T, index, Soil, Mean];
                        break;
                    case SiriusAxisValueType.TT_t_Air:
                        generatedPointPair.Y = (double)RunCore.run2[TT, index, Air];
                        break;
                    case SiriusAxisValueType.TT_t_LeafLayer:
                        generatedPointPair.Y = (double)RunCore.run2[TT, index, Leaf];
                        break;
                    case SiriusAxisValueType.TT_t_SoilLayer:
                        generatedPointPair.Y = (double)RunCore.run2[TT, index, Soil];
                        break;
                }*/
                return generatedPointPair;
            }
        }

        #endregion

        #region ICloneable Members

        public object Clone()
        {
            return new SiriusPointList(XType, YType, leafLayerIndex, soilLayerIndex);
        }

        #endregion
    }
}
