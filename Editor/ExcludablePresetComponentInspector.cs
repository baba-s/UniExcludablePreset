using UnityEditor;
using UnityEngine;

namespace Kogane.Internal
{
	/// <summary>
	/// ExcludablePresetComponent の Inspector を拡張するクラス
	/// </summary>
	[CustomEditor( typeof( ExcludablePresetComponent ) )]
	internal sealed class ExcludablePresetComponentInspector : Editor
	{
		//================================================================================
		// 関数
		//================================================================================
		/// <summary>
		/// Inspector の GUI を描画する時に呼び出されます
		/// </summary>
		public override void OnInspectorGUI()
		{
			var presetProperty = serializedObject.FindProperty( "m_preset" );
			var component      = ( ExcludablePresetComponent ) target;

			using ( var scope = new EditorGUI.ChangeCheckScope() )
			{
				EditorGUILayout.PropertyField( presetProperty );
				
				// この関数を呼び出さないと変更が反映されない
				serializedObject.ApplyModifiedProperties();

				if ( scope.changed )
				{
					component.Apply();
				}
			}

			using ( new EditorGUI.DisabledScope( !component.CanApply ) )
			{
				if ( GUILayout.Button( "Apply" ) )
				{
					component.Apply();
				}
			}
		}
	}
}