using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using MovieStreaming.Actors;
using Akka.Configuration;

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

            Console.ReadLine();

            MovieStreamingActorSystem.Terminate();
        }
    }
}
