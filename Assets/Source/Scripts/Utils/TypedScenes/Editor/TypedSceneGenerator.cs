#if UNITY_EDITOR
using ArenaHero.Utils.StateMachine;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;

namespace ArenaHero.Utils.TypedScenes.Editor
{
    public static class TypedSceneGenerator
    {
        public static string Generate(AnalyzableScene scene)
        {
            var sceneName = scene.Name;
            var targetUnit = new CodeCompileUnit();
            var targetNamespace = new CodeNamespace(TypedSceneSettings.Namespace);
            var targetClass = new CodeTypeDeclaration(sceneName);
            targetNamespace.Imports.Add(new CodeNamespaceImport("UnityEngine.SceneManagement"));
            targetNamespace.Imports.Add(new CodeNamespaceImport("ArenaHero.Utils.StateMachine"));
            targetClass.BaseTypes.Add(new CodeTypeReference("TypedScene",
                                        new CodeTypeReference[]
                                        { 
                                            new CodeTypeReference(typeof(GameStateMachine))
                                        }));

            targetClass.TypeAttributes = System.Reflection.TypeAttributes.Class | System.Reflection.TypeAttributes.Public;

            AddConstantValue(targetClass, typeof(string), "_sceneName", sceneName);

            AddLoadingMethod(targetClass, machine: typeof(GameStateMachine));

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

        private static void AddConstantValue(CodeTypeDeclaration targetClass, Type type, string name, string value)
        {
            var pathConstant = new CodeMemberField(type, name)
            {
                Attributes = MemberAttributes.Private | MemberAttributes.Const,
                InitExpression = new CodePrimitiveExpression(value)
            };
            
            targetClass.Members.Add(pathConstant);
        }

        private static void AddLoadingMethod(CodeTypeDeclaration targetClass, bool asyncLoad = false,
                                             Type machine = null)
        {
            var loadMethod = new CodeMemberMethod
            {
                Name = asyncLoad ? "LoadAsync" : "Load",
                Attributes = MemberAttributes.Public | MemberAttributes.Static
            };

            var loadingStatement = "LoadScene<TState, T>(_sceneName, loadSceneMode";

            AddParameter(nameof(GameStateMachine), nameof(machine));
            
            var targetTypeParameter = new CodeTypeParameter("TState");

            var tstate = new CodeTypeReference("State");
            tstate.TypeArguments.Add(new CodeTypeReference(nameof(GameStateMachine)));

            targetTypeParameter.Constraints.Add(tstate);

            loadMethod.TypeParameters.Add(targetTypeParameter);
            loadMethod.TypeParameters.Add(new CodeTypeParameter("T"));

            AddParameter("T", "argument",  " = default");

            loadingStatement += ")";
            
            if (asyncLoad)
            {
                loadMethod.ReturnType = new CodeTypeReference(typeof(AsyncOperation));
                loadingStatement = "return " + loadingStatement;
            }

            var loadingModeParameter = new CodeParameterDeclarationExpression(nameof(LoadSceneMode), "loadSceneMode = LoadSceneMode.Single");
            loadMethod.Parameters.Add(loadingModeParameter);
            loadMethod.Statements.Add(new CodeSnippetExpression(loadingStatement));
            targetClass.Members.Add(loadMethod);
            return;
            
            void AddParameter(string type, string argumentName, string defaultArgumentValue = "")
            {
                var parameter = new CodeParameterDeclarationExpression(type, argumentName + defaultArgumentValue);
                loadMethod.Parameters.Add(parameter);
                loadingStatement += $", {argumentName}";
            }
        }
    }
}
#endif
