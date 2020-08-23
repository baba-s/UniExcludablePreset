using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Kogane.Internal
{
	internal static class ExcludablePresetMenuItem
	{
		[MenuItem( "CONTEXT/RectTransform/Create Excludable Preset" )]
		private static void Create( MenuCommand command )
		{
			var rectTransform = ( RectTransform ) command.context;

			if ( rectTransform.TryGetComponent<Image>( out _ ) )
			{
				Save<ExcludablePreset_Image>( rectTransform );
			}
			else if ( rectTransform.TryGetComponent<TMP_Text>( out _ ) )
			{
				Save<ExcludablePreset_TMP_Text>( rectTransform );
			}
			else
			{
				Save<ExcludablePreset_RectTransform>( rectTransform );
			}
		}

		private static void Save<T>( RectTransform target )
			where T : ExcludablePreset_RectTransform
		{
			var selectedPath = EditorUtility.SaveFilePanel( "", "", typeof( T ).Name, "asset" );
			var relativePath = FileUtil.GetProjectRelativePath( selectedPath );
			var path         = AssetDatabase.GenerateUniqueAssetPath( relativePath );

			if ( string.IsNullOrWhiteSpace( path ) ) return;

			var preset = ScriptableObject.CreateInstance<T>();
			preset.ApplyFrom( target );

			AssetDatabase.CreateAsset( preset, path );
			AssetDatabase.SaveAssets();
		}
	}
}