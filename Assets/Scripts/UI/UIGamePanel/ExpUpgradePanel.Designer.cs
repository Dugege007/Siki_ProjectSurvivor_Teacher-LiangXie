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
		[SerializeField] public UnityEngine.UI.Text PairedName;
		[SerializeField] public RectTransform UpgradeRoot;
		[SerializeField] public UnityEngine.UI.Button SkipBtn;

		public void Clear()
		{
			ExpUpgradeItemTempleteBtn = null;
			PairedName = null;
			UpgradeRoot = null;
			SkipBtn = null;
		}

		public override string ComponentName
		{
			get { return "ExpUpgradePanel";}
		}
	}
}
