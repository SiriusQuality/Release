using System;
using SiriusModel.InOut.OutputWriter;
using SiriusModel.Model;

namespace SiriusModel.InOut
{
    public enum OutputVersion
    {
        V13,
        V15,
        Cus,
        Maize
    }

    public static class OutputFile
    {
        private static readonly OutputFileExtractorV13 V13 = new OutputFileExtractorV13();
        private static readonly OutputFileExtractorV15 V15 = new OutputFileExtractorV15();
        private static readonly OutputFileExtractorCus Cus = new OutputFileExtractorCus();
        private static readonly OutputFileExtractorMaize Maize = new OutputFileExtractorMaize();

        public static PageData ExtractNormalRun(Run runOld)
        {
            switch (ProjectFile.OutputVersion)
            {
                case OutputVersion.V13: return V13.NormalRun(runOld);
                case OutputVersion.V15: return V15.NormalRun(runOld);
                case OutputVersion.Cus: return Cus.NormalRun(runOld);
                case OutputVersion.Maize: return Maize.NormalRun(runOld);
                default: throw new NotImplementedException();
            }
        }

        public static PageData ExtractMultiRunHeader()
        {
            switch (ProjectFile.OutputVersion)
            {
                case OutputVersion.V13: return V13.MultiRunHeader();
                case OutputVersion.V15: return V15.MultiRunHeader();
                case OutputVersion.Cus: return Cus.MultiRunHeader();
                case OutputVersion.Maize: return Maize.MultiRunHeader();
                default: throw new NotImplementedException();
            }
        }

        public static LineData ExtractMultiRunLine(Run runOld)
        {
            switch (ProjectFile.OutputVersion)
            {
                // Prints the summary variables line by line;
                case OutputVersion.V13: return V13.MultiRunLine(runOld);
                case OutputVersion.V15: return V15.MultiRunLine(runOld);
                case OutputVersion.Cus: return Cus.MultiRunLine(runOld);
                case OutputVersion.Maize: return Maize.MultiRunLine(runOld);
                default: throw new NotImplementedException();
            }
        }

        public static PageData ExtractSensitivityRunHeader(string[] deltaHeader)
        {
            Run.SecondLine = false;
            switch (ProjectFile.OutputVersion)
            {
                case OutputVersion.V13: return V13.SensitivityRunHeader(deltaHeader);
                case OutputVersion.V15: return V15.SensitivityRunHeader(deltaHeader);
                case OutputVersion.Cus: return Cus.SensitivityRunHeader(deltaHeader);
                case OutputVersion.Maize: return Maize.SensitivityRunHeader(deltaHeader);
                default: throw new NotImplementedException();
            }
        }

        public static LineData ExtractSensitivityRunLine(Run runOld, string[] deltaHeader, double[] deltaValue)
        {
            switch (ProjectFile.OutputVersion)
            {
                ///<Behnam>
                case OutputVersion.V13: return V13.SensitivityRunLine(runOld, deltaHeader, deltaValue);
                case OutputVersion.V15: return V15.SensitivityRunLine(runOld, deltaHeader, deltaValue);
                case OutputVersion.Cus: return Cus.SensitivityRunLine(runOld, deltaHeader, deltaValue);
                case OutputVersion.Maize: return Maize.SensitivityRunLine(runOld, deltaHeader, deltaValue);
                ///</Behnam>
                default: throw new NotImplementedException();
            }
        }

        public static PageData ExtractWarningsNormalRun(Run runOld)
        {
             WarningFileExtractor Warnings = new WarningFileExtractor();
             return Warnings.ExtractWarningsNormalRun(runOld);
            
        }

        public static PageData ExtractWarningsHeader()
        {
            WarningFileExtractor Warnings = new WarningFileExtractor();
            PageData page = Warnings.PrintMultiRunHeader();
            return page;
        }

        public static LineData ExtractWarningsMultiRun(Run run)
        {
            WarningFileExtractor Warnings = new WarningFileExtractor();

            return Warnings.MultiRunLine(run);

        }


    }
}