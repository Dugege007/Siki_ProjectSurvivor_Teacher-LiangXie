/****************************************************************************
 * 2023.9 MSI
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectSurvivor
{
	public partial class ExpUpgradePanel
	{
		[SerializeField] public UnityEngine.UI.Button ExpUpgradeItemTempleteBtn;
		[SerializeField] public RectTransform UpgradeRoot;
		[SerializeField] public UnityEngine.UI.Button UpgradeBtn;
		[SerializeField] public UnityEngine.UI.Button SimpleDurationUpgradeBtn;

		public void Clear()
		{
			ExpUpgradeItemTempleteBtn = null;
			UpgradeRoot = null;
			UpgradeBtn = null;
			SimpleDurationUpgradeBtn = null;
		}

		public override string ComponentName
		{
			get { return "ExpUpgradePanel";}
		}
	}
}
