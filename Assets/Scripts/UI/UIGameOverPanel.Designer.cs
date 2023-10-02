using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectSurvivor
{
	// Generate Id:6bd1ec81-ff37-425f-802a-142a1024d3f5
	public partial class UIGameOverPanel
	{
		public const string Name = "UIGameOverPanel";
		
		[SerializeField]
		public UnityEngine.UI.Button RestartGameBtn;
		[SerializeField]
		public UnityEngine.UI.Button BackToStartBtn;
		
		private UIGameOverPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			RestartGameBtn = null;
			BackToStartBtn = null;
			
			mData = null;
		}
		
		public UIGameOverPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIGameOverPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIGameOverPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
