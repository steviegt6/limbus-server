using System;
using Cpp2IL.Core.Api;

namespace Tomat.LimbusServer.PacketScraper;

internal static class Program {
    internal static void Main(string[] args) {
        if (args.Length != 1) {
            Console.WriteLine("Usage: Tomat.LimbusServer.PacketScraper.exe <game folder>");
            return;
        }

        // Easier to use the extra utilities provided by Cpp2IL rather than
        // reimplement it on top of LibCpp2IL.
        var cpp2ILAsm = typeof(Cpp2IL.SoftException).Assembly;
        var cpp2ILProgram = cpp2ILAsm.GetType("Cpp2IL.Program");
        var cpp2ILProgramMain = cpp2ILProgram!.GetMethod("Main", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
        var gameFolder = args[0];

        OutputFormatRegistry.Register<PacketScraperOutputFormat>();
        cpp2ILProgramMain?.Invoke(null, new object?[] { new[] { "--game-path", gameFolder, "--output-as", "packet-scraper" } });
    }
}
