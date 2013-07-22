using CodeFiction.DarkMatterFramework.Libraries.IOLibrary;
using CodeFiction.InfinityFiction.Core.ResourceBuilderContracts;
using CodeFiction.InfinityFiction.Core.Resources.Dlg;
using CodeFiction.InfinityFiction.Structure.StructConverterContracts;
using CodeFiction.InfinityFiction.Structure.Structures.Dlg;

namespace CodeFiction.InfinityFiction.Core.ResourceBuilder
{
    public class DlgResourceBuilder : IDlgResourceBuilder
    {
        private readonly IGenericStructConverter _genericStructConverter;

        private readonly IResourceConverter _resourceConverter;

        public DlgResourceBuilder(IGenericStructConverter genericStructConverter, IResourceConverter resourceConverter)
        {
            _genericStructConverter = genericStructConverter;
            _resourceConverter = resourceConverter;
        }

        public DlgResource GetDlgResource(string dlgfilePath)
        {
            DlgResource dlgResource = new DlgResource();
            byte[] content = IoHelper.ReadBinaryFile(dlgfilePath);

            var header = _genericStructConverter.ConvertToStruct<Header>(content, 0);
            dlgResource.Path = dlgfilePath;
            dlgResource.Content = content;

            // uint numberOfStates = header.NumberOfStates;
            // uint numberOfTransitions = header.NumberOfTransitions;
            // uint numberStateTriggers = header.NumberStateTriggers;
            // uint numberTransitionTriggers = header.NumberTransitionTriggers;
            // uint numberActions = header.NumberActions;

            // uint statesOffset = header.StatesOffset;
            // uint transitionsOffset = header.TransitionsOffset;
            // uint stateTriggerOffset = header.StateTriggerOffset;
            // uint transitionTriggerOffset = header.TransitionTriggerOffset;
            // uint actionTableOffset = header.ActionTableOffset;

            // dlgResource.Header = new HeaderResource();
            // dlgResource.StateTableResources = new StateTableResource[numberOfStates];
            // dlgResource.TransitionTableResources = new TransitionTableResource[numberOfTransitions];
            // dlgResource.StateTriggerResources = new StateTriggerResource[numberStateTriggers];
            // dlgResource.TransitionTriggerResources = new TransitionTriggerResource[numberTransitionTriggers];
            // dlgResource.ActionTableResources = new ActionTableResource[numberActions];

            // const uint SizeOfStateTable = 16;
            // const uint SizeOfTransitionTable = 32;
            // uint sizeOfStateTrigger;
            // uint sizeOfTransitionTrigger;
            // uint sizeOfActionTable;
            // unsafe
            // {
            // sizeOfStateTrigger = (uint)sizeof(StateTrigger);
            // sizeOfTransitionTrigger = (uint)sizeof(TransitionTrigger);
            // sizeOfActionTable = (uint)sizeof(ActionTable);
            // }

            // _resourceConverter.Convert(header, dlgResource.Header);
            // _resourceConverter.Convert<StateTable, StateTableResource>(content, dlgResource.StateTableResources, statesOffset, SizeOfStateTable);
            // _resourceConverter.Convert<TransitionTable, TransitionTableResource>(content, dlgResource.TransitionTableResources, transitionsOffset, SizeOfTransitionTable);
            // _resourceConverter.Convert<StateTrigger, StateTriggerResource>(content, dlgResource.StateTriggerResources, stateTriggerOffset, sizeOfStateTrigger);
            // _resourceConverter.Convert<TransitionTrigger, TransitionTriggerResource>(content, dlgResource.TransitionTriggerResources, transitionTriggerOffset, sizeOfTransitionTrigger);
            // _resourceConverter.Convert<ActionTable, ActionTableResource>(content, dlgResource.ActionTableResources, actionTableOffset, sizeOfActionTable);

            return dlgResource;
        }
    }
}
