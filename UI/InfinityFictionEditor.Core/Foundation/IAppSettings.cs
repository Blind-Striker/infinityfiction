namespace InfinityFiction.UI.InfinityFictionEditor.Core.Foundation
{
    public interface IAppSettings
    {
        object this[string key] { get; set; }

        void Save();
    }
}