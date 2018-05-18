namespace SiriusModel.InOut.Base
{
    public interface IProjectDataFile : IProjectItem
    {
        string RelativeFileName { get; set; }
        string AbsoluteFileName { get; set; }
        string ID { get; }
        string FileExtension { get; }
        bool IsModified { get;  set; }
        string IsModifiedStr { get; }
        void CopyFrom(IProjectDataFile toCopy);
        bool Contains(string itemName);
    }
}
