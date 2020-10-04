using System.Diagnostics;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

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
			EditorSceneManager.sceneSaving += ( scene, path ) =>
			{
				var settings = ExcludablePresetSettings.GetInstance();

				// 経過時間のログを出力しない場合
				if ( !settings.EnabledLog )
				{
					OnSceneSaving( scene );
					return;
				}

				// 経過時間のログを出力する場合
				var sw = new Stopwatch();
				sw.Start();
				OnSceneSaving( scene );
				sw.Stop();
				Debug.LogFormat( settings.LogFormat, sw.Elapsed.TotalSeconds );
			};
		}

		/// <summary>
		/// シーンが保存される時に呼び出されます
		/// </summary>
		private static void OnSceneSaving( Scene scene )
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