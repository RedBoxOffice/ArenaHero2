﻿#if UNITY_EDITOR
using ArenaHero.Utils.StateMachine;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;

namespace Game.TypedScenes.Editor
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

            var loadingParameters = SceneAnalyzer.GetLoadingParameters(scene);
            foreach (var loadingParameter in loadingParameters)
            {
                AddLoadingMethod(targetClass, loadingParameter);
                AddLoadingMethod(targetClass, loadingParameter, asyncLoad: true);
                AddLoadingMethod(targetClass, loadingParameter, isStateLoad: true, machine: typeof(GameStateMachine));
            }

            targetNamespace.Types.Add(targetClass);
            targetUnit.Namespaces.Add(targetNamespace);

            var provider = CodeDomProvider.CreateProvider("CSharp");
            var options = new CodeGeneratorOptions();
            options.BracingStyle = "C";

            var code = new StringWriter();
            provider.GenerateCodeFromCompileUnit(targetUnit, code, options);

            return code.ToString();
        }

        private static void AddConstantValue(CodeTypeDeclaration targetClass, Type type, string name, string value)
        {
            var pathConstant = new CodeMemberField(type, name);
            pathConstant.Attributes = MemberAttributes.Private | MemberAttributes.Const;
            pathConstant.InitExpression = new CodePrimitiveExpression(value);
            targetClass.Members.Add(pathConstant);
        }

        private static void AddLoadingMethod(CodeTypeDeclaration targetClass, Type parameterType = null, bool asyncLoad = false,
                                             bool isStateLoad = false, Type machine = null)
        {
            var loadMethod = new CodeMemberMethod();
            loadMethod.Name = asyncLoad ? "LoadAsync" : "Load";
            loadMethod.Attributes = MemberAttributes.Public | MemberAttributes.Static;

            var loadingStatement = "LoadScene(_sceneName, loadSceneMode";

            void AddParameter(Type type, string argumentName)
            {
                var parameter = new CodeParameterDeclarationExpression(type, argumentName);
                loadMethod.Parameters.Add(parameter);
                loadingStatement += $", {argumentName}";
            }

            if (isStateLoad)
            {
                if (machine != null)
                    AddParameter(machine, nameof(machine));

                var targetTypeParameter = new CodeTypeParameter("TState");

                var tstate = new CodeTypeReference("State");
                tstate.TypeArguments.Add(new CodeTypeReference("GameStateMachine"));

                targetTypeParameter.Constraints.Add(tstate);

                loadMethod.TypeParameters.Add(targetTypeParameter);
            }

            if (parameterType != null)
                AddParameter(parameterType, "argument");

            if (asyncLoad)
            {
                loadMethod.ReturnType = new CodeTypeReference(typeof(AsyncOperation));
                loadingStatement = "return " + loadingStatement;
            }

            loadingStatement += ")";

            var loadingModeParameter = new CodeParameterDeclarationExpression(nameof(LoadSceneMode), "loadSceneMode = LoadSceneMode.Single");
            loadMethod.Parameters.Add(loadingModeParameter);
            loadMethod.Statements.Add(new CodeSnippetExpression(loadingStatement));
            targetClass.Members.Add(loadMethod);
        }
    }
}
#endif
