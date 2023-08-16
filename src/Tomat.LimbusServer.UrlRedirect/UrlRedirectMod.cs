using HarmonyLib;
using Il2CppServer;
using MelonLoader;
using Tomat.LimbusServer.UrlRedirect;

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

    [HarmonyPrefix]
    [HarmonyPatch(typeof(HttpApiRequester), nameof(HttpApiRequester.SendRequest))]
    private static bool PrefixSendRequest(HttpApiRequester __instance, HttpApiSchema httpApiSchema, bool isUrgent) {
        httpApiSchema._url = httpApiSchema._url.Replace("https://www.limbuscompanyapi-2.com", "http://127.0.0.1");
        httpApiSchema._url = httpApiSchema._url.Replace("https://www.limbuscompanyapi.com", "http://127.0.0.1");
        MelonLogger.Msg(httpApiSchema.URL);
        return true;
    }

    public override void OnInitializeMelon() {
        base.OnInitializeMelon();

        HarmonyInstance.PatchAll(typeof(UrlRedirectMod));
    }
}
