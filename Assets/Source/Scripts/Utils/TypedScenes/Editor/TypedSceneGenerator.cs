#if UNITY_EDITOR
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using ArenaHero.Utils.StateMachine;
using UnityEngine;
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

			targetClass.TypeAttributes = TypeAttributes.Class | TypeAttributes.Public;

			AddConstantValue(targetClass, typeof(string), "_sceneName", sceneName);

			AddLoadingMethod(targetClass, false, false, true);
			AddLoadingMethod(targetClass, true, false, true);
			AddLoadingMethod(targetClass, false, true, true);
			AddLoadingMethod(targetClass, true, true, true);
			AddLoadingMethod(targetClass, false, true, false);
			AddLoadingMethod(targetClass, true, true, false);

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

		private static void AddLoadingMethod(
			CodeTypeDeclaration targetClass,
			bool isAsyncLoad,
			bool isStateLoad,
			bool isArgumentLoad)
		{
			var loadMethod = new CodeMemberMethod
			{
				Name = isAsyncLoad ? "LoadAsync" : "Load",
				Attributes = MemberAttributes.Public | MemberAttributes.Static
			};

			var statementArguments = new List<string>();
			var statementTypeParameters = new List<string>();

			if (isStateLoad)
			{
				AddParameter(nameof(GameStateMachine), "machine");

				var targetTypeParameter = new CodeTypeParameter("TState");
				
				statementTypeParameters.Add("TState");
				
				var tState = new CodeTypeReference("State");
				tState.TypeArguments.Add(new CodeTypeReference(nameof(GameStateMachine)));

				targetTypeParameter.Constraints.Add(tState);

				loadMethod.TypeParameters.Add(targetTypeParameter);
			}

			if (isArgumentLoad)
			{
				loadMethod.TypeParameters.Add(new CodeTypeParameter("T"));

				statementTypeParameters.Add("T");
			
				AddParameter("T", "argument");
			}

			if (isAsyncLoad)
			{
				loadMethod.ReturnType = new CodeTypeReference(typeof(AsyncOperation));
			}

			var loadingStatement = GetLoadingStatement(isAsyncLoad, statementArguments, statementTypeParameters);

			var loadingModeParameter = new CodeParameterDeclarationExpression(nameof(LoadSceneMode), "loadSceneMode = LoadSceneMode.Single");
			loadMethod.Parameters.Add(loadingModeParameter);
			loadMethod.Statements.Add(new CodeSnippetExpression(loadingStatement));
			targetClass.Members.Add(loadMethod);

			return;

			void AddParameter(string type, string argumentName)
			{
				var parameter = new CodeParameterDeclarationExpression(type, argumentName);
				loadMethod.Parameters.Add(parameter);
				statementArguments.Add(argumentName);
			}
		}

		private static string GetLoadingStatement(bool isAsyncLoad, List<string> statementArguments, List<string> statementTypeParameters)
		{
			string loadingStatement = string.Empty;

			var name = "LoadScene";
			var typeParameters = string.Empty;
			var arguments = "(_sceneName, loadSceneMode";

			for (int i = 0; i < statementTypeParameters.Count; i++)
			{
				var type = string.Empty;
				
				if (i > 0)
				{
					type += ", ";
				}

				type += statementTypeParameters[i];

				typeParameters += type;
			}
			
			foreach (var argument in statementArguments)
			{
				arguments += $", {argument}";
			}

			loadingStatement = $"{name}<{typeParameters}>{arguments})";

			loadingStatement = isAsyncLoad ? $"return {loadingStatement}" : loadingStatement;

			return loadingStatement;
		}
	}
}
#endif