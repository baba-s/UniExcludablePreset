using TMPro;
using UnityEditor;
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
		[SerializeField] private OverrideString m_text     = new OverrideString( string.Empty );
		[SerializeField] private OverrideFloat  m_fontSize = new OverrideFloat( 36 );

		//================================================================================
		// 関数
		//================================================================================
		/// <summary>
		/// 指定されたオブジェクトにパラメータを反映します
		/// </summary>
		public override void ApplyTo( RectTransform rectTransform )
		{
			var target = rectTransform.GetComponent<TMP_Text>();

			Undo.RecordObject( target, nameof( ApplyTo ) );

			var text     = target.text;
			var fontSize = target.fontSize;

			m_text.Override( ref text );
			m_fontSize.Override( ref fontSize );

			target.text     = text;
			target.fontSize = fontSize;

			base.ApplyTo( target.rectTransform );
		}

		/// <summary>
		/// 指定されたオブジェクトからパラメータを読み込みます
		/// </summary>
		public override void ApplyFrom( RectTransform rectTransform )
		{
			var target = rectTransform.GetComponent<TMP_Text>();

			Undo.RecordObject( this, nameof( ApplyFrom ) );

			var text     = target.text;
			var fontSize = target.fontSize;

			m_text.Value     = text;
			m_fontSize.Value = fontSize;

			base.ApplyFrom( target.rectTransform );
		}
	}
}