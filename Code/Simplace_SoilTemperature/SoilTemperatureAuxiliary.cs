using System;
using System.Collections.Generic;

public class SoilTemperatureAuxiliary
{
    private double _SnowIsolationIndex;

    public SoilTemperatureAuxiliary() { }


    public SoilTemperatureAuxiliary(SoilTemperatureAuxiliary toCopy, bool copyAll) // copy constructor 
    {
        if (copyAll)
        {

            SnowIsolationIndex = toCopy.SnowIsolationIndex;
        }
    }
    public double SnowIsolationIndex
    {
        get { return this._SnowIsolationIndex; }
        set { this._SnowIsolationIndex = value; }
    }
}