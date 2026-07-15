using System;
using System.Threading;
using System.Threading.Tasks;
using StaticHtmlServer.Domains.Files;
using StaticHtmlServer.Domains.MimeTypes;
using StaticHtmlServer.Domains.Paths;
using StaticHtmlServer.Infrastructure;

//Instantiate Domain Services ---
// We create our low-level workers first.
IPathSanitizer sanitizer = new PathSanitizer();
IFileProvider fileProvider = new FileProvider();
IMimeTypeProvider mimeTypeProvider = new MimeTypeProvider();

// Assemble Infrastructure ---
// We feed our domain services into the pipeline coordinator.
IRequestPipeline pipeline = new StaticFilePipeline(
    fileProvider, 
    mimeTypeProvider, 
    sanitizer
);

// We pass our pipeline and chosen port to our server.
int port = 8080;
StaticFileServer server = new StaticFileServer(port, pipeline);

//Setup Graceful Shutdown & Start ---
using CancellationTokenSource cts = new CancellationTokenSource();

// Bind terminal exit events (like pressing Ctrl+C) to cancel our token
Console.CancelKeyPress += (sender, eventArgs) =>
{
    Console.WriteLine("\n[Server] Shutting down gracefully...");
    eventArgs.Cancel = true; // Prevents the process from terminating instantly
    cts.Cancel();            // Signals our server's while-loop to exit
};

try
{
    // Start the socket listener
    await server.StartAsync(cts.Token);
}
catch (OperationCanceledException)
{
    Console.WriteLine("[Server] Stopped safely. Goodbye!");
}
catch (Exception ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"[Server] Critical Error: {ex.Message}");
    Console.ResetColor();
}