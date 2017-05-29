using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Akka.Actor;

namespace CodeFiction.InfinityFiction.Core.ResourceManager.Messages
{
    public class FilePathMessage
    {
        public string FilePath { get; }

        public IActorRef ClientActor { get; }

        public FilePathMessage(string filePath, IActorRef clientActor)
        {
            FilePath = filePath;
            ClientActor = clientActor;
        }
    }
}
