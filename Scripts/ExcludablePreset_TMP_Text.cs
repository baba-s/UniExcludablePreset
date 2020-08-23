using TMPro;
using UnityEngine;

namespace Kogane
{
	/// <summary>
	/// TMP_Text の除外可能なプリセット
	/// </summary>
	//[CreateAssetMenu( fileName = nameof( ExcludablePreset_TMP_Text ), menuName = "UniExcludablePreset/" + nameof( ExcludablePreset_TMP_Text ) )]
	public sealed class ExcludablePreset_TMP_Text : ExcludablePreset_RectTransform
	{
		//================================================================================
		// 変数(SerializeField)
		//================================================================================
		[SerializeField] private OverrideString m_text     = new OverrideString( string.Empty ) { IsOverride = true };
		[SerializeField] private OverrideFloat  m_fontSize = new OverrideFloat( 36 ) { IsOverride = true };

		//================================================================================
		// 関数
		//================================================================================
		/// <summary>
		/// 指定されたオブジェクトにパラメータを反映します
		/// </summary>
		public override void ApplyTo( GameObject gameObject, bool isUndo = true )
		{
			if ( !gameObject.TryGetComponent<TMP_Text>( out var tmpText ) )
			{
				Debug.LogWarning( "TMP_Text コンポーネントがアタッチされていません", gameObject );
				return;
			}
			
#if UNITY_EDITOR
			if ( isUndo )
			{
				UnityEditor.Undo.RecordObject( tmpText, "Apply" );
			}
#endif

			var text     = tmpText.text;
			var fontSize = tmpText.fontSize;

			m_text.Override( ref text );
			m_fontSize.Override( ref fontSize );

			tmpText.text     = text;
			tmpText.fontSize = fontSize;

			base.ApplyTo( gameObject );
		}

		/// <summary>
		/// 指定されたオブジェクトからパラメータを読み込みます
		/// </summary>
		public override void ApplyFrom( GameObject gameObject, bool isUndo = true )
		{
			if ( !gameObject.TryGetComponent<TMP_Text>( out var tmpText ) )
			{
				Debug.LogWarning( "TMP_Text コンポーネントがアタッチされていません", gameObject );
				return;
			}
			
#if UNITY_EDITOR
			if ( isUndo )
			{
				UnityEditor.Undo.RecordObject( tmpText, "Apply" );
			}
#endif

			var text     = tmpText.text;
			var fontSize = tmpText.fontSize;

			m_text.Value     = text;
			m_fontSize.Value = fontSize;

			base.ApplyFrom( gameObject );
		}
	}
}