/****************************************************************************
 * 2023.10 MSI
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectSurvivor
{
	public partial class ExpUpgradePanel
	{
		[SerializeField] public UnityEngine.UI.Button ExpUpgradeItemTempleteBtn;
		[SerializeField] public UnityEngine.UI.Text PairedUpgradeName;
		[SerializeField] public UnityEngine.UI.Image Icon;
		[SerializeField] public RectTransform UpgradeRoot;
		[SerializeField] public UnityEngine.UI.Text Tips;
		[SerializeField] public UnityEngine.UI.Button SkipBtn;

		public void Clear()
		{
			ExpUpgradeItemTempleteBtn = null;
			PairedUpgradeName = null;
			Icon = null;
			UpgradeRoot = null;
			Tips = null;
			SkipBtn = null;
		}

		public override string ComponentName
		{
			get { return "ExpUpgradePanel";}
		}
	}
}
