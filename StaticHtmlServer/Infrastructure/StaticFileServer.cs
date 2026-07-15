using System.ComponentModel;
using System.Net;
using System.Net.Sockets;

namespace StaticHtmlServer.Infrastructure
{
    public class StaticFileServer
    {
        private readonly int _port;
        private readonly IPAddress _ipaddress;
        private readonly IRequestPipeline _pipeline;
        private TcpListener? _listener;
        //constructore
        public StaticFileServer(int port, IRequestPipeline pipeline)
        {
            _port = port;
            _ipaddress = IPAddress.Any; // listen to all local network adapters
            _pipeline = pipeline;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // intialize tcp listener
            _listener = new TcpListener(IPAddress.Any, _port);
            _listener.Start();
            // main loop to accept incoming client connections
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    // wait asynchronously for an incoming connection
                    TcpClient client = await _listener.AcceptTcpClientAsync(cancellationToken);
                    _ = HandleClientConnectionAsync(client, cancellationToken);

                }
            }
            finally
            {
                _listener.Stop();
            }

        }
        public async Task HandleClientConnectionAsync(TcpClient client, CancellationToken cancellationToken)
        {
            using(client)
            using(var stream = client.GetStream())
            {
                // read the raw bytes representing the http request from the socket stream
                byte[] buffer = new byte[4096];
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken);
                if(bytesRead == 0) return; // client disconnected immediately
                string requestedText = System.Text.Encoding.UTF8.GetString(buffer, 0, bytesRead);
                // extract and execute
                string rawPath = ExtractPath(requestedText);
                HttpResponse response = await _pipeline.ExecuteAsync(rawPath);

            }
        }
        private string ExtractPath(string fullPath)
        {
            // split the string by carriage return
            string[] lines = fullPath.Split(
                new[] {"\r\n", "\r", "\n" },
                StringSplitOptions.RemoveEmptyEntries
            );
            //ensure method recieve text lines to pars
            if(lines.Length == 0)
            {
                throw new ArgumentException("Request payload is empty.");
            }
            //take the first line
            string requestLine = lines[0];
            // split requestLine by space
            string[] splittedFirstLine = requestLine.Split(' ');
            // verify split has 3 parts to ensure its a valid http request
            if(splittedFirstLine.Length < 3)
            {
                throw new ArgumentException("path must be a valid http request");

            }
            // return index 1, which is the raw target path
            return splittedFirstLine[1];

        }
    }
}