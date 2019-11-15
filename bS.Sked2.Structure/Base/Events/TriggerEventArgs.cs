using System;

namespace bS.Sked2.Structure.Engine.Events
{
    public class TriggerEventArgs : EventArgs
    {
        public TriggerEventArgs(string name, DateTime lastExecutionTime, Guid? instanceID)
        {
            Name = name;
            LastExecutionTime = lastExecutionTime;
            InstanceID = instanceID;
            FiredTime = DateTime.Now;
        }
        public TriggerEventArgs(IEngineTrigger currentTrigger)
        {
            Name = currentTrigger.Name;
            LastExecutionTime = currentTrigger.LastExecutionTime;
            InstanceID = currentTrigger.InstanceID;
            FiredTime = DateTime.Now;
        }

        public string Name { get; set; }
        public DateTime LastExecutionTime { get; set; }
        public DateTime FiredTime { get; set; }
        public Guid? InstanceID { get; set; }
    }
}
