using System.Collections;
using HarmonyLib;
using Il2CppServer;
using MelonLoader;
using Tomat.LimbusServer.UrlRedirect;
using IEnumerator = Il2CppSystem.Collections.IEnumerator;

// ReSharper disable InconsistentNaming

[assembly: MelonInfo(typeof(UrlRedirectMod), UrlRedirectMod.MOD_NAME, UrlRedirectMod.MOD_VERSION, UrlRedirectMod.MOD_AUTHOR)]
[assembly: MelonGame(UrlRedirectMod.GAME_DEVELOPER, UrlRedirectMod.GAME_NAME)]

namespace Tomat.LimbusServer.UrlRedirect;

public sealed class UrlRedirectMod : MelonMod {
    public const string MOD_NAME = "Server URL Redirector";
    public const string MOD_VERSION = "0.0.0";
    public const string MOD_AUTHOR = "Tomat";

    public const string GAME_DEVELOPER = "ProjectMoon";
    public const string GAME_NAME = "LimbusCompany";

    /*[HarmonyPrefix]
    [HarmonyPatch(typeof(HttpApiSchema), new[] { typeof(string), typeof(string), typeof(DelegateEventString) })]
    private static bool PrefixSendRequest(ref string url, string requestJson, DelegateEventString responseEvent) {
        url = url.Replace("https://www.limbuscompanyapi-2.com", "http://127.0.0.1");
        return false;
    }*/

    [HarmonyPrefix]
    [HarmonyPatch(typeof(HttpApiRequester), nameof(HttpApiRequester.SendRequest))]
    private static bool PrefixSendRequest(HttpApiRequester __instance, HttpApiSchema httpApiSchema, bool isUrgent) {
        httpApiSchema._url = httpApiSchema._url.Replace("https://www.limbuscompanyapi-2.com", "http://127.0.0.1");
        httpApiSchema._url = httpApiSchema._url.Replace("https://www.limbuscompanyapi.com", "http://127.0.0.1");
        MelonLogger.Msg(httpApiSchema.URL);
        return true;
    }
    
    /*[HarmonyPatch(typeof(HttpApiRequester), nameof(HttpApiRequester.PostWebRequest))]
    [HarmonyPrefix]
    private static bool PrefixSendRequest(HttpApiRequester __instance, HttpApiSchema httpApiSchema, bool isUrgent, ref IEnumerator __result) {
        httpApiSchema._url = httpApiSchema._url.Replace("https://www.limbuscompanyapi-2.com", "http://127.0.0.1");
        httpApiSchema._url = httpApiSchema._url.Replace("https://www.limbuscompanyapi.com", "http://127.0.0.1");
        MelonLogger.Msg(httpApiSchema.URL);
        return true;
    }*/

    public override void OnInitializeMelon() {
        base.OnInitializeMelon();

        HarmonyInstance.PatchAll(typeof(UrlRedirectMod));
    }

    /*public override void OnInitializeMelon() {
        base.OnInitializeMelon();

        LoggerInstance.Msg("Init");

        var prefix = typeof(UrlRedirectMod).GetMethod(nameof(PrefixGetApiUrl), BindingFlags.Static | BindingFlags.NonPublic);
        var asm = typeof(HttpRequestCommand<,>).Assembly;
        var types = asm.GetTypes();
        var commands = types.Where(x => (x.BaseType?.IsGenericType ?? false) && x.BaseType.GetGenericTypeDefinition() == typeof(HttpRequestCommand<,>) && !x.IsAbstract).ToList();

        LoggerInstance.Msg("Found " + commands.Count + " commands");

        foreach (var command in commands) {
            LoggerInstance.Msg("Patching: " + command.Name);
            var method = command.GetProperty(nameof(HttpRequestCommand<object, object>.API_URL), BindingFlags.Public | BindingFlags.Instance)?.GetMethod;

            if (method is null) {
                LoggerInstance.Error("Failed to find API_URL property getter for " + command.Name);
                continue;
            }

            HarmonyInstance.Patch(method, new HarmonyMethod(prefix));
        }
    }

    // ReSharper disable once RedundantAssignment
    private static bool PrefixGetApiUrl(dynamic __instance, ref string __result) {
        __result = "http://127.0.0.1/" + __instance._apiClass.ToString() + __instance._apiPath;
        return true;
    }*/
}
