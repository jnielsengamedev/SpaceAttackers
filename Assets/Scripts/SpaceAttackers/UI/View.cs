using UnityEngine.UIElements;

namespace SpaceAttackers.UI
{
	public abstract class View
	{
		protected readonly VisualElement MainElement;
		protected readonly BaseController Controller;

		protected View(VisualElement element, BaseController controller)
		{
			MainElement = element;
			Controller = controller;
		}

		public abstract void GetElements();

		public abstract void RegisterEvents();

		public abstract void UnregisterEvents();

		protected virtual void ViewShown()
		{
		}

		public virtual void ViewUpdate()
		{
		}

		public void ShowView()
		{
			MainElement.style.display = DisplayStyle.Flex;
			ViewShown();
		}

		public void HideView()
		{
			MainElement.style.display = DisplayStyle.None;
		}
	}
}