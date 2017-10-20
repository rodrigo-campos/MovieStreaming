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

            Props userActorProps = Props.Create<UserActor>();
            IActorRef userActorRef = MovieStreamingActorSystem.ActorOf(userActorProps, nameof(UserActor));

            Console.ReadKey();
            Console.WriteLine("Sending a PlayMovieMessage (Inception)");
            userActorRef.Tell(new PlayMovieMessage("Inception", 99));

            Console.ReadKey();
            Console.WriteLine("Sending another PlayMovieMessage (Matrix)");
            userActorRef.Tell(new PlayMovieMessage("Matrix", 77));

            Console.ReadKey();
            Console.WriteLine("Sending a StopMovieMessage");
            userActorRef.Tell(new StopMovieMessage());

            Console.ReadKey();
            Console.WriteLine("Sending another StopMovieMessage");
            userActorRef.Tell(new StopMovieMessage());

            Console.ReadKey();
            MovieStreamingActorSystem.Terminate().Wait();
            Console.WriteLine("Actor system shutdown");

            Console.ReadKey();
        }
    }
}
