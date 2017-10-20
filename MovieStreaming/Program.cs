using Akka.Actor;
using Akka.Configuration;
using MovieStreaming.Actors;
using MovieStreaming.Messages;
using System;
using System.Threading;

namespace MovieStreaming
{
    class Program
    {
        private static ActorSystem MovieStreamingActorSystem;

        static void Main(string[] args)
        {
            var config = ConfigurationFactory.ParseString(@"
    akka {
      actor {
        serializers {
          hyperion = ""Akka.Serialization.HyperionSerializer, Akka.Serialization.Hyperion""
        }
            serialization-bindings {
          ""System.Object"" = hyperion
        }
      }
    }");

            ColorConsole.WriteLineGray("Creating MovieStreamingActorSystem");
            MovieStreamingActorSystem = ActorSystem.Create(nameof(MovieStreamingActorSystem), config);

            ColorConsole.WriteLineGray("Creating actor supervisory hierarchy");
            MovieStreamingActorSystem.ActorOf(Props.Create<PlaybackActor>(), "Playback");

            do
            {
                Wait();

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                ColorConsole.WriteLineGray("enter a command and hit enter");

                var command = Console.ReadLine();

                if (command.StartsWith("play"))
                {
                    int userId = int.Parse(command.Split(',')[1]);
                    string movieTitle = command.Split(',')[2];

                    var message = new PlayMovieMessage(movieTitle, userId);
                    MovieStreamingActorSystem.ActorSelection("/user/Playback/UserCoordinator").Tell(message);
                }
                else if (command.StartsWith("stop"))
                {
                    int userId = int.Parse(command.Split(',')[1]);

                    var message = new StopMovieMessage(userId);
                    MovieStreamingActorSystem.ActorSelection("/user/Playback/UserCoordinator").Tell(message);
                }
                else if (command == "exit")
                {
                    MovieStreamingActorSystem.Terminate().Wait();
                    ColorConsole.WriteLineGray("Actor system shutdown");
                    Console.ReadKey();
                    Environment.Exit(0);
                }

            } while (true);
        }

        private static void Wait()
        {
            Thread.Sleep(1000);
        }
    }
}
