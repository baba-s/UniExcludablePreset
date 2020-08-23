using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Kogane
{
	/// <summary>
	/// Image の除外可能なプリセット
	/// </summary>
	//[CreateAssetMenu( fileName = nameof( ExcludablePreset_Image ), menuName = "UniExcludablePreset/" + nameof( ExcludablePreset_Image ) )]
	public sealed class ExcludablePreset_Image : ExcludablePreset_RectTransform
	{
		//================================================================================
		// 変数(SerializeField)
		//================================================================================
		[SerializeField] private OverrideSprite m_sourceImage = new OverrideSprite( null );

		//================================================================================
		// 関数
		//================================================================================
		/// <summary>
		/// 指定されたオブジェクトにパラメータを反映します
		/// </summary>
		public override void ApplyTo( RectTransform rectTransform )
		{
			var target = rectTransform.GetComponent<Image>();

			Undo.RecordObject( target, nameof( ApplyTo ) );

			var sprite = target.sprite;

			m_sourceImage.Override( ref sprite );

			target.sprite = sprite;

			base.ApplyTo( target.rectTransform );
		}

		/// <summary>
		/// 指定されたオブジェクトからパラメータを読み込みます
		/// </summary>
		public override void ApplyFrom( RectTransform rectTransform )
		{
			var target = rectTransform.GetComponent<Image>();

			Undo.RecordObject( this, nameof( ApplyFrom ) );

			var sprite = target.sprite;

			m_sourceImage.Value = sprite;

			base.ApplyFrom( target.rectTransform );
		}
	}
}