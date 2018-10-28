using Digger.Core.Enums;
using Digger.Core.Exceptions;
using Digger.Core.Handlers;
using Digger.Core.Interfaces;
using Digger.Core.Models;
using Digger.Core.Utils;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Unity;

namespace Digger
{
    class Program
    {
        private static async Task Main(string[] args)
        {

            try
            {
                using (var source = new CancellationTokenSource())
                {
                    var config = ParseArguments(args);

                    var container = UnityConfig.RegisterComponents(config);

                    var actionHandler = container.Resolve<BaseHandler>(config.ActionType.ToString());

                    Console.WriteLine("Digger started working ...");
                    await Task.WhenAll(
                        CancelAsync(source),
                        actionHandler.HandleAsync(source.Token).Then((x) =>
                        {
                            Console.WriteLine("\nDigger finished work. \nLook at results for this path " + config.ResultFilePath);
                        }));
                }
            }
            catch (BadProgramArgumentsException) { Console.WriteLine("If you want me to start digging enter valid arguments, please."); }
            catch (TaskCanceledException) { Console.WriteLine("Oh, I was just beginning to enjoy ..."); }
            catch (IOException) { Console.WriteLine("An error occurred while working with file system."); }
            catch (Exception) { Console.WriteLine("Something bad happened ."); }

            Console.ReadLine();
        }

        private static IDiggerConfiguration ParseArguments(string[] args)
        {
            if (args.Length < 2) throw new BadProgramArgumentsException(nameof(args));

            var config = new DiggerConfiguration
            {
                RootFolder = Directory.Exists(args[0])
                    ? args[0]
                    : throw new DirectoryNotFoundException(),
                ActionType = Enum.TryParse(args[1], true, out DiggerActionType actionType)
                    ? actionType
                    : DiggerActionType.All,
                ResultFilePath = args.Length > 2
                    ? File.Exists(args[2]) ? args[2] : throw new FileNotFoundException()
                    : Constants.DefaultResultPath
            };

            return config;
        }

        private static Task CancelAsync(CancellationTokenSource cancellationTokenSource)
        {
            return Task.Run(() =>
            {
                Console.WriteLine("\nIf you get tired of waiting, while I dig, press key Enter");
                Console.ReadLine();
                cancellationTokenSource.Cancel();
            });
        }
    }
}

