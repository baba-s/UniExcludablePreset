using JetBrains.Annotations;
using UnityEngine;

namespace Kogane
{
	/// <summary>
	/// RectTransform の除外可能なプリセット
	/// </summary>
	//[CreateAssetMenu( fileName = nameof( ExcludablePreset_RectTransform ), menuName = "UniExcludablePreset/" + nameof( ExcludablePreset_RectTransform ) )]
	public class ExcludablePreset_RectTransform : ScriptableObject
	{
		//================================================================================
		// 変数(SerializeField)
		//================================================================================
		[SerializeField][UsedImplicitly][Multiline]
		private string m_note = string.Empty;

		[SerializeField] private OverrideFloat m_posX        = new OverrideFloat( 0 );
		[SerializeField] private OverrideFloat m_posY        = new OverrideFloat( 0 );
		[SerializeField] private OverrideFloat m_posZ        = new OverrideFloat( 0 );
		[SerializeField] private OverrideFloat m_width       = new OverrideFloat( 100 );
		[SerializeField] private OverrideFloat m_height      = new OverrideFloat( 100 );
		[SerializeField] private OverrideFloat m_anchorsMinX = new OverrideFloat( 0.5f );
		[SerializeField] private OverrideFloat m_anchorsMinY = new OverrideFloat( 0.5f );
		[SerializeField] private OverrideFloat m_anchorsMaxX = new OverrideFloat( 0.5f );
		[SerializeField] private OverrideFloat m_anchorsMaxY = new OverrideFloat( 0.5f );
		[SerializeField] private OverrideFloat m_pivotX      = new OverrideFloat( 0.5f );
		[SerializeField] private OverrideFloat m_pivotY      = new OverrideFloat( 0.5f );
		[SerializeField] private OverrideFloat m_scaleX      = new OverrideFloat( 1 );
		[SerializeField] private OverrideFloat m_scaleY      = new OverrideFloat( 1 );
		[SerializeField] private OverrideFloat m_scaleZ      = new OverrideFloat( 1 );

		//================================================================================
		// 関数
		//================================================================================
		/// <summary>
		/// 指定されたオブジェクトにパラメータを反映します
		/// </summary>
		public virtual void ApplyTo( GameObject gameObject, bool isUndo = true )
		{
			if ( !gameObject.TryGetComponent<RectTransform>( out var rectTransform ) )
			{
				Debug.LogWarning( "RectTransform コンポーネントがアタッチされていません", gameObject );
				return;
			}

#if UNITY_EDITOR
			if ( isUndo )
			{
				UnityEditor.Undo.RecordObject( rectTransform, "Apply" );
			}
#endif

			var anchoredPosition3D = rectTransform.anchoredPosition3D;
			var sizeDelta          = rectTransform.sizeDelta;
			var anchorMin          = rectTransform.anchorMin;
			var anchorMax          = rectTransform.anchorMax;
			var pivot              = rectTransform.pivot;
			var localScale         = rectTransform.localScale;

			m_posX.Override( ref anchoredPosition3D.x );
			m_posY.Override( ref anchoredPosition3D.y );
			m_posZ.Override( ref anchoredPosition3D.z );
			m_width.Override( ref sizeDelta.x );
			m_height.Override( ref sizeDelta.y );
			m_anchorsMinX.Override( ref anchorMin.x );
			m_anchorsMinY.Override( ref anchorMin.y );
			m_anchorsMaxX.Override( ref anchorMax.x );
			m_anchorsMaxY.Override( ref anchorMax.y );
			m_pivotX.Override( ref pivot.x );
			m_pivotY.Override( ref pivot.y );
			m_scaleX.Override( ref localScale.x );
			m_scaleY.Override( ref localScale.y );
			m_scaleZ.Override( ref localScale.z );

			rectTransform.anchoredPosition3D = anchoredPosition3D;
			rectTransform.sizeDelta          = sizeDelta;
			rectTransform.anchorMin          = anchorMin;
			rectTransform.anchorMax          = anchorMax;
			rectTransform.pivot              = pivot;
			rectTransform.localScale         = localScale;
		}

		/// <summary>
		/// 指定されたオブジェクトからパラメータを読み込みます
		/// </summary>
		public virtual void ApplyFrom( GameObject gameObject, bool isUndo = true )
		{
			if ( !gameObject.TryGetComponent<RectTransform>( out var rectTransform ) )
			{
				Debug.LogWarning( "RectTransform コンポーネントがアタッチされていません", gameObject );
				return;
			}
			
#if UNITY_EDITOR
			if ( isUndo )
			{
				UnityEditor.Undo.RecordObject( rectTransform, "Apply" );
			}
#endif

			var anchoredPosition3D = rectTransform.anchoredPosition3D;
			var sizeDelta          = rectTransform.sizeDelta;
			var anchorMin          = rectTransform.anchorMin;
			var anchorMax          = rectTransform.anchorMax;
			var pivot              = rectTransform.pivot;
			var localScale         = rectTransform.localScale;

			m_posX.Value        = anchoredPosition3D.x;
			m_posY.Value        = anchoredPosition3D.y;
			m_posZ.Value        = anchoredPosition3D.z;
			m_width.Value       = sizeDelta.x;
			m_height.Value      = sizeDelta.y;
			m_anchorsMinX.Value = anchorMin.x;
			m_anchorsMinY.Value = anchorMin.y;
			m_anchorsMaxX.Value = anchorMax.x;
			m_anchorsMaxY.Value = anchorMax.y;
			m_pivotX.Value      = pivot.x;
			m_pivotY.Value      = pivot.y;
			m_scaleX.Value      = localScale.x;
			m_scaleY.Value      = localScale.y;
			m_scaleZ.Value      = localScale.z;
		}
	}
}