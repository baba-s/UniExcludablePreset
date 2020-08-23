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
		[SerializeField] private OverrideSprite    m_sourceImage = new OverrideSprite( null ) { IsOverride                 = true };
		[SerializeField] private OverrideImageType m_imageType   = new OverrideImageType( Image.Type.Simple ) { IsOverride = true };

		//================================================================================
		// 関数
		//================================================================================
		/// <summary>
		/// 指定されたオブジェクトにパラメータを反映します
		/// </summary>
		public override void ApplyTo( GameObject gameObject, bool isUndo = true )
		{
			if ( !gameObject.TryGetComponent<Image>( out var image ) )
			{
				Debug.LogWarning( "Image コンポーネントがアタッチされていません", gameObject );
				return;
			}

#if UNITY_EDITOR
			if ( isUndo )
			{
				UnityEditor.Undo.RecordObject( image, "Apply" );
			}
#endif

			var sprite = image.sprite;
			var type   = image.type;

			m_sourceImage.Override( ref sprite );
			m_imageType.Override( ref type );

			image.sprite = sprite;
			image.type   = type;

			base.ApplyTo( gameObject, isUndo );
		}

		/// <summary>
		/// 指定されたオブジェクトからパラメータを読み込みます
		/// </summary>
		public override void ApplyFrom( GameObject gameObject, bool isUndo = true )
		{
			if ( !gameObject.TryGetComponent<Image>( out var image ) )
			{
				Debug.LogWarning( "Image コンポーネントがアタッチされていません", gameObject );
				return;
			}

#if UNITY_EDITOR
			if ( isUndo )
			{
				UnityEditor.Undo.RecordObject( image, "Apply" );
			}
#endif

			var sprite = image.sprite;
			var type   = image.type;

			m_sourceImage.Value = sprite;
			m_imageType.Value   = type;

			base.ApplyFrom( gameObject );
		}
	}
}