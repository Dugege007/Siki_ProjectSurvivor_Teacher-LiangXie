using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectSurvivor
{
	// Generate Id:6e831e5c-6a46-47d9-8551-c9d857383350
	public partial class UIGamePassPanel
	{
		public const string Name = "UIGamePassPanel";
		
		[SerializeField]
		public UnityEngine.UI.Button RestartGameBtn;
		[SerializeField]
		public UnityEngine.UI.Button BackToStartBtn;
		
		private UIGamePassPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			RestartGameBtn = null;
			BackToStartBtn = null;
			
			mData = null;
		}
		
		public UIGamePassPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIGamePassPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIGamePassPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
