using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectSurvivor
{
	// Generate Id:deb70fab-3827-4a8d-a988-ce03824e0c68
	public partial class UIGameStartPanel
	{
		public const string Name = "UIGameStartPanel";
		
		[SerializeField]
		public UnityEngine.UI.Button StartGameBtn;
		[SerializeField]
		public UnityEngine.UI.Button CoinUpgradeBtn;
		[SerializeField]
		public UnityEngine.UI.Button ResetUpgradeBtn;
		[SerializeField]
		public CoinUpgradePanel CoinUpgradePanel;
		
		private UIGameStartPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			StartGameBtn = null;
			CoinUpgradeBtn = null;
			ResetUpgradeBtn = null;
			CoinUpgradePanel = null;
			
			mData = null;
		}
		
		public UIGameStartPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIGameStartPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIGameStartPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
