using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using MovieStreaming.Actors;
using Akka.Configuration;
using MovieStreaming.Messages;

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

            MovieStreamingActorSystem = ActorSystem.Create(nameof(MovieStreamingActorSystem), config);
            Console.WriteLine("Actor system created");

            Props playbackActorProps = Props.Create<PlaybackActor>();
            IActorRef playbackActorRef = MovieStreamingActorSystem.ActorOf(playbackActorProps, "PlaybackActor");

            playbackActorRef.Tell(new PlayMovieMessage("Akka.NET: The Movie", 42));
            playbackActorRef.Tell(new PlayMovieMessage("Inception", 99));
            playbackActorRef.Tell(new PlayMovieMessage("Matrix", 77));
            playbackActorRef.Tell(new PlayMovieMessage("Titanic", 1));
            playbackActorRef.Tell(PoisonPill.Instance);

            Console.ReadKey();

            MovieStreamingActorSystem.Terminate().Wait();
            Console.WriteLine("Actor system shutdown");

            Console.ReadKey();
        }
    }
}
