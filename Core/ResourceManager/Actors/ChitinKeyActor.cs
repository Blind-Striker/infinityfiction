using Akka.Actor;

using CodeFiction.InfinityFiction.Core.ResourceManager.Messages;
using CodeFiction.InfinityFiction.Structure.Resources.Models;

namespace CodeFiction.InfinityFiction.Core.ResourceManager.Actors
{
    public class ChitinKeyActor : ReceiveActor
    {
        public ChitinKeyActor()
        {
            ChitinKeyManager chitinKeyManager = new ChitinKeyManager();

            Receive<FilePathMessage>(
                filePath =>
                {
                    ChitinKeyResource chitinKeyResource = chitinKeyManager.GetChitinKey(filePath.FilePath);

                    filePath.ClientActor.Tell(chitinKeyResource);
                });
        }
    }
}
