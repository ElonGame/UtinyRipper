﻿using System.Collections.Generic;
using uTinyRipper.SerializedFiles;

namespace uTinyRipper.Classes.AssetBundles
{
	public struct AssetInfo : IAssetReadable, IDependent
	{
		/// <summary>
		/// 2.5.0 and greater
		/// </summary>
		public static bool IsReadPreload(Version version)
		{
			return version.IsGreaterEqual(2, 5);
		}

		public void Read(AssetReader reader)
		{
			if (IsReadPreload(reader.Version))
			{	
				PreloadIndex = reader.ReadInt32();
				PreloadSize = reader.ReadInt32();
			}
			Asset.Read(reader);
		}

		public IEnumerable<Object> FetchDependencies(ISerializedFile file, bool isLog = false)
		{
			yield return Asset.FetchDependency(file, isLog, () => nameof(AssetInfo), "asset");
		}

		public int PreloadIndex { get; private set; }
		public int PreloadSize { get; private set; }

		public PPtr<Object> Asset;
	}
}
