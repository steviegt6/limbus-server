using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Tomat.LimbusServer.LogPassthrough;

internal static class Program {
    // This code is utter garbage and full of gross workarounds and hardcoded
    // elements that really shouldn't be hardcoded. That's because this is more
    // so a personal testing tool. If someone wants to rewrite this to not suck,
    // be my guest.

    // private const string hosts_path = @"C:\Windows\System32\drivers\etc\hosts";
    private const string host = "www.limbuscompanyapi.com";
    private const string api = "https://" + host;

    public static async Task Main() {
        //var listener = new TcpListener(IPAddress.Parse(localhost), port);
        using var listener = new HttpListener();
        listener.Prefixes.Add("http://127.0.0.1:80/");
        listener.Prefixes.Add("http://127.0.0.1:443/");
        listener.Prefixes.Add("http://127.0.0.1:8080/");
        listener.Start();

        Console.WriteLine("Listening...");

        while (true) {
            var context = await listener.GetContextAsync();
            await ProcessRequest(context);
        }

        /*while (true) {
            using var client = listener.AcceptTcpClient();
            HandleClientRequest(client);
        }*/

        /*while (true) {
            // AddHostsEntry("127.0.0.1", api);

            // Log request.
            var context = listener.GetContext();
            LogRequest(context.Request);

            // Forward request to actual API.
            // RemoveHostsEntry("127.0.0.1", api);
            var request = new HttpRequestMessage(new HttpMethod(context.Request.HttpMethod), api + context.Request.Url?.PathAndQuery);
            request.Method = new HttpMethod(context.Request.HttpMethod);

            foreach (var header in context.Request.Headers.AllKeys) {
                if (header is not null && !WebHeaderCollection.IsRestricted(header))
                    request.Headers.Add(header, context.Request.Headers[header]);
            }

            request.Headers.Host = host;
            request.Content = new StreamContent(context.Request.InputStream);
            var response = client.SendAsync(request).Result;

            // Log response.
            LogResponse(response);

            // Forward response to client.
            foreach (var header in response.Headers)
                context.Response.Headers.Add(header.Key, string.Join(", ", header.Value));
            context.Response.StatusCode = (int)response.StatusCode;
            context.Response.ContentLength64 = response.Content.Headers.ContentLength ?? 0;
            context.Response.ContentType = response.Content.Headers.ContentType?.ToString() ?? "text/plain";
            response.Content.CopyToAsync(context.Response.OutputStream).Wait();
            context.Response.Close();
        }*/
    }

    private static async Task ProcessRequest(HttpListenerContext context) {
        await LogRequest(context.Request);

        using var client = new HttpClient();
        var request = new HttpRequestMessage(new HttpMethod(context.Request.HttpMethod), api + context.Request.Url?.PathAndQuery);
        request.Content = new StreamContent(context.Request.InputStream);

        foreach (var header in context.Request.Headers.AllKeys) {
            if (header is not null)
                request.Headers.TryAddWithoutValidation(header, context.Request.Headers[header]);
        }

        request.Content.Headers.ContentLength = context.Request.ContentLength64;
        request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(context.Request.ContentType!);
        request.Headers.Host = host;
        request.Version = context.Request.ProtocolVersion;

        // log content
        var content = await request.Content.ReadAsStringAsync();
        Console.WriteLine("          " + content);

        // log headers
        /*foreach (var header in context.Request.Headers.AllKeys)
            Console.WriteLine($"{header}: {string.Join(", ", context.Request.Headers[header])}");
        foreach (var header in request.Headers)
            Console.WriteLine($"{header.Key}: {string.Join(", ", header.Value)}");*/

        try {
            var response = await client.SendAsync(request);
            await LogResponse(response);
            context.Response.StatusCode = (int)response.StatusCode;

            foreach (var header in response.Headers)
                context.Response.Headers.Add(header.Key, string.Join(", ", header.Value));

            // log headers
            /*foreach (var header in response.Headers)
                Console.WriteLine($"{header.Key}: {string.Join(", ", header.Value)}");*/

            await using var responseStream = await response.Content.ReadAsStreamAsync();
            await responseStream.CopyToAsync(context.Response.OutputStream);
        }
        catch (HttpRequestException e) {
            context.Response.StatusCode = 500;
            var bytes = Encoding.UTF8.GetBytes(e.Message);
            await context.Response.OutputStream.WriteAsync(bytes);
        }
        finally {
            context.Response.OutputStream.Close();
            context.Response.Close();
        }
    }

    /*private static void AddHostsEntry(string ip, string domain) {
        var hosts = File.ReadAllLines(hosts_path);
        var hostsList = hosts.ToList();
        if (hostsList.Contains($"{ip} {domain}"))
            return;

        hostsList.Add($"{ip} {domain}");
        File.WriteAllLines(hosts_path, hostsList);
    }

    private static void RemoveHostsEntry(string ip, string domain) {
        var hosts = File.ReadAllLines(hosts_path);
        var hostsList = hosts.ToList();
        if (hostsList.All(x => x != $"{ip} {domain}"))
            return;

        hostsList.Remove($"{ip} {domain}");
        File.WriteAllLines(hosts_path, hostsList);
    }*/

    private const int max_method_length = 8; // PROPFIND

    private static void WriteMethod(string method) {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write(method.PadLeft(max_method_length));
        Console.ResetColor();
    }

    private static void WriteStatusCode(HttpStatusCode statusCode) {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write($"<- {(int)statusCode}");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write($" ({statusCode})");
        Console.ResetColor();
    }

    private static Task LogRequest(HttpListenerRequest request) {
        Console.Write(' ');
        WriteMethod(request.HttpMethod);
        Console.Write(' ');
        //Console.WriteLine(request.Url);
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write(request.Url?.Host);
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write(request.Url?.AbsolutePath);
        Console.ResetColor();
        Console.WriteLine();
        return Task.CompletedTask;
    }

    private static Task LogResponse(HttpResponseMessage response) {
        Console.Write(' ');
        WriteMethod("");
        Console.Write(' ');
        WriteStatusCode(response.StatusCode);
        Console.Write(' ');
        Console.Write(response.Content.Headers.ContentType?.MediaType);
        Console.Write(" - ");
        Console.Write(response.Content.Headers.ContentLength + " bytes");

        if (response.Content.Headers.ContentType?.MediaType == "application/json") {
            var json = response.Content.ReadAsStringAsync().Result;
            var lines = json.Split('\n');
            Console.WriteLine();

            foreach (var line in lines) {
                WriteMethod("          ");
                Console.WriteLine(line);
            }
        }

        // Console.Write(" - ");
        // if (response.Content.Headers.ContentType?.MediaType == "text/json")
        Console.WriteLine();

        Console.WriteLine();
        return Task.CompletedTask;
    }
}
