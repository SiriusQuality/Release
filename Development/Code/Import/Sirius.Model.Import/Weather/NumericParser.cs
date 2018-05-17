using Ami.Framework.Tool;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Sirius.Model.Weather
{
  public class NumericParser : IDisposable
  {
    private const int Char0 = 48;
    private const int Char9 = 57;
    private const int CharDot = 46;
    private const int CharEnd1 = 10;
    private const int CharEnd2 = 13;
    private const int CharExp1 = 101;
    private const int CharExp2 = 69;
    private const int CharMinus = 45;
    private const int CharSpace = 32;
    private const int CharTab = 9;
    private readonly double[] doubleBuffer;
    private readonly int doubleBufferLength;
    private int columNumber;
    private bool isDisposed;
    private int lineNumber;
    private int peek;
    private StreamReader streamReader;

    public int LineNumber
    {
      get
      {
        if (this.isDisposed)
          throw new ObjectDisposedException("NumericParser");
        return this.lineNumber;
      }
    }

    public int ColumnNumber
    {
      get
      {
        if (this.isDisposed)
          throw new ObjectDisposedException("NumericParser");
        return this.columNumber;
      }
    }

    public bool EndOfStream
    {
      get
      {
        if (this.isDisposed)
          throw new ObjectDisposedException("NumericParser");
        return this.streamReader.EndOfStream;
      }
    }

    public NumericParser(string path, double[] numericBuffer)
    {
      Assert.AssertIsNotNull<double[]>(numericBuffer, "Numeric buffer must be not null.");
      Assert.AssertIsPositive(numericBuffer.Length, "Numeric buffer lenght must be positive.");
      this.isDisposed = false;
      this.streamReader = new StreamReader(path);
      this.doubleBuffer = numericBuffer;
      this.doubleBufferLength = this.doubleBuffer.Length;
      this.lineNumber = 1;
      this.columNumber = 1;
      this.SkipHeader();
      this.PeekStream();
    }

    private void PeekStream()
    {
      this.peek = this.streamReader.Peek();
    }

    private void ReadStream()
    {
      this.streamReader.Read();
      ++this.columNumber;
      this.peek = this.streamReader.Peek();
    }

    private void ReadEndLineStream()
    {
      this.streamReader.Read();
      this.columNumber = 1;
      ++this.lineNumber;
      this.peek = this.streamReader.Peek();
    }

    public void Read(out bool lineIsEmpty, out bool tooManyValues, out bool tooFewValues)
    {
      if (this.isDisposed)
        throw new ObjectDisposedException("NumericParser");
      this.SkipToNextLine();
      int index1 = 0;
      this.SkipToNextValue();
      for (; this.peek != -1 && !this.EndOfLine() && index1 < this.doubleBufferLength; ++index1)
        this.doubleBuffer[index1] = this.ReadValue();
      if (index1 == this.doubleBufferLength)
      {
        lineIsEmpty = false;
        tooManyValues = this.SkipEndLineValues();
        tooFewValues = false;
      }
      else
      {
        tooManyValues = false;
        tooFewValues = true;
        for (int index2 = index1; index2 < this.doubleBufferLength; ++index2)
          this.doubleBuffer[index2] = double.NaN;
        lineIsEmpty = index1 == 0;
      }
    }

    private double ReadValue()
    {
      this.SkipToNextValue();
      if ((this.peek < 48 || this.peek > 57) && this.peek != 45)
      {
        this.SkipCurrentValue();
        return double.NaN;
      }
      bool flag1 = this.peek == 45;
      if (flag1)
        this.ReadStream();
      int nbChar;
      double d1 = this.ReadIntValue(out nbChar);
      if (double.IsNaN(d1))
      {
        this.SkipCurrentValue();
        return double.NaN;
      }
      if (this.peek == 46)
      {
        this.ReadStream();
        double d2 = this.ReadIntValue(out nbChar);
        if (double.IsNaN(d2))
        {
          this.SkipCurrentValue();
          return double.NaN;
        }
        d1 += d2 * Math.Pow(10.0, (double) -nbChar);
      }
      if (this.peek == 46)
      {
        this.SkipCurrentValue();
        return double.NaN;
      }
      if (this.peek == 101 || this.peek == 69)
      {
        this.ReadStream();
        bool flag2 = this.peek == 45;
        if (flag2)
          this.ReadStream();
        double num = this.ReadIntValue(out nbChar);
        if (double.IsNaN(num))
        {
          this.SkipCurrentValue();
          return double.NaN;
        }
        if (flag2)
          num *= -1.0;
        d1 *= Math.Pow(10.0, num);
      }
      if (this.peek == -1 || this.peek == 10 || (this.peek == 13 || this.peek == 32) || this.peek == 9)
      {
        if (!flag1)
          return d1;
        return -1.0 * d1;
      }
      this.SkipCurrentValue();
      return double.NaN;
    }

    private double ReadIntValue(out int nbChar)
    {
      this.SkipToNextValue();
      if (this.peek == -1)
      {
        nbChar = 0;
        return double.NaN;
      }
      if (this.peek < 48 || this.peek > 57)
      {
        nbChar = 0;
        return double.NaN;
      }
      int num = this.peek - 48;
      nbChar = 1;
      this.ReadStream();
      while (this.peek != -1 && this.peek >= 48 && this.peek <= 57)
      {
        ++nbChar;
        num = num * 10 + (this.peek - 48);
        this.ReadStream();
      }
      if (this.peek == -1 || this.peek == 46 || (this.peek == 10 || this.peek == 13) || (this.peek == 101 || this.peek == 69 || (this.peek == 45 || this.peek == 32)) || this.peek == 9)
        return (double) num;
      return double.NaN;
    }

    private void SkipToNextLine()
    {
      while (this.peek != -1 && (this.peek == 32 || this.peek == 9 || (this.peek == 10 || this.peek == 13)))
      {
        if (this.peek == 32 || this.peek == 9 || this.peek == 13)
          this.ReadStream();
        else
          this.ReadEndLineStream();
      }
    }

    private void SkipToNextValue()
    {
      while (this.peek != -1 && (this.peek == 32 || this.peek == 9))
        this.ReadStream();
    }

    private void SkipCurrentValue()
    {
      while (this.peek != -1 && this.peek != 10 && (this.peek != 13 && this.peek != 32) && this.peek != 9)
        this.ReadStream();
    }

    private bool SkipEndLineValues()
    {
      bool flag = false;
      while (this.peek != -1 && this.peek != 10 && this.peek != 13)
      {
        if (this.peek != 32 && this.peek != 9)
          flag = true;
        this.ReadStream();
      }
      return !flag;
    }

    private void SkipHeader()
    {
      bool flag = true;
      int num = 0;
      while (flag)
      {
        string str1 = this.streamReader.ReadLine();
        string str2 = str1 != null ? str1.Trim() : (string) null;
        if (str2 == null || string.IsNullOrWhiteSpace(str2) || (str2.StartsWith("//") || str2.StartsWith("@")) || (str2.StartsWith("*") || Enumerable.Any<char>((IEnumerable<char>) str2, new Func<char, bool>(char.IsLetter))))
          ++num;
        else
          flag = false;
      }
      this.streamReader.BaseStream.Position = 0L;
      this.streamReader.DiscardBufferedData();
      for (int index = 0; index < num; ++index)
      {
        this.streamReader.ReadLine();
        ++this.lineNumber;
      }
    }

    private bool EndOfLine()
    {
      if (this.peek != 10)
        return this.peek == 13;
      return true;
    }

    public void Dispose()
    {
      if (this.streamReader != null)
      {
        this.streamReader.Dispose();
        this.streamReader = (StreamReader) null;
      }
      this.isDisposed = true;
    }
  }
}
