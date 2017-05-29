using System;
using System.Diagnostics;

using Akka.Actor;

using CodeFiction.InfinityFiction.Core.ResourceManager.Actors;
using CodeFiction.InfinityFiction.Core.ResourceManager.Messages;
using CodeFiction.InfinityFiction.Structure.Resources.Models;

namespace ResourceManagerSandbox
{
    class Program
    {
        public static readonly Stopwatch Stopwatch = new Stopwatch();

        static void Main(string[] args)
        {
            string filePath = "D:\\Beamdog Library\\00783\\chitin.key";

            ActorSystem actorSystem = ActorSystem.Create("InfinitySystem");
            
            IActorRef chitinKeyActor = actorSystem.ActorOf(Props.Create<ChitinKeyActor>(), "chitinKeyActor");
            IActorRef consoleActor = actorSystem.ActorOf(Props.Create<ConsoleActor>(), "consoleActor");

            Stopwatch.Start();

            FilePathMessage filePathMessage = new FilePathMessage(filePath, consoleActor);

            chitinKeyActor.Tell(filePathMessage);

            Console.WriteLine("Stop Actor System");

            Console.ReadLine();

            actorSystem.Terminate().Wait();
        }
    }

    public class ConsoleActor : ReceiveActor
    {
        public ConsoleActor()
        {
            Receive<ChitinKeyResource>(
                resource =>
                {
                    Program.Stopwatch.Stop();

                    Console.WriteLine($"Miliseconds : {Program.Stopwatch.ElapsedMilliseconds} - Ticks : {Program.Stopwatch.ElapsedTicks}");
                });
        }
    }
}