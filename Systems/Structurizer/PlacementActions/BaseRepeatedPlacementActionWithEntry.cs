﻿namespace EEMod.Systems.Structurizer.PlacementActions
{
    public abstract class BaseRepeatedPlacementActionWithEntry<TPlacementAction> : BaseRepeatedPlacementAction<TPlacementAction> 
        where TPlacementAction : IPlacementAction
    {
        public ushort EntryData { get; }

        protected BaseRepeatedPlacementActionWithEntry(byte repetitionCount, ushort entryData) : base(repetitionCount)
        {
            EntryData = entryData;
        }
    }
}