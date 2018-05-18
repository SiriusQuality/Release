using System;
using System.Collections.Generic;
using System.Linq;
using SiriusModel.InOut;

namespace SiriusView.File
{
    public class ProjectDataFileFormFactory
    {
        private readonly Dictionary<string, ProjectDataFileForm> forms;

        #region singleton design

        private static readonly ProjectDataFileFormFactory instance = new ProjectDataFileFormFactory();

        public static ProjectDataFileFormFactory This
        {
            get { return instance; }
        }

        private ProjectDataFileFormFactory()
        {
            forms = new Dictionary<string, ProjectDataFileForm>();
        }

        #endregion

        public bool Contains(string id)
        {
            return forms.ContainsKey(id);
        }

        public ProjectDataFileForm[] Forms
        {
            get { return forms.Values.ToArray(); }
        }
        
        public ProjectDataFileForm this[string id]
        {
            get
            {
                if (forms.ContainsKey(id))
                {
                    return forms[id];
                }
                else
                {
                    if (id == FileContainer.ManagementID)
                    {
                        var mf = new ManagementForm();
                        forms[id] = mf;
                        return mf;
                    }
                    else if (id == FileContainer.NonVarietyID)
                    {
                        var pf = new NonVarietyForm();
                        forms[id] = pf;
                        return pf;
                    }
                    else if (id == FileContainer.RunOptionID)
                    {
                        var rf = new RunOptionForm();
                        forms[id] = rf;
                        return rf;
                    }
                    else if (id == FileContainer.SiteID)
                    {
                        var sf = new SiteForm();
                        forms[id] = sf;
                        return sf;
                    }
                    else if (id == FileContainer.SoilID)
                    {
                        var sf = new SoilForm();
                        forms[id] = sf;
                        return sf;
                    }
                    else if (id == FileContainer.VarietyID)
                    {
                        var vf = new VarietyForm();
                        forms[id] = vf;
                        return vf;
                    }
                    else if (id == FileContainer.RunID)
                    {
                        var srf = new RunForm();
                        forms[id] = srf;
                        return srf;
                    }
                    else if (id == FileContainer.OptimizationID)
                    {
                        var opf = new OptimizationForm();
                        forms[id] = opf;
                        return opf;
                    }
                    else if (id == FileContainer.ObservationID)
                    {
                        var obs = new ObservationForm();
                        forms[id] = obs;
                        return obs;
                    }
                }

                throw new Exception("Unknown file ID : " + id);
            }
        }

        internal void Close(ProjectDataFileForm inputForm)
        {
            forms.Remove(inputForm.InputFileID);
        }

        internal void CloseAll()
        {
            var formsArray = forms.Values.ToArray();
            foreach (var projectDataFileForm in formsArray)
            {
                projectDataFileForm.Close();
            }
            forms.Clear();
        }
    }
}
