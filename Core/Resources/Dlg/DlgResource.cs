namespace CodeFiction.InfinityFiction.Core.Resources.Dlg
{
    public class DlgResource
    {
        public string Path { get; set; }

        public byte[] Content { get; set; }

        public HeaderResource Header { get; set; }

        public StateTableResource[] StateTableResources { get; set; }

        public TransitionTableResource[] TransitionTableResources { get; set; }

        public StateTriggerResource[] StateTriggerResources { get; set; }

        public TransitionTriggerResource[] TransitionTriggerResources { get; set; }

        public ActionTableResource[] ActionTableResources { get; set; }
    }
}
