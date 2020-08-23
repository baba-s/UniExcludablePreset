using System;
using UnityEngine;

namespace Kogane
{
	[Serializable]
	internal sealed class OverrideSprite : OverrideValueBase<Sprite>
	{
		public OverrideSprite()
		{
		}

		public OverrideSprite( Sprite defaultValue ) : base( defaultValue )
		{
		}

		public OverrideSprite( string label, Sprite defaultValue ) : base( label, defaultValue )
		{
		}
	}
}