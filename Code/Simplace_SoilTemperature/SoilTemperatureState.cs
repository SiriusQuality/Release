using System;
using System.Collections.Generic;
public class SoilTemperatureState
{
    private double _Albedo;
    private double _SnowWaterContent;
    private double _SoilSurfaceTemperature;
    private int _AgeOfSnow;
    private double[] _rSoilTempArrayRate;
    private double[] _pSoilLayerDepth;
    private double[] _SoilTempArray;

    public SoilTemperatureState() { }


    public SoilTemperatureState(SoilTemperatureState toCopy, bool copyAll) // copy constructor 
    {
        if (copyAll)
        {

            Albedo = toCopy.Albedo;
            SnowWaterContent = toCopy.SnowWaterContent;
            SoilSurfaceTemperature = toCopy.SoilSurfaceTemperature;
            AgeOfSnow = toCopy.AgeOfSnow;

            if(rSoilTempArrayRate != null)
            {
                rSoilTempArrayRate = new double[toCopy.rSoilTempArrayRate.Length];
                for (int i = 0; i < toCopy.rSoilTempArrayRate.Length; i++)
                { rSoilTempArrayRate[i] = toCopy.rSoilTempArrayRate[i]; }
            }

            if (pSoilLayerDepth != null)
            {
                pSoilLayerDepth = new double[toCopy.pSoilLayerDepth.Length];
                for (int i = 0; i < toCopy.pSoilLayerDepth.Length; i++)
                { pSoilLayerDepth[i] = toCopy.pSoilLayerDepth[i]; }
            }

            if (SoilTempArray != null)
            {
                SoilTempArray = new double[toCopy.SoilTempArray.Length];
                for (int i = 0; i < toCopy.SoilTempArray.Length; i++)
                { SoilTempArray[i] = toCopy.SoilTempArray[i]; }
            }

        }
    }
    public double Albedo
    {
        get { return this._Albedo; }
        set { this._Albedo = value; }
    }
    public double SnowWaterContent
    {
        get { return this._SnowWaterContent; }
        set { this._SnowWaterContent = value; }
    }
    public double SoilSurfaceTemperature
    {
        get { return this._SoilSurfaceTemperature; }
        set { this._SoilSurfaceTemperature = value; }
    }
    public int AgeOfSnow
    {
        get { return this._AgeOfSnow; }
        set { this._AgeOfSnow = value; }
    }
    public double[] rSoilTempArrayRate
    {
        get { return this._rSoilTempArrayRate; }
        set { this._rSoilTempArrayRate = value; }
    }
    public double[] pSoilLayerDepth
    {
        get { return this._pSoilLayerDepth; }
        set { this._pSoilLayerDepth = value; }
    }
    public double[] SoilTempArray
    {
        get { return this._SoilTempArray; }
        set { this._SoilTempArray = value; }
    }
}