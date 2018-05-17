using System;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using SiriusModel.InOut.Base;
using System.Collections.Generic;
using System.ComponentModel;

namespace SiriusModel.InOut
{
    [Serializable]
    public partial class ProjectFile : ProjectItem
    {
        ///<Behnam>
        public static List<string> FileNameList;
        public static OutputVersion OutputVersion;
        ///</Behnam>

        private readonly FileContainer[] fileContainerArray;

        public static volatile bool IsBindingSuspended;

        public static readonly string Version = typeof(ProjectFile).Assembly.GetName().Version.ToString(2);

        public static readonly string Build = typeof(ProjectFile).Assembly.GetName().Version.ToString(4);

        #region singleton design

        private static volatile ProjectFile[] Instances;

        static ProjectFile()
        {
            Instances = new ProjectFile[1] { new ProjectFile() };
        }

        public static ProjectFile This
        {
            get { return Instances[0]; }
        }

        public static ProjectFile[] ThisArray
        {
            get { return Instances; }
        }

        public ProjectFile()
        {
            fileContainerArray = new FileContainer[1];
            fileContainerArray[0] = new FileContainer();
            directory = "?";
            fileName = "?";
        }

        #endregion

        #region IProjectItem

        public override bool NotifyPropertyChanged(String info)
        {
            if (base.NotifyPropertyChanged(info))
            {
                if (info == "Path")
                {
                    foreach (var inputFileID in FileContainer.AllID)
                    {
                        fileContainerArray[0][inputFileID].NotifyPropertyChanged("AbsoluteFileName");
                    }
                }
                return true;
            }
            return false;
        }

        #endregion

        #region new load save

        public static void New()
        {
            partialNew();
        }

        static partial void partialNew();

        public static void Load(string fileName)
        {
            New();
            var tempFile = (ProjectFile)Serialization.DeserializeXml(typeof(ProjectFile), fileName);
            tempFile.ClearWarnings();
            This.Path = fileName;

            ///<Behnam>
            FileNameList = new List<string>();
            foreach (var id in new FileContainer().AllID)
            {
                This.FileContainer[id].AbsoluteFileName = This.GetAbsoluteFileName(tempFile.FileContainer[id].RelativeFileName);
                FileNameList.Add(This.FileContainer[id].AbsoluteFileName);
            }

            This.FileContainer.Comments = tempFile.FileContainer.Comments;
            // This.FileContainer.OutputVersion = tempFile.FileContainer.OutputVersion;
            This.FileContainer.LoadAll();
            This.FileContainer.IsModified = false;
        }
        ///</Behnam>

        public static void Save(string fileName)
        {
            var lastPath = This.Path;
            try
            {
                This.Path = fileName;

                Serialization.SerializeXml(This, This.Path);
                This.FileContainer.IsModified = false;
            }
            catch
            {
                This.Path = lastPath;
                throw;
            }
        }

        #endregion

        [XmlElement(ElementName = "Inputs")]
        public FileContainer FileContainer
        {
            get { return fileContainerArray[0]; }
            set
            {
                var fileContainer = FileContainer;

                foreach (var id in FileContainer.AllID)
                    fileContainer[id].AbsoluteFileName = value[id].RelativeFileName;

                fileContainer.Comments = value.Comments;
                // fileContainer.OutputVersion = value.OutputVersion;
            }
        }

        public FileContainer[] FileContainerArray
        {
            get { return fileContainerArray; }
        }

        #region path

        protected string directory;
        protected string fileName;

        [XmlIgnore]
        public string Directory
        {
            get { return directory; }
            set { if (this.SetObject(ref directory, ref value, "Directory")) FileContainer.IsModified = true; }
        }

        [XmlIgnore]
        public string FileName
        {
            get { return fileName; }
            set
            {
                if (this.SetObject(ref fileName, ref value, "FileName"))
                {
                    FileContainer.IsModified = true;
                    NotifyPropertyChanged("ProjectFileTitle");
                }
            }
        }

        //[XmlIgnore]
        //public OutputVersion OutputVersion
        //{
            //get { return FileContainer.OutputVersion; }
            //set
            //{
                //this.SetStruct(FileContainer, "OutputVersion", ref value, "OutputVersion");
            //}
        //}

        [XmlIgnore]
        public string ProjectFileTitle { get { return FileName + " - SiriusQuality " + Version; } }

        [XmlIgnore]
        public string Path
        {
            get { return Directory + "\\" + FileName; }
            set
            {
                if (value != "?\\?")
                {
                    var oldPath = Path;
                    try
                    {
                        Directory = System.IO.Path.GetDirectoryName(value);
                        FileName = System.IO.Path.GetFileName(value);

                        var newPath = Path;
                        this.SetObject(ref oldPath, ref newPath, "Path");
                    }
                    catch
                    {
                        if (oldPath == "?\\?" || File.Exists(oldPath))
                        {
                            Path = oldPath;
                        }
                        else Path = "?\\?";
                        throw;
                    }
                }
                else if (Path != "?\\?")
                {
                    Directory = "?";
                    FileName = "?";
                    NotifyPropertyChanged("Path");
                }
            }
        }

        public string GetAbsoluteFileName(string relativeFileName)
        {
            return FileHelper.GetAbsolute(Path, relativeFileName);
        }

        public string GetRelativeFileName(string absoluteFileName)
        {
            return FileHelper.GetRelative(Path, absoluteFileName);
        }

        public static string FileExtension
        {
            get { return ".sqpro"; }
        }

        #endregion

        public override void CheckWarnings()
        {

        }
    }

    public partial class ProjectFile
    {
        //static partial void partialNew()
        //{
        //    This.Path = "?\\?";
        //    This.FileContainer.ManagementFileName = "?";
        //    This.FileContainer.NonVarietyFileName = "?";
        //    This.FileContainer.RunOptionFileName = "?";
        //    This.FileContainer.RunFileName = "?";
        //    This.FileContainer.SiteFileName = "?";
        //    This.FileContainer.SoilFileName = "?";
        //    This.FileContainer.VarietyFileName = "?";

        //    This.FileContainer.Comments = "";
        //    This.FileContainer.OutputVersion = OutputVersion.V15;
        //    This.FileContainer.IsModified = false;
        //}
    }
}
