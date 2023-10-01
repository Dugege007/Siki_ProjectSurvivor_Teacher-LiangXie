﻿/****************************************************************************
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
		[SerializeField] public RectTransform UpgradeRoot;

		public void Clear()
		{
			ExpUpgradeItemTempleteBtn = null;
			UpgradeRoot = null;
		}

		public override string ComponentName
		{
			get { return "ExpUpgradePanel";}
		}
	}
}
