using ArenaHero.Utils.StateMachine;
using ArenaHero.Utils.TypedScenes.Loader;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using UnityEngine.SceneManagement;

namespace ArenaHero.Utils.TypedScenes.Editor.Loader
{
    public class TypedLoaderGenerator
    {
        public static string Generate(AnalyzableScene scene)
        {
            var sceneName = scene.Name;
            var targetUnit = new CodeCompileUnit();
            var targetNamespace = new CodeNamespace(TypedLoaderSettings.Namespace);
            var targetClass = new CodeTypeDeclaration(sceneName + TypedLoaderSettings.ClassPostName);
            targetNamespace.Imports.Add(new CodeNamespaceImport("ArenaHero.Utils.StateMachine"));
            targetClass.BaseTypes.Add(new CodeTypeReference(TypedLoaderSettings.BaseClass));

            var targetTypeParameter = new CodeTypeParameter("TState");
            var tstate = new CodeTypeReference("State");
            tstate.TypeArguments.Add(new CodeTypeReference(nameof(GameStateMachine)));
            targetTypeParameter.Constraints.Add(tstate);
            targetClass.TypeParameters.Add(targetTypeParameter);

            targetClass.TypeAttributes = System.Reflection.TypeAttributes.Class | System.Reflection.TypeAttributes.Public;

            AddLoadingMethod(targetClass, sceneName);

            targetNamespace.Types.Add(targetClass);
            targetUnit.Namespaces.Add(targetNamespace);

            var provider = CodeDomProvider.CreateProvider("CSharp");
            var options = new CodeGeneratorOptions
            {
                BracingStyle = "C"
            };

            var code = new StringWriter();
            provider.GenerateCodeFromCompileUnit(targetUnit, code, options);

            return code.ToString();
        }

        private static void AddLoadingMethod(CodeTypeDeclaration targetClass, string sceneName)
        {
            var loadMethod = new CodeMemberMethod();
            loadMethod.Name = "Load";
            loadMethod.Attributes = MemberAttributes.Public | MemberAttributes.Static;

            var loadingStatement = sceneName + ".Load<TState, T>(machine, argument)";

            var tParameter = new CodeTypeParameter("T");
            loadMethod.TypeParameters.Add(tParameter);

            var machineParameter = new CodeParameterDeclarationExpression("GameStateMachine", "machine");
            loadMethod.Parameters.Add(machineParameter);

            var argumentParameter = new CodeParameterDeclarationExpression("T", "argument = default");
            loadMethod.Parameters.Add(argumentParameter);

            loadMethod.Statements.Add(new CodeSnippetExpression(loadingStatement));
            targetClass.Members.Add(loadMethod);
        }
    }
}