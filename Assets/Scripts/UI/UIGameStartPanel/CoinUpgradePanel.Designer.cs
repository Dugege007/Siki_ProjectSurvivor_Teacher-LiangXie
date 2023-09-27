/****************************************************************************
 * 2023.9 MSI
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectSurvivor
{
	public partial class CoinUpgradePanel
	{
		[SerializeField] public UnityEngine.UI.Button CoinUpgradeItemTemplete;
		[SerializeField] public UnityEngine.RectTransform CoinUpgradeItemRoot;
		[SerializeField] public UnityEngine.UI.Button CoinPercentUpgradeBtn;
		[SerializeField] public UnityEngine.UI.Button ExpPercentUpgradeBtn;
		[SerializeField] public UnityEngine.UI.Button MaxHPUpgradeBtn;
		[SerializeField] public UnityEngine.UI.Text CoinText;
		[SerializeField] public UnityEngine.UI.Button CloseBtn;

		public void Clear()
		{
			CoinUpgradeItemTemplete = null;
			CoinUpgradeItemRoot = null;
			CoinPercentUpgradeBtn = null;
			ExpPercentUpgradeBtn = null;
			MaxHPUpgradeBtn = null;
			CoinText = null;
			CloseBtn = null;
		}

		public override string ComponentName
		{
			get { return "CoinUpgradePanel";}
		}
	}
}
