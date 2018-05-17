using System;
using System.IO;
using SiriusModel.InOut.Base;

namespace SiriusModel.InOut.Base
{
    public static class FileHelper
    {
        public static readonly string NotSet = "?";
        public static string GetAbsolute(string referenceFile, string relativeFile)
        {
            if (relativeFile != "?" && relativeFile != "?\\?")
            {
                var absoluteFile = relativeFile;
                if (!String.IsNullOrEmpty(referenceFile)
                    && referenceFile != "?" && referenceFile != "?\\?"
                    && !Path.IsPathRooted(relativeFile))
                {
                    var uri = new Uri(new Uri(referenceFile), relativeFile);
                    absoluteFile = uri.AbsolutePath.Replace("%23", "#").Replace("%20", " ").Replace("/", "\\");
                }
                return absoluteFile;
            }
            return relativeFile;
        }

        public static string GetRelative(string referenceFile, string absoluteFile)
        {
            if (absoluteFile != "?" && absoluteFile != "?\\?")
            {

                var relativeFile = absoluteFile;
                if (!String.IsNullOrEmpty(referenceFile)
                    && referenceFile != "?" && referenceFile != "?\\?"
                    && Path.IsPathRooted(absoluteFile))
                {
                    var uri = new Uri(referenceFile);
                    var realtiveUri = uri.MakeRelativeUri(new Uri(absoluteFile));
                    relativeFile = realtiveUri.OriginalString.Replace("%23", "#").Replace("%20", " ").Replace("/", "\\");
                }
                return relativeFile;
            }
            return absoluteFile;
        }

        public static string GetProjectAbsoluteFileName(this IProjectItem projectFileItem, string relativeFileName)
        {
            var projectFile = ProjectFile.This;
            if (projectFile != null && relativeFileName != "?")
            {
                return projectFile.GetAbsoluteFileName(relativeFileName);
            }
            return relativeFileName;
        }

        public static string GetProjectRelativeFileName(this IProjectItem projectFileItem, string absoluteFileName)
        {
            var projectFile = ProjectFile.This;
            if (projectFile != null && absoluteFileName != "?")
            {
                return projectFile.GetRelativeFileName(absoluteFileName);
            }
            return absoluteFileName;
        }
    }
}
