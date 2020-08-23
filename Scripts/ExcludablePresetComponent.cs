using UnityEngine;

namespace Kogane
{
	/// <summary>
	/// ExcludablePreset をゲームオブジェクトに反映するためのコンポーネント
	/// </summary>
	[DisallowMultipleComponent]
	public sealed class ExcludablePresetComponent : MonoBehaviour
	{
		//================================================================================
		// 変数(SerializeField)
		//================================================================================
		[SerializeField] private ExcludablePreset_RectTransform m_preset = default;
		
		//================================================================================
		// プロパティ
		//================================================================================
		public bool CanApply => m_preset != null;
		
		//================================================================================
		// 関数
		//================================================================================
		/// <summary>
		/// このコンポーネントがアタッチされているゲームオブジェクトにプリセットを適用します
		/// </summary>
		[ContextMenu( "Apply" )]
		public void Apply()
		{
			Apply( true );
		}
		
		/// <summary>
		/// このコンポーネントがアタッチされているゲームオブジェクトにプリセットを適用します
		/// </summary>
		public void Apply( bool isUndo )
		{
			if ( !CanApply ) return;
			m_preset.ApplyTo( gameObject, isUndo );
		}
	}
}