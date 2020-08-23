using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace Kogane.Internal
{
	/// <summary>
	/// シーンを保存する時に ExcludablePreset の設定をゲームオブジェクトに反映するエディタ拡張
	/// </summary>
	[InitializeOnLoad]
	internal static class ExcludablePresetApplier
	{
		//================================================================================
		// 関数(static)
		//================================================================================
		/// <summary>
		/// コンストラクタ
		/// </summary>
		static ExcludablePresetApplier()
		{
			EditorSceneManager.sceneSaving += OnSceneSaving;
		}

		/// <summary>
		/// シーンが保存される時に呼び出されます
		/// </summary>
		private static void OnSceneSaving( Scene scene, string path )
		{
			var list = scene
					.GetRootGameObjects()
					.SelectMany( x => x.GetComponentsInChildren<ExcludablePresetComponent>( true ) )
					.Where( x => x != null )
					.ToArray()
				;

			if ( list.Length <= 0 ) return;
			
			Undo.RecordObjects( list, "Apply" );

			foreach ( var n in list )
			{
				n.Apply( false );
			}
		}
	}
}