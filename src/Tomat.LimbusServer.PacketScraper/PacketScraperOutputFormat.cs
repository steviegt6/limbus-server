using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cpp2IL.Core.Api;
using Cpp2IL.Core.Logging;
using Cpp2IL.Core.Model.Contexts;

namespace Tomat.LimbusServer.PacketScraper;

public class PacketScraperOutputFormat : Cpp2IlOutputFormat {
    // private readonly Dictionary<>

    public override string OutputFormatId => "packet-scraper";

    public override string OutputFormatName => "Packet Scraper (Limbus Company only)";

    public override void DoOutput(ApplicationAnalysisContext context, string outputRoot) {
        // Initialize custom attributes.
        foreach (var asm in context.Assemblies) {
            asm.AnalyzeCustomAttributeData();

            foreach (var type in asm.Types) {
                type.AnalyzeCustomAttributeData();

                foreach (var method in type.Methods)
                    method.AnalyzeCustomAttributeData();

                foreach (var field in type.Fields)
                    field.AnalyzeCustomAttributeData();

                foreach (var property in type.Properties)
                    property.AnalyzeCustomAttributeData();
            }
        }

        var assemblyCSharp = context.Assemblies.Single(asm => asm.CleanAssemblyName == "Assembly-CSharp");
        var packetTypes = assemblyCSharp.Types.Where(x => x.BaseType?.FullName.StartsWith("Server.HttpRequestCommand`2") ?? false).ToList();

        Logger.InfoNewline($"Resolved {packetTypes.Count} packet types.");

        foreach (var packetType in packetTypes) {
            Logger.InfoNewline("Resolved packet type: " + packetType.FullName);
            Logger.InfoNewline("{");

            if (packetType.BaseType is not GenericInstanceTypeAnalysisContext genericInstance)
                throw new Exception("Packet base type is not generic instance.");

            var genericInstances = (List<TypeAnalysisContext>)typeof(ReferencedTypeAnalysisContext).GetProperty("GenericArguments", BindingFlags.Instance | BindingFlags.NonPublic)!.GetValue(genericInstance)!;
            var reqPacketType = genericInstances[0];
            var resPacketType = genericInstances[1];

            Logger.InfoNewline("    Request packet type: " + reqPacketType.FullName);
            Logger.InfoNewline("    {");
            LogSerializableFields(reqPacketType, 3);
            Logger.InfoNewline("    }");
            Logger.InfoNewline("    Response packet type: " + resPacketType.FullName);
            Logger.InfoNewline("    {");
            LogSerializableFields(resPacketType, 3);
            Logger.InfoNewline("    }");
        }

        Logger.InfoNewline("}");
    }

    private void LogSerializableFields(TypeAnalysisContext type, int indentLevel) {
        var indent = new string(' ', indentLevel * 4);

        foreach (var field in type.Fields.Where(x => x.CustomAttributes!.Any(y => y.Constructor.DeclaringType!.Name == "SerializeField"))) {
            Logger.InfoNewline(indent + field.Name + " (" + field.FieldTypeContext.FullName + ")");

            // if (field.FieldTypeContext.DeclaringAssembly.CleanAssemblyName == "mscorlib")
            //     continue;

            if (field.FieldTypeContext.Namespace.StartsWith("System"))
                continue;

            Logger.InfoNewline(indent + "{");
            LogSerializableFields(field.FieldTypeContext, indentLevel + 1);
            Logger.InfoNewline(indent + "}");
        }
    }
}
