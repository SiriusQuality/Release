using Ami.Framework.Tool;
using System;
using System.Collections.Generic;

namespace Sirius.Model.Weather
{
  public class NumericReader
  {
    private readonly double[] buffer;
    private readonly Action<NumericReader, Exception> onException;
    private readonly Action<NumericReader, NumericParser, bool, bool, bool> onLine;
    private int currentColmnNumber;
    private string currentFile;
    private int currentLineNumber;

    public string CurrentFile
    {
      get
      {
        return this.currentFile;
      }
    }

    public int CurrentLineNumbber
    {
      get
      {
        return this.currentLineNumber;
      }
    }

    public int CurrentColumnNumber
    {
      get
      {
        return this.currentColmnNumber;
      }
    }

    public NumericReader(double[] lineBuffer, Action<NumericReader, NumericParser, bool, bool, bool> readerOnLine, Action<NumericReader, Exception> readerOnException)
    {
      Assert.AssertIsNotNull<Action<NumericReader, NumericParser, bool, bool, bool>>(readerOnLine, "Reader on line must be not null.");
      Assert.AssertIsNotNull<Action<NumericReader, Exception>>(readerOnException, "Reader on exception must be not null.");
      this.onLine = readerOnLine;
      this.onException = readerOnException;
      this.buffer = lineBuffer;
    }

    public void Read(IEnumerable<string> files)
    {
      try
      {
        foreach (string path in files)
        {
          this.currentFile = path;
          this.ReadFile(path);
          this.currentFile = (string) null;
        }
      }
      catch (Exception ex)
      {
        this.onException(this, ex);
      }
    }

    private void ReadFile(string path)
    {
      this.currentLineNumber = 0;
      this.currentColmnNumber = 0;
      using (NumericParser numericParser = new NumericParser(path, this.buffer))
      {
        while (!numericParser.EndOfStream)
        {
          bool lineIsEmpty;
          bool tooManyValues;
          bool tooFewValues;
          numericParser.Read(out lineIsEmpty, out tooManyValues, out tooFewValues);
          this.currentLineNumber = numericParser.LineNumber;
          this.currentColmnNumber = numericParser.ColumnNumber;
          this.onLine(this, numericParser, lineIsEmpty, tooManyValues, tooFewValues);
        }
      }
      this.currentLineNumber = 0;
      this.currentColmnNumber = 0;
    }
  }
}
