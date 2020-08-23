using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Kogane.Internal
{
	/// <summary>
	/// RectTransform のコンテキストメニューから ExcludablePreset を作成できるようにするエディタ拡張
	/// </summary>
	internal static class ExcludablePresetMenuItem
	{
		//================================================================================
		// 関数(static)
		//================================================================================
		/// <summary>
		/// 選択された RectTransform を持つゲームオブジェクトの現在の設定をもとに
		/// ExcludablePreset を作成します
		/// </summary>
		[MenuItem( "CONTEXT/RectTransform/Create Excludable Preset" )]
		private static void Create( MenuCommand command )
		{
			var transform  = ( Transform ) command.context;
			var gameObject = transform.gameObject;

			if ( gameObject.TryGetComponent<Image>( out _ ) )
			{
				Save<ExcludablePreset_Image>( gameObject );
			}
			else if ( gameObject.TryGetComponent<TMP_Text>( out _ ) )
			{
				Save<ExcludablePreset_TMP_Text>( gameObject );
			}
			else
			{
				Save<ExcludablePreset_RectTransform>( gameObject );
			}
		}

		/// <summary>
		/// ExcludablePreset を作成して保存します
		/// </summary>
		private static void Save<T>( GameObject target )
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