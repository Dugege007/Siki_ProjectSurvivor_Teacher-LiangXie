/****************************************************************************
 * 2023.10 MSI
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectSurvivor
{
	public partial class TreasureChestPanel
	{
		[SerializeField] public UnityEngine.UI.Button OKBtn;

		public void Clear()
		{
			OKBtn = null;
		}

		public override string ComponentName
		{
			get { return "TreasureChestPanel";}
		}
	}
}
