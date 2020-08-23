using System;
using UnityEngine.UI;

namespace Kogane
{
	[Serializable]
	internal sealed class OverrideImageType : OverrideValueBase<Image.Type>
	{
		public OverrideImageType()
		{
		}

		public OverrideImageType( Image.Type defaultValue ) : base( defaultValue )
		{
		}

		public OverrideImageType( string label, Image.Type defaultValue ) : base( label, defaultValue )
		{
		}
	}
}