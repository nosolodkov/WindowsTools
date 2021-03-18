using System;

namespace Killer
{
    public class ProcessEventArgs : EventArgs
    {
        public string Name { get; internal set; }

        public int Id { get; internal set; }

        public DateTime TimeKilled { get; internal set; }

        public ProcessEventArgs(string name, int id, DateTime timeKilled)
        {
            Name = name;
            Id = id;
            TimeKilled = timeKilled;
        }

        public override string ToString()
        {
            return $"{TimeKilled} {Name}: id={Id}";
        }
    }
}
