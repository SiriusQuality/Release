using System;
using System.Collections.Generic;
public class SoilTemperatureCompState 
{
    private double[] _V;
    private double[] _B;
    private double[] _volumeMatrix;
    private double[] _volumeMatrixOld;
    private double[] _matrixPrimaryDiagonal;
    private double[] _matrixSecondaryDiagonal;
    private double[] _heatConductivity;
    private double[] _heatConductivityMean;
    private double[] _heatCapacity;
    private double[] _solution;
    private double[] _matrixDiagonal;
    private double[] _matrixLowerTriangle;
    private double[] _heatFlow;
    private double _soilSurfaceTemperature;
    private double[] _soilTemperature;
    private double _noSnowSoilSurfaceTemperature;
    
        public SoilTemperatureCompState() { }
    
    
    public SoilTemperatureCompState(SoilTemperatureCompState toCopy, bool copyAll) // copy constructor 
    {
    if (copyAll)
    {
    
            for (int i = 0; i < 100; i++)
            { V = toCopy.V; }
    
    
            for (int i = 0; i < 100; i++)
            { B = toCopy.B; }
    
            for (int i = 0; i < 100; i++)
            { volumeMatrix = toCopy.volumeMatrix; }
    
            for (int i = 0; i < 100; i++)
            { volumeMatrixOld = toCopy.volumeMatrixOld; }
    
            for (int i = 0; i < 100; i++)
            { matrixPrimaryDiagonal = toCopy.matrixPrimaryDiagonal; }
    
            for (int i = 0; i < 100; i++)
            { matrixSecondaryDiagonal = toCopy.matrixSecondaryDiagonal; }
    
            for (int i = 0; i < 100; i++)
            { heatConductivity = toCopy.heatConductivity; }
    
            for (int i = 0; i < 100; i++)
            { heatConductivityMean = toCopy.heatConductivityMean; }
    
            for (int i = 0; i < 100; i++)
            { heatCapacity = toCopy.heatCapacity; }
    
            for (int i = 0; i < 100; i++)
            { solution = toCopy.solution; }
    
            for (int i = 0; i < 100; i++)
            { matrixDiagonal = toCopy.matrixDiagonal; }
    
            for (int i = 0; i < 100; i++)
            { matrixLowerTriangle = toCopy.matrixLowerTriangle; }
    
            for (int i = 0; i < 100; i++)
            { heatFlow = toCopy.heatFlow; }
    
    soilSurfaceTemperature = toCopy.soilSurfaceTemperature;

            for (int i = 0; i < 100; i++)
            { soilTemperature = toCopy.soilTemperature ; }
    
    noSnowSoilSurfaceTemperature = toCopy.noSnowSoilSurfaceTemperature;
    }
    }
    public double[] V
        {
            get { return this._V; }
            set { this._V= value; } 
        }
    public double[] B
        {
            get { return this._B; }
            set { this._B= value; } 
        }
    public double[] volumeMatrix
        {
            get { return this._volumeMatrix; }
            set { this._volumeMatrix= value; } 
        }
    public double[] volumeMatrixOld
        {
            get { return this._volumeMatrixOld; }
            set { this._volumeMatrixOld= value; } 
        }
    public double[] matrixPrimaryDiagonal
        {
            get { return this._matrixPrimaryDiagonal; }
            set { this._matrixPrimaryDiagonal= value; } 
        }
    public double[] matrixSecondaryDiagonal
        {
            get { return this._matrixSecondaryDiagonal; }
            set { this._matrixSecondaryDiagonal= value; } 
        }
    public double[] heatConductivity
        {
            get { return this._heatConductivity; }
            set { this._heatConductivity= value; } 
        }
    public double[] heatConductivityMean
        {
            get { return this._heatConductivityMean; }
            set { this._heatConductivityMean= value; } 
        }
    public double[] heatCapacity
        {
            get { return this._heatCapacity; }
            set { this._heatCapacity= value; } 
        }
    public double[] solution
        {
            get { return this._solution; }
            set { this._solution= value; } 
        }
    public double[] matrixDiagonal
        {
            get { return this._matrixDiagonal; }
            set { this._matrixDiagonal= value; } 
        }
    public double[] matrixLowerTriangle
        {
            get { return this._matrixLowerTriangle; }
            set { this._matrixLowerTriangle= value; } 
        }
    public double[] heatFlow
        {
            get { return this._heatFlow; }
            set { this._heatFlow= value; } 
        }
    public double soilSurfaceTemperature
        {
            get { return this._soilSurfaceTemperature; }
            set { this._soilSurfaceTemperature= value; } 
        }
    public double[] soilTemperature
        {
            get { return this._soilTemperature; }
            set { this._soilTemperature= value; } 
        }
    public double noSnowSoilSurfaceTemperature
        {
            get { return this._noSnowSoilSurfaceTemperature; }
            set { this._noSnowSoilSurfaceTemperature= value; } 
        }
}