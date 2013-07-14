using CodeFiction.InfinityFiction.Core.Resources.Dlg;

namespace CodeFiction.InfinityFiction.Core.ResourceBuilderContracts
{
    public interface IDlgResourceBuilder
    {
        DlgResource GetDlgResource(string dlgfilePath);
    }
}