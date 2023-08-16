using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cpp2IL.Core.Api;
using Cpp2IL.Core.Logging;
using Cpp2IL.Core.Model.Contexts;

namespace Tomat.LimbusServer.PacketScraper;

public class PacketScraperOutputFormat : Cpp2IlOutputFormat {
    private readonly Dictionary<string, TypeAnalysisContext> serializedTypes = new();

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

            var genericInstances = GetGenericArgumentsFor(packetType.BaseType!);
            var reqPacketType = genericInstances[0];
            var resPacketType = genericInstances[1];

            // RecordSerializableTypes(reqPacketType);
            // RecordSerializableTypes(resPacketType);

            Logger.InfoNewline("    Request packet type: " + reqPacketType.FullName);
            Logger.InfoNewline("    {");
            LogPacket(reqPacketType, 3);
            Logger.InfoNewline("    }");
            Logger.InfoNewline("    Response packet type: " + resPacketType.FullName);
            Logger.InfoNewline("    {");
            LogPacket(resPacketType, 3);
            Logger.InfoNewline("    }");
        }

        Logger.InfoNewline("}");

        foreach (var type in serializedTypes.Values) {
            Logger.InfoNewline(FormatType(type) + ":");
            Logger.InfoNewline("{");
            LogPacket(type, 1);
            Logger.InfoNewline("}");
        }
    }

    private void LogPacket(TypeAnalysisContext type, int indentLevel) {
        var indent = new string(' ', indentLevel * 4);

        foreach (var field in type.Fields.Where(x => x.Attributes.HasFlag(FieldAttributes.Public) || x.CustomAttributes!.Any(y => y.Constructor.DeclaringType!.Name == "SerializeField"))) {
            RecordSerializableTypes(field.FieldTypeContext);
            Logger.InfoNewline(indent + field.Name + " (" + FormatType(field.FieldTypeContext) + ")");

            // if (field.FieldTypeContext.DeclaringAssembly.CleanAssemblyName == "mscorlib")
            //     continue;

            // if (field.FieldTypeContext.Namespace.StartsWith("System"))
            //    continue;

            // LogFieldWithoutChildFields(field.FieldTypeContext, indentLevel + 1);
        }
    }

    /*private void LogSerializableFields(TypeAnalysisContext type, int indentLevel) {
        var indent = new string(' ', indentLevel * 4);

        foreach (var field in type.Fields.Where(x => x.Attributes.HasFlag(FieldAttributes.Public) || x.CustomAttributes!.Any(y => y.Constructor.DeclaringType!.Name == "SerializeField"))) {
            Logger.InfoNewline(indent + field.Name + " (" + FormatType(field.FieldTypeContext) + ")");

            // if (field.FieldTypeContext.DeclaringAssembly.CleanAssemblyName == "mscorlib")
            //     continue;

            if (field.FieldTypeContext.Namespace.StartsWith("System"))
                continue;

            Logger.InfoNewline(indent + "{");
            LogSerializableFields(field.FieldTypeContext, indentLevel + 1);
            Logger.InfoNewline(indent + "}");
        }
    }*/

    private static string FormatType(TypeAnalysisContext type) {
        switch (type.FullName) {
            case "System.Int32":
                return "int";

            case "System.String":
                return "string";
        }

        if (type.FullName.StartsWith("System.Collections.Generic.List`1")) {
            var generics = GetGenericArgumentsFor(type);
            return FormatType(generics[0]) + "[]";
        }

        return type.FullName;
    }

    private void RecordSerializableTypes(TypeAnalysisContext type) {
        if (type.Namespace.StartsWith("System")) {
            if (type.FullName.StartsWith("System.Collections.Generic.List`1"))
                RecordSerializableTypes(GetGenericArgumentsFor(type)[0]);

            return;
        }

        // if (type.CustomAttributes!.All(x => x.Constructor.DeclaringType!.Name != "SerializableAttribute"))
        //    throw new Exception("Type is not serializable.");

        if (serializedTypes.ContainsKey(type.FullName))
            return;

        foreach (var field in type.Fields.Where(x => x.Attributes.HasFlag(FieldAttributes.Public) || x.CustomAttributes!.Any(y => y.Constructor.DeclaringType!.Name == "SerializeField")))
            RecordSerializableTypes(field.FieldTypeContext);

        serializedTypes.Add(type.FullName, type);
    }

    private static List<TypeAnalysisContext> GetGenericArgumentsFor(TypeAnalysisContext type) {
        if (type is not ReferencedTypeAnalysisContext referencedInstance)
            throw new Exception("Type type is not generic instance.");

        return (List<TypeAnalysisContext>)typeof(ReferencedTypeAnalysisContext).GetProperty("GenericArguments", BindingFlags.Instance | BindingFlags.NonPublic)!.GetValue(referencedInstance)!;
    }
}
