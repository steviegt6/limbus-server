using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tomat.LimbusServer.Api.Models;
using Tomat.LimbusServer.Api.Models.Packets;

namespace Tomat.LimbusServer.GoofySillyTesting;

internal static class Program {
    // This code is utter garbage and full of gross workarounds and hardcoded
    // elements that really shouldn't be hardcoded. That's because this is more
    // so a personal testing tool. If someone wants to rewrite this to not suck,
    // be my guest.

    private const string host = "www.limbuscompanyapi.com";
    private const string api = "https://" + host;

    public static async Task Main() {
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

        var content = await request.Content.ReadAsStringAsync();
        Console.WriteLine("          " + content);

        try {
            var response = await client.SendAsync(request);
            await LogResponse(response);
            context.Response.StatusCode = (int)response.StatusCode;

            foreach (var header in response.Headers)
                context.Response.Headers.Add(header.Key, string.Join(", ", header.Value));

            await using var responseStream = await response.Content.ReadAsStreamAsync();
            await using var manipulatedStream = ManipulateStream(responseStream, context.Request.Url?.AbsolutePath);
            await manipulatedStream.CopyToAsync(context.Response.OutputStream);
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

    private static Stream ManipulateStream(Stream stream, string? path) {
        using var reader = new StreamReader(stream);
        var json = reader.ReadToEnd();

        switch (path) {
            case "/log/GetMailLogAll":
                var model = JsonConvert.DeserializeObject<ResponseWrapper<GetMailLogAllResultPacket>>(json);
                var mail = model!.Result!.MailLogs[0];
                var origCount = mail.Attachments.Count;

                for (var i = 0; i < 10; i++) {
                    mail.ContentId = origCount + i;
                    model!.Result!.MailLogs.Add(mail);
                }

                json = JsonConvert.SerializeObject(model);
                break;
        }

        var bytes = Encoding.UTF8.GetBytes(json);
        return new MemoryStream(bytes);
    }

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

        Console.WriteLine();

        Console.WriteLine();
        return Task.CompletedTask;
    }
}
