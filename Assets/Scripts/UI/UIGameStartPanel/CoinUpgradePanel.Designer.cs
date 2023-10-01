/****************************************************************************
 * 2023.10 MSI
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectSurvivor
{
	public partial class CoinUpgradePanel
	{
		[SerializeField] public UnityEngine.RectTransform CoinUpgradeItemRoot;
		[SerializeField] public UnityEngine.UI.Button CoinUpgradeItemTempleteBtn;
		[SerializeField] public UnityEngine.UI.Text CoinText;
		[SerializeField] public UnityEngine.UI.Button CloseBtn;

		public void Clear()
		{
			CoinUpgradeItemRoot = null;
			CoinUpgradeItemTempleteBtn = null;
			CoinText = null;
			CloseBtn = null;
		}

		public override string ComponentName
		{
			get { return "CoinUpgradePanel";}
		}
	}
}
